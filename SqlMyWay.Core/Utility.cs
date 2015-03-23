using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using PoorMansTSqlFormatterLib;
using PoorMansTSqlFormatterLib.Formatters;
using System.Text.RegularExpressions;

namespace SqlMyWay.Core
{
	public static class Utility
	{
        public static string GetSqlMyWayEditorsChoiceFormattedSql(string sql)
		{
            //USE POOR MAN TO GET TO BEAUTY BASE ZERO
			var options = new TSqlStandardFormatterOptions 
            {
                ExpandCommaLists = true,
                TrailingCommas = true,
                BreakJoinOnSections = false,
                ExpandBetweenConditions = false,
                IndentString = "    ",
                HTMLColoring = false
            };
            sql = GetPoorMansFormattedSql(sql, options);

            //THEN FINISH OFF THE REST          

            //to make patterns simpler, convert CRLF to LF
            sql = sql.Replace("\r\n", "\n");

            //do not break before the then in a case statement
            sql = Regex.Replace(sql, @"\n\s*(THEN)", " THEN");

            //put first item in a comma list on a new line
            sql = Regex.Replace(sql, @"^(\s*)(([A-Z]+ )+)([^\(]*),", "$1$2\n$1    $4,", RegexOptions.Multiline);
            
            //put first multi-line parens on a new line.
            sql = Regex.Replace(sql, @"((\n\s*)[^\(\n]+)\((\n)", "$1$2($3");

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
                    if (nextLineLeadingSpaces - leadingSpaces > "    ".Length)
                    {
                        int overIndent = nextLineLeadingSpaces - leadingSpaces - "    ".Length;
                        int curParen = 0;
                        for (int j = i + 1; j < lines.Length; j++)
                        {
                            if (lines[j].Trim() == "(")
                                curParen++;
                            else if (lines[j].Trim().StartsWith(")"))
                                curParen--;

                            lines[j] = lines[j].Substring(overIndent);

                            if (curParen == -1)
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

            return sql;
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
	}
}