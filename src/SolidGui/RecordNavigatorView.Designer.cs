// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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
            this._previousButton = new System.Windows.Forms.Button();
            this._nextButton = new System.Windows.Forms.Button();
            this._recordNumber = new System.Windows.Forms.Label();
            this._firstButton = new System.Windows.Forms.Button();
            this._findButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.ForeColor = System.Drawing.Color.Yellow;
            this._descriptionLabel.Location = new System.Drawing.Point(82, 10);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(205, 16);
            this._descriptionLabel.TabIndex = 0;
            this._descriptionLabel.Text = "19 Records contain \"\\xe out of order\"";
            // 
            // _previousButton
            // 
            this._previousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._previousButton.FlatAppearance.BorderSize = 0;
            this._previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._previousButton.Image = global::SolidGui.Properties.Resources.BackwordOne;
            this._previousButton.Location = new System.Drawing.Point(349, 4);
            this._previousButton.Name = "_previousButton";
            this._previousButton.Size = new System.Drawing.Size(25, 25);
            this._previousButton.TabIndex = 1;
            this._previousButton.UseVisualStyleBackColor = true;
            this._previousButton.Click += new System.EventHandler(this._PreviousButton_Click);
            // 
            // _nextButton
            // 
            this._nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._nextButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._nextButton.FlatAppearance.BorderSize = 0;
            this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextButton.Image = global::SolidGui.Properties.Resources.ForwardOne;
            this._nextButton.Location = new System.Drawing.Point(406, 4);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(25, 25);
            this._nextButton.TabIndex = 2;
            this._nextButton.UseVisualStyleBackColor = false;
            this._nextButton.Click += new System.EventHandler(this._nextButton_Click);
            // 
            // _recordNumber
            // 
            this._recordNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._recordNumber.ForeColor = System.Drawing.Color.Yellow;
            this._recordNumber.Location = new System.Drawing.Point(376, 9);
            this._recordNumber.Name = "_recordNumber";
            this._recordNumber.Size = new System.Drawing.Size(31, 16);
            this._recordNumber.TabIndex = 3;
            this._recordNumber.Text = "17";
            // 
            // _firstButton
            // 
            this._firstButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._firstButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this._firstButton.FlatAppearance.BorderSize = 0;
            this._firstButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._firstButton.Image = ((System.Drawing.Image)(resources.GetObject("_firstButton.Image")));
            this._firstButton.Location = new System.Drawing.Point(323, 4);
            this._firstButton.Name = "_firstButton";
            this._firstButton.Size = new System.Drawing.Size(25, 25);
            this._firstButton.TabIndex = 5;
            this._firstButton.UseVisualStyleBackColor = true;
            this._firstButton.Click += new System.EventHandler(this._firstButton_Click);
            // 
            // _findButton
            // 
            this._findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._findButton.FlatAppearance.BorderSize = 0;
            this._findButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._findButton.Image = global::SolidGui.Properties.Resources.Search;
            this._findButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._findButton.Location = new System.Drawing.Point(294, 4);
            this._findButton.Name = "_findButton";
            this._findButton.Size = new System.Drawing.Size(25, 25);
            this._findButton.TabIndex = 6;
            this._findButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._findButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._findButton.UseVisualStyleBackColor = true;
            this._findButton.Click += new System.EventHandler(this._searchButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Lexicon";
            // 
            // _refreshButton
            // 
            this._refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._refreshButton.FlatAppearance.BorderSize = 0;
            this._refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._refreshButton.Image = global::SolidGui.Properties.Resources.RecheckRecord;
            this._refreshButton.Location = new System.Drawing.Point(435, 4);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(25, 25);
            this._refreshButton.TabIndex = 8;
            this._refreshButton.UseVisualStyleBackColor = true;
            // 
            // RecordNavigatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this._refreshButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._findButton);
            this.Controls.Add(this._firstButton);
            this.Controls.Add(this._recordNumber);
            this.Controls.Add(this._nextButton);
            this.Controls.Add(this._previousButton);
            this.Controls.Add(this._descriptionLabel);
            this.Name = "RecordNavigatorView";
            this.Size = new System.Drawing.Size(470, 34);
            this.Load += new System.EventHandler(this.RecordNavigatorView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.Button _previousButton;
        private System.Windows.Forms.Button _nextButton;
        private System.Windows.Forms.Label _recordNumber;
        private System.Windows.Forms.Button _firstButton;
        private System.Windows.Forms.Button _findButton;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button _refreshButton;
    }
}
