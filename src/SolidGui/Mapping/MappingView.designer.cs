// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

namespace SolidGui.Mapping
{
    partial class MappingView
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
            this.label1 = new System.Windows.Forms.Label();
            this._targetCombo = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._conceptList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this._htmlViewer = new System.Windows.Forms.WebBrowser();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target";
            // 
            // _targetCombo
            // 
            this._targetCombo.FormattingEnabled = true;
            this._targetCombo.Items.AddRange(new object[] {
                                                              "FLEX (Use LIFT instead)",
                                                              "Lexical Interchange Format (LIFT)"});
            this._targetCombo.Location = new System.Drawing.Point(47, 6);
            this._targetCombo.Name = "_targetCombo";
            this._targetCombo.Size = new System.Drawing.Size(240, 21);
            this._targetCombo.TabIndex = 1;
            this._targetCombo.Text = "Lexical Interchange Format (LIFT)";
            this._targetCombo.SelectedIndexChanged += new System.EventHandler(this._targetCombo_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._conceptList);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(467, 323);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 3;
            // 
            // _conceptList
            // 
            this._conceptList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                              | System.Windows.Forms.AnchorStyles.Left)
                                                                             | System.Windows.Forms.AnchorStyles.Right)));
            this._conceptList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                           this.columnHeader1});
            this._conceptList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this._conceptList.HideSelection = false;
            this._conceptList.Location = new System.Drawing.Point(3, 18);
            this._conceptList.MultiSelect = false;
            this._conceptList.Name = "_conceptList";
            this._conceptList.Size = new System.Drawing.Size(166, 305);
            this._conceptList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._conceptList.TabIndex = 2;
            this._conceptList.UseCompatibleStateImageBehavior = false;
            this._conceptList.View = System.Windows.Forms.View.Details;
            this._conceptList.SelectedIndexChanged += new System.EventHandler(this._conceptList_SelectedIndexChanged);
            this._conceptList.SizeChanged += new System.EventHandler(this._conceptList_SizeChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Concept";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                        | System.Windows.Forms.AnchorStyles.Left)
                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._htmlViewer);
            this.panel1.Location = new System.Drawing.Point(6, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 305);
            this.panel1.TabIndex = 2;
            // 
            // _htmlViewer
            // 
            this._htmlViewer.AllowWebBrowserDrop = false;
            this._htmlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._htmlViewer.IsWebBrowserContextMenuEnabled = false;
            this._htmlViewer.Location = new System.Drawing.Point(0, 0);
            this._htmlViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this._htmlViewer.Name = "_htmlViewer";
            this._htmlViewer.Size = new System.Drawing.Size(276, 303);
            this._htmlViewer.TabIndex = 2;
            this._htmlViewer.TabStop = false;
            this._htmlViewer.WebBrowserShortcutsEnabled = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Information";
            // 
            // MappingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._targetCombo);
            this.Controls.Add(this.label1);
            this.Name = "MappingView";
            this.Size = new System.Drawing.Size(476, 359);
            this.Load += new System.EventHandler(this.OnLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _targetCombo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView _conceptList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser _htmlViewer;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}