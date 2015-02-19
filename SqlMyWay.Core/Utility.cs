using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using PoorMansTSqlFormatterLib;

namespace SqlMyWay.Core
{
	public static class Utility
	{
		public static string GetMicrosoftFormattedSql(string sql)
		{
			var tree = GetTSqlFragmentTree(sql);
			return GenerateMicrosoftTSqlScript(tree);
		}
		public static string GetPoorMansFormattedSql(string sql)
		{
			return SqlFormattingManager.DefaultFormat(sql);
		}

		public static TSqlFragment GetTSqlFragmentTree(string sql)
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


	}
}