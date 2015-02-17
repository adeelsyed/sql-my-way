using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SqlMyWay.Core;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace SqlMyWay.ConsoleApp
{
	static class Program
	{
		static void Main(string[] args)
		{
			//get args
			string sqlFilePath = args[0]; //default value provided by project: "..\..\testsql.sql"

			//debug
			//CreateDebugFiles(sqlFilePath);

			//write formatted sql to console window
			Console.Write(Utility.FormatSql(sqlFilePath));
			Console.Read();
		}

		private static void CreateDebugFiles(string sqlFilePath)
		{
			var tree = Utility.GetTSqlFragmentTree(sqlFilePath);
			CreateTokenListCsv(tree);
			CreateXmlVisualizer(tree);
		}

		private static void CreateTokenListCsv(TSqlFragment tree)
		{
			var sb = new StringBuilder();

			sb.AppendLine("Id,Text,TokenType,IsKeyword");

			for(int i = 0; i < tree.ScriptTokenStream.Count; i++)
			{
				var token = tree.ScriptTokenStream[i];

				if(token.TokenType == TSqlTokenType.WhiteSpace)
					continue;

				sb.Append(i);
				sb.Append(',');
				sb.Append(token.TokenType == TSqlTokenType.Comma ? "\",\"" : token.TokenType == TSqlTokenType.SingleLineComment ? "'" + token.Text : token.Text);
				sb.Append(',');
				sb.Append(token.TokenType);
				sb.Append(',');
				sb.AppendLine(token.IsKeyword().ToString());
			}

			File.WriteAllText("..\\..\\TokenListCsv.csv", sb.ToString());
		}

		private static void CreateXmlVisualizer(TSqlFragment tree)
		{
			File.WriteAllText("..\\..\\XmlVisualiser.xml", tree.ToXml(true));
		}
	}
}
