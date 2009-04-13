namespace SolidGui
{
    partial class QuickFixForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickFixForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._goLink = new System.Windows.Forms.LinkLabel();
            this._cancelButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Markers to move up out of senses, to the top of the entry:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 13);
            this.label2.MaximumSize = new System.Drawing.Size(300, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 51);
            this.label2.TabIndex = 0;
            this.label2.Text = "Warning: this is very dangerous.  Backup before this, and check the results. ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(104, 109);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(288, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "bw, hm, lt, lc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(436, 78);
            this.label3.TabIndex = 0;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // _goLink
            // 
            this._goLink.AutoSize = true;
            this._goLink.Location = new System.Drawing.Point(25, 249);
            this._goLink.Name = "_goLink";
            this._goLink.Size = new System.Drawing.Size(212, 13);
            this._goLink.TabIndex = 2;
            this._goLink.TabStop = true;
            this._goLink.Text = "I know what I\'m doing and have backed up";
            this._goLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._goLink_LinkClicked);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(386, 260);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 3;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SolidGui.Properties.Resources.WarningHS;
            this.pictureBox1.Location = new System.Drawing.Point(28, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // QuickFixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(487, 295);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._goLink);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickFixForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Quick Fixes";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel _goLink;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}