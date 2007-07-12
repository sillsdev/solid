namespace SolidGui
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
            this._markerListBox = new System.Windows.Forms.ListBox();
            this._structureTabControl = new System.Windows.Forms.TabControl();
            this._structurePage = new System.Windows.Forms.TabPage();
            this._structurePropertiesView = new SolidGui.StructurePropertiesView();
            this._writingSystemPage = new System.Windows.Forms.TabPage();
            this._mappingPage = new System.Windows.Forms.TabPage();
            this._mappingView = new SolidGui.MappingView();
            this._structureTabControl.SuspendLayout();
            this._structurePage.SuspendLayout();
            this._mappingPage.SuspendLayout();
            this.SuspendLayout();
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
            this._structureTabControl.Size = new System.Drawing.Size(343, 282);
            this._structureTabControl.TabIndex = 2;
            // 
            // _structurePage
            // 
            this._structurePage.Controls.Add(this._structurePropertiesView);
            this._structurePage.Controls.Add(this._markerListBox);
            this._structurePage.Location = new System.Drawing.Point(4, 22);
            this._structurePage.Name = "_structurePage";
            this._structurePage.Padding = new System.Windows.Forms.Padding(3);
            this._structurePage.Size = new System.Drawing.Size(335, 256);
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
            this._structurePropertiesView.Size = new System.Drawing.Size(329, 250);
            this._structurePropertiesView.TabIndex = 0;
            // 
            // _writingSystemPage
            // 
            this._writingSystemPage.Location = new System.Drawing.Point(4, 22);
            this._writingSystemPage.Name = "_writingSystemPage";
            this._writingSystemPage.Size = new System.Drawing.Size(329, 239);
            this._writingSystemPage.TabIndex = 1;
            this._writingSystemPage.Text = "Writing Systems";
            this._writingSystemPage.UseVisualStyleBackColor = true;
            // 
            // _mappingPage
            // 
            this._mappingPage.Controls.Add(this._mappingView);
            this._mappingPage.Location = new System.Drawing.Point(4, 22);
            this._mappingPage.Name = "_mappingPage";
            this._mappingPage.Size = new System.Drawing.Size(329, 239);
            this._mappingPage.TabIndex = 2;
            this._mappingPage.Text = "Mapping";
            this._mappingPage.UseVisualStyleBackColor = true;
            // 
            // _mappingView
            // 
            this._mappingView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mappingView.Location = new System.Drawing.Point(0, 0);
            this._mappingView.Model = null;
            this._mappingView.Name = "_mappingView";
            this._mappingView.Size = new System.Drawing.Size(329, 239);
            this._mappingView.TabIndex = 0;
            // 
            // MarkerSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._structureTabControl);
            this.Name = "MarkerSettingsView";
            this.Size = new System.Drawing.Size(346, 294);
            this.Load += new System.EventHandler(this.MarkerSettingsView_Load);
            this._structureTabControl.ResumeLayout(false);
            this._structurePage.ResumeLayout(false);
            this._mappingPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _structureTabControl;
        private System.Windows.Forms.TabPage _structurePage;
        private StructurePropertiesView _structurePropertiesView;
        private System.Windows.Forms.TabPage _writingSystemPage;
        private System.Windows.Forms.TabPage _mappingPage;
        private System.Windows.Forms.ListBox _markerListBox;
        private MappingView _mappingView;
    }
}
