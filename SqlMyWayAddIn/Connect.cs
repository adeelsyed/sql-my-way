using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Extensibility;
using Microsoft.VisualStudio.CommandBars;
using SqlMyWay.Core;
using Thread = System.Threading.Thread;

namespace SqlMyWayAddIn
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
	public class Connect : IDTExtensibility2, IDTCommandTarget
	{
		private AddIn _addInInstance;
		private DTE2 _applicationObject;
		private SqlMyWayOptions sqlMyWayOptions;

		//command event handler
		/// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
		/// <param term='commandName'>The name of the command to execute.</param>
		/// <param term='executeOption'>Describes how the command should be run.</param>
		/// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
		/// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
		/// <param term='handled'>Informs the caller if the command was handled or not.</param>
		/// <seealso class='Exec' />
		public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
		{
			handled = false;
			if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
			{
				if (commandName == "SqlMyWayAddIn.Connect.SqlMyWayFormat")
				{
					string ext = Path.GetExtension(_applicationObject.ActiveDocument.FullName);
					if (ext == null || ext.ToUpper() != ".SQL")
					{
						MessageBox.Show("The active document is not a .sql file.");
						handled = true;
						return;
					}

					Document activeDocument = _applicationObject.ActiveDocument;
					string unformattedSql = SelectAllCodeFromDocument(activeDocument);
					string formattedSql = Utility.GetSqlMyWay(unformattedSql, GetOptionsFromApplicationSettings());
					ReplaceAllCodeInDocument(activeDocument, formattedSql);

					handled = true;
				}
				if (commandName == "SqlMyWayAddIn.Connect.SqlMyWayOptions")
				{
					OptionsForm form = new OptionsForm(SqlMyWayOptionSettings.Default);
					form.ShowDialog(); //will take care of saving options to application settings
					form.Dispose();
					handled = true;
				}
			}
		}

		//create and load the menu items/commands
		/// <summary>
		///     Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in
		///     is being loaded.
		/// </summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2) application;
			_addInInstance = (AddIn) addInInst;
			if (connectMode == ext_ConnectMode.ext_cm_Startup)
			{
				//Find the Tools command bar on the MenuBar command bar:
				CommandBar menuBarCommandBar = ((CommandBars) _applicationObject.CommandBars)["MenuBar"];
				var toolsPopup = (CommandBarPopup) menuBarCommandBar.Controls["Tools"];

				//get commands list so we can add to it
				var commands = (Commands2) _applicationObject.Commands;

				//duplicate try/catch for each command. just make sure you also update the QueryStatus/Exec methods below
				try
				{
					//add command to commands collection
					Command command = commands.AddNamedCommand2(_addInInstance, "SqlMyWayFormat", "Format T-SQL My Way", null, true);

					//Add a control for the command to the tools menu:
					if ((command != null) && (toolsPopup != null))
						command.AddControl(toolsPopup.CommandBar, 1);
				}
				catch (ArgumentException)
				{
					//If we are here, then the exception is probably because a command with that name
					//  already exists. If so there is no need to recreate the command and we can 
					//  safely ignore the exception.
				}
				try
				{
					Command command = commands.AddNamedCommand2(_addInInstance, "SqlMyWayOptions", "SQL My Way Options...", null, true);

					if ((command != null) && (toolsPopup != null))
						command.AddControl(toolsPopup.CommandBar, 2);
				}
				catch (ArgumentException)
				{
				}
			}
		}

		//deletes the command every time app quits. Safer and easier.
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
			var commands = (Commands2) _applicationObject.Commands;
			try
			{
				Command addinCommand = commands.Item("SqlMyWayAddIn.Connect.SqlMyWayFormat");
				addinCommand.Delete();
				Command addinCommand2 = commands.Item("SqlMyWayAddIn.Connect.SqlMyWayOptions");
				addinCommand2.Delete();
			}
			catch (ArgumentException e)
			{
				Debug.Print("Error deleting commands on disconnection!");
			}
		}

		//This is called when the command's availability is updated
		public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
		{
			if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
				if (commandName == "SqlMyWayAddIn.Connect.SqlMyWayFormat" || commandName == "SqlMyWayAddIn.Connect.SqlMyWayOptions")
					status = vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
		}

		//satisfy interface reqs
		public void OnAddInsUpdate(ref Array custom)
		{
		}
		public void OnStartupComplete(ref Array custom)
		{
		}
		public void OnBeginShutdown(ref Array custom)
		{
		}

		//helpers
		private static void ReplaceAllCodeInDocument(Document targetDoc, string newText)
		{
			var textDoc = targetDoc.Object("TextDocument") as TextDocument;
			if (textDoc != null)
			{
				textDoc.StartPoint.CreateEditPoint().Delete(textDoc.EndPoint);
				textDoc.StartPoint.CreateEditPoint().Insert(newText);
			}
		}
		private static string SelectAllCodeFromDocument(Document targetDoc)
		{
			string outText = "";
			var textDoc = targetDoc.Object("TextDocument") as TextDocument;
			if (textDoc != null)
				outText = textDoc.StartPoint.CreateEditPoint().GetText(textDoc.EndPoint);
			return outText;
		}
		private SqlMyWayOptions GetOptionsFromApplicationSettings()
		{
			var o = new SqlMyWayOptions();
			var app = SqlMyWayOptionSettings.Default;

			o.LineBreaks_BetweenStatements = app.LineBreaks_BetweenStatements;
			o.LineBreaks_BetweenClauses = app.LineBreaks_BetweenClauses;
			o.Capitalize_Keywords = app.Capitalize_Keywords;
			o.Capitalize_DataTypes = app.Capitalize_DataTypes;
			o.Capitalize_BuiltInFunctions = app.Capitalize_BuiltInFunctions;
			o.CommaLists_Stacked = app.CommaLists_Stacked;
			o.CommaLists_TrailingCommas = app.CommaLists_TrailingCommas;
			o.Joins_Indented = app.Joins_Indented;
			o.Joins_TableOnSameLine = app.Joins_TableOnSameLine;
			o.Joins_OnClauseOnSameLine = app.Joins_OnClauseOnSameLine;
			o.Parentheses_SpacesOutside = app.Parentheses_SpacesOutside;
			o.Parentheses_SpacesInside = app.Parentheses_SpacesInside;
			o.Semicolons_Add = app.Semicolons_Add;
			o.Comments_ExtraLineBeforeBlocks = app.Comments_ExtraLineBeforeBlocks;
			o.Comments_ExtraLineAfterBlocks = app.Comments_ExtraLineAfterBlocks;

			return o;
		}
	}
}