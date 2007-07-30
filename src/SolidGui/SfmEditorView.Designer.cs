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
            this.components = new System.ComponentModel.Container();
            this._contentsBox = new System.Windows.Forms.RichTextBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.superToolTip1 = new Elsehemy.SuperToolTip(this.components);
            this.SuspendLayout();
            // 
            // _contentsBox
            // 
            this._contentsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._contentsBox.HideSelection = false;
            this._contentsBox.Location = new System.Drawing.Point(0, 0);
            this._contentsBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this._contentsBox.Name = "_contentsBox";
            this._contentsBox.Size = new System.Drawing.Size(225, 277);
            this._contentsBox.TabIndex = 0;
            this._contentsBox.Text = "";
            this._contentsBox.WordWrap = false;
            this._contentsBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this._contentsBox_MouseMove);
            this._contentsBox.MouseLeave += new System.EventHandler(this._contentsBox_MouseLeave);
            this._contentsBox.TextChanged += new System.EventHandler(this._contentsBox_TextChanged);
            this._contentsBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this._contentsBox_MouseDown);
            // 
            // superToolTip1
            // 
            this.superToolTip1.FadingInterval = 10;
            // 
            // SfmEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this._contentsBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "SfmEditorView";
            this.Size = new System.Drawing.Size(225, 277);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox _contentsBox;
        private System.Windows.Forms.Timer _timer;
        private Elsehemy.SuperToolTip superToolTip1;

    }
}
