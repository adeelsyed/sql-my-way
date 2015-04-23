using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlMyWay.Core;

namespace SqlMyWayAddIn
{
	public partial class OptionsForm : Form
	{
		private readonly SqlMyWayOptionSettings appSettings;

		internal OptionsForm(SqlMyWayOptionSettings appSettings)
		{
			InitializeComponent();

			this.appSettings = appSettings;
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			//load user-selected options to form
			LineBreaks_BetweenStatements.Text = appSettings.LineBreaks_BetweenStatements.ToString();
			LineBreaks_BetweenClauses.Text = appSettings.LineBreaks_BetweenClauses.ToString();
			Capitalize_Keywords.Checked = appSettings.Capitalize_Keywords;
			Capitalize_DataTypes.Checked = appSettings.Capitalize_DataTypes;
			Capitalize_BuiltInFunctions.Checked = appSettings.Capitalize_BuiltInFunctions;
			CommaLists_Inline.Checked = !appSettings.CommaLists_Stacked;
			CommaLists_Stacked.Checked = appSettings.CommaLists_Stacked;
			CommaLists_TrailingCommas.Checked = appSettings.CommaLists_TrailingCommas;
			Joins_Indented.Checked = appSettings.Joins_Indented;
			Joins_TableOnSameLine.Checked = appSettings.Joins_TableOnSameLine;
			Joins_OnClauseOnSameLine.Checked = appSettings.Joins_OnClauseOnSameLine;
			Parentheses_SpacesOutside.Checked = appSettings.Parentheses_SpacesOutside;
			Parentheses_SpacesInside.Checked = appSettings.Parentheses_SpacesInside;
			Semicolons_Add.Checked = appSettings.Semicolons_Add;
			Comments_ExtraLineBeforeBlocks.Checked = appSettings.Comments_ExtraLineBeforeBlocks;
			Comments_ExtraLineAfterBlocks.Checked = appSettings.Comments_ExtraLineAfterBlocks;
		}

		private void CommaListStyle_Changed(object sender, EventArgs e)
		{
			//disable leading commas when values not stacked
			CommaLists_TrailingCommas.Enabled = CommaLists_Stacked.Checked;
			CommaLists_TrailingCommas.Checked = true;
		}

		private void SaveBtn_Click(object sender, EventArgs e)
		{
			//save user-selected options to application setting so they persist
			int i;
			if (int.TryParse(LineBreaks_BetweenStatements.Text, out i))
				appSettings.LineBreaks_BetweenStatements = i;
			if (int.TryParse(LineBreaks_BetweenClauses.Text, out i))
				appSettings.LineBreaks_BetweenClauses = i;
			appSettings.LineBreaks_BetweenStatements = int.TryParse(LineBreaks_BetweenStatements.Text, out i) ? i : 2;
			appSettings.LineBreaks_BetweenClauses = int.TryParse(LineBreaks_BetweenClauses.Text, out i) ? i : 1;
			appSettings.Capitalize_Keywords = Capitalize_Keywords.Checked;
			appSettings.Capitalize_DataTypes = Capitalize_DataTypes.Checked;
			appSettings.Capitalize_BuiltInFunctions = Capitalize_BuiltInFunctions.Checked;
			appSettings.CommaLists_Stacked = CommaLists_Stacked.Checked;
			appSettings.CommaLists_TrailingCommas = CommaLists_TrailingCommas.Checked;
			appSettings.Joins_Indented = Joins_Indented.Checked;
			appSettings.Joins_TableOnSameLine = Joins_TableOnSameLine.Checked;
			appSettings.Joins_OnClauseOnSameLine = Joins_OnClauseOnSameLine.Checked;
			appSettings.Parentheses_SpacesOutside = Parentheses_SpacesOutside.Checked;
			appSettings.Parentheses_SpacesInside = Parentheses_SpacesInside.Checked;
			appSettings.Semicolons_Add = Semicolons_Add.Checked;
			appSettings.Comments_ExtraLineBeforeBlocks = Comments_ExtraLineBeforeBlocks.Checked;
			appSettings.Comments_ExtraLineAfterBlocks = Comments_ExtraLineAfterBlocks.Checked;

			appSettings.Save();
		}



	}
}
