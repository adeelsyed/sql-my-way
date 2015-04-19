using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlMyWay.Core
{
    public struct SqlMyWayOptions
    {
        //not using automatic properties because they cannot be used as out parameters
        public int LineBreaks_BetweenStatements;
        public int LineBreaks_BetweenClauses;
        public bool Capitalize_Keywords;
        public bool Capitalize_DataTypes;
        public bool Capitalize_BuiltInFunctions;
		public bool CommaLists_Stacked;
		public bool CommaLists_TrailingCommas;
        public bool Joins_Indented;
        public bool Joins_TableOnSameLine;
        public bool Joins_OnClauseOnSameLine;
        public bool Parentheses_SpacesOutside;
        public bool Parentheses_SpacesInside;
        public bool Semicolons_Add;
        public bool Comments_ExtraLineBeforeBlocks;
        public bool Comments_ExtraLineAfterBlocks;
    }

}
