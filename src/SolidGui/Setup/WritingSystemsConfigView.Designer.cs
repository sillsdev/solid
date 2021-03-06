﻿// Copyright (c) 2007-2014 SIL International
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
            this._lblTo = new System.Windows.Forms.Label();
            this._wscTo = new SIL.Windows.Forms.WritingSystems.WSPickerUsingComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this._btnApply = new System.Windows.Forms.Button();
            this._cbFrom = new System.Windows.Forms.ComboBox();
            this._lblSetupWritingSystems = new System.Windows.Forms.LinkLabel();
            this._lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _lblFrom
            // 
            this._lblFrom.AutoSize = true;
            this._lblFrom.Location = new System.Drawing.Point(7, 97);
            this._lblFrom.Name = "_lblFrom";
            this._lblFrom.Size = new System.Drawing.Size(51, 13);
            this._lblFrom.TabIndex = 3;
            this._lblFrom.Text = "Find WS:";
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
            // _lblTo
            // 
            this._lblTo.AutoSize = true;
            this._lblTo.Location = new System.Drawing.Point(8, 117);
            this._lblTo.Name = "_lblTo";
            this._lblTo.Size = new System.Drawing.Size(72, 13);
            this._lblTo.TabIndex = 3;
            this._lblTo.Text = "Replace with:";
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
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(52, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 77);
            this.label6.TabIndex = 8;
            this.label6.Text = "After setting up your specific WS\'s, use those specific ones to replace the gener" +
    "ic pseudo-writing-systems. E.g. replace \'nat\' with \'fr\' if French is the nationa" +
    "l language.";
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
            this._cbFrom.Location = new System.Drawing.Point(92, 88);
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
            this.Controls.Add(this._btnAdvanced);
            this.Controls.Add(this._lblFrom);
            this.Name = "WritingSystemsConfigView";
            this.Size = new System.Drawing.Size(251, 198);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _lblFrom;
        private System.Windows.Forms.Button _btnAdvanced;
        private SIL.Windows.Forms.WritingSystems.WSPickerUsingComboBox _wscTo;
		private System.Windows.Forms.Label _lblTo;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button _btnApply;
		private System.Windows.Forms.ComboBox _cbFrom;
		private System.Windows.Forms.LinkLabel _lblSetupWritingSystems;
		private System.Windows.Forms.Label _lblInfo;
	}
}
