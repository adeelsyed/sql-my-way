<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SqlMyWay.WebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
	    <p>Enter the SQL code here</p>
	    <asp:TextBox runat="server" ID="InputSqlTextBox" TextMode="MultiLine" Rows="20" Width="100%"></asp:TextBox>
		<asp:Button runat="server" ID="FormatButton" Text="Format SQL" OnClick="FormatButton_Click" />
		<p>Formatted SQL:</p>
		<asp:TextBox runat="server" ID="OutputSqlTextBox" TextMode="MultiLine" Rows="20" Width="100%"></asp:TextBox>
    </form>
</body>
</html>
