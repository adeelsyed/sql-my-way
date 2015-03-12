<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SqlMyWay.WebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>SQL My Way</title>
	<script type="text/javascript">
		function CopyToClipboard()
		{
			var formattedSql = document.getElementById('<%=OutputSqlTextBox.ClientID%>').value;
			window.clipboardData.setData('Text', formattedSql);
			alert('Copied formatted SQL to clipboard');
		}
	</script>
    <script type="text/javascript">
        var appInsights=window.appInsights||function(config){
            function s(config){t[config]=function(){var i=arguments;t.queue.push(function(){t[config].apply(t,i)})}}var t={config:config},r=document,f=window,e="script",o=r.createElement(e),i,u;for(o.src=config.url||"//az416426.vo.msecnd.net/scripts/a/ai.0.js",r.getElementsByTagName(e)[0].parentNode.appendChild(o),t.cookie=r.cookie,t.queue=[],i=["Event","Exception","Metric","PageView","Trace"];i.length;)s("track"+i.pop());return config.disableExceptionTracking||(i="onerror",s("_"+i),u=f[i],f[i]=function(config,r,f,e,o){var s=u&&u(config,r,f,e,o);return s!==!0&&t["_"+i](config,r,f,e,o),s}),t
        }({
            instrumentationKey:"036f905b-a762-4316-bd97-7acf20262577"
        });
    
        window.appInsights=appInsights;
        appInsights.trackPageView();
    </script>
</head>
<body>
    <form id="form1" runat="server">
		<div id="right" style="float: right; width: 300px">
			<p style="font-weight: bold">Formatting Options</p>
			<asp:RadioButton runat="server" GroupName="FormatStyle" ID="MicrosoftScriptGeneratorFormatStyleOption"  Text="Microsoft Script Generator" />
			<br/>
			<asp:RadioButton runat="server" GroupName="FormatStyle" ID="PoorMansTSqlFormatterFormatStyleOption"  Text="Poor Man's T-SQL Formatter" Checked="True" />
		</div>
		<div id="left" style="margin-right: 320px;">
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
			<br/><br/>
			<div>
				<div style="float: left">Formatted SQL:</div>
				<div style="float: right">
					<input type="button" value="Copy Formatted SQL to Clipboard" onclick="CopyToClipboard()" />
				</div>
			</div>
			<br/><br/>
			<asp:TextBox runat="server" ID="OutputSqlTextBox" TextMode="MultiLine" Rows="20" Width="100%" ReadOnly="True"></asp:TextBox>
			<br/>
		</div>
    </form>
</body>
</html>
