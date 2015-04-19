using System;
using System.Text;
using SqlMyWay.Core;
using System.IO;

namespace SqlMyWay.WebApp
{
	public partial class Default : System.Web.UI.Page
	{      
        protected override void OnLoad(EventArgs e)
        {
            //first page load; set defaults
            if(!IsPostBack)
            {
                CustomOption.Checked = true;
                SetDefaultMyWayOptions();

                //testing only
                //UseSampleScriptLink_Click(null, null);
                //FormatButton_Click(null, null);
            }

        }

        #region event handlers

        protected void FormatButton_Click(object sender, EventArgs e)
		{
            //get sql
			if (!ValidateSqlInput())
				return;
            string sql = FileUploader.HasFile ? Encoding.Default.GetString(FileUploader.FileBytes) : InputSqlTextBox.Text;

            //format sql based on selected style
            OutputSqlTextBox.Text =
                EditorsChoiceOption.Checked ? Utility.GetSqlMyWayEditorsChoiceFormattedSql(sql) :
                PoorMansOption.Checked ? Utility.GetPoorMansFormattedSql(sql) :
                MicrosoftOption.Checked ? Utility.GetMicrosoftFormattedSql(sql) :
                Utility.GetSqlMyWay(sql, GetMyWayOptions());
		}
        protected void UseSampleScriptLink_Click(object sender, EventArgs e)
        {
            InputSqlTextBox.Text = File.ReadAllText(Server.MapPath("~\\SqlInput.sql"));
        }
		protected void FormatOption_Changed(object sender, EventArgs e)
		{
			OptionsPanel.Enabled = CustomOption.Checked;
		}
		protected void CommaListStyle_Changed(object sender, EventArgs e)
		{
			CommaLists_TrailingCommas.Enabled = CommaLists_Stacked.Checked;
			CommaLists_TrailingCommas.Checked = true;
		}

        #endregion

        #region private methods

        private void SetDefaultMyWayOptions()
        {
            LineBreaks_BetweenStatements.Text = "2";
            LineBreaks_BetweenClauses.Text = "1";
            Capitalize_Keywords.Checked = true;
            Capitalize_DataTypes.Checked = true;
            Capitalize_BuiltInFunctions.Checked = true;
			CommaLists_Inline.Checked = false;
			CommaLists_Stacked.Checked = true;
			CommaLists_TrailingCommas.Checked = true;
            Joins_Indented.Checked = true;
            Joins_TableOnSameLine.Checked = true;
            Joins_OnClauseOnSameLine.Checked = true;
            Parentheses_SpacesOutside.Checked = true;
            Parentheses_SpacesInside.Checked = false;
            Semicolons_Add.Checked = false;
            Comments_ExtraLineBeforeBlocks.Checked = false;
            Comments_ExtraLineAfterBlocks.Checked = false;
        }
        private bool ValidateSqlInput()
        {
            if (FileUploader.HasFile && InputSqlTextBox.Text.Trim().Length > 0)
            {
                ErrorMsg.Text = "Please choose one input method or the other. Either select a file, or paste SQL in the text box. You cannot do both.";
                ErrorPanel.Visible = true;
                return false;
            }
            else if(FileUploader.HasFile && !FileUploader.FileName.EndsWith(".sql"))
            {
                ErrorMsg.Text = "You must select a .sql file";
                ErrorPanel.Visible = true;
                return false;
            }
            else
            {
                //valid
                ErrorPanel.Visible = false;
                return true;
            }
        }
        private SqlMyWayOptions GetMyWayOptions()
        {
            var o = new SqlMyWayOptions();

            int.TryParse(LineBreaks_BetweenStatements.Text, out o.LineBreaks_BetweenStatements);
            int.TryParse(LineBreaks_BetweenClauses.Text, out o.LineBreaks_BetweenClauses);
            o.Capitalize_Keywords = Capitalize_Keywords.Checked;
            o.Capitalize_DataTypes = Capitalize_DataTypes.Checked;
            o.Capitalize_BuiltInFunctions = Capitalize_BuiltInFunctions.Checked;
			o.CommaLists_Stacked = CommaLists_Stacked.Checked;
			o.CommaLists_TrailingCommas = CommaLists_TrailingCommas.Checked;
            o.Joins_Indented = Joins_Indented.Checked;
            o.Joins_TableOnSameLine = Joins_TableOnSameLine.Checked;
            o.Joins_OnClauseOnSameLine = Joins_OnClauseOnSameLine.Checked;
            o.Parentheses_SpacesOutside = Parentheses_SpacesOutside.Checked;
            o.Parentheses_SpacesInside = Parentheses_SpacesInside.Checked;
            o.Semicolons_Add = Semicolons_Add.Checked;
            o.Comments_ExtraLineBeforeBlocks = Comments_ExtraLineBeforeBlocks.Checked;
            o.Comments_ExtraLineAfterBlocks = Comments_ExtraLineAfterBlocks.Checked;

            return o;
        }
        #endregion

    }
}