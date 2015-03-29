using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMyWay.Core
{
    static class Const
    {
        public static string[] ClauseKeywords = new string[]
        {
            "WITH",
            "SELECT",
            "INTO",
            "FROM",
            "JOIN",
            "INNER JOIN",
            "LEFT JOIN",
            "LEFT OUTER JOIN",
            "RIGHT OUTER JOIN",
            "FULL OUTER JOIN",
            "OUTER JOIN",
            "WHERE",
            "GROUP BY",
            "HAVING",
            "ORDER BY",
            "UNION",
            "INSERT",
            "INSERT INTO",
            "DELETE",
            "DROP",
            "UPDATE"
        };
    }
}
