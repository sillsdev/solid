namespace SolidGui
{
    partial class SfmEditorView
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
            this._contentsBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // _contentsBox
            // 
            this._contentsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentsBox.Location = new System.Drawing.Point(0, 0);
            this._contentsBox.Name = "_contentsBox";
            this._contentsBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._contentsBox.Size = new System.Drawing.Size(150, 150);
            this._contentsBox.TabIndex = 0;
            this._contentsBox.Text = "";
            this._contentsBox.Leave += new System.EventHandler(this._contentsBox_Leave);
            this._contentsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._contentsBox_KeyPress);
            // 
            // SfmEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this._contentsBox);
            this.Name = "SfmEditorView";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox _contentsBox;

    }
}
