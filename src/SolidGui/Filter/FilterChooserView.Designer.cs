namespace SolidGui.Filter
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
            this._warningFilterListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _warningFilterListBox
            // 
            this._warningFilterListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._warningFilterListBox.FormattingEnabled = true;
            this._warningFilterListBox.Location = new System.Drawing.Point(0, 0);
            this._warningFilterListBox.Name = "_warningFilterListBox";
            this._warningFilterListBox.Size = new System.Drawing.Size(150, 147);
            this._warningFilterListBox.TabIndex = 1;
            this._warningFilterListBox.SelectedIndexChanged += new System.EventHandler(this._filterList_SelectedIndexChanged);
            // 
            // FilterChooserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._warningFilterListBox);
            this.Name = "FilterChooserView";
            this.Load += new System.EventHandler(this.FilterChooserView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _warningFilterListBox;

    }
}
