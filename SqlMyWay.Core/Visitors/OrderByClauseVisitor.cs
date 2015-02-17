using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace SqlMyWay.Core.Visitors
{
	public class OrderByClauseVisitor : TSqlFragmentVisitor
	{
		public override void ExplicitVisit(OrderByClause node)
		{
			//get tokens
			var sb = new StringBuilder();
			for (int i = node.FirstTokenIndex; i <= node.LastTokenIndex; i++)
			{
				var t = node.ScriptTokenStream[i];
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
