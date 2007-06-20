namespace SolidGui
{
    partial class FilterChooserView
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("j");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("k");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("p");
            this._listControl = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // _filterList
            // 
            this._listControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listControl.HideSelection = false;
            this._listControl.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this._listControl.Location = new System.Drawing.Point(0, 0);
            this._listControl.MultiSelect = false;
            this._listControl.Name = "_listControl";
            this._listControl.Size = new System.Drawing.Size(150, 150);
            this._listControl.TabIndex = 0;
            this._listControl.UseCompatibleStateImageBehavior = false;
            this._listControl.View = System.Windows.Forms.View.List;
            this._listControl.SelectedIndexChanged += new System.EventHandler(this._filterList_SelectedIndexChanged);
            // 
            // FilterListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._listControl);
            this.Name = "FilterListView";
            this.Load += new System.EventHandler(this.FilterChooserView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _listControl;
    }
}
