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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStrip toolStrip1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowView));
            this._openButton = new System.Windows.Forms.ToolStripButton();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._changeTemplate = new System.Windows.Forms.ToolStripButton();
            this._aboutBoxButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._recheckButton = new System.Windows.Forms.Button();
            this._markerDetails = new SolidGui.MarkerDetails();
            this._filterChooserView = new SolidGui.FilterChooserView();
            this._sfmEditorView = new SolidGui.SfmEditorView();
            this._recordNavigatorView = new SolidGui.RecordNavigatorView();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openButton,
            this._saveButton,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this._processButton,
            this._changeTemplate,
            this._aboutBoxButton});
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(891, 25);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // _openButton
            // 
            this._openButton.Image = global::SolidGui.Properties.Resources.folder_open;
            this._openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._openButton.Name = "_openButton";
            this._openButton.Size = new System.Drawing.Size(104, 22);
            this._openButton.Text = "Open Lexicon...";
            this._openButton.Click += new System.EventHandler(this._openButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Enabled = false;
            this._saveButton.Image = global::SolidGui.Properties.Resources.save;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(51, 22);
            this._saveButton.Text = "Save";
            this._saveButton.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _processButton
            // 
            this._processButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this._processButton.Image = global::SolidGui.Properties.Resources.play;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Black;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(103, 22);
            this._processButton.Text = "Process Lexicon";
            this._processButton.ToolTipText = "Reprocess the lexicon using the current settings.";
            this._processButton.Visible = false;
            this._processButton.Click += new System.EventHandler(this.OnProcessButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _changeTemplate
            // 
            this._changeTemplate.Image = global::SolidGui.Properties.Resources.template;
            this._changeTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._changeTemplate.Name = "_changeTemplate";
            this._changeTemplate.Size = new System.Drawing.Size(123, 22);
            this._changeTemplate.Text = "Change Template...";
            this._changeTemplate.Click += new System.EventHandler(this.OnChangeTemplate_Click);
            // 
            // _aboutBoxButton
            // 
            this._aboutBoxButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._aboutBoxButton.Image = global::SolidGui.Properties.Resources.info2;
            this._aboutBoxButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._aboutBoxButton.Name = "_aboutBoxButton";
            this._aboutBoxButton.Size = new System.Drawing.Size(23, 22);
            this._aboutBoxButton.Text = "About Solid...";
            this._aboutBoxButton.Click += new System.EventHandler(this.OnAboutBoxButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 26);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Enabled = false;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._sfmEditorView);
            this.splitContainer1.Panel2.Controls.Add(this._recordNavigatorView);
            this.splitContainer1.Size = new System.Drawing.Size(889, 426);
            this.splitContainer1.SplitterDistance = 436;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._markerDetails);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(436, 426);
            this.splitContainer2.SplitterDistance = 266;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(-2, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.splitContainer3.Panel1.Controls.Add(this._recheckButton);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._filterChooserView);
            this.splitContainer3.Size = new System.Drawing.Size(436, 154);
            this.splitContainer3.SplitterDistance = 28;
            this.splitContainer3.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Results of Check";
            // 
            // _recheckButton
            // 
            this._recheckButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._recheckButton.FlatAppearance.BorderSize = 0;
            this._recheckButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._recheckButton.Font = new System.Drawing.Font("Gulim", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._recheckButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._recheckButton.Image = ((System.Drawing.Image)(resources.GetObject("_recheckButton.Image")));
            this._recheckButton.Location = new System.Drawing.Point(325, -3);
            this._recheckButton.Name = "_recheckButton";
            this._recheckButton.Size = new System.Drawing.Size(85, 31);
            this._recheckButton.TabIndex = 4;
            this._recheckButton.Text = "Recheck";
            this._recheckButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._recheckButton.UseVisualStyleBackColor = true;
            this._recheckButton.Click += new System.EventHandler(this.OnProcessButtonClick);
            // 
            // _markerDetails
            // 
            this._markerDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this._markerDetails.Location = new System.Drawing.Point(0, 0);
            this._markerDetails.Name = "_markerDetails";
            this._markerDetails.Size = new System.Drawing.Size(436, 266);
            this._markerDetails.TabIndex = 0;
            // 
            // _filterChooserView
            // 
            this._filterChooserView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._filterChooserView.Enabled = false;
            this._filterChooserView.Location = new System.Drawing.Point(7, 7);
            this._filterChooserView.Model = null;
            this._filterChooserView.Name = "_filterChooserView";
            this._filterChooserView.Size = new System.Drawing.Size(422, 112);
            this._filterChooserView.TabIndex = 2;
            // 
            // _sfmEditorView
            // 
            this._sfmEditorView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._sfmEditorView.AutoScroll = true;
            this._sfmEditorView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._sfmEditorView.Location = new System.Drawing.Point(0, 44);
            this._sfmEditorView.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this._sfmEditorView.Name = "_sfmEditorView";
            this._sfmEditorView.Size = new System.Drawing.Size(449, 382);
            this._sfmEditorView.TabIndex = 2;
            // 
            // _recordNavigatorView
            // 
            this._recordNavigatorView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._recordNavigatorView.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._recordNavigatorView.Location = new System.Drawing.Point(-2, 1);
            this._recordNavigatorView.Model = null;
            this._recordNavigatorView.Name = "_recordNavigatorView";
            this._recordNavigatorView.Size = new System.Drawing.Size(450, 42);
            this._recordNavigatorView.TabIndex = 3;
            // 
            // MainWindowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 452);
            this.Controls.Add(toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainWindowView";
            this.Text = "SOLID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindowView_FormClosing);
            this.Load += new System.EventHandler(this.MainWindowView_Load);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private RecordNavigatorView _recordNavigatorView;
        private SfmEditorView _sfmEditorView;
        private SearchView _searchView;
        private System.Windows.Forms.ToolStripButton _openButton;
        private System.Windows.Forms.ToolStripButton _saveButton;
        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _aboutBoxButton;
        private System.Windows.Forms.ToolStripButton _changeTemplate;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private FilterChooserView _filterChooserView;
        private MarkerDetails _markerDetails;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button _recheckButton;
    }
}

