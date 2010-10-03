namespace SolidGui.Search
{
    partial class SearchView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchView));
            this.label1 = new System.Windows.Forms.Label();
            this._findTextbox = new System.Windows.Forms.TextBox();
            this._findNextButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._replaceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._replaceButton = new System.Windows.Forms.Button();
            this._scopeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find";
            // 
            // _findTextbox
            // 
            this._findTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                             | System.Windows.Forms.AnchorStyles.Right)));
            this._findTextbox.Location = new System.Drawing.Point(59, 47);
            this._findTextbox.Name = "_findTextbox";
            this._findTextbox.Size = new System.Drawing.Size(175, 20);
            this._findTextbox.TabIndex = 0;
            this._findTextbox.TextChanged += new System.EventHandler(this._findTextbox_TextChanged);
            // 
            // _findNextButton
            // 
            this._findNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._findNextButton.Location = new System.Drawing.Point(244, 12);
            this._findNextButton.Name = "_findNextButton";
            this._findNextButton.Size = new System.Drawing.Size(81, 30);
            this._findNextButton.TabIndex = 3;
            this._findNextButton.Text = "Find &Next";
            this._findNextButton.UseVisualStyleBackColor = true;
            this._findNextButton.Click += new System.EventHandler(this._findNextButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(244, 86);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(81, 30);
            this._cancelButton.TabIndex = 5;
            this._cancelButton.Text = "&Close";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _replaceTextBox
            // 
            this._replaceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this._replaceTextBox.Location = new System.Drawing.Point(59, 78);
            this._replaceTextBox.Name = "_replaceTextBox";
            this._replaceTextBox.Size = new System.Drawing.Size(175, 20);
            this._replaceTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Replace";
            // 
            // _replaceButton
            // 
            this._replaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._replaceButton.Location = new System.Drawing.Point(244, 49);
            this._replaceButton.Name = "_replaceButton";
            this._replaceButton.Size = new System.Drawing.Size(81, 30);
            this._replaceButton.TabIndex = 4;
            this._replaceButton.Text = "&Replace Next";
            this._replaceButton.UseVisualStyleBackColor = true;
            this._replaceButton.Click += new System.EventHandler(this._replaceButton_Click);
            // 
            // _scopeComboBox
            // 
            this._scopeComboBox.FormattingEnabled = true;
            this._scopeComboBox.Items.AddRange(new object[] {
                                                                "Check Result",
                                                                "Entire Dictionary"});
            this._scopeComboBox.Location = new System.Drawing.Point(59, 16);
            this._scopeComboBox.Name = "_scopeComboBox";
            this._scopeComboBox.Size = new System.Drawing.Size(175, 21);
            this._scopeComboBox.TabIndex = 9;
            this._scopeComboBox.SelectedIndexChanged += new System.EventHandler(this._scopeComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Search In";
            // 
            // SearchView
            // 
            this.AcceptButton = this._findNextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(335, 129);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._scopeComboBox);
            this.Controls.Add(this._replaceButton);
            this.Controls.Add(this._replaceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._findNextButton);
            this.Controls.Add(this._findTextbox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchView";
            this.Text = "Find";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _findTextbox;
        private System.Windows.Forms.Button _findNextButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.TextBox _replaceTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _replaceButton;
        private System.Windows.Forms.ComboBox _scopeComboBox;
        private System.Windows.Forms.Label label3;
    }
}