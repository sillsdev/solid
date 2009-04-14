namespace SolidGui
{
    partial class MarkerDetails
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
            this._listView = new GlacialComponents.Controls.GlacialList();
            this.SuspendLayout();
            // 
            // _listView
            // 
            this._listView.AllowColumnResize = true;
            this._listView.AllowMultiselect = false;
            this._listView.AlternateBackground = System.Drawing.Color.DarkGreen;
            this._listView.AlternatingColors = false;
            this._listView.AutoHeight = true;
            this._listView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._listView.BackgroundStretchToFit = true;
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
            glColumn3.Text = "Structure";
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
            this._listView.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5});
            this._listView.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listView.FullRowSelect = true;
            this._listView.GridColor = System.Drawing.Color.LightGray;
            this._listView.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this._listView.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this._listView.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this._listView.HeaderHeight = 22;
            this._listView.HeaderVisible = true;
            this._listView.HeaderWordWrap = false;
            this._listView.HotColumnTracking = false;
            this._listView.HotItemTracking = false;
            this._listView.HotTrackingColor = System.Drawing.Color.LightGray;
            this._listView.HoverEvents = false;
            this._listView.HoverTime = 1;
            this._listView.ImageList = null;
            this._listView.ItemHeight = 17;
            this._listView.ItemWordWrap = false;
            this._listView.Location = new System.Drawing.Point(0, 0);
            this._listView.Name = "_listView";
            this._listView.Selectable = true;
            this._listView.SelectedTextColor = System.Drawing.Color.White;
            this._listView.SelectionColor = System.Drawing.Color.DarkBlue;
            this._listView.ShowBorder = true;
            this._listView.ShowFocusRect = false;
            this._listView.Size = new System.Drawing.Size(421, 213);
            this._listView.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
            this._listView.SuperFlatHeaderColor = System.Drawing.Color.White;
            this._listView.TabIndex = 0;
            this._listView.Text = "glacialList1";
            this._listView.DoubleClick += new System.EventHandler(this._listView_DoubleClick);
            this._listView.SelectedIndexChanged += new GlacialComponents.Controls.GlacialList.ClickedEventHandler(this._listView_SelectedIndexChanged);
            // 
            // MarkerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._listView);
            this.Name = "MarkerDetails";
            this.Size = new System.Drawing.Size(421, 213);
            this.ResumeLayout(false);

        }

        #endregion

        private GlacialComponents.Controls.GlacialList _listView;

    }
}
