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
		public static string FormatSql(string path)
		{
			TSqlFragment tree = GetTSqlFragmentTree(path);
			AcceptVisitors(tree);
			return tree.ToText();
		}

		/// <summary>
		/// Takes a SQL script and parses it into a hierarchy of TSQLFragments.
		/// </summary>
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

		/// <summary>
		/// Allows specialized classes to visit parts of the code and make changes
		/// </summary>
		private static void AcceptVisitors(TSqlFragment tree)
		{
			//tree.Accept(new OrderByClauseVisitor());
		}
	}
}