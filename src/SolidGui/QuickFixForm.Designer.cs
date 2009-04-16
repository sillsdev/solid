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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._moveUpGoLink = new System.Windows.Forms.LinkLabel();
            this._moveUpMarkers = new System.Windows.Forms.TextBox();
            this._goLink = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._executeRemoveEmpty = new System.Windows.Forms.LinkLabel();
            this._removeEmptyMarkers = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(399, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 100);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(403, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(12, 9);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(463, 305);
            this.tabControl2.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pictureBox2);
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Controls.Add(this._moveUpGoLink);
            this.tabPage3.Controls.Add(this._moveUpMarkers);
            this.tabPage3.Controls.Add(this._goLink);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(455, 279);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Move Up";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SolidGui.Properties.Resources.WarningHS;
            this.pictureBox2.Location = new System.Drawing.Point(6, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(67, 65);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SolidGui.Properties.Resources.WarningHS;
            this.pictureBox1.Location = new System.Drawing.Point(6, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // _moveUpGoLink
            // 
            this._moveUpGoLink.AutoSize = true;
            this._moveUpGoLink.Location = new System.Drawing.Point(3, 240);
            this._moveUpGoLink.Name = "_moveUpGoLink";
            this._moveUpGoLink.Size = new System.Drawing.Size(212, 13);
            this._moveUpGoLink.TabIndex = 13;
            this._moveUpGoLink.TabStop = true;
            this._moveUpGoLink.Text = "I know what I\'m doing and have backed up";
            this._moveUpGoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteMoveUp);
            // 
            // _moveUpMarkers
            // 
            this._moveUpMarkers.Location = new System.Drawing.Point(82, 100);
            this._moveUpMarkers.Name = "_moveUpMarkers";
            this._moveUpMarkers.Size = new System.Drawing.Size(288, 20);
            this._moveUpMarkers.TabIndex = 11;
            this._moveUpMarkers.Text = "bw, hm, lc";
            // 
            // _goLink
            // 
            this._goLink.AutoSize = true;
            this._goLink.Location = new System.Drawing.Point(3, 240);
            this._goLink.Name = "_goLink";
            this._goLink.Size = new System.Drawing.Size(212, 13);
            this._goLink.TabIndex = 14;
            this._goLink.TabStop = true;
            this._goLink.Text = "I know what I\'m doing and have backed up";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(79, 4);
            this.label6.MaximumSize = new System.Drawing.Size(300, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(261, 51);
            this.label6.TabIndex = 10;
            this.label6.Text = "Warning: this is very dangerous.  Backup before this, and check the results. ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(436, 78);
            this.label5.TabIndex = 5;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 4);
            this.label2.MaximumSize = new System.Drawing.Size(300, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 51);
            this.label2.TabIndex = 7;
            this.label2.Text = "Warning: this is very dangerous.  Backup before this, and check the results. ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(277, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Markers to move up out of senses, to the top of the entry:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(436, 78);
            this.label3.TabIndex = 8;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Markers to move up out of senses, to the top of the entry:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this._executeRemoveEmpty);
            this.tabPage4.Controls.Add(this._removeEmptyMarkers);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(455, 279);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Remove empty";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // _executeRemoveEmpty
            // 
            this._executeRemoveEmpty.AutoSize = true;
            this._executeRemoveEmpty.Location = new System.Drawing.Point(15, 234);
            this._executeRemoveEmpty.Name = "_executeRemoveEmpty";
            this._executeRemoveEmpty.Size = new System.Drawing.Size(212, 13);
            this._executeRemoveEmpty.TabIndex = 16;
            this._executeRemoveEmpty.TabStop = true;
            this._executeRemoveEmpty.Text = "I know what I\'m doing and have backed up";
            this._executeRemoveEmpty.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteRemoveEmpty);
            // 
            // _removeEmptyMarkers
            // 
            this._removeEmptyMarkers.Location = new System.Drawing.Point(47, 39);
            this._removeEmptyMarkers.Name = "_removeEmptyMarkers";
            this._removeEmptyMarkers.Size = new System.Drawing.Size(288, 20);
            this._removeEmptyMarkers.TabIndex = 15;
            this._removeEmptyMarkers.Text = "bw, hm, lt, lc, nt, so, co, ge, de, gn, gr, dr, ps";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(206, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Field to remove if they only have a marker:";
            // 
            // QuickFixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 355);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickFixForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Quick Fixes";
            this.tabControl1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel _moveUpGoLink;
        private System.Windows.Forms.TextBox _moveUpMarkers;
        private System.Windows.Forms.LinkLabel _goLink;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _executeRemoveEmpty;
        private System.Windows.Forms.TextBox _removeEmptyMarkers;
        private System.Windows.Forms.Label label7;
    }
}