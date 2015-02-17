using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace SqlMyWay.Core.Visitors
{
	public class OrderByClauseVisitor : TSqlFragmentVisitor
	{
		public override void Visit(OrderByClause f)
		{
			//get tokens
			var sb = new StringBuilder();
			for (int i = f.FirstTokenIndex; i <= f.LastTokenIndex; i++)
			{
				var t = f.ScriptTokenStream[i];
				switch (t.TokenType)
				{
					case TSqlTokenType.Order:
						t.Text = "\nORDER BY\n";
						break;
					case TSqlTokenType.Identifier:
						t.Text = "\t" + t.Text;
						break;
					case TSqlTokenType.Comma:
						t.Text = ",\n";
						break;
					case TSqlTokenType.By:
					case TSqlTokenType.WhiteSpace:
						t.Text = "";
						break;
					default:
						sb.Append(t.Text);
						break;
				}

			}
		}
	}
}
