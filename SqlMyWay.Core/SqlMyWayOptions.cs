using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlMyWay.Core
{
    public class SqlMyWayOptions
    {
	    public SqlMyWayOptions()
	    {
			//set defaults
			LineBreaks_BetweenStatements = 2;
			LineBreaks_BetweenClauses = 1;
			Capitalize_Keywords = true;
			Capitalize_DataTypes = true;
			Capitalize_BuiltInFunctions = true;
			CommaLists_Stacked = true;
			CommaLists_TrailingCommas = true;
			Joins_Indented = true;
			Joins_TableOnSameLine = true;
			Joins_OnClauseOnSameLine = true;
			Parentheses_SpacesOutside = true;
			Parentheses_SpacesInside = false;
			Semicolons_Add = false;
			Comments_ExtraLineBeforeBlocks = false;
			Comments_ExtraLineAfterBlocks = false;
		}

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
