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
            this.label1 = new System.Windows.Forms.Label();
            this._findTextbox = new System.Windows.Forms.TextBox();
            this._findNextButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._findPreviousButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find";
            // 
            // _findTextbox
            // 
            this._findTextbox.Location = new System.Drawing.Point(59, 41);
            this._findTextbox.Name = "_findTextbox";
            this._findTextbox.Size = new System.Drawing.Size(209, 20);
            this._findTextbox.TabIndex = 1;
            // 
            // _findNextButton
            // 
            this._findNextButton.Location = new System.Drawing.Point(106, 99);
            this._findNextButton.Name = "_findNextButton";
            this._findNextButton.Size = new System.Drawing.Size(83, 30);
            this._findNextButton.TabIndex = 2;
            this._findNextButton.Text = "Find &Next";
            this._findNextButton.UseVisualStyleBackColor = true;
            this._findNextButton.Click += new System.EventHandler(this._findNextButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(195, 99);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(73, 30);
            this._cancelButton.TabIndex = 3;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _findPreviousButton
            // 
            this._findPreviousButton.Location = new System.Drawing.Point(17, 99);
            this._findPreviousButton.Name = "_findPreviousButton";
            this._findPreviousButton.Size = new System.Drawing.Size(83, 30);
            this._findPreviousButton.TabIndex = 4;
            this._findPreviousButton.Text = "Find &Previous";
            this._findPreviousButton.UseVisualStyleBackColor = true;
            this._findPreviousButton.Click += new System.EventHandler(this._findPreviousButton_Click);
            // 
            // SearchView
            // 
            this.AcceptButton = this._findNextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(292, 152);
            this.ControlBox = false;
            this.Controls.Add(this._findPreviousButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._findNextButton);
            this.Controls.Add(this._findTextbox);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Button _findPreviousButton;
    }
}