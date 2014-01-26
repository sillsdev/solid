// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

namespace SolidGui.Setup
{
	partial class WritingSystemsConfigDialog
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
            this._writingSystemsConfigView = new SolidGui.Setup.WritingSystemsConfigView();
            this._btnCanc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _writingSystemsConfigView
            // 
            this._writingSystemsConfigView.FromWritingSystem = null;
            this._writingSystemsConfigView.Location = new System.Drawing.Point(0, -1);
            this._writingSystemsConfigView.Name = "_writingSystemsConfigView";
            this._writingSystemsConfigView.Size = new System.Drawing.Size(262, 250);
            this._writingSystemsConfigView.TabIndex = 0;
            // 
            // _btnCanc
            // 
            this._btnCanc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCanc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCanc.Location = new System.Drawing.Point(158, 255);
            this._btnCanc.Name = "_btnCanc";
            this._btnCanc.Size = new System.Drawing.Size(75, 23);
            this._btnCanc.TabIndex = 1;
            this._btnCanc.Text = "&Cancel";
            this._btnCanc.UseVisualStyleBackColor = true;
            // 
            // WritingSystemsConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCanc;
            this.ClientSize = new System.Drawing.Size(261, 284);
            this.Controls.Add(this._btnCanc);
            this.Controls.Add(this._writingSystemsConfigView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "WritingSystemsConfigDialog";
            this.Text = "Specify Writing Systems";
            this.ResumeLayout(false);

		}

		#endregion

		private WritingSystemsConfigView _writingSystemsConfigView;
        private System.Windows.Forms.Button _btnCanc;

	}
}