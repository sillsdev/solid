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
			this._wscVernacular = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
			this._wscNational = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
			this._wscRegional = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
			this._lblVernacular = new System.Windows.Forms.Label();
			this._lblNational = new System.Windows.Forms.Label();
			this._lblRegional = new System.Windows.Forms.Label();
			this._btnAdvanced = new System.Windows.Forms.Button();
			this._pnlAdvanced = new System.Windows.Forms.Panel();
			this._lblTo = new System.Windows.Forms.Label();
			this._lblFieldsMatching = new System.Windows.Forms.Label();
			this._wscTo = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
			this._tbFieldsMatching = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this._btnApply = new System.Windows.Forms.Button();
			this._pnlAdvanced.SuspendLayout();
			this.SuspendLayout();
			// 
			// _wscVernacular
			// 
			this._wscVernacular.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._wscVernacular.FormattingEnabled = true;
			this._wscVernacular.Location = new System.Drawing.Point(92, 55);
			this._wscVernacular.Name = "_wscVernacular";
			this._wscVernacular.Size = new System.Drawing.Size(140, 21);
			this._wscVernacular.TabIndex = 0;
			// 
			// _wscNational
			// 
			this._wscNational.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._wscNational.FormattingEnabled = true;
			this._wscNational.Location = new System.Drawing.Point(92, 92);
			this._wscNational.Name = "_wscNational";
			this._wscNational.Size = new System.Drawing.Size(140, 21);
			this._wscNational.TabIndex = 1;
			// 
			// _wscRegional
			// 
			this._wscRegional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._wscRegional.FormattingEnabled = true;
			this._wscRegional.Location = new System.Drawing.Point(92, 129);
			this._wscRegional.Name = "_wscRegional";
			this._wscRegional.Size = new System.Drawing.Size(140, 21);
			this._wscRegional.TabIndex = 2;
			// 
			// _lblVernacular
			// 
			this._lblVernacular.AutoSize = true;
			this._lblVernacular.Location = new System.Drawing.Point(8, 58);
			this._lblVernacular.Name = "_lblVernacular";
			this._lblVernacular.Size = new System.Drawing.Size(58, 13);
			this._lblVernacular.TabIndex = 3;
			this._lblVernacular.Text = "Vernacular";
			// 
			// _lblNational
			// 
			this._lblNational.AutoSize = true;
			this._lblNational.Location = new System.Drawing.Point(8, 95);
			this._lblNational.Name = "_lblNational";
			this._lblNational.Size = new System.Drawing.Size(46, 13);
			this._lblNational.TabIndex = 4;
			this._lblNational.Text = "National";
			// 
			// _lblRegional
			// 
			this._lblRegional.AutoSize = true;
			this._lblRegional.Location = new System.Drawing.Point(8, 132);
			this._lblRegional.Name = "_lblRegional";
			this._lblRegional.Size = new System.Drawing.Size(49, 13);
			this._lblRegional.TabIndex = 5;
			this._lblRegional.Text = "Regional";
			// 
			// _btnAdvanced
			// 
			this._btnAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._btnAdvanced.Location = new System.Drawing.Point(3, 248);
			this._btnAdvanced.Name = "_btnAdvanced";
			this._btnAdvanced.Size = new System.Drawing.Size(75, 23);
			this._btnAdvanced.TabIndex = 6;
			this._btnAdvanced.Text = "Advanced";
			this._btnAdvanced.UseVisualStyleBackColor = true;
			this._btnAdvanced.Click += new System.EventHandler(this.OnAdvanced_Click);
			// 
			// _pnlAdvanced
			// 
			this._pnlAdvanced.Controls.Add(this._lblTo);
			this._pnlAdvanced.Controls.Add(this._lblFieldsMatching);
			this._pnlAdvanced.Controls.Add(this._wscTo);
			this._pnlAdvanced.Controls.Add(this._tbFieldsMatching);
			this._pnlAdvanced.Location = new System.Drawing.Point(0, 156);
			this._pnlAdvanced.Name = "_pnlAdvanced";
			this._pnlAdvanced.Size = new System.Drawing.Size(247, 86);
			this._pnlAdvanced.TabIndex = 7;
			// 
			// _lblTo
			// 
			this._lblTo.AutoSize = true;
			this._lblTo.Location = new System.Drawing.Point(8, 46);
			this._lblTo.Name = "_lblTo";
			this._lblTo.Size = new System.Drawing.Size(20, 13);
			this._lblTo.TabIndex = 3;
			this._lblTo.Text = "To";
			// 
			// _lblFieldsMatching
			// 
			this._lblFieldsMatching.AutoSize = true;
			this._lblFieldsMatching.Location = new System.Drawing.Point(8, 20);
			this._lblFieldsMatching.Name = "_lblFieldsMatching";
			this._lblFieldsMatching.Size = new System.Drawing.Size(80, 13);
			this._lblFieldsMatching.TabIndex = 2;
			this._lblFieldsMatching.Text = "Fields matching";
			// 
			// _wscTo
			// 
			this._wscTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._wscTo.FormattingEnabled = true;
			this._wscTo.Location = new System.Drawing.Point(92, 43);
			this._wscTo.Name = "_wscTo";
			this._wscTo.Size = new System.Drawing.Size(140, 21);
			this._wscTo.TabIndex = 1;
			// 
			// _tbFieldsMatching
			// 
			this._tbFieldsMatching.Location = new System.Drawing.Point(92, 17);
			this._tbFieldsMatching.Name = "_tbFieldsMatching";
			this._tbFieldsMatching.Size = new System.Drawing.Size(140, 20);
			this._tbFieldsMatching.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(39, 19);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 13);
			this.label6.TabIndex = 8;
			this.label6.Text = "label6";
			// 
			// _btnApply
			// 
			this._btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._btnApply.Location = new System.Drawing.Point(172, 248);
			this._btnApply.Name = "_btnApply";
			this._btnApply.Size = new System.Drawing.Size(75, 23);
			this._btnApply.TabIndex = 9;
			this._btnApply.Text = "Apply";
			this._btnApply.UseVisualStyleBackColor = true;
			this._btnApply.Click += new System.EventHandler(this.OnApply_Click);
			// 
			// WritingSystemsConfigView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._btnApply);
			this.Controls.Add(this.label6);
			this.Controls.Add(this._pnlAdvanced);
			this.Controls.Add(this._btnAdvanced);
			this.Controls.Add(this._lblRegional);
			this.Controls.Add(this._lblNational);
			this.Controls.Add(this._lblVernacular);
			this.Controls.Add(this._wscRegional);
			this.Controls.Add(this._wscNational);
			this.Controls.Add(this._wscVernacular);
			this.Name = "WritingSystemsConfigView";
			this.Size = new System.Drawing.Size(262, 276);
			this._pnlAdvanced.ResumeLayout(false);
			this._pnlAdvanced.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox _wscVernacular;
		private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox _wscNational;
		private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox _wscRegional;
		private System.Windows.Forms.Label _lblVernacular;
		private System.Windows.Forms.Label _lblNational;
		private System.Windows.Forms.Label _lblRegional;
		private System.Windows.Forms.Button _btnAdvanced;
		private System.Windows.Forms.Panel _pnlAdvanced;
		private System.Windows.Forms.Label _lblFieldsMatching;
		private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox _wscTo;
		private System.Windows.Forms.TextBox _tbFieldsMatching;
		private System.Windows.Forms.Label _lblTo;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button _btnApply;
	}
}
