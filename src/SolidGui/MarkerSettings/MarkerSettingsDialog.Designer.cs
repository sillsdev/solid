namespace SolidGui.MarkerSettings
{
    partial class MarkerSettingsDialog
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
            this._outerTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._cbUnicode = new System.Windows.Forms.CheckBox();
            this._tabControl = new System.Windows.Forms.TabControl();
            this.structureTabPage = new System.Windows.Forms.TabPage();
            this._structurePropertiesView = new SolidGui.StructurePropertiesView();
            this.mappingTabPage = new System.Windows.Forms.TabPage();
            this._mappingView = new SolidGui.Mapping.MappingView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._closeButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this._wsPalasoPicker = new SIL.Windows.Forms.WritingSystems.WSPickerUsingComboBox();
            this._setupWsLink = new System.Windows.Forms.LinkLabel();
            this._markersListBox = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._outerTableLayoutPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.structureTabPage.SuspendLayout();
            this.mappingTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _outerTableLayoutPanel
            // 
            this._outerTableLayoutPanel.ColumnCount = 1;
            this._outerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._outerTableLayoutPanel.Controls.Add(this._cbUnicode, 0, 1);
            this._outerTableLayoutPanel.Controls.Add(this._tabControl, 0, 0);
            this._outerTableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this._outerTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._outerTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._outerTableLayoutPanel.Name = "_outerTableLayoutPanel";
            this._outerTableLayoutPanel.RowCount = 3;
            this._outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._outerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._outerTableLayoutPanel.Size = new System.Drawing.Size(449, 350);
            this._outerTableLayoutPanel.TabIndex = 0;
            // 
            // _cbUnicode
            // 
            this._cbUnicode.Dock = System.Windows.Forms.DockStyle.Top;
            this._cbUnicode.Location = new System.Drawing.Point(62, 293);
            this._cbUnicode.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this._cbUnicode.Name = "_cbUnicode";
            this._cbUnicode.Size = new System.Drawing.Size(384, 20);
            this._cbUnicode.TabIndex = 0;
            this._cbUnicode.Text = "Data for this marker is already encoded as UTF-8 Unicode";
            this._cbUnicode.UseVisualStyleBackColor = true;
            this._cbUnicode.CheckedChanged += new System.EventHandler(this._uiEditMade);
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this.structureTabPage);
            this._tabControl.Controls.Add(this.mappingTabPage);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(58, 3);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(388, 284);
            this._tabControl.TabIndex = 0;
            this._tabControl.Leave += new System.EventHandler(this._structureTabControl_Leave);
            // 
            // structureTabPage
            // 
            this.structureTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.structureTabPage.Controls.Add(this._structurePropertiesView);
            this.structureTabPage.Location = new System.Drawing.Point(4, 22);
            this.structureTabPage.Name = "structureTabPage";
            this.structureTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.structureTabPage.Size = new System.Drawing.Size(380, 258);
            this.structureTabPage.TabIndex = 0;
            this.structureTabPage.Text = "Settings";
            // 
            // _structurePropertiesView
            // 
            this._structurePropertiesView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._structurePropertiesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._structurePropertiesView.Location = new System.Drawing.Point(3, 3);
            this._structurePropertiesView.MinimumSize = new System.Drawing.Size(377, 239);
            this._structurePropertiesView.Model = null;
            this._structurePropertiesView.Name = "_structurePropertiesView";
            this._structurePropertiesView.Size = new System.Drawing.Size(377, 252);
            this._structurePropertiesView.TabIndex = 0;
            // 
            // mappingTabPage
            // 
            this.mappingTabPage.Controls.Add(this._mappingView);
            this.mappingTabPage.Location = new System.Drawing.Point(4, 22);
            this.mappingTabPage.Name = "mappingTabPage";
            this.mappingTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mappingTabPage.Size = new System.Drawing.Size(380, 258);
            this.mappingTabPage.TabIndex = 1;
            this.mappingTabPage.Text = "Export";
            this.mappingTabPage.UseVisualStyleBackColor = true;
            // 
            // _mappingView
            // 
            this._mappingView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mappingView.Location = new System.Drawing.Point(3, 3);
            this._mappingView.Name = "_mappingView";
            this._mappingView.Size = new System.Drawing.Size(374, 252);
            this._mappingView.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.Controls.Add(this._closeButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(55, 320);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(394, 30);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._closeButton.Location = new System.Drawing.Point(316, 3);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 3;
            this._closeButton.Text = "&Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this._wsPalasoPicker);
            this.flowLayoutPanel1.Controls.Add(this._setupWsLink);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(312, 30);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 6, 1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Writing System:";
            // 
            // _wsPalasoPicker
            // 
            this._wsPalasoPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._wsPalasoPicker.BackColor = System.Drawing.SystemColors.Window;
            this._wsPalasoPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._wsPalasoPicker.FormattingEnabled = true;
            this._wsPalasoPicker.Location = new System.Drawing.Point(88, 3);
            this._wsPalasoPicker.Name = "_wsPalasoPicker";
            this._wsPalasoPicker.Size = new System.Drawing.Size(121, 21);
            this._wsPalasoPicker.TabIndex = 1;
            this._wsPalasoPicker.SelectedIndexChanged += new System.EventHandler(this._uiEditMade);
            // 
            // _setupWsLink
            // 
            this._setupWsLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._setupWsLink.AutoSize = true;
            this._setupWsLink.Location = new System.Drawing.Point(215, 6);
            this._setupWsLink.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this._setupWsLink.Name = "_setupWsLink";
            this._setupWsLink.Size = new System.Drawing.Size(47, 13);
            this._setupWsLink.TabIndex = 2;
            this._setupWsLink.TabStop = true;
            this._setupWsLink.Text = "Set up...";
            this._setupWsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._setupWsLink_LinkClicked);
            // 
            // _markersListBox
            // 
            this._markersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._markersListBox.FormattingEnabled = true;
            this._markersListBox.ItemHeight = 25;
            this._markersListBox.Location = new System.Drawing.Point(2, 0);
            this._markersListBox.Margin = new System.Windows.Forms.Padding(6);
            this._markersListBox.Name = "_markersListBox";
            this._markersListBox.Size = new System.Drawing.Size(49, 344);
            this._markersListBox.TabIndex = 4;
            this._markersListBox.SelectedIndexChanged += new System.EventHandler(this._markersListBox_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._markersListBox);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._outerTableLayoutPanel);
            this.splitContainer1.Size = new System.Drawing.Size(449, 344);
            this.splitContainer1.SplitterDistance = 75;
            this.splitContainer1.TabIndex = 1;
            // 
            // MarkerSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(449, 350);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(433, 355);
            this.Name = "MarkerSettingsDialog";
            this.ShowIcon = false;
            this.Text = "Marker Settings";
            this.Deactivate += new System.EventHandler(this.MarkerSettingsDialog_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MarkerSettingsDialog_FormClosing);
            this._outerTableLayoutPanel.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this.structureTabPage.ResumeLayout(false);
            this.mappingTabPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _outerTableLayoutPanel;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage structureTabPage;
        private System.Windows.Forms.TabPage mappingTabPage;
        private System.Windows.Forms.ListBox _markersListBox;
        private Mapping.MappingView _mappingView;
        private System.Windows.Forms.Button _closeButton;
        private StructurePropertiesView _structurePropertiesView;
        private System.Windows.Forms.CheckBox _cbUnicode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private SIL.Windows.Forms.WritingSystems.WSPickerUsingComboBox _wsPalasoPicker;
        private System.Windows.Forms.LinkLabel _setupWsLink;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
    }
}