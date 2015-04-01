using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlMyWay.Core
{
    public struct SqlMyWayOptions
    {
        //not using automatic properties because they cannot be used as out parameters
        public int NLineBreaksBetweenStatements;
        public int NLineBreaksBetweenClauses;
        public bool CapitalizeKeywords;
        public bool CapitalizeDataTypes;
        public bool CapitalizeBuiltInFunctions;
		public bool CommaListStacked;
		public bool CommaListTrailingCommas;
    }

}
