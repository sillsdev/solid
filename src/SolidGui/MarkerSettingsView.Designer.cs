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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._structureTabControl = new System.Windows.Forms.TabControl();
            this._structureTabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this._markersListView = new System.Windows.Forms.ListView();
            this._structurePropertiesView = new SolidGui.StructurePropertiesView();
            this.groupBox1.SuspendLayout();
            this._structureTabControl.SuspendLayout();
            this._structureTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._structureTabControl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._markersListView);
            this.groupBox1.Location = new System.Drawing.Point(13, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 351);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Field Settings";
            // 
            // _structureTabControl
            // 
            this._structureTabControl.Controls.Add(this._structureTabPage1);
            this._structureTabControl.Controls.Add(this.tabPage2);
            this._structureTabControl.Location = new System.Drawing.Point(70, 33);
            this._structureTabControl.Name = "_structureTabControl";
            this._structureTabControl.SelectedIndex = 0;
            this._structureTabControl.Size = new System.Drawing.Size(337, 302);
            this._structureTabControl.TabIndex = 2;
            // 
            // _structureTabPage1
            // 
            this._structureTabPage1.Controls.Add(this._structurePropertiesView);
            this._structureTabPage1.Location = new System.Drawing.Point(4, 22);
            this._structureTabPage1.Name = "_structureTabPage1";
            this._structureTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this._structureTabPage1.Size = new System.Drawing.Size(329, 276);
            this._structureTabPage1.TabIndex = 0;
            this._structureTabPage1.Text = "Structure";
            this._structureTabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(329, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Marker";
            // 
            // _markersListView
            // 
            this._markersListView.Location = new System.Drawing.Point(18, 63);
            this._markersListView.Name = "_markersListView";
            this._markersListView.Size = new System.Drawing.Size(35, 272);
            this._markersListView.TabIndex = 0;
            this._markersListView.UseCompatibleStateImageBehavior = false;
            this._markersListView.View = System.Windows.Forms.View.List;
            this._markersListView.SelectedIndexChanged += new System.EventHandler(this._markersListView_SelectedIndexChanged);
            // 
            // _structurePropertiesView
            // 
            this._structurePropertiesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._structurePropertiesView.Location = new System.Drawing.Point(3, 3);
            this._structurePropertiesView.Name = "_structurePropertiesView";
            this._structurePropertiesView.Size = new System.Drawing.Size(323, 270);
            this._structurePropertiesView.TabIndex = 0;
            // 
            // MarkerSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "MarkerSettingsView";
            this.Size = new System.Drawing.Size(446, 397);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._structureTabControl.ResumeLayout(false);
            this._structureTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView _markersListView;
        private System.Windows.Forms.TabControl _structureTabControl;
        private System.Windows.Forms.TabPage _structureTabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private StructurePropertiesView _structurePropertiesView;
    }
}
