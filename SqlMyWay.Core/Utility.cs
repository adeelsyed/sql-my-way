using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using SqlMyWay.Core.Visitors;

namespace SqlMyWay.Core
{
	public static class Utility
	{
		public static string GetMicrosoftFormattedSql(string path)
		{
			var tree = GetTSqlFragmentTree(path);
			return GenerateMicrosoftTSqlScript(tree);
		}
		public static string GetPoorMansFormattedSql(string path)
		{
			return PoorMansTSqlFormatterLib.SqlFormattingManager.DefaultFormat(File.ReadAllText(path));
		}

		public static TSqlFragment GetTSqlFragmentTree(string path)
		{
			using (TextReader txtRdr = new StreamReader(path))
			{
				IList<ParseError> errors;
				TSqlFragment sqlFragment = new TSql110Parser(true).Parse(txtRdr, out errors);
				if (errors.Count == 0)
					return sqlFragment;
				else
				{
					var sb = new StringBuilder(string.Format("The SQL has {0} errors.", errors.Count));
					sb.AppendLine();
					foreach (var parseError in errors)
						sb.AppendFormat("--Error {0} on line {2}, column {3}: {1}\n", parseError.Number, parseError.Message, parseError.Line, parseError.Column);

					throw new Exception(sb.ToString());
				}
			}
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