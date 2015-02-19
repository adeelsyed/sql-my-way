using System;
using SqlMyWay.Core;

namespace SqlMyWay.WebApp
{
	public partial class Default : System.Web.UI.Page
	{
		protected void FormatButton_Click(object sender, EventArgs e)
		{
			OutputSqlTextBox.Text = Utility.GetPoorMansFormattedSql(InputSqlTextBox.Text);
		}
	}
}