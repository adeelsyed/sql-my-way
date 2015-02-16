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

		static void Main()
		{
			TSqlFragment sqlFragment = ParseSql("..//..//testsql.sql");

            //default formatting. can take an SqlScriptGeneratorOptions argument for very basic formatting options
            Console.WriteLine("--DEFAULT FORMATTING\n");
            GenerateScript(sqlFragment, null);

            //token iteration
            foreach (var fragment in sqlFragment.ScriptTokenStream.Where(t => t.TokenType != TSqlTokenType.WhiteSpace))
            {
                Console.Write(fragment.Text);
                Console.SetCursorPosition(20, Console.CursorTop);
                Console.Write(fragment.TokenType);
                Console.SetCursorPosition(40, Console.CursorTop);
                Console.WriteLine(fragment.IsKeyword());
            }

            foreach (var t in sqlFragment.ScriptTokenStream.Where(t => t.TokenType != TSqlTokenType.WhiteSpace))
            {
                if (t.IsKeyword())
                    Console.WriteLine();

                Console.Write(t.Text);

                if (t.TokenType == TSqlTokenType.Comma || t.IsKeyword())
                    Console.WriteLine();
            }

            //xml visualizer
            StringBuilder result = new StringBuilder();
            ScriptDomWalk(sqlFragment, "root", result);
            Console.Write(result);

            //visitor pattern
            sqlFragment.Accept(new PutEachStatementOnItsOwnLine());
            sqlFragment.Accept(new PutEachClauseOnItsOwnLine());

			Console.ReadLine();
		}

        //TODO: move to shared library
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

        private static void ScriptDomWalk(object fragment, string memberName, StringBuilder result)
        {
            if (fragment.GetType().BaseType.Name != "Enum")
            {
                result.AppendLine("<" + fragment.GetType().Name + " memberName = '" + memberName + "'>");
            }
            else
            {
                result.AppendLine("<" + fragment.GetType().Name + "." + fragment.ToString() + "/>");
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
                List<string> ignore = new List<string>() { "StartOffset", "FragmentLength", "StartLine", "StartColumn", "FirstTokenIndex", "LastTokenIndex" };
                if (pi.GetIndexParameters().Length != 0 || ignore.Contains(pi.Name))
                {
                    continue;
                }

                if (pi.PropertyType.BaseType != null)
                {
                    if (pi.PropertyType.BaseType.Name == "ValueType")
                    {
                        result.Append("<" + pi.Name + ">" + pi.GetValue(fragment, null).ToString() + "</" + pi.Name + ">");
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
                            ScriptDomWalk(listItem, pi.Name, result);
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
                            result.Append(pi.GetValue(fragment, null));
                        }
                        else
                        {
                            ScriptDomWalk(childObj, pi.Name, result);
                        }
                    }
                }
            }

            result.AppendLine("</" + fragment.GetType().Name + ">");
        }

	}

    internal class PutEachStatementOnItsOwnLine : TSqlFragmentVisitor
    {
        public override void Visit(TSqlStatement node)
        {
            Console.WriteLine(node.ToText());
            Console.WriteLine();
        }
    }
    internal class PutEachClauseOnItsOwnLine : TSqlFragmentVisitor
    {
        public override void Visit(TSqlFragment fragment)
        {
            Console.WriteLine(fragment.ToText());
        }

        public override void Visit(TSqlBatch batch)
        {
            foreach(var statement in batch.Statements)
            {
                Console.WriteLine(statement.ToText());
            }
        }
    }

    //TODO: move to shared library
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
