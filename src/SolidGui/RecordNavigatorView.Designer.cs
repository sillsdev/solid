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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordNavigatorView));
            this._descriptionLabel = new System.Windows.Forms.Label();
            this._PreviousButton = new System.Windows.Forms.Button();
            this._nextButton = new System.Windows.Forms.Button();
            this._recordNumber = new System.Windows.Forms.Label();
            this._firstButton = new System.Windows.Forms.Button();
            this._searchButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.ForeColor = System.Drawing.Color.Yellow;
            this._descriptionLabel.Location = new System.Drawing.Point(82, 14);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(205, 16);
            this._descriptionLabel.TabIndex = 0;
            this._descriptionLabel.Text = "19 Records contain \"\\xe out of order\"";
            // 
            // _PreviousButton
            // 
            this._PreviousButton.FlatAppearance.BorderSize = 0;
            this._PreviousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._PreviousButton.Image = ((System.Drawing.Image)(resources.GetObject("_PreviousButton.Image")));
            this._PreviousButton.Location = new System.Drawing.Point(328, 8);
            this._PreviousButton.Name = "_PreviousButton";
            this._PreviousButton.Size = new System.Drawing.Size(20, 21);
            this._PreviousButton.TabIndex = 1;
            this._PreviousButton.UseVisualStyleBackColor = true;
            this._PreviousButton.Click += new System.EventHandler(this._PreviousButton_Click);
            // 
            // _nextButton
            // 
            this._nextButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._nextButton.FlatAppearance.BorderSize = 0;
            this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextButton.Image = ((System.Drawing.Image)(resources.GetObject("_nextButton.Image")));
            this._nextButton.Location = new System.Drawing.Point(368, -1);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(44, 41);
            this._nextButton.TabIndex = 2;
            this._nextButton.UseVisualStyleBackColor = false;
            this._nextButton.Click += new System.EventHandler(this._nextButton_Click);
            // 
            // _recordNumber
            // 
            this._recordNumber.AutoSize = true;
            this._recordNumber.ForeColor = System.Drawing.Color.Yellow;
            this._recordNumber.Location = new System.Drawing.Point(349, 13);
            this._recordNumber.Name = "_recordNumber";
            this._recordNumber.Size = new System.Drawing.Size(19, 13);
            this._recordNumber.TabIndex = 3;
            this._recordNumber.Text = "17";
            // 
            // _firstButton
            // 
            this._firstButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this._firstButton.FlatAppearance.BorderSize = 0;
            this._firstButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._firstButton.Image = ((System.Drawing.Image)(resources.GetObject("_firstButton.Image")));
            this._firstButton.Location = new System.Drawing.Point(299, 3);
            this._firstButton.Name = "_firstButton";
            this._firstButton.Size = new System.Drawing.Size(31, 33);
            this._firstButton.TabIndex = 5;
            this._firstButton.UseVisualStyleBackColor = true;
            this._firstButton.Click += new System.EventHandler(this._firstButton_Click);
            // 
            // _searchButton
            // 
            this._searchButton.FlatAppearance.BorderSize = 0;
            this._searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._searchButton.Image = ((System.Drawing.Image)(resources.GetObject("_searchButton.Image")));
            this._searchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._searchButton.Location = new System.Drawing.Point(407, 2);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(39, 35);
            this._searchButton.TabIndex = 6;
            this._searchButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._searchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this._searchButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Lexicon";
            // 
            // RecordNavigatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.label2);
            this.Controls.Add(this._searchButton);
            this.Controls.Add(this._firstButton);
            this.Controls.Add(this._recordNumber);
            this.Controls.Add(this._nextButton);
            this.Controls.Add(this._PreviousButton);
            this.Controls.Add(this._descriptionLabel);
            this.Name = "RecordNavigatorView";
            this.Size = new System.Drawing.Size(447, 41);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.Button _PreviousButton;
        private System.Windows.Forms.Button _nextButton;
        private System.Windows.Forms.Label _recordNumber;
        private System.Windows.Forms.Button _firstButton;
        private System.Windows.Forms.Button _searchButton;
        private System.Windows.Forms.Label label2;
    }
}
