using System;
using System.Text;
using SqlMyWay.Core;
using System.IO;

namespace SqlMyWay.WebApp
{
	public partial class Default : System.Web.UI.Page
	{
#if DEBUG
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            
            //automatically format sample text using selected options
                string sql = File.ReadAllText("C:\\Users\\Adeel\\Documents\\Visual Studio 2013\\Projects\\SqlMyWay\\SqlInput.sql");
                OutputSqlTextBox.Text = Utility.GetSqlMyWayEditorsChoiceFormattedSql(sql);
                OutputSqlTextBox.Height = 1000;
        }
#endif
        
        protected void FormatButton_Click(object sender, EventArgs e)
		{
			if (!ValidateInput())
				return;

            string sql = FileUploader.HasFile ? Encoding.Default.GetString(FileUploader.FileBytes) : InputSqlTextBox.Text;

            OutputSqlTextBox.Text = 
                EditorsChoiceOption.Checked ? Utility.GetSqlMyWayEditorsChoiceFormattedSql(sql) : 
                MicrosoftOption.Checked ? Utility.GetMicrosoftFormattedSql(sql) : 
                Utility.GetPoorMansFormattedSql(sql);
		}

		private bool ValidateInput()
		{
			if (FileUploader.HasFile && InputSqlTextBox.Text.Trim().Length > 0)
			{
				//invalid
				ErrorMsg.Text = "Please choose one input method or the other. Either select a file, or paste SQL in the text box. You cannot do both.";
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
	}
}