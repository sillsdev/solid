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
            this._filterListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _filterListBox
            // 
            this._filterListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterListBox.FormattingEnabled = true;
            this._filterListBox.Location = new System.Drawing.Point(0, 0);
            this._filterListBox.Name = "_filterListBox";
            this._filterListBox.Size = new System.Drawing.Size(150, 147);
            this._filterListBox.TabIndex = 1;
            this._filterListBox.SelectedIndexChanged += new System.EventHandler(this._filterList_SelectedIndexChanged);
            // 
            // FilterChooserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._filterListBox);
            this.Name = "FilterChooserView";
            this.Load += new System.EventHandler(this.FilterChooserView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _filterListBox;

    }
}
