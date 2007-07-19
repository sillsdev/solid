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
            this._listView = new EXControls.EXListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this._editMarkerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _listView
            // 
            this._listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6});
            this._listView.ControlPadding = 4;
            this._listView.FullRowSelect = true;
            this._listView.Location = new System.Drawing.Point(3, 3);
            this._listView.MultiSelect = false;
            this._listView.Name = "_listView";
            this._listView.OwnerDraw = true;
            this._listView.Size = new System.Drawing.Size(421, 162);
            this._listView.TabIndex = 0;
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.View = System.Windows.Forms.View.Details;
            this._listView.DoubleClick += new System.EventHandler(this._listView_DoubleClick);
            this._listView.SelectedIndexChanged += new System.EventHandler(this._listView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Marker";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Count";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Structure";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Writing System";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "FLEx";
            // 
            // _editMarkerButton
            // 
            this._editMarkerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._editMarkerButton.Location = new System.Drawing.Point(343, 165);
            this._editMarkerButton.Name = "_editMarkerButton";
            this._editMarkerButton.Size = new System.Drawing.Size(75, 23);
            this._editMarkerButton.TabIndex = 1;
            this._editMarkerButton.Text = "&Edit Settings...";
            this._editMarkerButton.UseVisualStyleBackColor = true;
            this._editMarkerButton.Click += new System.EventHandler(this.OnEditSettingsClick);
            // 
            // MarkerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._editMarkerButton);
            this.Controls.Add(this._listView);
            this.Name = "MarkerDetails";
            this.Size = new System.Drawing.Size(421, 188);
            this.ResumeLayout(false);

        }

        #endregion

        private EXControls.EXListView _listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button _editMarkerButton;
    }
}
