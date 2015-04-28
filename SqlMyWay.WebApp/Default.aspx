<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SqlMyWay.WebApp.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>SQL My Way</title>
    <style>
        body { margin: 0 10px; }
        .myWay { font-family:'Freestyle Script', 'Comic Sans MS', cursive }
        .optionsPanel { background-color: #ddd; border-radius: 10px; padding: 10px }
        .optionsPanel > ul { margin : 5px 0; padding: 0 0 0 15px}
        .optionsPanel li { margin: 5px;}
        .tbNum { width: 30px }
    </style>
    <script type="text/javascript">
        //Copy to Clipboard
        function CopyToClipboard() {
            var formattedSql = document.getElementById('<%=OutputSqlTextBox.ClientID%>').value;
            window.clipboardData.setData('Text', formattedSql);
            alert('Copied formatted SQL to clipboard');
        }

        //azure application insights
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
        <div id="header">
            <h1>SQL <span class="myWay">My Way</span></h1>
			<ul style="position:absolute; right: 30px; top: 20px; padding: 0; margin: 0;">
				<li><a href="SqlMyWayAddIn.zip">Download SSMS add-in</a></li>
				<li><a href="https://github.com/adeelsyed/sql-my-way">Download/view source code</a></li>
			</ul>
        </div>
		<div id="right" style="float: right; width: 350px">
			<p style="margin-top:0">
                <img src="img/steptwo.png" style="vertical-align:middle; margin-right: 5px;" />
                <b>Select Formatting Style</b>
			</p>
            <p>
			    <asp:RadioButton runat="server" GroupName="FormatStyle" ID="EditorsChoiceOption" Text="SQL My Way Editor's Choice" OnCheckedChanged="FormatOption_Changed" AutoPostBack="true" />
			    <br />
			    <asp:RadioButton runat="server" GroupName="FormatStyle" ID="PoorMansOption" Text="Poor Man's T-SQL Formatter"  OnCheckedChanged="FormatOption_Changed" AutoPostBack="true" />
			    <br />
			    <asp:RadioButton runat="server" GroupName="FormatStyle" ID="MicrosoftOption" Text="Microsoft Script Generator"  OnCheckedChanged="FormatOption_Changed" AutoPostBack="true" />
			    <br /><br />
			    <asp:RadioButton runat="server" GroupName="FormatStyle" ID="CustomOption" Text="My Way" CssClass="myWay" Font-Size="22px" Font-Bold="true" OnCheckedChanged="FormatOption_Changed" AutoPostBack="true" />
            </p>
            <asp:Panel ID="OptionsPanel" runat="server" Enabled="true" CssClass="optionsPanel">

                <b>Line Breaks</b>
                <ul>
                    <li>Place <asp:TextBox ID="LineBreaks_BetweenStatements" runat="server" CssClass="tbNum" /> line breaks between statements</li>
                    <li>Place <asp:TextBox ID="LineBreaks_BetweenClauses" runat="server" CssClass="tbNum" /> line breaks between clauses</li>
                </ul>
                <b>Capitalization</b>
                <ul>
                    <li><asp:CheckBox ID="Capitalize_Keywords" runat="server" /> Capitalize keywords</li>                    
                    <li><asp:CheckBox ID="Capitalize_DataTypes" runat="server" /> Capitalize data types</li>                    
                    <li><asp:CheckBox ID="Capitalize_BuiltInFunctions" runat="server" /> Capitalize built-in functions</li>                    
                </ul>
                <b>Comma-Separated Lists</b>
                <ul>
                    <li>
						<asp:RadioButton runat="server" GroupName="ListStyle" ID="CommaLists_Stacked" Text="Stacked" OnCheckedChanged="CommaListStyle_Changed" AutoPostBack="true" />
						<asp:RadioButton runat="server" GroupName="ListStyle" ID="CommaLists_Inline" Text="Inline" OnCheckedChanged="CommaListStyle_Changed" AutoPostBack="true" />
                    </li>                    
                    <li><asp:CheckBox ID="CommaLists_TrailingCommas" runat="server" /> Trailing commas</li>                    
                </ul>
                <b>Joins</b>
                <ul>
                    <li><asp:CheckBox ID="Joins_Indented" runat="server" /> Indent joins</li>
                    <li><asp:CheckBox ID="Joins_TableOnSameLine" runat="server" /> Put table on same line</li>
                    <li><asp:CheckBox ID="Joins_OnClauseOnSameLine" runat="server" /> Put ON clause on same line</li>
                </ul>
                <b>Parentheses</b>
                <ul>
                    <li><asp:CheckBox ID="Parentheses_SpacesOutside" runat="server" /> Spaces outside</li>
                    <li><asp:CheckBox ID="Parentheses_SpacesInside" runat="server" /> Spaces inside</li>
                </ul>
                <b>Semicolon</b>
                <ul>
                    <li><asp:CheckBox ID="Semicolons_Add" runat="server" /> Add semicolon at end of each statement</li>
                </ul>
                <b>Comments</b>
                <ul>
                    <li><asp:CheckBox ID="Comments_ExtraLineBeforeBlocks" runat="server" /> Add extra line before block comments</li>
                    <li><asp:CheckBox ID="Comments_ExtraLineAfterBlocks" runat="server" /> Add extra line after block comments</li>
                </ul>
            </asp:Panel>
		</div>
		<div id="left" style="margin-right: 370px;">	
            <p>
                <img src="img/stepone.png" style="vertical-align:middle; margin-right: 5px;" />
                <b>Input SQL</b>: Choose a SQL file to format, <b>or</b> paste SQL directly into the textbox below, <b>or</b> <asp:LinkButton ID="UseSampleScriptLink" runat="server" OnClick="UseSampleScriptLink_Click" Text="try out a sample script" />.
            </p>
			<asp:Panel runat="server" ID="ErrorPanel" Visible="False">
				<asp:Label runat="server" ID="ErrorMsg" ForeColor="Red"></asp:Label>
				<br/><br/>
			</asp:Panel>
			<asp:FileUpload runat="server" ID="FileUploader" Width="750" />
			<br/><br/>
			<asp:TextBox runat="server" ID="InputSqlTextBox" TextMode="MultiLine" Rows="20" Width="100%"></asp:TextBox>
			<br/><br/>
            <div>
                <div style="float: left">
                    <img src="img/stepthree.png" style="vertical-align:middle; margin-right: 5px;" />
			        <asp:Button runat="server" ID="FormatButton" Text="Format SQL" Font-Bold="true" OnClick="FormatButton_Click" />  
                </div>
			    <div id="divCopyToClipboard" style="float: right">
     			    <input type="button" value="Copy Formatted SQL to Clipboard" onclick="CopyToClipboard()" style="font-weight:bold" />
			    </div>
            </div>
			<br/><br/><br/>
			<asp:TextBox runat="server" ID="OutputSqlTextBox" TextMode="MultiLine" Rows="20" Width="100%" ReadOnly="True"></asp:TextBox>
			<br/>
		</div>
    </form>
    <script type="text/javascript">
        //Copy to Clipboard -- IE only
        var ua = window.navigator.userAgent;
        if (ua.indexOf("MSIE ") + ua.indexOf('Trident/') + ua.indexOf('Edge/') <= 0) {
            //not ie
            divCopyToClipboard.style.display = 'none';
        }
    </script>
</body>
</html>
