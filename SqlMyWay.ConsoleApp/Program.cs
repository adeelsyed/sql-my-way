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
			//paths
			const string sqlInput = "..\\..\\SqlInput.sql";
			const string sqlOutput = "..\\..\\SqlOutput.sql";

			//input
			string sql = File.ReadAllText(sqlInput);

			//Microsoft Default Style
			string ms = Utility.GetMicrosoftFormattedSql(sql);
			File.WriteAllText(sqlOutput, ms);
			
			//Poor Man's Default Style
			string pm = Utility.GetPoorMansFormattedSql(sql);
			File.WriteAllText(sqlOutput, pm);

			//debug
			CreateDebugFiles(sql);

			//write formatted sql to console window
			Console.WriteLine(ms);
			Console.WriteLine("\n\n------------------------------------------------\n\n");
			Console.WriteLine(pm);
			Console.Read();
		}

		private static void CreateDebugFiles(string sql)
		{
			var tree = Utility.GetTSqlFragmentTree(sql);
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
