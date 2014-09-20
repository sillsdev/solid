// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using MappingView=SolidGui.Mapping.MappingView;

namespace SolidGui.MarkerSettings
{
    partial class MarkerSettingsView
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
            this._mappingPage = new System.Windows.Forms.TabPage();
            this._mappingView = new SolidGui.Mapping.MappingView();
            this._writingSystemPage = new System.Windows.Forms.TabPage();
            this.wsPickerUsingComboBox1 = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
            this._cbUnicode = new System.Windows.Forms.CheckBox();
            this._setupWsLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this._structurePage = new System.Windows.Forms.TabPage();
            this._splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._splitContainerInner = new System.Windows.Forms.SplitContainer();
            this._structurePropertiesView = new SolidGui.StructurePropertiesView();
            this._structureTabControl = new System.Windows.Forms.TabControl();
            this._markerListView = new System.Windows.Forms.ListView();
            this._descriptionTextBox = new System.Windows.Forms.TextBox();
            this._mappingPage.SuspendLayout();
            this._structurePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerOuter)).BeginInit();
            this._splitContainerOuter.Panel1.SuspendLayout();
            this._splitContainerOuter.Panel2.SuspendLayout();
            this._splitContainerOuter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerInner)).BeginInit();
            this._splitContainerInner.Panel1.SuspendLayout();
            this._splitContainerInner.Panel2.SuspendLayout();
            this._splitContainerInner.SuspendLayout();
            this._structureTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mappingPage
            // 
            this._mappingPage.Controls.Add(this._mappingView);
            this._mappingPage.Location = new System.Drawing.Point(4, 22);
            this._mappingPage.Name = "_mappingPage";
            this._mappingPage.Size = new System.Drawing.Size(477, 368);
            this._mappingPage.TabIndex = 2;
            this._mappingPage.Text = "Mappings";
            this._mappingPage.UseVisualStyleBackColor = true;
            // 
            // _mappingView
            // 
            this._mappingView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mappingView.Location = new System.Drawing.Point(0, 0);
            this._mappingView.Model = null;
            this._mappingView.Name = "_mappingView";
            this._mappingView.Size = new System.Drawing.Size(477, 368);
            this._mappingView.TabIndex = 0;
            // 
            // _writingSystemPage
            // 
            this._writingSystemPage.Location = new System.Drawing.Point(4, 22);
            this._writingSystemPage.Name = "_writingSystemPage";
            this._writingSystemPage.Size = new System.Drawing.Size(477, 368);
            this._writingSystemPage.TabIndex = 1;
            this._writingSystemPage.Text = "Writing Systems";
            this._writingSystemPage.UseVisualStyleBackColor = true;
            // 
            // wsPickerUsingComboBox1
            // 
            this.wsPickerUsingComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.wsPickerUsingComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wsPickerUsingComboBox1.FormattingEnabled = true;
            this.wsPickerUsingComboBox1.Location = new System.Drawing.Point(87, 3);
            this.wsPickerUsingComboBox1.Name = "wsPickerUsingComboBox1";
            this.wsPickerUsingComboBox1.Size = new System.Drawing.Size(121, 21);
            this.wsPickerUsingComboBox1.TabIndex = 2;
            this.wsPickerUsingComboBox1.SelectedComboIndexChanged += new System.EventHandler(this.wsPickerUsingComboBox1_SelectedComboIndexChanged);
            this.wsPickerUsingComboBox1.SelectedIndexChanged += new System.EventHandler(this.wsPickerUsingComboBox1_SelectedComboIndexChanged);
            // 
            // _cbUnicode
            // 
            this._cbUnicode.Dock = System.Windows.Forms.DockStyle.Top;
            this._cbUnicode.Location = new System.Drawing.Point(7, 305);
            this._cbUnicode.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this._cbUnicode.Name = "_cbUnicode";
            this._cbUnicode.Size = new System.Drawing.Size(410, 20);
            this._cbUnicode.TabIndex = 1;
            this._cbUnicode.Text = "Data for this marker is already encoded as UTF-8 Unicode";
            this._cbUnicode.UseVisualStyleBackColor = true;
            this._cbUnicode.CheckedChanged += new System.EventHandler(this._cbUnicode_CheckedChanged);
            // 
            // _setupWsLink
            // 
            this._setupWsLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._setupWsLink.AutoSize = true;
            this._setupWsLink.Location = new System.Drawing.Point(214, 6);
            this._setupWsLink.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this._setupWsLink.Name = "_setupWsLink";
            this._setupWsLink.Size = new System.Drawing.Size(47, 13);
            this._setupWsLink.TabIndex = 4;
            this._setupWsLink.TabStop = true;
            this._setupWsLink.Text = "Set up...";
            this._setupWsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._setupWsLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Writing System:";
            // 
            // _structurePage
            // 
            this._structurePage.Controls.Add(this._splitContainerOuter);
            this._structurePage.Location = new System.Drawing.Point(4, 22);
            this._structurePage.Name = "_structurePage";
            this._structurePage.Padding = new System.Windows.Forms.Padding(3);
            this._structurePage.Size = new System.Drawing.Size(477, 368);
            this._structurePage.TabIndex = 0;
            this._structurePage.Text = "Structure";
            this._structurePage.UseVisualStyleBackColor = true;
            // 
            // _splitContainerOuter
            // 
            this._splitContainerOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerOuter.Location = new System.Drawing.Point(3, 3);
            this._splitContainerOuter.Name = "_splitContainerOuter";
            // 
            // _splitContainerOuter.Panel1
            // 
            this._splitContainerOuter.Panel1.Controls.Add(this._markerListView);
            // 
            // _splitContainerOuter.Panel2
            // 
            this._splitContainerOuter.Panel2.Controls.Add(this.tableLayoutPanel1);
            this._splitContainerOuter.Size = new System.Drawing.Size(471, 362);
            this._splitContainerOuter.SplitterDistance = 47;
            this._splitContainerOuter.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._cbUnicode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._splitContainerInner, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(420, 362);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.wsPickerUsingComboBox1);
            this.flowLayoutPanel1.Controls.Add(this._setupWsLink);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 332);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(420, 28);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // _splitContainerInner
            // 
            this._splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerInner.Location = new System.Drawing.Point(3, 3);
            this._splitContainerInner.Name = "_splitContainerInner";
            this._splitContainerInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainerInner.Panel1
            // 
            this._splitContainerInner.Panel1.Controls.Add(this._structurePropertiesView);
            // 
            // _splitContainerInner.Panel2
            // 
            this._splitContainerInner.Panel2.Controls.Add(this._descriptionTextBox);
            this._splitContainerInner.Size = new System.Drawing.Size(414, 296);
            this._splitContainerInner.SplitterDistance = 251;
            this._splitContainerInner.TabIndex = 6;
            // 
            // _structurePropertiesView
            // 
            this._structurePropertiesView.Location = new System.Drawing.Point(4, 3);
            this._structurePropertiesView.MinimumSize = new System.Drawing.Size(377, 224);
            this._structurePropertiesView.Model = null;
            this._structurePropertiesView.Name = "_structurePropertiesView";
            this._structurePropertiesView.Size = new System.Drawing.Size(377, 224);
            this._structurePropertiesView.TabIndex = 0;
            // 
            // _structureTabControl
            // 
            this._structureTabControl.Controls.Add(this._structurePage);
            this._structureTabControl.Controls.Add(this._writingSystemPage);
            this._structureTabControl.Controls.Add(this._mappingPage);
            this._structureTabControl.Location = new System.Drawing.Point(0, 0);
            this._structureTabControl.Name = "_structureTabControl";
            this._structureTabControl.SelectedIndex = 0;
            this._structureTabControl.Size = new System.Drawing.Size(485, 394);
            this._structureTabControl.TabIndex = 2;
            this._structureTabControl.Leave += new System.EventHandler(this._structureTabControl_Leave);
            // 
            // _markerListView
            // 
            this._markerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._markerListView.Location = new System.Drawing.Point(0, 0);
            this._markerListView.Name = "_markerListView";
            this._markerListView.Size = new System.Drawing.Size(47, 362);
            this._markerListView.TabIndex = 3;
            this._markerListView.UseCompatibleStateImageBehavior = false;
            // 
            // _descriptionTextBox
            // 
            this._descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._descriptionTextBox.Location = new System.Drawing.Point(0, 0);
            this._descriptionTextBox.Multiline = true;
            this._descriptionTextBox.Name = "_descriptionTextBox";
            this._descriptionTextBox.Size = new System.Drawing.Size(414, 41);
            this._descriptionTextBox.TabIndex = 0;
            // 
            // MarkerSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._structureTabControl);
            this.MinimumSize = new System.Drawing.Size(395, 258);
            this.Name = "MarkerSettingsView";
            this.Size = new System.Drawing.Size(795, 397);
            this.Load += new System.EventHandler(this.MarkerSettingsView_Load);
            this._mappingPage.ResumeLayout(false);
            this._structurePage.ResumeLayout(false);
            this._splitContainerOuter.Panel1.ResumeLayout(false);
            this._splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerOuter)).EndInit();
            this._splitContainerOuter.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this._splitContainerInner.Panel1.ResumeLayout(false);
            this._splitContainerInner.Panel2.ResumeLayout(false);
            this._splitContainerInner.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerInner)).EndInit();
            this._splitContainerInner.ResumeLayout(false);
            this._structureTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage _mappingPage;
        private MappingView _mappingView;
        private System.Windows.Forms.TabPage _writingSystemPage;
        private System.Windows.Forms.CheckBox _cbUnicode;
        private System.Windows.Forms.TabPage _structurePage;
        private StructurePropertiesView _structurePropertiesView;
        private System.Windows.Forms.TabControl _structureTabControl;
        private System.Windows.Forms.LinkLabel _setupWsLink;
        private System.Windows.Forms.Label label1;
        private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox wsPickerUsingComboBox1;
        private System.Windows.Forms.SplitContainer _splitContainerOuter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer _splitContainerInner;
        private System.Windows.Forms.ListView _markerListView;
        private System.Windows.Forms.TextBox _descriptionTextBox;

    }
}