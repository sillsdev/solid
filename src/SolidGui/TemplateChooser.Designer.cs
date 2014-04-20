// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateChooser));
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._templateChooser = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._lblInstructions = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._pnlListView = new System.Windows.Forms.Panel();
            this._labelSaveFirst1 = new System.Windows.Forms.Label();
            this._warningImage = new System.Windows.Forms.PictureBox();
            this._pnlWarning = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this._warningImage)).BeginInit();
            this._pnlWarning.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(4, 3);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(85, 3);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "&OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this.OnOKButton_Click);
            // 
            // _templateChooser
            // 
            this._templateChooser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this._templateChooser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._templateChooser.HideSelection = false;
            this._templateChooser.Location = new System.Drawing.Point(3, 186);
            this._templateChooser.MultiSelect = false;
            this._templateChooser.Name = "_templateChooser";
            this._templateChooser.Size = new System.Drawing.Size(490, 164);
            this._templateChooser.TabIndex = 0;
            this._templateChooser.UseCompatibleStateImageBehavior = false;
            this._templateChooser.View = System.Windows.Forms.View.Details;
            this._templateChooser.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            this._templateChooser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 194;
            // 
            // _lblInstructions
            // 
            this._lblInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lblInstructions.Location = new System.Drawing.Point(3, 36);
            this._lblInstructions.Name = "_lblInstructions";
            this._lblInstructions.ReadOnly = true;
            this._lblInstructions.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this._lblInstructions.Size = new System.Drawing.Size(488, 125);
            this._lblInstructions.TabIndex = 5;
            this._lblInstructions.Text = resources.GetString("_lblInstructions.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Choose one of the following templates as your starting point for building setting" +
                "s for this dictionary.";
            // 
            // _pnlListView
            // 
            this._pnlListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._pnlListView.AutoSize = true;
            this._pnlListView.BackColor = System.Drawing.SystemColors.Control;
            this._pnlListView.Location = new System.Drawing.Point(852, 298);
            this._pnlListView.Name = "_pnlListView";
            this._pnlListView.Size = new System.Drawing.Size(128, 33);
            this._pnlListView.TabIndex = 9;
            // 
            // _labelSaveFirst1
            // 
            this._labelSaveFirst1.AutoSize = true;
            this._labelSaveFirst1.Location = new System.Drawing.Point(33, 3);
            this._labelSaveFirst1.Name = "_labelSaveFirst1";
            this._labelSaveFirst1.Size = new System.Drawing.Size(348, 13);
            this._labelSaveFirst1.TabIndex = 3;
            this._labelSaveFirst1.Text = "This will rename your existing settings file to .solid.bak or .solid.bak2, etc.";
            // 
            // _warningImage
            // 
            this._warningImage.Image = global::SolidGui.Properties.Resources.WarningHS;
            this._warningImage.Location = new System.Drawing.Point(3, 3);
            this._warningImage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._warningImage.Name = "_warningImage";
            this._warningImage.Size = new System.Drawing.Size(24, 26);
            this._warningImage.TabIndex = 4;
            this._warningImage.TabStop = false;
            // 
            // _pnlWarning
            // 
            this._pnlWarning.Controls.Add(this._warningImage);
            this._pnlWarning.Controls.Add(this._labelSaveFirst1);
            this._pnlWarning.Location = new System.Drawing.Point(3, 3);
            this._pnlWarning.Name = "_pnlWarning";
            this._pnlWarning.Size = new System.Drawing.Size(477, 27);
            this._pnlWarning.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._pnlWarning);
            this.flowLayoutPanel1.Controls.Add(this._lblInstructions);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(490, 177);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._templateChooser, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(496, 388);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this._okButton);
            this.flowLayoutPanel2.Controls.Add(this._cancelButton);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(330, 356);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(163, 29);
            this.flowLayoutPanel2.TabIndex = 11;
            // 
            // TemplateChooser
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(496, 388);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this._pnlListView);
            this.MinimumSize = new System.Drawing.Size(512, 370);
            this.Name = "TemplateChooser";
            this.ShowInTaskbar = false;
            this.Text = "Choose Solid Template...";
            this.Load += new System.EventHandler(this.OnTemplateChooser_Load);
            ((System.ComponentModel.ISupportInitialize)(this._warningImage)).EndInit();
            this._pnlWarning.ResumeLayout(false);
            this._pnlWarning.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.ListView _templateChooser;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.RichTextBox _lblInstructions;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel _pnlListView;
        private System.Windows.Forms.Label _labelSaveFirst1;
        private System.Windows.Forms.PictureBox _warningImage;
        private System.Windows.Forms.Panel _pnlWarning;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}