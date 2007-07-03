namespace SolidGui
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
            this._forwardRadioButton = new System.Windows.Forms.RadioButton();
            this._backwardRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._replaceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._replaceButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find";
            // 
            // _findTextbox
            // 
            this._findTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._findTextbox.Location = new System.Drawing.Point(59, 15);
            this._findTextbox.Name = "_findTextbox";
            this._findTextbox.Size = new System.Drawing.Size(175, 20);
            this._findTextbox.TabIndex = 0;
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
            this._cancelButton.Location = new System.Drawing.Point(244, 85);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(81, 30);
            this._cancelButton.TabIndex = 5;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _forwardRadioButton
            // 
            this._forwardRadioButton.AutoSize = true;
            this._forwardRadioButton.Checked = true;
            this._forwardRadioButton.Location = new System.Drawing.Point(13, 19);
            this._forwardRadioButton.Name = "_forwardRadioButton";
            this._forwardRadioButton.Size = new System.Drawing.Size(63, 17);
            this._forwardRadioButton.TabIndex = 5;
            this._forwardRadioButton.TabStop = true;
            this._forwardRadioButton.Text = "Forward";
            this._forwardRadioButton.UseVisualStyleBackColor = true;
            // 
            // _backwardRadioButton
            // 
            this._backwardRadioButton.AutoSize = true;
            this._backwardRadioButton.Location = new System.Drawing.Point(86, 19);
            this._backwardRadioButton.Name = "_backwardRadioButton";
            this._backwardRadioButton.Size = new System.Drawing.Size(73, 17);
            this._backwardRadioButton.TabIndex = 6;
            this._backwardRadioButton.Text = "Backward";
            this._backwardRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._forwardRadioButton);
            this.groupBox1.Controls.Add(this._backwardRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(55, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 52);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction";
            // 
            // _replaceTextBox
            // 
            this._replaceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._replaceTextBox.Location = new System.Drawing.Point(59, 46);
            this._replaceTextBox.Name = "_replaceTextBox";
            this._replaceTextBox.Size = new System.Drawing.Size(175, 20);
            this._replaceTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
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
            // SearchView
            // 
            this.AcceptButton = this._findNextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(335, 134);
            this.Controls.Add(this._replaceButton);
            this.Controls.Add(this._replaceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._findNextButton);
            this.Controls.Add(this._findTextbox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchView";
            this.Text = "Find";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _findTextbox;
        private System.Windows.Forms.Button _findNextButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.RadioButton _forwardRadioButton;
        private System.Windows.Forms.RadioButton _backwardRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _replaceTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _replaceButton;
    }
}