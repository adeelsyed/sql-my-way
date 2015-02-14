using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace ConsoleApp
{
	static class Program
	{
		static void Main()
		{
			TSqlFragment sqlFragment = ParseSql("..//..//testsql.sql");

			//default formatting. can take an SqlScriptGeneratorOptions argument for very basic formatting options
			Console.WriteLine("--DEFAULT FORMATTING\n");
			GenerateScript(sqlFragment, null);

			//prototype of propective vistor pattern for formatting fragment by fragment
			Console.WriteLine("--JUST SELECT STATEMENTS\n");
			var maid = new SqlMaid();
			sqlFragment.Accept(maid);

			Console.ReadLine();
		}

		private static TSqlFragment ParseSql(string path)
		{
			using (TextReader txtRdr = new StreamReader(path))
			{
				IList<ParseError> errors;
				TSqlFragment sqlFragment = new TSql110Parser(true).Parse(txtRdr, out errors);

				if (errors.Count > 0)
				{
					foreach (var parseError in errors)
						Console.WriteLine("Error {0} on line {2}, column {3}: {1}", parseError.Number, parseError.Message, parseError.Line, parseError.Column);

					throw new Exception("SQL has errors. See console for details.");
				}

				return sqlFragment;
			}
		}

		private static void GenerateScript(TSqlFragment sqlFragment, SqlScriptGeneratorOptions options = null)
		{
			SqlScriptGenerator generator = options == null ? new Sql110ScriptGenerator() : new Sql110ScriptGenerator(options);

			using (TextWriter consoleWriter = new StreamWriter(Console.OpenStandardOutput()))
			{
				generator.GenerateScript(sqlFragment, consoleWriter);
			}
		}
	}

	/// <summary>
	/// Visits each TSqlFragment in a TSqlFragment tree and applies formatting rules based on fragment type
	/// </summary>
	internal class SqlMaid : TSqlFragmentVisitor
	{
		//only write back if is a select statement
		public override void ExplicitVisit(SelectStatement node)
		{
			Console.WriteLine(node.ToText());
			Console.WriteLine();
		}
	}

	internal static class Extensions
	{
		/// <summary>
		/// Extends TSqlFragment to allow easy conversion of a node to a string.
		/// </summary>
		public static string ToText(this TSqlFragment fragment)
		{
			StringBuilder tokenText = new StringBuilder();

			for (int i = fragment.FirstTokenIndex; i <= fragment.LastTokenIndex; i++)
				tokenText.Append(fragment.ScriptTokenStream[i].Text);

			return tokenText.ToString();
		}
	}
}
