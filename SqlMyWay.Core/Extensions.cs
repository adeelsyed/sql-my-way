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
		public static string ToXml(this TSqlFragment fragment)
		{
			XmlVisualizerRecursiveScriptDomWalk(fragment, "root");
			return XmlBuilder.ToString();
		}

		/// <summary>
		/// Courtesy of Arvind Shyamsundar (http://blogs.msdn.com/b/arvindsh/archive/2013/10/30/xml-visualizer-for-the-transactsql-scriptdom-parse-tree.aspx)
		/// </summary>
		private static void XmlVisualizerRecursiveScriptDomWalk(object fragment, string memberName)
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
							XmlVisualizerRecursiveScriptDomWalk(listItem, pi.Name);
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
							XmlVisualizerRecursiveScriptDomWalk(childObj, pi.Name);
						}
					}
				}
			}

			XmlBuilder.AppendLine("</" + fragment.GetType().Name + ">");
		}
	}
}
