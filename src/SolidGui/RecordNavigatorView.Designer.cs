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
            this._firstButton = new System.Windows.Forms.Button();
            this._lastButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.Location = new System.Drawing.Point(41, 0);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(300, 26);
            this._descriptionLabel.TabIndex = 0;
            this._descriptionLabel.Text = "label1";
            // 
            // _PreviousButton
            // 
            this._PreviousButton.FlatAppearance.BorderSize = 0;
            this._PreviousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._PreviousButton.Image = global::SolidGui.Properties.Resources.skip_backward;
            this._PreviousButton.Location = new System.Drawing.Point(44, 31);
            this._PreviousButton.Name = "_PreviousButton";
            this._PreviousButton.Size = new System.Drawing.Size(34, 34);
            this._PreviousButton.TabIndex = 1;
            this._PreviousButton.UseVisualStyleBackColor = true;
            this._PreviousButton.Click += new System.EventHandler(this._PreviousButton_Click);
            // 
            // _nextButton
            // 
            this._nextButton.FlatAppearance.BorderSize = 0;
            this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextButton.Image = global::SolidGui.Properties.Resources.skip_forward;
            this._nextButton.Location = new System.Drawing.Point(160, 31);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(34, 34);
            this._nextButton.TabIndex = 2;
            this._nextButton.UseVisualStyleBackColor = true;
            this._nextButton.Click += new System.EventHandler(this._nextButton_Click);
            // 
            // _recordNumber
            // 
            this._recordNumber.AutoSize = true;
            this._recordNumber.Location = new System.Drawing.Point(92, 42);
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
            // _firstButton
            // 
            this._firstButton.FlatAppearance.BorderSize = 0;
            this._firstButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._firstButton.Image = global::SolidGui.Properties.Resources.rewind;
            this._firstButton.Location = new System.Drawing.Point(6, 31);
            this._firstButton.Name = "_firstButton";
            this._firstButton.Size = new System.Drawing.Size(34, 34);
            this._firstButton.TabIndex = 5;
            this._firstButton.UseVisualStyleBackColor = true;
            this._firstButton.Click += new System.EventHandler(this._firstButton_Click);
            // 
            // _lastButton
            // 
            this._lastButton.FlatAppearance.BorderSize = 0;
            this._lastButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._lastButton.Image = global::SolidGui.Properties.Resources.fast_forward;
            this._lastButton.Location = new System.Drawing.Point(200, 31);
            this._lastButton.Name = "_lastButton";
            this._lastButton.Size = new System.Drawing.Size(34, 34);
            this._lastButton.TabIndex = 6;
            this._lastButton.UseVisualStyleBackColor = true;
            this._lastButton.Click += new System.EventHandler(this._lastButton_Click);
            // 
            // RecordNavigatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._lastButton);
            this.Controls.Add(this._firstButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._recordNumber);
            this.Controls.Add(this._nextButton);
            this.Controls.Add(this._PreviousButton);
            this.Controls.Add(this._descriptionLabel);
            this.Name = "RecordNavigatorView";
            this.Size = new System.Drawing.Size(411, 78);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.Button _PreviousButton;
        private System.Windows.Forms.Button _nextButton;
        private System.Windows.Forms.Label _recordNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _firstButton;
        private System.Windows.Forms.Button _lastButton;
    }
}
