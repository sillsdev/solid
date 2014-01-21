// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

namespace SolidGui.MarkerSettings
{
    partial class MarkerSettingsListView
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
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
            this._markerListView = new GlacialComponents.Controls.GlacialList();
            this.SuspendLayout();
            // 
            // _markerListView
            // 
            this._markerListView.AllowColumnResize = true;
            this._markerListView.AllowMultiselect = false;
            this._markerListView.AlternateBackground = System.Drawing.Color.DarkGreen;
            this._markerListView.AlternatingColors = false;
            this._markerListView.AutoHeight = true;
            this._markerListView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._markerListView.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn1.CheckBoxes = false;
            glColumn1.ComparisonFunction = null;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "marker";
            glColumn1.NumericSort = false;
            glColumn1.Text = "Marker";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 60;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ComparisonFunction = null;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "count";
            glColumn2.NumericSort = false;
            glColumn2.Text = "Count";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 60;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ComparisonFunction = null;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "structure";
            glColumn3.NumericSort = false;
            glColumn3.Text = "Under";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 80;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ComparisonFunction = null;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "writingSystem";
            glColumn4.NumericSort = false;
            glColumn4.Text = "Writing System";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn4.Width = 90;
            glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn5.CheckBoxes = false;
            glColumn5.ComparisonFunction = null;
            glColumn5.ImageIndex = -1;
            glColumn5.Name = "liftConcept";
            glColumn5.NumericSort = false;
            glColumn5.Text = "LIFT";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn5.Width = 100;
            this._markerListView.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5});
            this._markerListView.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this._markerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._markerListView.FullRowSelect = true;
            this._markerListView.GridColor = System.Drawing.Color.LightGray;
            this._markerListView.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this._markerListView.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this._markerListView.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this._markerListView.HeaderHeight = 22;
            this._markerListView.HeaderVisible = true;
            this._markerListView.HeaderWordWrap = false;
            this._markerListView.HotColumnTracking = false;
            this._markerListView.HotItemTracking = false;
            this._markerListView.HotTrackingColor = System.Drawing.Color.LightGray;
            this._markerListView.HoverEvents = false;
            this._markerListView.HoverTime = 1;
            this._markerListView.ImageList = null;
            this._markerListView.ItemHeight = 17;
            this._markerListView.ItemWordWrap = false;
            this._markerListView.Location = new System.Drawing.Point(0, 0);
            this._markerListView.Name = "_markerListView";
            this._markerListView.Selectable = true;
            this._markerListView.SelectedTextColor = System.Drawing.Color.White;
            this._markerListView.SelectionColor = System.Drawing.Color.DarkBlue;
            this._markerListView.ShowBorder = true;
            this._markerListView.ShowFocusRect = false;
            this._markerListView.Size = new System.Drawing.Size(421, 213);
            this._markerListView.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
            this._markerListView.SuperFlatHeaderColor = System.Drawing.Color.White;
            this._markerListView.TabIndex = 0;
            this._markerListView.Text = "glacialList1";
            this._markerListView.SelectedIndexChanged += new GlacialComponents.Controls.GlacialList.ClickedEventHandler(this._markerListView_SelectedIndexChanged);
            this._markerListView.DoubleClick += new System.EventHandler(this._markerListView_DoubleClick);
            // 
            // MarkerSettingsListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._markerListView);
            this.Name = "MarkerSettingsListView";
            this.Size = new System.Drawing.Size(421, 213);
            this.ResumeLayout(false);

        }

        #endregion

        private GlacialComponents.Controls.GlacialList _markerListView;

    }
}