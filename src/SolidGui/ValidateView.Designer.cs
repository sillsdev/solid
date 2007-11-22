namespace SolidGui
{
    partial class ValidateView
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
            this._ebRegularExpression = new System.Windows.Forms.TextBox();
            this._labelRegEx = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _ebRegularExpression
            // 
            this._ebRegularExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._ebRegularExpression.Location = new System.Drawing.Point(120, 14);
            this._ebRegularExpression.Multiline = true;
            this._ebRegularExpression.Name = "_ebRegularExpression";
            this._ebRegularExpression.Size = new System.Drawing.Size(120, 71);
            this._ebRegularExpression.TabIndex = 0;
            // 
            // _labelRegEx
            // 
            this._labelRegEx.AutoSize = true;
            this._labelRegEx.Location = new System.Drawing.Point(16, 17);
            this._labelRegEx.Name = "_labelRegEx";
            this._labelRegEx.Size = new System.Drawing.Size(98, 13);
            this._labelRegEx.TabIndex = 1;
            this._labelRegEx.Text = "Regular Expression";
            // 
            // ValidateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this._labelRegEx);
            this.Controls.Add(this._ebRegularExpression);
            this.Name = "ValidateView";
            this.Size = new System.Drawing.Size(246, 92);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _ebRegularExpression;
        private System.Windows.Forms.Label _labelRegEx;
    }
}
