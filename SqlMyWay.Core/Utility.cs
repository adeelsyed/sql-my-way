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

            //line breaks between statements.
            sql = sql.Replace("\n\n", new string('\n', o.NLineBreaksBetweenStatements));

            //line breaks between clauses: put a \n bf every clause keyword            
            pat = @"\n(\s*(" + string.Join("|", Const.ClauseKeywords) + @")\s*\n)";
            sql = Regex.Replace(sql, pat, new string('\n', o.NLineBreaksBetweenClauses) + @"$1");
            
            //keyword capitalization
            var kw = Const.AllKeywords.Except(Const.BuiltInFunctions).Except(Const.DataTypeKeywords);
            pat = @"\b(" + string.Join("|", kw) + @")\b";
            sql = Regex.Replace(sql, pat, x => o.CapitalizeKeywords ? x.Groups[1].ToString().ToUpper() : x.Groups[1].ToString().ToLower(), RegexOptions.IgnoreCase);

            //datatype capitalization
            pat = @"\b(" + string.Join("|", Const.DataTypeKeywords) + @")\b";
            sql = Regex.Replace(sql, pat, x => o.CapitalizeDataTypes ? x.Groups[1].ToString().ToUpper() : x.Groups[1].ToString().ToLower(), RegexOptions.IgnoreCase);

            //built-in function capitalization
            pat = @"\b(" + string.Join("|", Const.BuiltInFunctions.Except(Const.DataTypeKeywords)) + @")\(";
            sql = Regex.Replace(sql, pat, x => o.CapitalizeBuiltInFunctions ? x.Groups[1].ToString().ToUpper() + "(" : x.Groups[1].ToString().ToLower() + "(", RegexOptions.IgnoreCase);


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