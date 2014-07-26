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
            this._pnlListView = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lblInstructions = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._textBoxWarning = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(422, 3);
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
            this._okButton.Location = new System.Drawing.Point(503, 3);
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
            this._templateChooser.Location = new System.Drawing.Point(3, 118);
            this._templateChooser.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this._templateChooser.MultiSelect = false;
            this._templateChooser.Name = "_templateChooser";
            this._templateChooser.Size = new System.Drawing.Size(581, 225);
            this._templateChooser.TabIndex = 0;
            this._templateChooser.UseCompatibleStateImageBehavior = false;
            this._templateChooser.View = System.Windows.Forms.View.Details;
            this._templateChooser.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            this._templateChooser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 567;
            // 
            // _pnlListView
            // 
            this._pnlListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._pnlListView.AutoSize = true;
            this._pnlListView.BackColor = System.Drawing.SystemColors.Control;
            this._pnlListView.Location = new System.Drawing.Point(852, 298);
            this._pnlListView.Name = "_pnlListView";
            this._pnlListView.Size = new System.Drawing.Size(128, 43);
            this._pnlListView.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._lblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._templateChooser, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.45931F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.5407F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(587, 384);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // _lblInstructions
            // 
            this._lblInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblInstructions.Location = new System.Drawing.Point(3, 3);
            this._lblInstructions.Name = "_lblInstructions";
            this._lblInstructions.ReadOnly = true;
            this._lblInstructions.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this._lblInstructions.Size = new System.Drawing.Size(581, 112);
            this._lblInstructions.TabIndex = 12;
            this._lblInstructions.Text = resources.GetString("_lblInstructions.Text");
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this._okButton);
            this.flowLayoutPanel2.Controls.Add(this._cancelButton);
            this.flowLayoutPanel2.Controls.Add(this._textBoxWarning);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 352);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(581, 29);
            this.flowLayoutPanel2.TabIndex = 11;
            // 
            // _textBoxWarning
            // 
            this._textBoxWarning.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._textBoxWarning.Location = new System.Drawing.Point(8, 3);
            this._textBoxWarning.Name = "_textBoxWarning";
            this._textBoxWarning.ReadOnly = true;
            this._textBoxWarning.Size = new System.Drawing.Size(408, 13);
            this._textBoxWarning.TabIndex = 3;
            this._textBoxWarning.Text = "This will rename your existing settings file to .solid.bak or .solid.bak2, etc.\n " +
                "";
            this._textBoxWarning.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TemplateChooser
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(587, 384);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this._pnlListView);
            this.MinimumSize = new System.Drawing.Size(465, 300);
            this.Name = "TemplateChooser";
            this.ShowInTaskbar = false;
            this.Text = "Choose Solid Template...";
            this.Load += new System.EventHandler(this.OnTemplateChooser_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.ListView _templateChooser;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel _pnlListView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RichTextBox _lblInstructions;
        private System.Windows.Forms.TextBox _textBoxWarning;
    }
}