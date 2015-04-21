namespace SqlMyWayAddIn
{
	partial class OptionsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.LineBreaks_BetweenStatements = new System.Windows.Forms.TextBox();
			this.LineBreaks_BetweenClauses = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.Capitalize_Keywords = new System.Windows.Forms.CheckBox();
			this.Capitalize_DataTypes = new System.Windows.Forms.CheckBox();
			this.Capitalize_BuiltInFunctions = new System.Windows.Forms.CheckBox();
			this.CommaLists_TrailingCommas = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.CommaLists_Stacked = new System.Windows.Forms.RadioButton();
			this.CommaLists_Inline = new System.Windows.Forms.RadioButton();
			this.Joins_OnClauseOnSameLine = new System.Windows.Forms.CheckBox();
			this.Joins_TableOnSameLine = new System.Windows.Forms.CheckBox();
			this.Joins_Indented = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.Parentheses_SpacesInside = new System.Windows.Forms.CheckBox();
			this.Parentheses_SpacesOutside = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.Semicolons_Add = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.Comments_ExtraLineAfterBlocks = new System.Windows.Forms.CheckBox();
			this.Comments_ExtraLineBeforeBlocks = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.SaveBtn = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Line Breaks";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(33, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(234, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Place                 line breaks between statements";
			// 
			// LineBreaks_BetweenStatements
			// 
			this.LineBreaks_BetweenStatements.Location = new System.Drawing.Point(69, 29);
			this.LineBreaks_BetweenStatements.Name = "LineBreaks_BetweenStatements";
			this.LineBreaks_BetweenStatements.Size = new System.Drawing.Size(36, 20);
			this.LineBreaks_BetweenStatements.TabIndex = 2;
			// 
			// LineBreaks_BetweenClauses
			// 
			this.LineBreaks_BetweenClauses.Location = new System.Drawing.Point(69, 53);
			this.LineBreaks_BetweenClauses.Name = "LineBreaks_BetweenClauses";
			this.LineBreaks_BetweenClauses.Size = new System.Drawing.Size(36, 20);
			this.LineBreaks_BetweenClauses.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(33, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(219, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Place                 line breaks between clauses";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(13, 76);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Capitalization";
			// 
			// Capitalize_Keywords
			// 
			this.Capitalize_Keywords.AutoSize = true;
			this.Capitalize_Keywords.Location = new System.Drawing.Point(36, 93);
			this.Capitalize_Keywords.Name = "Capitalize_Keywords";
			this.Capitalize_Keywords.Size = new System.Drawing.Size(119, 17);
			this.Capitalize_Keywords.TabIndex = 6;
			this.Capitalize_Keywords.Text = "Capitalize keywords";
			this.Capitalize_Keywords.UseVisualStyleBackColor = true;
			// 
			// Capitalize_DataTypes
			// 
			this.Capitalize_DataTypes.AutoSize = true;
			this.Capitalize_DataTypes.Location = new System.Drawing.Point(36, 117);
			this.Capitalize_DataTypes.Name = "Capitalize_DataTypes";
			this.Capitalize_DataTypes.Size = new System.Drawing.Size(123, 17);
			this.Capitalize_DataTypes.TabIndex = 7;
			this.Capitalize_DataTypes.Text = "Capitalize data types";
			this.Capitalize_DataTypes.UseVisualStyleBackColor = true;
			// 
			// Capitalize_BuiltInFunctions
			// 
			this.Capitalize_BuiltInFunctions.AutoSize = true;
			this.Capitalize_BuiltInFunctions.Location = new System.Drawing.Point(36, 141);
			this.Capitalize_BuiltInFunctions.Name = "Capitalize_BuiltInFunctions";
			this.Capitalize_BuiltInFunctions.Size = new System.Drawing.Size(150, 17);
			this.Capitalize_BuiltInFunctions.TabIndex = 8;
			this.Capitalize_BuiltInFunctions.Text = "Capitalize built-in functions";
			this.Capitalize_BuiltInFunctions.UseVisualStyleBackColor = true;
			// 
			// CommaLists_TrailingCommas
			// 
			this.CommaLists_TrailingCommas.AutoSize = true;
			this.CommaLists_TrailingCommas.Location = new System.Drawing.Point(36, 201);
			this.CommaLists_TrailingCommas.Name = "CommaLists_TrailingCommas";
			this.CommaLists_TrailingCommas.Size = new System.Drawing.Size(102, 17);
			this.CommaLists_TrailingCommas.TabIndex = 12;
			this.CommaLists_TrailingCommas.Text = "Trailing commas";
			this.CommaLists_TrailingCommas.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(13, 161);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(139, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Comma-Separated Lists";
			// 
			// CommaLists_Stacked
			// 
			this.CommaLists_Stacked.AutoSize = true;
			this.CommaLists_Stacked.Location = new System.Drawing.Point(36, 178);
			this.CommaLists_Stacked.Name = "CommaLists_Stacked";
			this.CommaLists_Stacked.Size = new System.Drawing.Size(65, 17);
			this.CommaLists_Stacked.TabIndex = 13;
			this.CommaLists_Stacked.TabStop = true;
			this.CommaLists_Stacked.Text = "Stacked";
			this.CommaLists_Stacked.UseVisualStyleBackColor = true;
			this.CommaLists_Stacked.CheckedChanged += new System.EventHandler(this.CommaListStyle_Changed);
			// 
			// CommaLists_Inline
			// 
			this.CommaLists_Inline.AutoSize = true;
			this.CommaLists_Inline.Location = new System.Drawing.Point(108, 178);
			this.CommaLists_Inline.Name = "CommaLists_Inline";
			this.CommaLists_Inline.Size = new System.Drawing.Size(50, 17);
			this.CommaLists_Inline.TabIndex = 14;
			this.CommaLists_Inline.TabStop = true;
			this.CommaLists_Inline.Text = "Inline";
			this.CommaLists_Inline.UseVisualStyleBackColor = true;
			this.CommaLists_Inline.CheckedChanged += new System.EventHandler(this.CommaListStyle_Changed);
			// 
			// Joins_OnClauseOnSameLine
			// 
			this.Joins_OnClauseOnSameLine.AutoSize = true;
			this.Joins_OnClauseOnSameLine.Location = new System.Drawing.Point(36, 286);
			this.Joins_OnClauseOnSameLine.Name = "Joins_OnClauseOnSameLine";
			this.Joins_OnClauseOnSameLine.Size = new System.Drawing.Size(157, 17);
			this.Joins_OnClauseOnSameLine.TabIndex = 18;
			this.Joins_OnClauseOnSameLine.Text = "Put ON clause on same line";
			this.Joins_OnClauseOnSameLine.UseVisualStyleBackColor = true;
			// 
			// Joins_TableOnSameLine
			// 
			this.Joins_TableOnSameLine.AutoSize = true;
			this.Joins_TableOnSameLine.Location = new System.Drawing.Point(36, 262);
			this.Joins_TableOnSameLine.Name = "Joins_TableOnSameLine";
			this.Joins_TableOnSameLine.Size = new System.Drawing.Size(130, 17);
			this.Joins_TableOnSameLine.TabIndex = 17;
			this.Joins_TableOnSameLine.Text = "Put table on same line";
			this.Joins_TableOnSameLine.UseVisualStyleBackColor = true;
			// 
			// Joins_Indented
			// 
			this.Joins_Indented.AutoSize = true;
			this.Joins_Indented.Location = new System.Drawing.Point(36, 238);
			this.Joins_Indented.Name = "Joins_Indented";
			this.Joins_Indented.Size = new System.Drawing.Size(80, 17);
			this.Joins_Indented.TabIndex = 16;
			this.Joins_Indented.Text = "Indent joins";
			this.Joins_Indented.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(13, 221);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "Joins";
			// 
			// Parentheses_SpacesInside
			// 
			this.Parentheses_SpacesInside.AutoSize = true;
			this.Parentheses_SpacesInside.Location = new System.Drawing.Point(36, 347);
			this.Parentheses_SpacesInside.Name = "Parentheses_SpacesInside";
			this.Parentheses_SpacesInside.Size = new System.Drawing.Size(92, 17);
			this.Parentheses_SpacesInside.TabIndex = 21;
			this.Parentheses_SpacesInside.Text = "Spaces inside";
			this.Parentheses_SpacesInside.UseVisualStyleBackColor = true;
			// 
			// Parentheses_SpacesOutside
			// 
			this.Parentheses_SpacesOutside.AutoSize = true;
			this.Parentheses_SpacesOutside.Location = new System.Drawing.Point(36, 323);
			this.Parentheses_SpacesOutside.Name = "Parentheses_SpacesOutside";
			this.Parentheses_SpacesOutside.Size = new System.Drawing.Size(99, 17);
			this.Parentheses_SpacesOutside.TabIndex = 20;
			this.Parentheses_SpacesOutside.Text = "Spaces outside";
			this.Parentheses_SpacesOutside.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(13, 306);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 13);
			this.label7.TabIndex = 19;
			this.label7.Text = "Parentheses";
			// 
			// Semicolons_Add
			// 
			this.Semicolons_Add.AutoSize = true;
			this.Semicolons_Add.Location = new System.Drawing.Point(36, 384);
			this.Semicolons_Add.Name = "Semicolons_Add";
			this.Semicolons_Add.Size = new System.Drawing.Size(216, 17);
			this.Semicolons_Add.TabIndex = 23;
			this.Semicolons_Add.Text = "Add semicolon at end of each statement";
			this.Semicolons_Add.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(13, 367);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 13);
			this.label8.TabIndex = 22;
			this.label8.Text = "Semicolon";
			// 
			// Comments_ExtraLineAfterBlocks
			// 
			this.Comments_ExtraLineAfterBlocks.AutoSize = true;
			this.Comments_ExtraLineAfterBlocks.Location = new System.Drawing.Point(36, 445);
			this.Comments_ExtraLineAfterBlocks.Name = "Comments_ExtraLineAfterBlocks";
			this.Comments_ExtraLineAfterBlocks.Size = new System.Drawing.Size(194, 17);
			this.Comments_ExtraLineAfterBlocks.TabIndex = 26;
			this.Comments_ExtraLineAfterBlocks.Text = "Add extra line after block comments";
			this.Comments_ExtraLineAfterBlocks.UseVisualStyleBackColor = true;
			// 
			// Comments_ExtraLineBeforeBlocks
			// 
			this.Comments_ExtraLineBeforeBlocks.AutoSize = true;
			this.Comments_ExtraLineBeforeBlocks.Location = new System.Drawing.Point(36, 421);
			this.Comments_ExtraLineBeforeBlocks.Name = "Comments_ExtraLineBeforeBlocks";
			this.Comments_ExtraLineBeforeBlocks.Size = new System.Drawing.Size(203, 17);
			this.Comments_ExtraLineBeforeBlocks.TabIndex = 25;
			this.Comments_ExtraLineBeforeBlocks.Text = "Add extra line before block comments";
			this.Comments_ExtraLineBeforeBlocks.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(13, 404);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 13);
			this.label9.TabIndex = 24;
			this.label9.Text = "Comments";
			// 
			// SaveBtn
			// 
			this.SaveBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.SaveBtn.Location = new System.Drawing.Point(207, 473);
			this.SaveBtn.Name = "SaveBtn";
			this.SaveBtn.Size = new System.Drawing.Size(75, 23);
			this.SaveBtn.TabIndex = 27;
			this.SaveBtn.Text = "Save";
			this.SaveBtn.UseVisualStyleBackColor = true;
			this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
			// 
			// CancelBtn
			// 
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new System.Drawing.Point(126, 473);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 28;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(294, 508);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.SaveBtn);
			this.Controls.Add(this.Comments_ExtraLineAfterBlocks);
			this.Controls.Add(this.Comments_ExtraLineBeforeBlocks);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.Semicolons_Add);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.Parentheses_SpacesInside);
			this.Controls.Add(this.Parentheses_SpacesOutside);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.Joins_OnClauseOnSameLine);
			this.Controls.Add(this.Joins_TableOnSameLine);
			this.Controls.Add(this.Joins_Indented);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.CommaLists_Inline);
			this.Controls.Add(this.CommaLists_Stacked);
			this.Controls.Add(this.CommaLists_TrailingCommas);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.Capitalize_BuiltInFunctions);
			this.Controls.Add(this.Capitalize_DataTypes);
			this.Controls.Add(this.Capitalize_Keywords);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.LineBreaks_BetweenClauses);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.LineBreaks_BetweenStatements);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "OptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SQL My Way Options";
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox LineBreaks_BetweenStatements;
		private System.Windows.Forms.TextBox LineBreaks_BetweenClauses;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox Capitalize_Keywords;
		private System.Windows.Forms.CheckBox Capitalize_DataTypes;
		private System.Windows.Forms.CheckBox Capitalize_BuiltInFunctions;
		private System.Windows.Forms.CheckBox CommaLists_TrailingCommas;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.RadioButton CommaLists_Stacked;
		private System.Windows.Forms.RadioButton CommaLists_Inline;
		private System.Windows.Forms.CheckBox Joins_OnClauseOnSameLine;
		private System.Windows.Forms.CheckBox Joins_TableOnSameLine;
		private System.Windows.Forms.CheckBox Joins_Indented;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox Parentheses_SpacesInside;
		private System.Windows.Forms.CheckBox Parentheses_SpacesOutside;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox Semicolons_Add;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox Comments_ExtraLineAfterBlocks;
		private System.Windows.Forms.CheckBox Comments_ExtraLineBeforeBlocks;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button SaveBtn;
		private System.Windows.Forms.Button CancelBtn;
	}
}