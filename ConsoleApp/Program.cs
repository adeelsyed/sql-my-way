using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Reflection;
using System.Linq;

namespace ConsoleApp
{
	static class Program
	{
		static private readonly StringBuilder XmlBuilder = new StringBuilder();

		static void Main()
		{
			TSqlFragment tree = GetTSqlFragmentTree("..//..//testsql.sql");

            //DefaultFormatter(SqlFragment, null);
			//XmlVisualiser(SqlFragment);
			//VisitorPattern(SqlFragment);
			Beta(tree);
			
			Console.ReadLine();
		}

		private static TSqlFragment GetTSqlFragmentTree(string path)
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
        private static void DefaultFormatter(TSqlFragment sqlFragment, SqlScriptGeneratorOptions options = null)
        {
			var generator = new Sql110ScriptGenerator(options ?? new SqlScriptGeneratorOptions());

			using (var consoleWriter = new StreamWriter(Console.OpenStandardOutput()))
                generator.GenerateScript(sqlFragment, consoleWriter);
        }
		private static void XmlVisualiser(TSqlFragment sqlFragment)
		{
			XmlVisualizerScriptDomWalk(sqlFragment, "root");
			using(var writer = new StreamWriter(File.Create("..//..//XmlVisualiser.xml")))
				writer.Write(XmlBuilder);
		}
		private static void XmlVisualizerScriptDomWalk(object fragment, string memberName)
		{
			if (fragment.GetType().BaseType.Name != "Enum")
			{
				XmlBuilder.AppendLine("<" + fragment.GetType().Name + " memberName = '" + memberName + "'>");
			}
			else
			{
				XmlBuilder.AppendLine("<" + fragment.GetType().Name + "." + fragment.ToString() + "/>");
				return;
			}

			Type t = fragment.GetType();

			PropertyInfo[] pibase;
			if (null == t.BaseType)
			{
				pibase = null;
			}
			else
			{
				pibase = t.BaseType.GetProperties();
			}

			foreach (PropertyInfo pi in t.GetProperties())
			{
				if (pi.GetIndexParameters().Length != 0)
				{
					continue;
				}

				if (pi.PropertyType.BaseType != null)
				{
					if (pi.PropertyType.BaseType.Name == "ValueType")
					{
						XmlBuilder.Append("<" + pi.Name + ">" + pi.GetValue(fragment, null).ToString() + "</" + pi.Name + ">");
						continue;
					}
				}

				if (pi.PropertyType.Name.Contains(@"IList`1"))
				{
					if ("ScriptTokenStream" != pi.Name)
					{
						var listMembers = pi.GetValue(fragment, null) as IEnumerable<object>;

						foreach (object listItem in listMembers)
						{
							XmlVisualizerScriptDomWalk(listItem, pi.Name);
						}
					}
				}
				else
				{
					object childObj = pi.GetValue(fragment, null);

					if (childObj != null)
					{
						if (childObj.GetType() == typeof(string))
						{
							XmlBuilder.Append(pi.GetValue(fragment, null));
						}
						else
						{
							XmlVisualizerScriptDomWalk(childObj, pi.Name);
						}
					}
				}
			}

			XmlBuilder.AppendLine("</" + fragment.GetType().Name + ">");
		}
		private static void VisitorPattern(TSqlFragment sqlFragment)
		{
			sqlFragment.Accept(new BatchVisitor());
			Console.WriteLine("-------------------");
			sqlFragment.Accept(new StatementVisitor());
		}
		private static void Beta(TSqlFragment sqlFragment)
		{
			//format order by
			sqlFragment.Accept(new OrderByVisitor());

			Console.Write(sqlFragment.ToText());
		}
	}

	internal class BatchVisitor : TSqlFragmentVisitor
	{
		public override void ExplicitVisit(TSqlBatch batch)
		{
			Console.WriteLine(batch.ToText());
			Console.WriteLine("GO");
		}
	}
	internal class StatementVisitor : TSqlFragmentVisitor
	{
		public override void Visit(TSqlStatement statement)
		{
			Console.WriteLine(statement.ToText());
			Console.WriteLine();
		}
	}
	internal class OrderByVisitor : TSqlFragmentVisitor
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
	internal static class Extensions
	{
		/// <summary>
		/// Extends TSqlFragment to allow easy conversion of a fragment to a string.
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
