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
            this._structureTabPage1 = new System.Windows.Forms.TabPage();
            this._structurePropertiesView = new SolidGui.StructurePropertiesView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._mappingView = new SolidGui.MappingView();
            this.label1 = new System.Windows.Forms.Label();
            this._structureTabControl.SuspendLayout();
            this._structureTabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _markerListBox
            // 
            this._markerListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._markerListBox.FormattingEnabled = true;
            this._markerListBox.Location = new System.Drawing.Point(255, -31);
            this._markerListBox.Name = "_markerListBox";
            this._markerListBox.Size = new System.Drawing.Size(51, 264);
            this._markerListBox.TabIndex = 3;
            this._markerListBox.Visible = false;
            this._markerListBox.SelectedIndexChanged += new System.EventHandler(this._markerListBox_SelectedIndexChanged);
            // 
            // _structureTabControl
            // 
            this._structureTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._structureTabControl.Controls.Add(this._structureTabPage1);
            this._structureTabControl.Controls.Add(this.tabPage1);
            this._structureTabControl.Controls.Add(this.tabPage2);
            this._structureTabControl.Location = new System.Drawing.Point(9, 20);
            this._structureTabControl.Name = "_structureTabControl";
            this._structureTabControl.SelectedIndex = 0;
            this._structureTabControl.Size = new System.Drawing.Size(337, 265);
            this._structureTabControl.TabIndex = 2;
            // 
            // _structureTabPage1
            // 
            this._structureTabPage1.Controls.Add(this._structurePropertiesView);
            this._structureTabPage1.Controls.Add(this._markerListBox);
            this._structureTabPage1.Location = new System.Drawing.Point(4, 22);
            this._structureTabPage1.Name = "_structureTabPage1";
            this._structureTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this._structureTabPage1.Size = new System.Drawing.Size(329, 239);
            this._structureTabPage1.TabIndex = 0;
            this._structureTabPage1.Text = "Structure";
            this._structureTabPage1.UseVisualStyleBackColor = true;
            // 
            // _structurePropertiesView
            // 
            this._structurePropertiesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._structurePropertiesView.Location = new System.Drawing.Point(3, 3);
            this._structurePropertiesView.Model = null;
            this._structurePropertiesView.Name = "_structurePropertiesView";
            this._structurePropertiesView.Size = new System.Drawing.Size(323, 233);
            this._structurePropertiesView.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(346, 239);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Writing Systems";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._mappingView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(346, 239);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Mapping";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _mappingView
            // 
            this._mappingView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mappingView.Location = new System.Drawing.Point(0, 0);
            this._mappingView.Model = null;
            this._mappingView.Name = "_mappingView";
            this._mappingView.Size = new System.Drawing.Size(346, 239);
            this._mappingView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Marker";
            // 
            // MarkerSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._structureTabControl);
            this.Controls.Add(this.label1);
            this.Name = "MarkerSettingsView";
            this.Size = new System.Drawing.Size(346, 294);
            this.Load += new System.EventHandler(this.MarkerSettingsView_Load);
            this._structureTabControl.ResumeLayout(false);
            this._structureTabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl _structureTabControl;
        private System.Windows.Forms.TabPage _structureTabPage1;
        private System.Windows.Forms.Label label1;
        private StructurePropertiesView _structurePropertiesView;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox _markerListBox;
        private MappingView _mappingView;
    }
}
