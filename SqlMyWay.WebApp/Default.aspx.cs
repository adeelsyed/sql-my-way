using System;
using System.Text;
using SqlMyWay.Core;
using System.IO;

namespace SqlMyWay.WebApp
{
	public partial class Default : System.Web.UI.Page
	{      
#if DEBUG
        protected void Page_Load(EventArgs e)
        {          
            UseSampleScriptLink_Click(null, null);
            FormatButton_Click(null, null);
        }
#endif
        
        protected void FormatButton_Click(object sender, EventArgs e)
		{
			if (!ValidateInput())
				return;

            string sql = FileUploader.HasFile ? Encoding.Default.GetString(FileUploader.FileBytes) : InputSqlTextBox.Text;

            OutputSqlTextBox.Text =
                EditorsChoiceOption.Checked ? Utility.GetSqlMyWayEditorsChoiceFormattedSql(sql) :
                PoorMansOption.Checked ? Utility.GetPoorMansFormattedSql(sql) :
                MicrosoftOption.Checked ? Utility.GetMicrosoftFormattedSql(sql) :
                Utility.GetSqlMyWay(sql, GetOptions());
		}
        protected void UseSampleScriptLink_Click(object sender, EventArgs e)
        {
            InputSqlTextBox.Text = File.ReadAllText("C:\\Users\\Adeel\\Documents\\Visual Studio 2013\\Projects\\SqlMyWay\\SqlInput.sql");
        }
        protected void FormatOption_Changed(object sender, EventArgs e)
        {
            OptionsPanel.Enabled = CustomOption.Checked;
        }

        private bool ValidateInput()
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
        private SqlMyWayOptions GetOptions()
        {
            throw new NotImplementedException();
        }


    }
}