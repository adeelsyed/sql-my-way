using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace SqlMyWay.Core
{
	public static class Extensions
	{
		static private readonly StringBuilder XmlBuilder = new StringBuilder();
		private static readonly List<string> CharPosInfoFields = new List<string> {"StartOffset", "FragmentLength", "StartLine", "StartColumn", "FirstTokenIndex", "LastTokenIndex"};

		/// <summary>
		/// Converts the fragment to SQL text.
		/// </summary>
		public static string ToText(this TSqlFragment fragment)
		{
			return string.Join("", fragment.ScriptTokenStream
				.Skip(fragment.FirstTokenIndex)
				.Take(fragment.LastTokenIndex - fragment.FirstTokenIndex + 1)
				.Select(t => t.Text));
		}

		/// <summary>
		/// Converts the fragment into an XML representation. Useful for visualizing the ScriptDom.
		/// </summary>
		/// <param name="fragment"></param>
		/// <param name="outputCharPosInfo">Includes details about the line, column, offset, length, and token range of each fragment</param>
		public static string ToXml(this TSqlFragment fragment, bool outputCharPosInfo = false)
		{
			XmlVisualizerRecursiveScriptDomWalk(fragment, "root", outputCharPosInfo);
			return XmlBuilder.ToString();
		}

		/// <summary>
		/// Courtesy of Arvind Shyamsundar (http://blogs.msdn.com/b/arvindsh/archive/2013/10/30/xml-visualizer-for-the-transactsql-scriptdom-parse-tree.aspx)
		/// Added tha ability to ignore 
		/// </summary>
		private static void XmlVisualizerRecursiveScriptDomWalk(object fragment, string memberName, bool outputCharPosInfo)
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
				if (pi.GetIndexParameters().Length != 0 || !outputCharPosInfo && CharPosInfoFields.Contains(pi.Name))
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
							XmlVisualizerRecursiveScriptDomWalk(listItem, pi.Name, outputCharPosInfo);
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
							XmlVisualizerRecursiveScriptDomWalk(childObj, pi.Name, outputCharPosInfo);
						}
					}
				}
			}

			XmlBuilder.AppendLine("</" + fragment.GetType().Name + ">");
		}
	}
}
