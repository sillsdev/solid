using SolidGui.Filter;
using SolidGui.MarkerSettings;
using SolidGui.Search;

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
            this._exportButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._changeTemplate = new System.Windows.Forms.ToolStripButton();
            this._changeWritingSystems = new System.Windows.Forms.ToolStripButton();
            this._quickFixButton = new System.Windows.Forms.ToolStripButton();
            this._aboutBoxButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this._editMarkerProperties = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._recheckButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._markerSettingsList = new SolidGui.MarkerSettings.MarkerSettingsListView();
            this._filterChooserView = new SolidGui.Filter.FilterChooserView();
            this._sfmEditorView = new SolidGui.SfmEditorView();
            this._recordNavigatorView = new SolidGui.RecordNavigatorView();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openButton,
            this._saveButton,
            this._exportButton,
            this.toolStripSeparator2,
            this._changeTemplate,
            this._changeWritingSystems,
            this._quickFixButton,
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
            this._openButton.Text = "&Open Lexicon...";
            this._openButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._openButton.ToolTipText = "Open Lexicon... Ctrl+O";
            this._openButton.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // _saveButton
            // 
            this._saveButton.Enabled = false;
            this._saveButton.Image = global::SolidGui.Properties.Resources.save;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(51, 22);
            this._saveButton.Text = "&Save";
            this._saveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._saveButton.ToolTipText = "Save Ctrl+S";
            this._saveButton.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // _exportButton
            // 
            this._exportButton.Image = global::SolidGui.Properties.Resources.folder_export;
            this._exportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new System.Drawing.Size(71, 22);
            this._exportButton.Text = "&Export...";
            this._exportButton.Click += new System.EventHandler(this.OnExportButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _changeTemplate
            // 
            this._changeTemplate.Image = global::SolidGui.Properties.Resources.template;
            this._changeTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._changeTemplate.Name = "_changeTemplate";
            this._changeTemplate.Size = new System.Drawing.Size(123, 22);
            this._changeTemplate.Text = "&Change Template...";
            this._changeTemplate.Click += new System.EventHandler(this.OnChangeTemplate_Click);
            // 
            // _changeWritingSystems
            // 
            this._changeWritingSystems.Image = global::SolidGui.Properties.Resources.template;
            this._changeWritingSystems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._changeWritingSystems.Name = "_changeWritingSystems";
            this._changeWritingSystems.Size = new System.Drawing.Size(156, 22);
            this._changeWritingSystems.Text = "Change &Writing Systems...";
            this._changeWritingSystems.Click += new System.EventHandler(this.OnChangeWritingSystems_Click);
            // 
            // _quickFixButton
            // 
            this._quickFixButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._quickFixButton.Image = ((System.Drawing.Image)(resources.GetObject("_quickFixButton.Image")));
            this._quickFixButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._quickFixButton.Name = "_quickFixButton";
            this._quickFixButton.Size = new System.Drawing.Size(77, 22);
            this._quickFixButton.Text = "&Quick Fixes...";
            this._quickFixButton.Click += new System.EventHandler(this.OnQuickFix);
            // 
            // _aboutBoxButton
            // 
            this._aboutBoxButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._aboutBoxButton.Image = global::SolidGui.Properties.Resources.info2;
            this._aboutBoxButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._aboutBoxButton.Name = "_aboutBoxButton";
            this._aboutBoxButton.Size = new System.Drawing.Size(23, 22);
            this._aboutBoxButton.Text = "&About Solid...";
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
            this.splitContainer1.SplitterDistance = 435;
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
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            this.splitContainer2.Panel1.Controls.Add(this._markerSettingsList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Panel2.Controls.Add(this._filterChooserView);
            this.splitContainer2.Size = new System.Drawing.Size(435, 426);
            this.splitContainer2.SplitterDistance = 266;
            this.splitContainer2.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this._editMarkerProperties);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(0, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(435, 34);
            this.panel2.TabIndex = 1;
            // 
            // _editMarkerProperties
            // 
            this._editMarkerProperties.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._editMarkerProperties.FlatAppearance.BorderSize = 0;
            this._editMarkerProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._editMarkerProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editMarkerProperties.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._editMarkerProperties.Image = global::SolidGui.Properties.Resources.cog;
            this._editMarkerProperties.Location = new System.Drawing.Point(324, 3);
            this._editMarkerProperties.Name = "_editMarkerProperties";
            this._editMarkerProperties.Size = new System.Drawing.Size(97, 28);
            this._editMarkerProperties.TabIndex = 6;
            this._editMarkerProperties.Text = "Properties";
            this._editMarkerProperties.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editMarkerProperties.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._editMarkerProperties.UseVisualStyleBackColor = true;
            this._editMarkerProperties.Click += new System.EventHandler(this.OnEditMarkerPropertiesClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Marker Settings";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._recheckButton);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 34);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Check Results";
            // 
            // _recheckButton
            // 
            this._recheckButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._recheckButton.FlatAppearance.BorderSize = 0;
            this._recheckButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._recheckButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._recheckButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._recheckButton.Image = ((System.Drawing.Image)(resources.GetObject("_recheckButton.Image")));
            this._recheckButton.Location = new System.Drawing.Point(336, 2);
            this._recheckButton.Name = "_recheckButton";
            this._recheckButton.Size = new System.Drawing.Size(85, 31);
            this._recheckButton.TabIndex = 4;
            this._recheckButton.Text = "&Recheck";
            this._recheckButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this._recheckButton, "Recheck all records (Ctrl+F5)");
            this._recheckButton.UseVisualStyleBackColor = true;
            this._recheckButton.Click += new System.EventHandler(this.OnRecheckButtonClick);
            // 
            // _markerSettingsList
            // 
            this._markerSettingsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._markerSettingsList.Location = new System.Drawing.Point(0, 38);
            this._markerSettingsList.Name = "_markerSettingsList";
            this._markerSettingsList.Size = new System.Drawing.Size(435, 228);
            this._markerSettingsList.TabIndex = 0;
            // 
            // _filterChooserView
            // 
            this._filterChooserView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._filterChooserView.Enabled = false;
            this._filterChooserView.Location = new System.Drawing.Point(3, 42);
            this._filterChooserView.Name = "_filterChooserView";
            this._filterChooserView.Size = new System.Drawing.Size(429, 114);
            this._filterChooserView.TabIndex = 2;
            // 
            // _sfmEditorView
            // 
            this._sfmEditorView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._sfmEditorView.AutoScroll = true;
            this._sfmEditorView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._sfmEditorView.HighlightMarkers = null;
            this._sfmEditorView.Indent = 130;
            this._sfmEditorView.Location = new System.Drawing.Point(0, 35);
            this._sfmEditorView.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this._sfmEditorView.Name = "_sfmEditorView";
            this._sfmEditorView.Size = new System.Drawing.Size(450, 391);
            this._sfmEditorView.TabIndex = 2;
            // 
            // _recordNavigatorView
            // 
            this._recordNavigatorView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._recordNavigatorView.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._recordNavigatorView.Enabled = false;
            this._recordNavigatorView.Location = new System.Drawing.Point(-2, 1);
            this._recordNavigatorView.Name = "_recordNavigatorView";
            this._recordNavigatorView.Size = new System.Drawing.Size(451, 34);
            this._recordNavigatorView.TabIndex = 3;
            // 
            // MainWindowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 452);
            this.Controls.Add(toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindowView";
            this.Text = "Solid";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindowView_FormClosing);
            this.Load += new System.EventHandler(this.MainWindowView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindowView_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindowView_KeyUp);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton _aboutBoxButton;
        private System.Windows.Forms.ToolStripButton _changeTemplate;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private FilterChooserView _filterChooserView;
        private MarkerSettingsListView _markerSettingsList;
        private System.Windows.Forms.Button _recheckButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton _exportButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _editMarkerProperties;
        private System.Windows.Forms.ToolStripButton _quickFixButton;
		private System.Windows.Forms.ToolStripButton _changeWritingSystems;
    }
}

