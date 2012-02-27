namespace SolidGui.Setup
{
	partial class WritingSystemsConfigDialog
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
			this._writingSystemsConfigView = new SolidGui.Setup.WritingSystemsConfigView();
			this.SuspendLayout();
			// 
			// _writingSystemsConfigView
			// 
			this._writingSystemsConfigView.FromMatching = "";
			this._writingSystemsConfigView.Location = new System.Drawing.Point(0, -1);
			this._writingSystemsConfigView.Name = "_writingSystemsConfigView";
			this._writingSystemsConfigView.Size = new System.Drawing.Size(262, 276);
			this._writingSystemsConfigView.TabIndex = 0;
			// 
			// WritingSystemsConfigDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(261, 288);
			this.Controls.Add(this._writingSystemsConfigView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "WritingSystemsConfigDialog";
			this.Text = "Writing Systems";
			this.ResumeLayout(false);

		}

		#endregion

		private WritingSystemsConfigView _writingSystemsConfigView;

	}
}