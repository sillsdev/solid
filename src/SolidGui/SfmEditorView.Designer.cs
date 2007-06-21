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
            this._contentsBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _contentsBox
            // 
            this._contentsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._contentsBox.Location = new System.Drawing.Point(0, 0);
            this._contentsBox.Multiline = true;
            this._contentsBox.Name = "_contentsBox";
            this._contentsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._contentsBox.Size = new System.Drawing.Size(150, 150);
            this._contentsBox.TabIndex = 0;
            this._contentsBox.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // SfmEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this._contentsBox);
            this.Name = "SfmEditorView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _contentsBox;
    }
}
