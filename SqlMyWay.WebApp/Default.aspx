<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SqlMyWay.WebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
	    <p>Browse for a file or paste your unformatted SQL below</p>
		<asp:Panel runat="server" ID="ErrorPanel" Visible="False">
			<asp:Label runat="server" ID="ErrorMsg" ForeColor="Red"></asp:Label>
			<br/><br/>
		</asp:Panel>
		<asp:FileUpload runat="server" ID="FileUploader" Width="750" />
		<br/><br/>
	    <asp:TextBox runat="server" ID="InputSqlTextBox" TextMode="MultiLine" Rows="20" Width="100%"></asp:TextBox>
		<br/><br/>
		<asp:Button runat="server" ID="FormatButton" Text="Format SQL" OnClick="FormatButton_Click" />
		<p>Formatted SQL:</p>
		<asp:TextBox runat="server" ID="OutputSqlTextBox" TextMode="MultiLine" Rows="20" Width="100%"></asp:TextBox>
		<br/>
    </form>
</body>
</html>
