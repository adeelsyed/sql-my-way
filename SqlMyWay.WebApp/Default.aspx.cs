using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;
using SqlMyWay.Core;

namespace SqlMyWay.WebApp
{
	public partial class Default : System.Web.UI.Page
	{
		protected void FormatButton_Click(object sender, EventArgs e)
		{
			if (!ValidateInput())
				return;

			string sql = FileUploader.HasFile ? Encoding.Default.GetString(FileUploader.FileBytes) : InputSqlTextBox.Text;

			OutputSqlTextBox.Text = Utility.GetPoorMansFormattedSql(sql);
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