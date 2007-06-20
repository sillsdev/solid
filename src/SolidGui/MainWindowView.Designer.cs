namespace SolidGui
{
    partial class MainWindowView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._recordNavigatorView = new SolidGui.RecordNavigatorView();
            this._sfmEditorView = new SolidGui.SfmEditorView();
            this._filterChooser = new SolidGui.FilterChooserView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._filterChooser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._recordNavigatorView);
            this.splitContainer1.Panel2.Controls.Add(this._sfmEditorView);
            this.splitContainer1.Size = new System.Drawing.Size(567, 320);
            this.splitContainer1.SplitterDistance = 146;
            this.splitContainer1.TabIndex = 2;
            // 
            // _recordNavigatorView1
            // 
            this._recordNavigatorView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._recordNavigatorView.Location = new System.Drawing.Point(3, 0);
            this._recordNavigatorView.Model = null;
            this._recordNavigatorView.Name = "_recordNavigatorView";
            this._recordNavigatorView.Size = new System.Drawing.Size(411, 53);
            this._recordNavigatorView.TabIndex = 3;
            // 
            // _sfmEditorView
            // 
            this._sfmEditorView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._sfmEditorView.Location = new System.Drawing.Point(3, 59);
            this._sfmEditorView.Name = "_sfmEditorView";
            this._sfmEditorView.Size = new System.Drawing.Size(411, 258);
            this._sfmEditorView.TabIndex = 2;
            // 
            // _filterList
            // 
            this._filterChooser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterChooser.Location = new System.Drawing.Point(0, 0);
            this._filterChooser.Name = "_filterChooser";
            this._filterChooser.Size = new System.Drawing.Size(146, 320);
            this._filterChooser.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(569, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 349);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "SOLID";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private RecordNavigatorView _recordNavigatorView;
        private SfmEditorView _sfmEditorView;
        private FilterChooserView _filterChooser;
        private System.Windows.Forms.ToolStrip toolStrip1;

    }
}

