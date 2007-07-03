namespace SolidGui
{
    partial class RecordNavigatorView
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
            this._descriptionLabel = new System.Windows.Forms.Label();
            this._PreviousButton = new System.Windows.Forms.Button();
            this._nextButton = new System.Windows.Forms.Button();
            this._recordNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.AutoSize = true;
            this._descriptionLabel.Location = new System.Drawing.Point(41, 0);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(35, 13);
            this._descriptionLabel.TabIndex = 0;
            this._descriptionLabel.Text = "label1";
            // 
            // _PreviousButton
            // 
            this._PreviousButton.Location = new System.Drawing.Point(3, 31);
            this._PreviousButton.Name = "_PreviousButton";
            this._PreviousButton.Size = new System.Drawing.Size(21, 23);
            this._PreviousButton.TabIndex = 1;
            this._PreviousButton.Text = "<";
            this._PreviousButton.UseVisualStyleBackColor = true;
            this._PreviousButton.Click += new System.EventHandler(this._PreviousButton_Click);
            // 
            // _nextButton
            // 
            this._nextButton.Location = new System.Drawing.Point(85, 30);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(23, 23);
            this._nextButton.TabIndex = 2;
            this._nextButton.Text = ">";
            this._nextButton.UseVisualStyleBackColor = true;
            this._nextButton.Click += new System.EventHandler(this._nextButton_Click);
            // 
            // _recordNumber
            // 
            this._recordNumber.AutoSize = true;
            this._recordNumber.Location = new System.Drawing.Point(39, 36);
            this._recordNumber.Name = "_recordNumber";
            this._recordNumber.Size = new System.Drawing.Size(30, 13);
            this._recordNumber.TabIndex = 3;
            this._recordNumber.Text = "2/30";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filter:";
            // 
            // RecordNavigatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this._recordNumber);
            this.Controls.Add(this._nextButton);
            this.Controls.Add(this._PreviousButton);
            this.Controls.Add(this._descriptionLabel);
            this.Name = "RecordNavigatorView";
            this.Size = new System.Drawing.Size(115, 56);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.Button _PreviousButton;
        private System.Windows.Forms.Button _nextButton;
        private System.Windows.Forms.Label _recordNumber;
        private System.Windows.Forms.Label label1;
    }
}
