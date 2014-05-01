// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._recheckButton = new System.Windows.Forms.Button();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainerLeftRight = new System.Windows.Forms.SplitContainer();
            this.splitContainerUpDown = new System.Windows.Forms.SplitContainer();
            this._markerSettingsListView = new SolidGui.MarkerSettings.MarkerSettingsListView();
            this.panelMarkerSettings = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._editMarkerProperties = new System.Windows.Forms.Button();
            this.buttonTree = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._filterChooserView = new SolidGui.Filter.FilterChooserView();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._sfmEditorView = new SolidGui.SfmEditorView();
            this._recordNavigatorView = new SolidGui.RecordNavigatorView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exportXmlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._markersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._propertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._switchTemplatesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._changeWritingSystemsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._findMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._fixMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._suggestFixesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._recipesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._splitSemicolonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._trimSpacesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unwrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._globallyDeleteFieldsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._quickFixesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._moveUpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._deleteFieldsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._makeMarkersRealMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._addGuidsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._fixLinksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._fixPsSnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._recheckMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._goFirstMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._goPreviousMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._goNextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._goLastMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._markerHierarchyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._addFilterMarkersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._addFilterDataShapeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._reportProblemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftRight)).BeginInit();
            this.splitContainerLeftRight.Panel1.SuspendLayout();
            this.splitContainerLeftRight.Panel2.SuspendLayout();
            this.splitContainerLeftRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerUpDown)).BeginInit();
            this.splitContainerUpDown.Panel1.SuspendLayout();
            this.splitContainerUpDown.Panel2.SuspendLayout();
            this.splitContainerUpDown.SuspendLayout();
            this.panelMarkerSettings.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
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
            toolStrip1.Location = new System.Drawing.Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            toolStrip1.Size = new System.Drawing.Size(891, 25);
            toolStrip1.Stretch = true;
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // _openButton
            // 
            this._openButton.Image = global::SolidGui.Properties.Resources.folder_open;
            this._openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._openButton.Name = "_openButton";
            this._openButton.Size = new System.Drawing.Size(108, 22);
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
            this._exportButton.Size = new System.Drawing.Size(69, 22);
            this._exportButton.Text = "E&xport...";
            this._exportButton.ToolTipText = "Export As LIFT... (Experimental)";
            this._exportButton.Click += new System.EventHandler(this.OnExportButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // _changeTemplate
            // 
            this._changeTemplate.Image = global::SolidGui.Properties.Resources.template;
            this._changeTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._changeTemplate.Name = "_changeTemplate";
            this._changeTemplate.Size = new System.Drawing.Size(130, 22);
            this._changeTemplate.Text = "&Change Template...";
            this._changeTemplate.Visible = false;
            this._changeTemplate.Click += new System.EventHandler(this.OnChangeTemplate_Click);
            // 
            // _changeWritingSystems
            // 
            this._changeWritingSystems.Image = global::SolidGui.Properties.Resources.template;
            this._changeWritingSystems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._changeWritingSystems.Name = "_changeWritingSystems";
            this._changeWritingSystems.Size = new System.Drawing.Size(165, 22);
            this._changeWritingSystems.Text = "Change &Writing Systems...";
            this._changeWritingSystems.Visible = false;
            this._changeWritingSystems.Click += new System.EventHandler(this.OnChangeWritingSystems_Click);
            // 
            // _quickFixButton
            // 
            this._quickFixButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._quickFixButton.Image = ((System.Drawing.Image)(resources.GetObject("_quickFixButton.Image")));
            this._quickFixButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._quickFixButton.Name = "_quickFixButton";
            this._quickFixButton.Size = new System.Drawing.Size(79, 22);
            this._quickFixButton.Text = "&Quick Fixes...";
            this._quickFixButton.Visible = false;
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
            this._aboutBoxButton.Visible = false;
            this._aboutBoxButton.Click += new System.EventHandler(this.OnAboutBoxButton_Click);
            // 
            // _recheckButton
            // 
            this._recheckButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._recheckButton.FlatAppearance.BorderSize = 0;
            this._recheckButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._recheckButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._recheckButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._recheckButton.Image = ((System.Drawing.Image)(resources.GetObject("_recheckButton.Image")));
            this._recheckButton.Location = new System.Drawing.Point(338, 2);
            this._recheckButton.Name = "_recheckButton";
            this._recheckButton.Size = new System.Drawing.Size(85, 31);
            this._recheckButton.TabIndex = 4;
            this._recheckButton.Text = "&Recheck";
            this._recheckButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this._recheckButton, "Recheck all records (Ctrl+F5)");
            this._recheckButton.UseVisualStyleBackColor = true;
            this._recheckButton.Click += new System.EventHandler(this.OnRecheckButtonClick);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerLeftRight);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(891, 403);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(891, 452);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // splitContainerLeftRight
            // 
            this.splitContainerLeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeftRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeftRight.Name = "splitContainerLeftRight";
            // 
            // splitContainerLeftRight.Panel1
            // 
            this.splitContainerLeftRight.Panel1.Controls.Add(this.splitContainerUpDown);
            this.splitContainerLeftRight.Panel1.Enabled = false;
            this.splitContainerLeftRight.Panel1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // splitContainerLeftRight.Panel2
            // 
            this.splitContainerLeftRight.Panel2.Controls.Add(this._sfmEditorView);
            this.splitContainerLeftRight.Panel2.Controls.Add(this._recordNavigatorView);
            this.splitContainerLeftRight.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.splitContainerLeftRight.Size = new System.Drawing.Size(891, 403);
            this.splitContainerLeftRight.SplitterDistance = 435;
            this.splitContainerLeftRight.SplitterWidth = 5;
            this.splitContainerLeftRight.TabIndex = 3;
            // 
            // splitContainerUpDown
            // 
            this.splitContainerUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerUpDown.Location = new System.Drawing.Point(2, 0);
            this.splitContainerUpDown.Name = "splitContainerUpDown";
            this.splitContainerUpDown.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerUpDown.Panel1
            // 
            this.splitContainerUpDown.Panel1.Controls.Add(this._markerSettingsListView);
            this.splitContainerUpDown.Panel1.Controls.Add(this.panelMarkerSettings);
            // 
            // splitContainerUpDown.Panel2
            // 
            this.splitContainerUpDown.Panel2.Controls.Add(this._filterChooserView);
            this.splitContainerUpDown.Panel2.Controls.Add(this.panelFilters);
            this.splitContainerUpDown.Size = new System.Drawing.Size(433, 403);
            this.splitContainerUpDown.SplitterDistance = 265;
            this.splitContainerUpDown.TabIndex = 2;
            // 
            // _markerSettingsListView
            // 
            this._markerSettingsListView.AutoSize = true;
            this._markerSettingsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._markerSettingsListView.Location = new System.Drawing.Point(0, 34);
            this._markerSettingsListView.Name = "_markerSettingsListView";
            this._markerSettingsListView.Size = new System.Drawing.Size(433, 231);
            this._markerSettingsListView.TabIndex = 0;
            // 
            // panelMarkerSettings
            // 
            this.panelMarkerSettings.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelMarkerSettings.Controls.Add(this.flowLayoutPanel1);
            this.panelMarkerSettings.Controls.Add(this.label2);
            this.panelMarkerSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMarkerSettings.Location = new System.Drawing.Point(0, 0);
            this.panelMarkerSettings.Name = "panelMarkerSettings";
            this.panelMarkerSettings.Size = new System.Drawing.Size(433, 34);
            this.panelMarkerSettings.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this._editMarkerProperties);
            this.flowLayoutPanel1.Controls.Add(this.buttonTree);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(287, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(145, 32);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // _editMarkerProperties
            // 
            this._editMarkerProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._editMarkerProperties.FlatAppearance.BorderSize = 0;
            this._editMarkerProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._editMarkerProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editMarkerProperties.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._editMarkerProperties.Image = global::SolidGui.Properties.Resources.cog;
            this._editMarkerProperties.Location = new System.Drawing.Point(47, 1);
            this._editMarkerProperties.Margin = new System.Windows.Forms.Padding(3, 1, 1, 3);
            this._editMarkerProperties.Name = "_editMarkerProperties";
            this._editMarkerProperties.Size = new System.Drawing.Size(97, 28);
            this._editMarkerProperties.TabIndex = 6;
            this._editMarkerProperties.Text = "Properties";
            this._editMarkerProperties.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editMarkerProperties.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._editMarkerProperties.UseVisualStyleBackColor = true;
            this._editMarkerProperties.Click += new System.EventHandler(this.OnEditMarkerPropertiesClick);
            // 
            // buttonTree
            // 
            this.buttonTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTree.FlatAppearance.BorderSize = 0;
            this.buttonTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTree.Image = ((System.Drawing.Image)(resources.GetObject("buttonTree.Image")));
            this.buttonTree.Location = new System.Drawing.Point(21, 2);
            this.buttonTree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.buttonTree.Name = "buttonTree";
            this.buttonTree.Size = new System.Drawing.Size(20, 25);
            this.buttonTree.TabIndex = 10;
            this.buttonTree.UseVisualStyleBackColor = true;
            this.buttonTree.Visible = false;
            this.buttonTree.Click += new System.EventHandler(this.buttonTree_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Marker Settings/Filters";
            // 
            // _filterChooserView
            // 
            this._filterChooserView.AutoSize = true;
            this._filterChooserView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterChooserView.Enabled = false;
            this._filterChooserView.Location = new System.Drawing.Point(0, 34);
            this._filterChooserView.Name = "_filterChooserView";
            this._filterChooserView.Size = new System.Drawing.Size(433, 100);
            this._filterChooserView.TabIndex = 2;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelFilters.Controls.Add(this.label1);
            this.panelFilters.Controls.Add(this._recheckButton);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 0);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(433, 34);
            this.panelFilters.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Error Filters";
            // 
            // _sfmEditorView
            // 
            this._sfmEditorView.AutoScroll = true;
            this._sfmEditorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sfmEditorView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._sfmEditorView.HighlightMarkers = null;
            this._sfmEditorView.Location = new System.Drawing.Point(0, 34);
            this._sfmEditorView.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this._sfmEditorView.Name = "_sfmEditorView";
            this._sfmEditorView.Size = new System.Drawing.Size(449, 369);
            this._sfmEditorView.TabIndex = 2;
            // 
            // _recordNavigatorView
            // 
            this._recordNavigatorView.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._recordNavigatorView.Dock = System.Windows.Forms.DockStyle.Top;
            this._recordNavigatorView.Enabled = false;
            this._recordNavigatorView.Location = new System.Drawing.Point(0, 0);
            this._recordNavigatorView.Name = "_recordNavigatorView";
            this._recordNavigatorView.Size = new System.Drawing.Size(449, 34);
            this._recordNavigatorView.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenuItem,
            this._markersMenuItem,
            this._editMenuItem,
            this._fixMenuItem,
            this._viewMenuItem,
            this._helpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(891, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // _fileMenuItem
            // 
            this._fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openMenuItem,
            this._saveMenuItem,
            this._saveAsMenuItem,
            this._exportXmlMenuItem,
            this._exitMenuItem});
            this._fileMenuItem.Name = "_fileMenuItem";
            this._fileMenuItem.Size = new System.Drawing.Size(37, 22);
            this._fileMenuItem.Text = "&File";
            // 
            // _openMenuItem
            // 
            this._openMenuItem.Image = global::SolidGui.Properties.Resources.folder_open;
            this._openMenuItem.Name = "_openMenuItem";
            this._openMenuItem.Size = new System.Drawing.Size(261, 22);
            this._openMenuItem.Text = "&Open Lexicon... (Ctrl+O, Alt+O)";
            this._openMenuItem.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // _saveMenuItem
            // 
            this._saveMenuItem.Enabled = false;
            this._saveMenuItem.Image = global::SolidGui.Properties.Resources.save;
            this._saveMenuItem.Name = "_saveMenuItem";
            this._saveMenuItem.Size = new System.Drawing.Size(261, 22);
            this._saveMenuItem.Text = "&Save (Ctrl+S, Alt+S)";
            this._saveMenuItem.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // _saveAsMenuItem
            // 
            this._saveAsMenuItem.Name = "_saveAsMenuItem";
            this._saveAsMenuItem.Size = new System.Drawing.Size(261, 22);
            this._saveAsMenuItem.Text = "Save a Copy As...";
            this._saveAsMenuItem.Click += new System.EventHandler(this.OnSaveAsClick);
            // 
            // _exportXmlMenuItem
            // 
            this._exportXmlMenuItem.Name = "_exportXmlMenuItem";
            this._exportXmlMenuItem.Size = new System.Drawing.Size(261, 22);
            this._exportXmlMenuItem.Text = "Export as &LIFT XML (experimental)...";
            this._exportXmlMenuItem.Click += new System.EventHandler(this.OnExportButton_Click);
            // 
            // _exitMenuItem
            // 
            this._exitMenuItem.Name = "_exitMenuItem";
            this._exitMenuItem.Size = new System.Drawing.Size(261, 22);
            this._exitMenuItem.Text = "E&xit (Alt + F4)";
            this._exitMenuItem.Click += new System.EventHandler(this._exitMenuItem_Click);
            // 
            // _markersMenuItem
            // 
            this._markersMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._propertiesMenuItem,
            this._switchTemplatesMenuItem,
            this._changeWritingSystemsMenuItem});
            this._markersMenuItem.Name = "_markersMenuItem";
            this._markersMenuItem.Size = new System.Drawing.Size(61, 22);
            this._markersMenuItem.Text = "&Markers";
            // 
            // _propertiesMenuItem
            // 
            this._propertiesMenuItem.Name = "_propertiesMenuItem";
            this._propertiesMenuItem.Size = new System.Drawing.Size(243, 22);
            this._propertiesMenuItem.Text = "&Properties (for current marker)...";
            this._propertiesMenuItem.Click += new System.EventHandler(this.OnEditMarkerPropertiesClick);
            // 
            // _switchTemplatesMenuItem
            // 
            this._switchTemplatesMenuItem.Name = "_switchTemplatesMenuItem";
            this._switchTemplatesMenuItem.Size = new System.Drawing.Size(243, 22);
            this._switchTemplatesMenuItem.Text = "Switch &Templates...";
            this._switchTemplatesMenuItem.Click += new System.EventHandler(this.OnChangeTemplate_Click);
            // 
            // _changeWritingSystemsMenuItem
            // 
            this._changeWritingSystemsMenuItem.Name = "_changeWritingSystemsMenuItem";
            this._changeWritingSystemsMenuItem.Size = new System.Drawing.Size(243, 22);
            this._changeWritingSystemsMenuItem.Text = "Specify &Writing Systems...";
            this._changeWritingSystemsMenuItem.Click += new System.EventHandler(this.OnChangeWritingSystems_Click);
            // 
            // _editMenuItem
            // 
            this._editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._findMenuItem,
            this.toolStripSeparator1,
            this._cutMenuItem,
            this._copyMenuItem,
            this._pasteMenuItem});
            this._editMenuItem.Name = "_editMenuItem";
            this._editMenuItem.Size = new System.Drawing.Size(39, 22);
            this._editMenuItem.Text = "&Edit";
            // 
            // _findMenuItem
            // 
            this._findMenuItem.Name = "_findMenuItem";
            this._findMenuItem.Size = new System.Drawing.Size(150, 22);
            this._findMenuItem.Text = "&Find... (Ctrl+F)";
            this._findMenuItem.Click += new System.EventHandler(this._findMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // _cutMenuItem
            // 
            this._cutMenuItem.Enabled = false;
            this._cutMenuItem.Name = "_cutMenuItem";
            this._cutMenuItem.Size = new System.Drawing.Size(150, 22);
            this._cutMenuItem.Text = "Cu&t (Ctrl+X)";
            this._cutMenuItem.Visible = false;
            // 
            // _copyMenuItem
            // 
            this._copyMenuItem.Enabled = false;
            this._copyMenuItem.Name = "_copyMenuItem";
            this._copyMenuItem.Size = new System.Drawing.Size(150, 22);
            this._copyMenuItem.Text = "&Copy (Ctrl+C)";
            this._copyMenuItem.Visible = false;
            this._copyMenuItem.Click += new System.EventHandler(this._copyMenuItem_Click);
            // 
            // _pasteMenuItem
            // 
            this._pasteMenuItem.Enabled = false;
            this._pasteMenuItem.Name = "_pasteMenuItem";
            this._pasteMenuItem.Size = new System.Drawing.Size(150, 22);
            this._pasteMenuItem.Text = "&Paste (Ctrl+V)";
            this._pasteMenuItem.Visible = false;
            // 
            // _fixMenuItem
            // 
            this._fixMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._suggestFixesMenuItem,
            this._recipesMenuItem,
            this._quickFixesMenuItem,
            this._moveUpMenuItem,
            this._deleteFieldsMenuItem,
            this._makeMarkersRealMenuItem,
            this._addGuidsMenuItem,
            this._fixLinksMenuItem,
            this._fixPsSnMenuItem});
            this._fixMenuItem.Name = "_fixMenuItem";
            this._fixMenuItem.Size = new System.Drawing.Size(33, 22);
            this._fixMenuItem.Text = "F&ix";
            // 
            // _suggestFixesMenuItem
            // 
            this._suggestFixesMenuItem.Name = "_suggestFixesMenuItem";
            this._suggestFixesMenuItem.Size = new System.Drawing.Size(229, 22);
            this._suggestFixesMenuItem.Text = "&Suggest Fixes...";
            this._suggestFixesMenuItem.Visible = false;
            // 
            // _recipesMenuItem
            // 
            this._recipesMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._splitSemicolonMenuItem,
            this._trimSpacesMenuItem,
            this.unwrapToolStripMenuItem,
            this._globallyDeleteFieldsMenuItem});
            this._recipesMenuItem.Name = "_recipesMenuItem";
            this._recipesMenuItem.Size = new System.Drawing.Size(229, 22);
            this._recipesMenuItem.Text = "&Find/Replace Recipes";
            // 
            // _splitSemicolonMenuItem
            // 
            this._splitSemicolonMenuItem.Name = "_splitSemicolonMenuItem";
            this._splitSemicolonMenuItem.Size = new System.Drawing.Size(258, 22);
            this._splitSemicolonMenuItem.Text = "&Split on semicolon (typical fields)...";
            this._splitSemicolonMenuItem.Click += new System.EventHandler(this.toolStripSplitOnSemicolon);
            // 
            // _trimSpacesMenuItem
            // 
            this._trimSpacesMenuItem.Name = "_trimSpacesMenuItem";
            this._trimSpacesMenuItem.Size = new System.Drawing.Size(258, 22);
            this._trimSpacesMenuItem.Text = "&Trim spaces...";
            this._trimSpacesMenuItem.Click += new System.EventHandler(this.trimSpacesToolStripMenuItem_Click);
            // 
            // unwrapToolStripMenuItem
            // 
            this.unwrapToolStripMenuItem.Name = "unwrapToolStripMenuItem";
            this.unwrapToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.unwrapToolStripMenuItem.Text = "&Unwrap...";
            this.unwrapToolStripMenuItem.Click += new System.EventHandler(this.unwrapToolStripMenuItem_Click);
            // 
            // _globallyDeleteFieldsMenuItem
            // 
            this._globallyDeleteFieldsMenuItem.Name = "_globallyDeleteFieldsMenuItem";
            this._globallyDeleteFieldsMenuItem.Size = new System.Drawing.Size(258, 22);
            this._globallyDeleteFieldsMenuItem.Text = "Globally &Delete fields...";
            this._globallyDeleteFieldsMenuItem.Click += new System.EventHandler(this._globallyDeleteFieldsMenuItem_Click);
            // 
            // _quickFixesMenuItem
            // 
            this._quickFixesMenuItem.Name = "_quickFixesMenuItem";
            this._quickFixesMenuItem.Size = new System.Drawing.Size(229, 22);
            this._quickFixesMenuItem.Text = "&Quick Fixes...";
            this._quickFixesMenuItem.Click += new System.EventHandler(this.OnQuickFix);
            // 
            // _moveUpMenuItem
            // 
            this._moveUpMenuItem.Name = "_moveUpMenuItem";
            this._moveUpMenuItem.Size = new System.Drawing.Size(229, 22);
            this._moveUpMenuItem.Text = "Move &Up...";
            this._moveUpMenuItem.Visible = false;
            // 
            // _deleteFieldsMenuItem
            // 
            this._deleteFieldsMenuItem.Name = "_deleteFieldsMenuItem";
            this._deleteFieldsMenuItem.Size = new System.Drawing.Size(229, 22);
            this._deleteFieldsMenuItem.Text = "&Delete Fields...";
            this._deleteFieldsMenuItem.Visible = false;
            // 
            // _makeMarkersRealMenuItem
            // 
            this._makeMarkersRealMenuItem.Name = "_makeMarkersRealMenuItem";
            this._makeMarkersRealMenuItem.Size = new System.Drawing.Size(229, 22);
            this._makeMarkersRealMenuItem.Text = "Make Markers &Real...";
            // 
            // _addGuidsMenuItem
            // 
            this._addGuidsMenuItem.Name = "_addGuidsMenuItem";
            this._addGuidsMenuItem.Size = new System.Drawing.Size(229, 22);
            this._addGuidsMenuItem.Text = "Add &GUIDs...";
            // 
            // _fixLinksMenuItem
            // 
            this._fixLinksMenuItem.Name = "_fixLinksMenuItem";
            this._fixLinksMenuItem.Size = new System.Drawing.Size(229, 22);
            this._fixLinksMenuItem.Text = "Fix &Links (experimental)...";
            this._fixLinksMenuItem.Visible = false;
            // 
            // _fixPsSnMenuItem
            // 
            this._fixPsSnMenuItem.Name = "_fixPsSnMenuItem";
            this._fixPsSnMenuItem.Size = new System.Drawing.Size(229, 22);
            this._fixPsSnMenuItem.Text = "Fix &ps and sn (experimental)...";
            this._fixPsSnMenuItem.Visible = false;
            // 
            // _viewMenuItem
            // 
            this._viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._recheckMenuItem,
            this._refreshMenuItem,
            this.toolStripSeparator3,
            this._goFirstMenuItem,
            this._goPreviousMenuItem,
            this._goNextMenuItem,
            this._goLastMenuItem,
            this._markerHierarchyMenuItem,
            this._addFilterMarkersMenuItem,
            this._addFilterDataShapeMenuItem});
            this._viewMenuItem.Name = "_viewMenuItem";
            this._viewMenuItem.Size = new System.Drawing.Size(44, 22);
            this._viewMenuItem.Text = "&View";
            // 
            // _recheckMenuItem
            // 
            this._recheckMenuItem.Name = "_recheckMenuItem";
            this._recheckMenuItem.Size = new System.Drawing.Size(227, 22);
            this._recheckMenuItem.Text = "Re&check all records (Ctrl+F5)";
            this._recheckMenuItem.Click += new System.EventHandler(this.OnRecheckButtonClick);
            // 
            // _refreshMenuItem
            // 
            this._refreshMenuItem.Name = "_refreshMenuItem";
            this._refreshMenuItem.Size = new System.Drawing.Size(227, 22);
            this._refreshMenuItem.Text = "&Refresh right pane (F5)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(224, 6);
            // 
            // _goFirstMenuItem
            // 
            this._goFirstMenuItem.Name = "_goFirstMenuItem";
            this._goFirstMenuItem.Size = new System.Drawing.Size(227, 22);
            this._goFirstMenuItem.Text = "Go to &First (Ctrl+Shift+PgUp)";
            this._goFirstMenuItem.Click += new System.EventHandler(this._goFirstMenuItem_Click);
            // 
            // _goPreviousMenuItem
            // 
            this._goPreviousMenuItem.Name = "_goPreviousMenuItem";
            this._goPreviousMenuItem.Size = new System.Drawing.Size(227, 22);
            this._goPreviousMenuItem.Text = "Go to &Previous (Ctrl+PgUp)";
            this._goPreviousMenuItem.Click += new System.EventHandler(this._goPreviousMenuItem_Click);
            // 
            // _goNextMenuItem
            // 
            this._goNextMenuItem.Name = "_goNextMenuItem";
            this._goNextMenuItem.Size = new System.Drawing.Size(227, 22);
            this._goNextMenuItem.Text = "Go to &Next (Ctrl+PgDn)";
            this._goNextMenuItem.Click += new System.EventHandler(this._goNextMenuItem_Click);
            // 
            // _goLastMenuItem
            // 
            this._goLastMenuItem.Name = "_goLastMenuItem";
            this._goLastMenuItem.Size = new System.Drawing.Size(227, 22);
            this._goLastMenuItem.Text = "Go to &Last (Ctrl+Shift+PgDn)";
            this._goLastMenuItem.Click += new System.EventHandler(this._goLastMenuItem_Click);
            // 
            // _markerHierarchyMenuItem
            // 
            this._markerHierarchyMenuItem.Name = "_markerHierarchyMenuItem";
            this._markerHierarchyMenuItem.Size = new System.Drawing.Size(227, 22);
            this._markerHierarchyMenuItem.Text = "Marker &Hierarchy...";
            this._markerHierarchyMenuItem.Visible = false;
            // 
            // _addFilterMarkersMenuItem
            // 
            this._addFilterMarkersMenuItem.Name = "_addFilterMarkersMenuItem";
            this._addFilterMarkersMenuItem.Size = new System.Drawing.Size(227, 22);
            this._addFilterMarkersMenuItem.Text = "Add filter: Markers that E&xit...";
            this._addFilterMarkersMenuItem.Visible = false;
            // 
            // _addFilterDataShapeMenuItem
            // 
            this._addFilterDataShapeMenuItem.Name = "_addFilterDataShapeMenuItem";
            this._addFilterDataShapeMenuItem.Size = new System.Drawing.Size(227, 22);
            this._addFilterDataShapeMenuItem.Text = "Add filter: Data &Shape...";
            this._addFilterDataShapeMenuItem.Visible = false;
            // 
            // _helpMenuItem
            // 
            this._helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openHelpMenuItem,
            this._aboutMenuItem,
            this._reportProblemMenuItem});
            this._helpMenuItem.Name = "_helpMenuItem";
            this._helpMenuItem.Size = new System.Drawing.Size(44, 22);
            this._helpMenuItem.Text = "&Help";
            // 
            // _openHelpMenuItem
            // 
            this._openHelpMenuItem.Name = "_openHelpMenuItem";
            this._openHelpMenuItem.Size = new System.Drawing.Size(238, 22);
            this._openHelpMenuItem.Text = "Open PDF &Help Manual (F1)";
            this._openHelpMenuItem.Click += new System.EventHandler(this._openHelpMenuItem_Click);
            // 
            // _aboutMenuItem
            // 
            this._aboutMenuItem.Name = "_aboutMenuItem";
            this._aboutMenuItem.Size = new System.Drawing.Size(238, 22);
            this._aboutMenuItem.Text = "&About Solid...";
            this._aboutMenuItem.Click += new System.EventHandler(this.OnAboutBoxButton_Click);
            // 
            // _reportProblemMenuItem
            // 
            this._reportProblemMenuItem.Name = "_reportProblemMenuItem";
            this._reportProblemMenuItem.Size = new System.Drawing.Size(238, 22);
            this._reportProblemMenuItem.Text = "&Report a problem/suggestion...";
            this._reportProblemMenuItem.Click += new System.EventHandler(this.reportAProblemsuggestionToolStripMenuItem_Click);
            // 
            // MainWindowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 452);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1430, 5);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(629, 371);
            this.Name = "MainWindowView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Solid";
            this.Deactivate += new System.EventHandler(this.MainWindowView_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindowView_FormClosing);
            this.Load += new System.EventHandler(this.MainWindowView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindowView_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindowView_KeyUp);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainerLeftRight.Panel1.ResumeLayout(false);
            this.splitContainerLeftRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftRight)).EndInit();
            this.splitContainerLeftRight.ResumeLayout(false);
            this.splitContainerUpDown.Panel1.ResumeLayout(false);
            this.splitContainerUpDown.Panel1.PerformLayout();
            this.splitContainerUpDown.Panel2.ResumeLayout(false);
            this.splitContainerUpDown.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerUpDown)).EndInit();
            this.splitContainerUpDown.ResumeLayout(false);
            this.panelMarkerSettings.ResumeLayout(false);
            this.panelMarkerSettings.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton _openButton;
        private System.Windows.Forms.ToolStripButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton _aboutBoxButton;
        private System.Windows.Forms.ToolStripButton _changeTemplate;
        private System.Windows.Forms.ToolStripButton _exportButton;
        private System.Windows.Forms.ToolStripButton _quickFixButton;
		private System.Windows.Forms.ToolStripButton _changeWritingSystems;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainerLeftRight;
        private System.Windows.Forms.SplitContainer splitContainerUpDown;
        private System.Windows.Forms.Panel panelMarkerSettings;
        private System.Windows.Forms.Button _editMarkerProperties;
        private System.Windows.Forms.Label label2;
        private MarkerSettingsListView _markerSettingsListView;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _recheckButton;
        private FilterChooserView _filterChooserView;
        private SfmEditorView _sfmEditorView;
        private RecordNavigatorView _recordNavigatorView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _exportXmlMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _markersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _switchTemplatesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _changeWritingSystemsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _propertiesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _pasteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _findMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fixMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _quickFixesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _recheckMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _refreshMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _goPreviousMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _goNextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _goLastMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _openHelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _aboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _reportProblemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _goFirstMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _cutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem _saveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _suggestFixesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _moveUpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _deleteFieldsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _makeMarkersRealMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _addGuidsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fixLinksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fixPsSnMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _markerHierarchyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _addFilterMarkersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _addFilterDataShapeMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonTree;
        private System.Windows.Forms.ToolStripMenuItem _recipesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _splitSemicolonMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _trimSpacesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _globallyDeleteFieldsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unwrapToolStripMenuItem;
    }
}

