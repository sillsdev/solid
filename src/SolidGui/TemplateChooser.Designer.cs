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
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._templateChooser = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this._saveCurrentFirst = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._instructionsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(240, 314);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(159, 314);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "&OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this.OnOKButton_Click);
            // 
            // _templateChooser
            // 
            this._templateChooser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._templateChooser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this._templateChooser.HideSelection = false;
            this._templateChooser.Location = new System.Drawing.Point(12, 67);
            this._templateChooser.MultiSelect = false;
            this._templateChooser.Name = "_templateChooser";
            this._templateChooser.Size = new System.Drawing.Size(300, 172);
            this._templateChooser.TabIndex = 0;
            this._templateChooser.UseCompatibleStateImageBehavior = false;
            this._templateChooser.View = System.Windows.Forms.View.Details;
            this._templateChooser.SelectedIndexChanged += new System.EventHandler(this._templateChooser_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 194;
            // 
            // _saveCurrentFirst
            // 
            this._saveCurrentFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._saveCurrentFirst.AutoSize = true;
            this._saveCurrentFirst.Enabled = false;
            this._saveCurrentFirst.Location = new System.Drawing.Point(95, 268);
            this._saveCurrentFirst.Name = "_saveCurrentFirst";
            this._saveCurrentFirst.Size = new System.Drawing.Size(199, 13);
            this._saveCurrentFirst.TabIndex = 3;
            this._saveCurrentFirst.TabStop = true;
            this._saveCurrentFirst.Text = "save current settings  under a new name";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "You may wish to";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "before switching.";
            // 
            // _instructionsLabel
            // 
            this._instructionsLabel.AutoSize = true;
            this._instructionsLabel.Location = new System.Drawing.Point(12, 9);
            this._instructionsLabel.MaximumSize = new System.Drawing.Size(300, 0);
            this._instructionsLabel.Name = "_instructionsLabel";
            this._instructionsLabel.Size = new System.Drawing.Size(300, 39);
            this._instructionsLabel.TabIndex = 3;
            this._instructionsLabel.Text = "Choose one of the following to use as a starting point.  As you then customize it" +
                " to match this lexicon, the settings will be saved to a file named {0} in the sa" +
                "me directory as the lexicon.";
            // 
            // TemplateChooser
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(327, 349);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this._instructionsLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._saveCurrentFirst);
            this.Controls.Add(this._templateChooser);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancelButton);
            this.Name = "TemplateChooser";
            this.ShowInTaskbar = false;
            this.Text = "Choose Template...";
            this.Load += new System.EventHandler(this.TemplateChooser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.ListView _templateChooser;
        private System.Windows.Forms.LinkLabel _saveCurrentFirst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _instructionsLabel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}