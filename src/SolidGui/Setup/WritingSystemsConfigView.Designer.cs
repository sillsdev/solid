// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

namespace SolidGui.Setup
{
	partial class WritingSystemsConfigView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._lblFrom = new System.Windows.Forms.Label();
			this._btnAdvanced = new System.Windows.Forms.Button();
			this._pnlAdvanced = new System.Windows.Forms.Panel();
			this._lblTo = new System.Windows.Forms.Label();
			this._lblFieldsMatching = new System.Windows.Forms.Label();
			this._wscTo = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
			this._tbFieldsMatching = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this._btnApply = new System.Windows.Forms.Button();
			this._cbFrom = new System.Windows.Forms.ComboBox();
			this._lblSetupWritingSystems = new System.Windows.Forms.LinkLabel();
			this._lblInfo = new System.Windows.Forms.Label();
			this._pnlAdvanced.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblFrom
			// 
			this._lblFrom.AutoSize = true;
			this._lblFrom.Location = new System.Drawing.Point(8, 63);
			this._lblFrom.Name = "_lblFrom";
			this._lblFrom.Size = new System.Drawing.Size(30, 13);
			this._lblFrom.TabIndex = 3;
			this._lblFrom.Text = "From";
			// 
			// _btnAdvanced
			// 
			this._btnAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._btnAdvanced.Location = new System.Drawing.Point(11, 172);
			this._btnAdvanced.Name = "_btnAdvanced";
			this._btnAdvanced.Size = new System.Drawing.Size(75, 23);
			this._btnAdvanced.TabIndex = 6;
			this._btnAdvanced.Text = "Advanced";
			this._btnAdvanced.UseVisualStyleBackColor = true;
			this._btnAdvanced.Visible = false;
			this._btnAdvanced.Click += new System.EventHandler(this.OnAdvanced_Click);
			// 
			// _pnlAdvanced
			// 
			this._pnlAdvanced.Controls.Add(this._lblFieldsMatching);
			this._pnlAdvanced.Controls.Add(this._tbFieldsMatching);
			this._pnlAdvanced.Location = new System.Drawing.Point(1, 77);
			this._pnlAdvanced.Name = "_pnlAdvanced";
			this._pnlAdvanced.Size = new System.Drawing.Size(247, 29);
			this._pnlAdvanced.TabIndex = 7;
			// 
			// _lblTo
			// 
			this._lblTo.AutoSize = true;
			this._lblTo.Location = new System.Drawing.Point(8, 117);
			this._lblTo.Name = "_lblTo";
			this._lblTo.Size = new System.Drawing.Size(20, 13);
			this._lblTo.TabIndex = 3;
			this._lblTo.Text = "To";
			// 
			// _lblFieldsMatching
			// 
			this._lblFieldsMatching.AutoSize = true;
			this._lblFieldsMatching.Location = new System.Drawing.Point(6, 7);
			this._lblFieldsMatching.Name = "_lblFieldsMatching";
			this._lblFieldsMatching.Size = new System.Drawing.Size(80, 13);
			this._lblFieldsMatching.TabIndex = 2;
			this._lblFieldsMatching.Text = "Fields matching";
			// 
			// _wscTo
			// 
			this._wscTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._wscTo.FormattingEnabled = true;
			this._wscTo.Location = new System.Drawing.Point(92, 114);
			this._wscTo.Name = "_wscTo";
			this._wscTo.Size = new System.Drawing.Size(140, 21);
			this._wscTo.TabIndex = 1;
			// 
			// _tbFieldsMatching
			// 
			this._tbFieldsMatching.Location = new System.Drawing.Point(92, 4);
			this._tbFieldsMatching.Name = "_tbFieldsMatching";
			this._tbFieldsMatching.Size = new System.Drawing.Size(140, 20);
			this._tbFieldsMatching.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(52, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(181, 40);
			this.label6.TabIndex = 8;
			this.label6.Text = "Set writing systems by renaming from the template writing systems  to your prefer" +
				"red writing system.";
			// 
			// _btnApply
			// 
			this._btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._btnApply.Location = new System.Drawing.Point(157, 172);
			this._btnApply.Name = "_btnApply";
			this._btnApply.Size = new System.Drawing.Size(75, 23);
			this._btnApply.TabIndex = 9;
			this._btnApply.Text = "Apply";
			this._btnApply.UseVisualStyleBackColor = true;
			this._btnApply.Click += new System.EventHandler(this.OnApply_Click);
			// 
			// _cbFrom
			// 
			this._cbFrom.FormattingEnabled = true;
			this._cbFrom.Location = new System.Drawing.Point(93, 54);
			this._cbFrom.Name = "_cbFrom";
			this._cbFrom.Size = new System.Drawing.Size(140, 21);
			this._cbFrom.TabIndex = 10;
			// 
			// _lblSetupWritingSystems
			// 
			this._lblSetupWritingSystems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblSetupWritingSystems.AutoSize = true;
			this._lblSetupWritingSystems.Location = new System.Drawing.Point(8, 148);
			this._lblSetupWritingSystems.Name = "_lblSetupWritingSystems";
			this._lblSetupWritingSystems.Size = new System.Drawing.Size(113, 13);
			this._lblSetupWritingSystems.TabIndex = 11;
			this._lblSetupWritingSystems.TabStop = true;
			this._lblSetupWritingSystems.Text = "Setup Writing Systems";
			this._lblSetupWritingSystems.Click += new System.EventHandler(this.OnSetupWritingSystems_Click);
			// 
			// _lblInfo
			// 
			this._lblInfo.Image = global::SolidGui.Properties.Resources.info2;
			this._lblInfo.Location = new System.Drawing.Point(8, 8);
			this._lblInfo.Name = "_lblInfo";
			this._lblInfo.Size = new System.Drawing.Size(32, 32);
			this._lblInfo.TabIndex = 12;
			// 
			// WritingSystemsConfigView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._lblInfo);
			this.Controls.Add(this._lblSetupWritingSystems);
			this.Controls.Add(this._cbFrom);
			this.Controls.Add(this._lblTo);
			this.Controls.Add(this._btnApply);
			this.Controls.Add(this.label6);
			this.Controls.Add(this._wscTo);
			this.Controls.Add(this._pnlAdvanced);
			this.Controls.Add(this._btnAdvanced);
			this.Controls.Add(this._lblFrom);
			this.Name = "WritingSystemsConfigView";
			this.Size = new System.Drawing.Size(251, 198);
			this._pnlAdvanced.ResumeLayout(false);
			this._pnlAdvanced.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _lblFrom;
		private System.Windows.Forms.Button _btnAdvanced;
		private System.Windows.Forms.Panel _pnlAdvanced;
		private System.Windows.Forms.Label _lblFieldsMatching;
		private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox _wscTo;
		private System.Windows.Forms.TextBox _tbFieldsMatching;
		private System.Windows.Forms.Label _lblTo;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button _btnApply;
		private System.Windows.Forms.ComboBox _cbFrom;
		private System.Windows.Forms.LinkLabel _lblSetupWritingSystems;
		private System.Windows.Forms.Label _lblInfo;
	}
}
