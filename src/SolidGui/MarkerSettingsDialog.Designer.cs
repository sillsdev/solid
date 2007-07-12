namespace SolidGui
{
    partial class MarkerSettingsDialog
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
            this._markerSettingsView = new SolidGui.MarkerSettingsView();
            this._closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _markerSettingsView
            // 
            this._markerSettingsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._markerSettingsView.Location = new System.Drawing.Point(6, 8);
            this._markerSettingsView.Model = null;
            this._markerSettingsView.Name = "_markerSettingsView";
            this._markerSettingsView.Size = new System.Drawing.Size(428, 309);
            this._markerSettingsView.TabIndex = 0;
            this._markerSettingsView.Load += new System.EventHandler(this.OnMarkerSettings_Load);
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.Location = new System.Drawing.Point(359, 315);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "&Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // MarkerSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 345);
            this.ControlBox = false;
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._markerSettingsView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MarkerSettingsDialog";
            this.Text = "MarkerSettingsDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private MarkerSettingsView _markerSettingsView;
        private System.Windows.Forms.Button _closeButton;
    }
}