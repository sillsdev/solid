namespace SolidGui
{
    partial class TemplateChooser
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (TemplateChooser));
			this._cancelButton = new System.Windows.Forms.Button ();
			this._okButton = new System.Windows.Forms.Button ();
			this._templateChooser = new System.Windows.Forms.ListView ();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader ();
			this._saveCurrentFirst = new System.Windows.Forms.LinkLabel ();
			this._labelSaveFirst1 = new System.Windows.Forms.Label ();
			this._labelSaveFirst2 = new System.Windows.Forms.Label ();
			this._instructionsLabelForReplacement = new System.Windows.Forms.Label ();
			this._warningImage = new System.Windows.Forms.PictureBox ();
			this._instructionsLabel = new System.Windows.Forms.RichTextBox ();
			this.label1 = new System.Windows.Forms.Label ();
			this.label2 = new System.Windows.Forms.Label ();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel ();
			this.label3 = new System.Windows.Forms.Label ();
			((System.ComponentModel.ISupportInitialize) (this._warningImage)).BeginInit ();
			this.SuspendLayout ();
			// 
			// _cancelButton
			// 
			this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Location = new System.Drawing.Point (414, 350);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size (75, 23);
			this._cancelButton.TabIndex = 2;
			this._cancelButton.Text = "&Cancel";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler (this.OnCancelButtonClick);
			// 
			// _okButton
			// 
			this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._okButton.Location = new System.Drawing.Point (333, 350);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size (75, 23);
			this._okButton.TabIndex = 1;
			this._okButton.Text = "&OK";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler (this.OnOKButton_Click);
			// 
			// _templateChooser
			// 
			this._templateChooser.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._templateChooser.Columns.AddRange (new System.Windows.Forms.ColumnHeader [] {
            this.columnHeader1});
			this._templateChooser.HideSelection = false;
			this._templateChooser.Location = new System.Drawing.Point (9, 191);
			this._templateChooser.MultiSelect = false;
			this._templateChooser.Name = "_templateChooser";
			this._templateChooser.Size = new System.Drawing.Size (480, 153);
			this._templateChooser.TabIndex = 0;
			this._templateChooser.UseCompatibleStateImageBehavior = false;
			this._templateChooser.View = System.Windows.Forms.View.Details;
			this._templateChooser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler (this.OnMouseDoubleClick);
			this._templateChooser.SelectedIndexChanged += new System.EventHandler (this.OnSelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 194;
			// 
			// _saveCurrentFirst
			// 
			this._saveCurrentFirst.AutoSize = true;
			this._saveCurrentFirst.Location = new System.Drawing.Point (66, 150);
			this._saveCurrentFirst.Name = "_saveCurrentFirst";
			this._saveCurrentFirst.Size = new System.Drawing.Size (28, 13);
			this._saveCurrentFirst.TabIndex = 3;
			this._saveCurrentFirst.TabStop = true;
			this._saveCurrentFirst.Text = "here";
			this._saveCurrentFirst.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler (this.OnSaveCurrentSettingsLinkClicked);
			// 
			// _labelSaveFirst1
			// 
			this._labelSaveFirst1.AutoSize = true;
			this._labelSaveFirst1.Location = new System.Drawing.Point (39, 137);
			this._labelSaveFirst1.Name = "_labelSaveFirst1";
			this._labelSaveFirst1.Size = new System.Drawing.Size (334, 13);
			this._labelSaveFirst1.TabIndex = 3;
			this._labelSaveFirst1.Text = "Choosing a new template will overwrite your existing Solid settings file.";
			// 
			// _labelSaveFirst2
			// 
			this._labelSaveFirst2.AutoSize = true;
			this._labelSaveFirst2.Location = new System.Drawing.Point (97, 150);
			this._labelSaveFirst2.Name = "_labelSaveFirst2";
			this._labelSaveFirst2.Size = new System.Drawing.Size (276, 13);
			this._labelSaveFirst2.TabIndex = 3;
			this._labelSaveFirst2.Text = "to preserve the current Solid settings  under a new name.";
			// 
			// _instructionsLabelForReplacement
			// 
			this._instructionsLabelForReplacement.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._instructionsLabelForReplacement.AutoSize = true;
			this._instructionsLabelForReplacement.Location = new System.Drawing.Point (12, 347);
			this._instructionsLabelForReplacement.MaximumSize = new System.Drawing.Size (300, 0);
			this._instructionsLabelForReplacement.Name = "_instructionsLabelForReplacement";
			this._instructionsLabelForReplacement.Size = new System.Drawing.Size (297, 26);
			this._instructionsLabelForReplacement.TabIndex = 3;
			this._instructionsLabelForReplacement.Text = "Choose one of the following to use as a new starting point for building the SOLID" +
				" settings for this dictoinary.";
			// 
			// _warningImage
			// 
			this._warningImage.Image = global::SolidGui.Properties.Resources.WarningHS;
			this._warningImage.Location = new System.Drawing.Point (9, 137);
			this._warningImage.Name = "_warningImage";
			this._warningImage.Size = new System.Drawing.Size (24, 26);
			this._warningImage.TabIndex = 4;
			this._warningImage.TabStop = false;
			// 
			// _instructionsLabel
			// 
			this._instructionsLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._instructionsLabel.Location = new System.Drawing.Point (12, 12);
			this._instructionsLabel.Name = "_instructionsLabel";
			this._instructionsLabel.ReadOnly = true;
			this._instructionsLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this._instructionsLabel.Size = new System.Drawing.Size (486, 125);
			this._instructionsLabel.TabIndex = 5;
			this._instructionsLabel.Text = resources.GetString ("_instructionsLabel.Text");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point (9, 175);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size (278, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "As a starting point, choose one of the following templates.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point (39, 150);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size (30, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Click";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point (66, 150);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size (28, 13);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "here";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler (this.OnSaveCurrentSettingsLinkClicked);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point (94, 150);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size (276, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "to preserve the current Solid settings  under a new name.";
			// 
			// TemplateChooser
			// 
			this.AcceptButton = this._okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size (501, 385);
			this.ControlBox = false;
			this.Controls.Add (this.label2);
			this.Controls.Add (this.label1);
			this.Controls.Add (this._instructionsLabel);
			this.Controls.Add (this._warningImage);
			this.Controls.Add (this.label3);
			this.Controls.Add (this._labelSaveFirst2);
			this.Controls.Add (this._instructionsLabelForReplacement);
			this.Controls.Add (this._labelSaveFirst1);
			this.Controls.Add (this.linkLabel1);
			this.Controls.Add (this._saveCurrentFirst);
			this.Controls.Add (this._templateChooser);
			this.Controls.Add (this._okButton);
			this.Controls.Add (this._cancelButton);
			this.Name = "TemplateChooser";
			this.ShowInTaskbar = false;
			this.Text = "Choose SOLID Template...";
			this.Load += new System.EventHandler (this.TemplateChooser_Load);
			((System.ComponentModel.ISupportInitialize) (this._warningImage)).EndInit ();
			this.ResumeLayout (false);
			this.PerformLayout ();

        }

        #endregion

        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.ListView _templateChooser;
        private System.Windows.Forms.LinkLabel _saveCurrentFirst;
        private System.Windows.Forms.Label _labelSaveFirst1;
        private System.Windows.Forms.Label _labelSaveFirst2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label _instructionsLabelForReplacement;
        private System.Windows.Forms.PictureBox _warningImage;
        private System.Windows.Forms.RichTextBox _instructionsLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label3;
    }
}