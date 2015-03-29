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

        private void SetDefaultMyWayOptions()
        {
            NLineBreaksBetweenStatements.Text = "2";
            NLineBreaksBetweenClauses.Text = "1";
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
            var options = new SqlMyWayOptions();

            int.TryParse(NLineBreaksBetweenStatements.Text, out options.NLineBreaksBetweenStatements);
            int.TryParse(NLineBreaksBetweenClauses.Text, out options.NLineBreaksBetweenClauses);

            return options;
        }


    }
}