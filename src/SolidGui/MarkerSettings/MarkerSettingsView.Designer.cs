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
            this._setupWsLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.wsPickerUsingComboBox1 = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
            this._cbUnicode = new System.Windows.Forms.CheckBox();
            this._structurePage = new System.Windows.Forms.TabPage();
            this._structurePropertiesView = new SolidGui.StructurePropertiesView();
            this._markerListBox = new System.Windows.Forms.ListBox();
            this._structureTabControl = new System.Windows.Forms.TabControl();
            this._mappingPage.SuspendLayout();
            this._writingSystemPage.SuspendLayout();
            this._structurePage.SuspendLayout();
            this._structureTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mappingPage
            // 
            this._mappingPage.Controls.Add(this._mappingView);
            this._mappingPage.Location = new System.Drawing.Point(4, 22);
            this._mappingPage.Name = "_mappingPage";
            this._mappingPage.Size = new System.Drawing.Size(445, 364);
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
            this._mappingView.Size = new System.Drawing.Size(445, 364);
            this._mappingView.TabIndex = 0;
            // 
            // _writingSystemPage
            // 
            this._writingSystemPage.Controls.Add(this._setupWsLink);
            this._writingSystemPage.Controls.Add(this.label1);
            this._writingSystemPage.Controls.Add(this.wsPickerUsingComboBox1);
            this._writingSystemPage.Controls.Add(this._cbUnicode);
            this._writingSystemPage.Location = new System.Drawing.Point(4, 22);
            this._writingSystemPage.Name = "_writingSystemPage";
            this._writingSystemPage.Size = new System.Drawing.Size(445, 364);
            this._writingSystemPage.TabIndex = 1;
            this._writingSystemPage.Text = "Writing Systems";
            this._writingSystemPage.UseVisualStyleBackColor = true;
            // 
            // _setupWsLink
            // 
            this._setupWsLink.AutoSize = true;
            this._setupWsLink.Location = new System.Drawing.Point(257, 40);
            this._setupWsLink.Name = "_setupWsLink";
            this._setupWsLink.Size = new System.Drawing.Size(120, 13);
            this._setupWsLink.TabIndex = 4;
            this._setupWsLink.TabStop = true;
            this._setupWsLink.Text = "Set up writing systems...";
            this._setupWsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._setupWsLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Writing System";
            // 
            // wsPickerUsingComboBox1
            // 
            this.wsPickerUsingComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wsPickerUsingComboBox1.FormattingEnabled = true;
            this.wsPickerUsingComboBox1.Location = new System.Drawing.Point(102, 38);
            this.wsPickerUsingComboBox1.Name = "wsPickerUsingComboBox1";
            this.wsPickerUsingComboBox1.Size = new System.Drawing.Size(121, 21);
            this.wsPickerUsingComboBox1.TabIndex = 2;
            this.wsPickerUsingComboBox1.SelectedComboIndexChanged += new System.EventHandler(this.wsPickerUsingComboBox1_SelectedComboIndexChanged);
            this.wsPickerUsingComboBox1.SelectedIndexChanged += new System.EventHandler(this.wsPickerUsingComboBox1_SelectedComboIndexChanged);
            // 
            // _cbUnicode
            // 
            this._cbUnicode.AutoSize = true;
            this._cbUnicode.Location = new System.Drawing.Point(19, 81);
            this._cbUnicode.Name = "_cbUnicode";
            this._cbUnicode.Size = new System.Drawing.Size(216, 17);
            this._cbUnicode.TabIndex = 1;
            this._cbUnicode.Text = "Data for this marker is Unicode encoded";
            this._cbUnicode.UseVisualStyleBackColor = true;
            this._cbUnicode.CheckedChanged += new System.EventHandler(this._cbUnicode_CheckedChanged);
            // 
            // _structurePage
            // 
            this._structurePage.Controls.Add(this._structurePropertiesView);
            this._structurePage.Controls.Add(this._markerListBox);
            this._structurePage.Location = new System.Drawing.Point(4, 22);
            this._structurePage.Name = "_structurePage";
            this._structurePage.Padding = new System.Windows.Forms.Padding(3);
            this._structurePage.Size = new System.Drawing.Size(445, 364);
            this._structurePage.TabIndex = 0;
            this._structurePage.Text = "Structure";
            this._structurePage.UseVisualStyleBackColor = true;
            // 
            // _structurePropertiesView
            // 
            this._structurePropertiesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._structurePropertiesView.Location = new System.Drawing.Point(3, 3);
            this._structurePropertiesView.Model = null;
            this._structurePropertiesView.Name = "_structurePropertiesView";
            this._structurePropertiesView.Size = new System.Drawing.Size(439, 358);
            this._structurePropertiesView.TabIndex = 0;
            // 
            // _markerListBox
            // 
            this._markerListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._markerListBox.FormattingEnabled = true;
            this._markerListBox.Location = new System.Drawing.Point(255, -31);
            this._markerListBox.Name = "_markerListBox";
            this._markerListBox.Size = new System.Drawing.Size(51, 277);
            this._markerListBox.TabIndex = 3;
            this._markerListBox.Visible = false;
            this._markerListBox.SelectedIndexChanged += new System.EventHandler(this._markerListBox_SelectedIndexChanged);
            // 
            // _structureTabControl
            // 
            this._structureTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._structureTabControl.Controls.Add(this._structurePage);
            this._structureTabControl.Controls.Add(this._writingSystemPage);
            this._structureTabControl.Controls.Add(this._mappingPage);
            this._structureTabControl.Location = new System.Drawing.Point(3, 3);
            this._structureTabControl.Name = "_structureTabControl";
            this._structureTabControl.SelectedIndex = 0;
            this._structureTabControl.Size = new System.Drawing.Size(453, 390);
            this._structureTabControl.TabIndex = 2;
            this._structureTabControl.Leave += new System.EventHandler(this._structureTabControl_Leave);
            // 
            // MarkerSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._structureTabControl);
            this.Name = "MarkerSettingsView";
            this.Size = new System.Drawing.Size(459, 396);
            this.Load += new System.EventHandler(this.MarkerSettingsView_Load);
            this._mappingPage.ResumeLayout(false);
            this._writingSystemPage.ResumeLayout(false);
            this._writingSystemPage.PerformLayout();
            this._structurePage.ResumeLayout(false);
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
        private System.Windows.Forms.ListBox _markerListBox;
        private System.Windows.Forms.TabControl _structureTabControl;
        private System.Windows.Forms.LinkLabel _setupWsLink;
        private System.Windows.Forms.Label label1;
        private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox wsPickerUsingComboBox1;

    }
}