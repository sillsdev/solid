namespace SolidGui.Export
{
    partial class ExportLogDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportLogDialog));
            this._logBox = new Palaso.Progress.LogBox.LogBox();
            this._close = new System.Windows.Forms.Button();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this._updateDisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _logBox
            // 
            this._logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._logBox.BackColor = System.Drawing.Color.Transparent;
            this._logBox.CancelRequested = false;
            this._logBox.ErrorEncountered = false;
            this._logBox.GetDiagnosticsMethod = null;
            this._logBox.Location = new System.Drawing.Point(12, 12);
            this._logBox.Name = "_logBox";
            this._logBox.Size = new System.Drawing.Size(487, 200);
            this._logBox.TabIndex = 0;
            // 
            // _close
            // 
            this._close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._close.Location = new System.Drawing.Point(424, 218);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(75, 23);
            this._close.TabIndex = 1;
            this._close.Text = "&Close";
            this._close.UseVisualStyleBackColor = true;
            this._close.Click += new System.EventHandler(this._close_Click);
            // 
            // _updateDisplayTimer
            // 
            this._updateDisplayTimer.Enabled = true;
            this._updateDisplayTimer.Interval = 1000;
            this._updateDisplayTimer.Tick += new System.EventHandler(this._updateDisplayTimer_Tick);
            // 
            // ExportLogDialog
            // 
            this.AcceptButton = this._close;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._close;
            this.ClientSize = new System.Drawing.Size(525, 255);
            this.ControlBox = false;
            this.Controls.Add(this._close);
            this.Controls.Add(this._logBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ExportLogDialog";
            this.Text = "Export Log";
            this.Load += new System.EventHandler(this.ExportLogDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Palaso.Progress.LogBox.LogBox _logBox;
        private System.Windows.Forms.Button _close;
        public System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.Timer _updateDisplayTimer;
    }
}