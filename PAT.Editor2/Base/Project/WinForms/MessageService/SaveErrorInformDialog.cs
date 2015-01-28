﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 3702 $</version>
// </file>

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ICSharpCode.Core.WinForms
{
	/// <summary>
	///     Add summary description for SaveErrorInformDialog
	/// </summary>
	sealed class SaveErrorInformDialog : System.Windows.Forms.Form 
	{
		System.Windows.Forms.Label   descriptionLabel;
		System.Windows.Forms.TextBox descriptionTextBox;
		System.Windows.Forms.Button  exceptionButton;
		System.Windows.Forms.Button  okButton;
		
		string    displayMessage;
		Exception exceptionGot;
		
		public SaveErrorInformDialog(string fileName, string message, string dialogName, Exception exceptionGot) 
		{
			this.Text = StringParser.Parse(dialogName);
			//  Must be called for initialization
			this.InitializeComponent2();
			//RightToLeftConverter.ConvertRecursive(this);
			
			displayMessage = StringParser.Parse(message, new string[,] {
				{"FileName", fileName},
				{"Path",     Path.GetDirectoryName(fileName)},
				{"FileNameWithoutPath", Path.GetFileName(fileName)},
				{"Exception", exceptionGot.GetType().FullName},
			});
			descriptionTextBox.Lines = this.displayMessage.Split('\n');
			
			this.exceptionGot = exceptionGot;
		}
		
		void ShowException(object sender, EventArgs e)
		{
			MessageService.ShowMessage(exceptionGot.ToString(), "Exception got");
		}
		
		/// <summary>
		///     This method was autogenerated - do not change the contents manually
		/// </summary>
		private void InitializeComponent2() 
		{
			//
			//  Set up generated class SaveErrorInformDialog
			// 
			this.ClientSize = new Size(508, 320);
			this.SuspendLayout();
			// 
			//  Set up member descriptionLabel
			// 
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.descriptionLabel.Location = new System.Drawing.Point(8, 8);
			this.descriptionLabel.Size = new System.Drawing.Size(584, 24);
			this.descriptionLabel.TabIndex = 3;
			this.descriptionLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top 
						| (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
			this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.descriptionLabel.Text = StringParser.Parse("${res:ICSharpCode.Core.Services.ErrorDialogs.DescriptionLabel}");
			this.descriptionLabel.Name = "descriptionLabel";
			this.Controls.Add(descriptionLabel);
			
			// 
			//  Set up member descriptionTextBox
			// 
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Size = new System.Drawing.Size(584, 237);
			this.descriptionTextBox.Location = new System.Drawing.Point(8, 40);
			this.descriptionTextBox.TabIndex = 2;
			this.descriptionTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Top 
						| (System.Windows.Forms.AnchorStyles.Bottom 
						| (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.descriptionTextBox.ReadOnly = true;
			this.Controls.Add(descriptionTextBox);
			
			// 
			//  Set up member exceptionButton
			// 
			this.exceptionButton = new System.Windows.Forms.Button();
			this.exceptionButton.TabIndex = 1;
			this.exceptionButton.Name = "exceptionButton";
			this.exceptionButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.exceptionButton.Text = StringParser.Parse("${res:ICSharpCode.Core.Services.ErrorDialogs.ShowExceptionButton}");
			this.exceptionButton.Size = new System.Drawing.Size(120, 27);
			this.exceptionButton.Location = new System.Drawing.Point(372, 285);
			this.exceptionButton.Click += new EventHandler(ShowException);
			this.Controls.Add(exceptionButton);
			
			// 
			//  Set up member chooseLocationButton
			// 
			this.okButton = new System.Windows.Forms.Button();
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 0;
			this.okButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.okButton.Text = StringParser.Parse("${res:Global.OKButtonText}");
			this.okButton.Size = new System.Drawing.Size(120, 27);
			this.okButton.Location = new System.Drawing.Point(244, 285);
			this.okButton.DialogResult = DialogResult.OK;
			this.Controls.Add(okButton);
			
			
			this.MaximizeBox = false;
			this.Name = "SaveErrorInformDialog";
			this.MinimizeBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterScreen;
			
			this.ResumeLayout(false);
			this.Size = new System.Drawing.Size(526, 262);
		}
	}
}
