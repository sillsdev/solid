namespace SolidGui
{
    partial class MainWindowView
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
            System.Windows.Forms.ToolStrip toolStrip1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowView));
            this._openButton = new System.Windows.Forms.ToolStripButton();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._processButton = new System.Windows.Forms.Button();
            this._filterChooserView = new SolidGui.FilterChooserView();
            this._recordNavigatorView = new SolidGui.RecordNavigatorView();
            this._sfmEditorView = new SolidGui.SfmEditorView();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openButton,
            this._saveButton});
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(569, 25);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // _openButton
            // 
            this._openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._openButton.Image = ((System.Drawing.Image)(resources.GetObject("_openButton.Image")));
            this._openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._openButton.Name = "_openButton";
            this._openButton.Size = new System.Drawing.Size(23, 22);
            this._openButton.Text = "toolStripButton1";
            this._openButton.Click += new System.EventHandler(this._openButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._saveButton.Enabled = false;
            this._saveButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveButton.Image")));
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(23, 22);
            this._saveButton.Text = "toolStripButton1";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._processButton);
            this.splitContainer1.Panel1.Controls.Add(this._filterChooserView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._recordNavigatorView);
            this.splitContainer1.Panel2.Controls.Add(this._sfmEditorView);
            this.splitContainer1.Size = new System.Drawing.Size(567, 320);
            this.splitContainer1.SplitterDistance = 146;
            this.splitContainer1.TabIndex = 2;
            // 
            // _processButton
            // 
            this._processButton.Location = new System.Drawing.Point(10, 1);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(121, 23);
            this._processButton.TabIndex = 1;
            this._processButton.Text = "Process Lexicon";
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _filterChooserView
            // 
            this._filterChooserView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._filterChooserView.Enabled = false;
            this._filterChooserView.Location = new System.Drawing.Point(0, 30);
            this._filterChooserView.Model = null;
            this._filterChooserView.Name = "_filterChooserView";
            this._filterChooserView.Size = new System.Drawing.Size(146, 290);
            this._filterChooserView.TabIndex = 0;
            // 
            // _recordNavigatorView
            // 
            this._recordNavigatorView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._recordNavigatorView.Location = new System.Drawing.Point(3, 0);
            this._recordNavigatorView.Model = null;
            this._recordNavigatorView.Name = "_recordNavigatorView";
            this._recordNavigatorView.Size = new System.Drawing.Size(411, 53);
            this._recordNavigatorView.TabIndex = 3;
            // 
            // _sfmEditorView
            // 
            this._sfmEditorView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._sfmEditorView.AutoScroll = true;
            this._sfmEditorView.Location = new System.Drawing.Point(3, 59);
            this._sfmEditorView.Name = "_sfmEditorView";
            this._sfmEditorView.Size = new System.Drawing.Size(411, 258);
            this._sfmEditorView.TabIndex = 2;
            // 
            // MainWindowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 349);
            this.Controls.Add(toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainWindowView";
            this.Text = "SOLID";
            this.Load += new System.EventHandler(this.MainWindowView_Load);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private RecordNavigatorView _recordNavigatorView;
        private SfmEditorView _sfmEditorView;
        private FilterChooserView _filterChooserView;
        private System.Windows.Forms.Button _processButton;
        private System.Windows.Forms.ToolStripButton _openButton;
        private System.Windows.Forms.ToolStripButton _saveButton;
    }
}

