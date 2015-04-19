using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using PoorMansTSqlFormatterLib;
using PoorMansTSqlFormatterLib.Formatters;
using System.Text.RegularExpressions;
using System.Linq;

namespace SqlMyWay.Core
{
	public static class Utility
	{
        public static string GetSqlMyWayEditorsChoiceFormattedSql(string sql)
		{
            return GetSqlBeautyBaseZero(sql);
		}
		public static string GetMicrosoftFormattedSql(string sql)
		{
			var tree = GetMicrosoftTSqlFragmentTree(sql);
			return GenerateMicrosoftTSqlScript(tree);
		}
        public static string GetPoorMansFormattedSql(string sql)
        {
            return SqlFormattingManager.DefaultFormat(sql);
        }
        public static string GetSqlMyWay(string sql, SqlMyWayOptions o)
        {
            sql = GetSqlBeautyBaseZero(sql);
            string pat;
	        string[] lines;

            #region line breaks

            //line breaks between statements.
            sql = sql.Replace("\n\n", new string('\n', o.LineBreaks_BetweenStatements));

            //line breaks between clauses: put a \n bf every clause keyword            
            pat = @"\n(\s*(" + string.Join("|", Const.ClauseKeywords) + @")\s*\n)";
            sql = Regex.Replace(sql, pat, new string('\n', o.LineBreaks_BetweenClauses) + @"$1");

            #endregion

            #region capitalization

            //keyword capitalization
            var kw = Const.AllKeywords.Except(Const.BuiltInFunctions).Except(Const.DataTypeKeywords);
            pat = @"\b(" + string.Join("|", kw) + @")\b";
            sql = Regex.Replace(sql, pat, x => o.Capitalize_Keywords ? x.Groups[1].ToString().ToUpper() : x.Groups[1].ToString().ToLower(), RegexOptions.IgnoreCase);

            //datatype capitalization
            pat = @"\b(" + string.Join("|", Const.DataTypeKeywords) + @")\b";
            sql = Regex.Replace(sql, pat, x => o.Capitalize_DataTypes ? x.Groups[1].ToString().ToUpper() : x.Groups[1].ToString().ToLower(), RegexOptions.IgnoreCase);

            //built-in function capitalization
            pat = @"\b(" + string.Join("|", Const.BuiltInFunctions.Except(Const.DataTypeKeywords)) + @")\(";
            sql = Regex.Replace(sql, pat, x => o.Capitalize_BuiltInFunctions ? x.Groups[1].ToString().ToUpper() + "(" : x.Groups[1].ToString().ToLower() + "(", RegexOptions.IgnoreCase);

            #endregion

            #region comma-separated values

            //leading commas or inline comma lists
            if (!o.CommaLists_TrailingCommas || !o.CommaLists_Stacked)
	        {
		        lines = sql.Split('\n');
		        for (int i = 0; i < lines.Length; i++)
		        {
					//find comma list
			        if (Regex.IsMatch(lines[i].Trim(), "SELECT|GROUP BY|ORDER BY") || (lines[i-1].Trim().EndsWith("IN") && lines[i].Trim() == "("))
			        {
				        int listIndent = -1;
				        for (int j = i + 1; j < lines.Length; j++)
				        {
					        //get indent of next line. everything with the same indent is part of the list
					        int indent = Regex.Match(lines[j], @"^\s*").Length;

					        //save indent of first item in select list
					        if (listIndent == -1)
						        listIndent = indent;

					        //only process commas for items that have the same indent as the first item
							if (indent == listIndent && !o.CommaLists_TrailingCommas)
					        {
						        pat = @"^(\s+)(.+?),{0,1}$"; //$1 is indent, $2 is item without comma
						        string repl = j == i + 1 ? "$1$2" : "$1,$2";
						        lines[j] = Regex.Replace(lines[j], pat, repl, RegexOptions.Multiline);
					        }

							//process inline for lines with same indent or more
							if (indent >= listIndent && !o.CommaLists_Stacked)
					        {
						        lines[j] += "::LAST LINE. REMOVE THIS::";
						        lines[j - 1] = lines[j - 1].Replace("::LAST LINE. REMOVE THIS::", "::REPLACE THIS AND THE FOLLOWING WHITESPACE::");
					        }

					        //break if indent is ever less than first indent
					        if (indent < listIndent)
						        break;
				        }
			        }
		        }
		        sql = string.Join("\n", lines);
		        sql = sql.Replace("::LAST LINE. REMOVE THIS::", "");
				sql = Regex.Replace(sql, @"::REPLACE THIS AND THE FOLLOWING WHITESPACE::\s*", " ");

            }

            #endregion

            #region joins

            //indent joins
            if (o.Joins_Indented)
            {
                pat = @"^(\s*)(" + string.Join("|", Const.JoinKeywords) + @")$";
                sql = Regex.Replace(sql, pat, "$1" + Const.Tab + "$2", RegexOptions.Multiline);
            }

            //table on same line
            pat = @"^(\s*)(" + string.Join("|", Const.JoinKeywords) + @")(\n\s*)";
            sql = Regex.Replace(sql, pat, "$1$2" + (o.Joins_TableOnSameLine ? " " : "$3" + Const.Tab), RegexOptions.Multiline);

            //on clause on same line
            if (!o.Joins_OnClauseOnSameLine)
            {
                pat = @"^(\s*)(.+?) ON ";
                sql = Regex.Replace(sql, pat, "$1$2\n$1" + Const.Tab + "ON ", RegexOptions.Multiline);
            }

            #endregion

            #region parentheses

            //spaces inside
            if(o.Parentheses_SpacesInside)
            {
                pat = @"\(([^\s])";
                sql = Regex.Replace(sql, pat, "( $1");
                pat = @"([^\s])\)";
                sql = Regex.Replace(sql, pat, "$1 )");
            }

            //spaces outside
            if(!o.Parentheses_SpacesOutside)
            {
                pat = @"\) ([^\s])";
                sql = Regex.Replace(sql, pat, ")$1");
                pat = @"([^\s]) \(";
                sql = Regex.Replace(sql, pat, "$1(");
            }

            #endregion

            return sql;
        }
        
		public static TSqlFragment GetMicrosoftTSqlFragmentTree(string sql)
		{
			IList<ParseError> errors;
			using (var txtRdr = new StringReader(sql))
			{
				TSqlFragment sqlFragment = new TSql110Parser(true).Parse(txtRdr, out errors);
				if (errors.Count == 0)
					return sqlFragment;
			}

			//error handling
			var sb = new StringBuilder(string.Format("The SQL has {0} errors:\n", errors.Count));
			foreach (var parseError in errors)
				sb.AppendFormat("--Error {0} on line {1}, column {2}: {3}\n", parseError.Number, parseError.Line, parseError.Column, parseError.Message);

			throw new Exception(sb.ToString());
		}
		private static string GenerateMicrosoftTSqlScript(TSqlFragment tree)
		{
			var gen = new Sql110ScriptGenerator(new SqlScriptGeneratorOptions() { SqlVersion = SqlVersion.Sql110 });

			string output;
			gen.GenerateScript(tree, out output);
			return output;
		}
        private static string GetPoorMansFormattedSql(string sql, TSqlStandardFormatterOptions options)
        {
            var formatter = new TSqlStandardFormatter(options);
            var manager = new SqlFormattingManager(formatter);
            return manager.Format(sql);
        }
        private static string GetSqlBeautyBaseZero(string sql)
        {
            //USE POOR MAN TO START
            var options = new TSqlStandardFormatterOptions
            {
                ExpandCommaLists = true,
                TrailingCommas = true,
                BreakJoinOnSections = false,
                ExpandBetweenConditions = false,
                IndentString = Const.Tab,
                HTMLColoring = false
            };
            sql = GetPoorMansFormattedSql(sql, options);

            //THEN FINISH OFF THE REST WITH REGEX
            string pat;

            //to make patterns simpler, convert CRLF to LF
            sql = sql.Replace("\r\n", "\n");
			sql = sql.Replace(" +\n", "\n");

            //do not break before the then in a case statement
            sql = Regex.Replace(sql, @"\n\s*(THEN)", " THEN");

            //put first multi-line parens on a new line.
            pat = @"((\n\s*)[^\(\n]+)\((\n)";
            sql = Regex.Replace(sql, pat, "$1$2($3");

            //now fix indentation. lines between parens are over-indented
            string[] lines = sql.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Trim() == "(")
                {
                    //count leading spaces before opening parens
                    int leadingSpaces = line.Split('(')[0].Length;

                    //count leading spaces of following line
                    int nextLineLeadingSpaces = Regex.Match(lines[i + 1], @"\s*").Length;

                    //if next line is overindented, unindent until next close parens
                    if (nextLineLeadingSpaces - leadingSpaces > Const.Tab.Length)
                    {
                        int overIndent = nextLineLeadingSpaces - leadingSpaces - Const.Tab.Length;
                        int parenCount = 1;
                        for (int j = i + 1; j < lines.Length; j++)
                        {
                            if (lines[j].Trim() == "(")
                                parenCount++;
                            else if (lines[j].Trim().StartsWith(")"))
                                parenCount--;

                            lines[j] = lines[j].Substring(overIndent);

                            if (parenCount == 0)
                                break;
                        }
                    }
                }
            }
            sql = string.Join("\n", lines);

            //make closing parens line up with opening parens
            var stackOfSpaceCounts = new Stack<int>();
            lines = sql.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Trim() == "(")
                {
                    //count leading spaces before opening parens
                    int leadingSpaces = line.Split('(')[0].Length;
                    stackOfSpaceCounts.Push(leadingSpaces);
                }
                else if (line.Trim().StartsWith(")"))
                {
                    //add leading spaces before corresponding closing parens
                    lines[i] = new string(' ', stackOfSpaceCounts.Pop()) + line.Trim();
                }
            }
            sql = string.Join("\n", lines);

            //remove extra newline before open parens
            sql = Regex.Replace(sql, @"\n\s*\n(\s*)\(", "\n$1(");

            //put each clause keyword on its own line
            pat = @"^( *)(" + string.Join("|", Const.ClauseKeywords) + @") (.+)";
            sql = Regex.Replace(sql, pat, "$1$2\n$1" + "@#SQLMYWAY" + Const.Tab + "$3", RegexOptions.Multiline);
            sql = sql.Replace("@#SQLMYWAY    (", "(");
            sql = sql.Replace("@#SQLMYWAY    ", Const.Tab);

            //fix parentheses blocks to be indented the same as prior line
            lines = sql.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Trim() == "(")
                {
                    //count leading spaces of prior line
                    int priorLineLeadingSpaces = Regex.Match(lines[i - 1], @"^ *").Length;

                    //count leading spaces before opening parens
                    int leadingSpaces = line.Split('(')[0].Length;

                    //if parens line is underindented, indent until next close parens
                    if (leadingSpaces < priorLineLeadingSpaces)
                    {
                        int underIndent = priorLineLeadingSpaces - leadingSpaces;
                        int parenCount = 0;
                        for (int j = i; j < lines.Length; j++)
                        {
                            if (lines[j].Trim() == "(")
                                parenCount++;
                            else if (lines[j].Trim().StartsWith(")"))
                                parenCount--;

                            lines[j] = new string(' ', underIndent) + lines[j];

                            if (parenCount == 0)
                                break;
                        }
                    }
                }
            }
            sql = string.Join("\n", lines);


            return sql;
        }
    }
}