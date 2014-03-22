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
            this._previousButton = new System.Windows.Forms.Button();
            this._nextButton = new System.Windows.Forms.Button();
            this._recordNumber = new System.Windows.Forms.Label();
            this._firstButton = new System.Windows.Forms.Button();
            this._findButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.buttonTree = new System.Windows.Forms.Button();
            this.buttonFlat = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._descriptionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _previousButton
            // 
            this._previousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._previousButton.FlatAppearance.BorderSize = 0;
            this._previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._previousButton.Image = global::SolidGui.Properties.Resources.BackwordOne;
            this._previousButton.Location = new System.Drawing.Point(142, 3);
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
            this._nextButton.Location = new System.Drawing.Point(210, 3);
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
            this._recordNumber.Location = new System.Drawing.Point(173, 3);
            this._recordNumber.Margin = new System.Windows.Forms.Padding(3);
            this._recordNumber.Name = "_recordNumber";
            this._recordNumber.Size = new System.Drawing.Size(31, 25);
            this._recordNumber.TabIndex = 3;
            this._recordNumber.Text = "17";
            this._recordNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _firstButton
            // 
            this._firstButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._firstButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this._firstButton.FlatAppearance.BorderSize = 0;
            this._firstButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._firstButton.Image = ((System.Drawing.Image)(resources.GetObject("_firstButton.Image")));
            this._firstButton.Location = new System.Drawing.Point(111, 3);
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
            this._findButton.Location = new System.Drawing.Point(80, 3);
            this._findButton.Name = "_findButton";
            this._findButton.Size = new System.Drawing.Size(25, 25);
            this._findButton.TabIndex = 6;
            this._findButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._findButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._findButton.UseVisualStyleBackColor = true;
            this._findButton.Click += new System.EventHandler(this._findButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.FlatAppearance.BorderSize = 0;
            this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshButton.Image = global::SolidGui.Properties.Resources.RecheckRecord;
            this.RefreshButton.Location = new System.Drawing.Point(241, 3);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(25, 25);
            this.RefreshButton.TabIndex = 8;
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // buttonTree
            // 
            this.buttonTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTree.FlatAppearance.BorderSize = 0;
            this.buttonTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTree.Image = ((System.Drawing.Image)(resources.GetObject("buttonTree.Image")));
            this.buttonTree.Location = new System.Drawing.Point(54, 3);
            this.buttonTree.Name = "buttonTree";
            this.buttonTree.Size = new System.Drawing.Size(20, 25);
            this.buttonTree.TabIndex = 9;
            this.buttonTree.UseVisualStyleBackColor = true;
            this.buttonTree.Click += new System.EventHandler(this.buttonTree_Click);
            // 
            // buttonFlat
            // 
            this.buttonFlat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFlat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFlat.FlatAppearance.BorderSize = 0;
            this.buttonFlat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFlat.Image = ((System.Drawing.Image)(resources.GetObject("buttonFlat.Image")));
            this.buttonFlat.Location = new System.Drawing.Point(28, 3);
            this.buttonFlat.Name = "buttonFlat";
            this.buttonFlat.Size = new System.Drawing.Size(20, 25);
            this.buttonFlat.TabIndex = 10;
            this.buttonFlat.UseVisualStyleBackColor = true;
            this.buttonFlat.Click += new System.EventHandler(this.buttonFlat_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.RefreshButton);
            this.flowLayoutPanel1.Controls.Add(this._nextButton);
            this.flowLayoutPanel1.Controls.Add(this._recordNumber);
            this.flowLayoutPanel1.Controls.Add(this._previousButton);
            this.flowLayoutPanel1.Controls.Add(this._firstButton);
            this.flowLayoutPanel1.Controls.Add(this._findButton);
            this.flowLayoutPanel1.Controls.Add(this.buttonTree);
            this.flowLayoutPanel1.Controls.Add(this.buttonFlat);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(201, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(269, 34);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._descriptionLabel.ForeColor = System.Drawing.Color.Yellow;
            this._descriptionLabel.Location = new System.Drawing.Point(82, 10);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(205, 16);
            this._descriptionLabel.TabIndex = 0;
            this._descriptionLabel.Text = "19 Records contain \"\\xe out of order\"";
            this._descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // RecordNavigatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._descriptionLabel);
            this.Name = "RecordNavigatorView";
            this.Size = new System.Drawing.Size(470, 34);
            this.Load += new System.EventHandler(this.RecordNavigatorView_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _previousButton;
        private System.Windows.Forms.Button _nextButton;
        private System.Windows.Forms.Label _recordNumber;
        private System.Windows.Forms.Button _firstButton;
        private System.Windows.Forms.Button _findButton;
        public System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button buttonTree;
        private System.Windows.Forms.Button buttonFlat;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.Label label2;
    }
}
