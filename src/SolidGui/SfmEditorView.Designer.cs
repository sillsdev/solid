// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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
            this.ContentsBox = new System.Windows.Forms.RichTextBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.superToolTip1 = new Palaso.UI.WindowsForms.SuperToolTip.SuperToolTip(this.components);
            this.SuspendLayout();
            // 
            // ContentsBox
            // 
            this.ContentsBox.AcceptsTab = true;
            this.ContentsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentsBox.EnableAutoDragDrop = true;
            this.ContentsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContentsBox.HideSelection = false;
            this.ContentsBox.Location = new System.Drawing.Point(0, 0);
            this.ContentsBox.Margin = new System.Windows.Forms.Padding(0, 6, 4, 6);
            this.ContentsBox.Name = "ContentsBox";
            this.ContentsBox.ShowSelectionMargin = true;
            this.ContentsBox.Size = new System.Drawing.Size(225, 277);
            this.ContentsBox.TabIndex = 0;
            this.ContentsBox.Text = "";
            this.ContentsBox.WordWrap = false;
            this.ContentsBox.TextChanged += new System.EventHandler(this._contentsBox_TextChanged);
            this.ContentsBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this._contentsBox_KeyDown);
            this.ContentsBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this._contentsBox_MouseDown);
            this.ContentsBox.MouseLeave += new System.EventHandler(this._contentsBox_MouseLeave);
            this.ContentsBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this._contentsBox_MouseMove);
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
            this.Controls.Add(this.ContentsBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "SfmEditorView";
            this.Size = new System.Drawing.Size(225, 277);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox ContentsBox;
        private System.Windows.Forms.Timer _timer;
        private Palaso.UI.WindowsForms.SuperToolTip.SuperToolTip superToolTip1;

    }
}
