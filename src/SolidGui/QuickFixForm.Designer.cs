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
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this._moveUpGoLink = new System.Windows.Forms.LinkLabel();
            this._moveUpRoots = new System.Windows.Forms.TextBox();
            this._moveUpMarkers = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._executeRemoveEmpty = new System.Windows.Forms.LinkLabel();
            this._removeEmptyMarkers = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._exectueFLExFixes = new System.Windows.Forms.LinkLabel();
            this._createReferredToItems = new System.Windows.Forms.CheckBox();
            this._makeInferedRealBox = new System.Windows.Forms.CheckBox();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(396, 423);
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
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Location = new System.Drawing.Point(12, 9);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(463, 408);
            this.tabControl2.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Controls.Add(this.linkLabel3);
            this.tabPage3.Controls.Add(this.linkLabel2);
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this._moveUpGoLink);
            this.tabPage3.Controls.Add(this._moveUpRoots);
            this.tabPage3.Controls.Add(this._moveUpMarkers);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(455, 382);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Move Up";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SolidGui.Properties.Resources.WarningHS;
            this.pictureBox1.Location = new System.Drawing.Point(24, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.Location = new System.Drawing.Point(47, 227);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(149, 17);
            this.linkLabel3.TabIndex = 13;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "(bw, hm, lc) to under (lx)";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnPremadeLabelClick);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(47, 263);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(110, 17);
            this.linkLabel2.TabIndex = 13;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "(sd) to under (ps)";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnPremadeLabelClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(47, 194);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(160, 17);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "(lt, ph) to under (lx, se, va)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnPremadeLabelClick);
            // 
            // _moveUpGoLink
            // 
            this._moveUpGoLink.AutoSize = true;
            this._moveUpGoLink.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._moveUpGoLink.Location = new System.Drawing.Point(21, 336);
            this._moveUpGoLink.Name = "_moveUpGoLink";
            this._moveUpGoLink.Size = new System.Drawing.Size(257, 17);
            this._moveUpGoLink.TabIndex = 13;
            this._moveUpGoLink.TabStop = true;
            this._moveUpGoLink.Text = "I know what I\'m doing and have backed up";
            this._moveUpGoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteMoveUp);
            // 
            // _moveUpRoots
            // 
            this._moveUpRoots.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._moveUpRoots.Location = new System.Drawing.Point(201, 128);
            this._moveUpRoots.Name = "_moveUpRoots";
            this._moveUpRoots.Size = new System.Drawing.Size(120, 25);
            this._moveUpRoots.TabIndex = 11;
            this._moveUpRoots.Text = "lx, se";
            // 
            // _moveUpMarkers
            // 
            this._moveUpMarkers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._moveUpMarkers.Location = new System.Drawing.Point(201, 105);
            this._moveUpMarkers.Name = "_moveUpMarkers";
            this._moveUpMarkers.Size = new System.Drawing.Size(120, 25);
            this._moveUpMarkers.TabIndex = 11;
            this._moveUpMarkers.Text = "bw, hm, lc";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(106, 27);
            this.label6.MaximumSize = new System.Drawing.Size(300, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(261, 51);
            this.label6.TabIndex = 10;
            this.label6.Text = "Warning: this is very dangerous.  Backup before this, and check the results. ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(258, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Click one of these to fill in the fields above:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "To underneath these roots:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Move these markers:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this._executeRemoveEmpty);
            this.tabPage4.Controls.Add(this._removeEmptyMarkers);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(455, 382);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Remove empty";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // _executeRemoveEmpty
            // 
            this._executeRemoveEmpty.AutoSize = true;
            this._executeRemoveEmpty.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._executeRemoveEmpty.Location = new System.Drawing.Point(15, 234);
            this._executeRemoveEmpty.Name = "_executeRemoveEmpty";
            this._executeRemoveEmpty.Size = new System.Drawing.Size(257, 17);
            this._executeRemoveEmpty.TabIndex = 16;
            this._executeRemoveEmpty.TabStop = true;
            this._executeRemoveEmpty.Text = "I know what I\'m doing and have backed up";
            this._executeRemoveEmpty.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteRemoveEmpty);
            // 
            // _removeEmptyMarkers
            // 
            this._removeEmptyMarkers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._removeEmptyMarkers.Location = new System.Drawing.Point(49, 53);
            this._removeEmptyMarkers.Name = "_removeEmptyMarkers";
            this._removeEmptyMarkers.Size = new System.Drawing.Size(288, 25);
            this._removeEmptyMarkers.TabIndex = 15;
            this._removeEmptyMarkers.Text = "bw, hm, lt, lc, nt, so, co, ge, de, gn, gr, dr, ps";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Fields to remove if they only have a marker:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this._exectueFLExFixes);
            this.tabPage1.Controls.Add(this._createReferredToItems);
            this.tabPage1.Controls.Add(this._makeInferedRealBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(455, 382);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "FLEx Import Fixes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(72, 54);
            this.label5.MaximumSize = new System.Drawing.Size(250, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(250, 26);
            this.label5.TabIndex = 18;
            this.label5.Text = "Increases the chance that FLEx has the same idea about the structure as your SOLI" +
                "D rules.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 130);
            this.label2.MaximumSize = new System.Drawing.Size(250, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 195);
            this.label2.TabIndex = 18;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // _exectueFLExFixes
            // 
            this._exectueFLExFixes.AutoSize = true;
            this._exectueFLExFixes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._exectueFLExFixes.Location = new System.Drawing.Point(19, 340);
            this._exectueFLExFixes.Name = "_exectueFLExFixes";
            this._exectueFLExFixes.Size = new System.Drawing.Size(257, 17);
            this._exectueFLExFixes.TabIndex = 17;
            this._exectueFLExFixes.TabStop = true;
            this._exectueFLExFixes.Text = "I know what I\'m doing and have backed up";
            this._exectueFLExFixes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteFLExFixes_LinkClicked);
            // 
            // _createReferredToItems
            // 
            this._createReferredToItems.AutoSize = true;
            this._createReferredToItems.Location = new System.Drawing.Point(34, 110);
            this._createReferredToItems.Name = "_createReferredToItems";
            this._createReferredToItems.Size = new System.Drawing.Size(296, 17);
            this._createReferredToItems.TabIndex = 0;
            this._createReferredToItems.Text = "Add entries for all referred to items (e.g. \\cf, \\va, \\sy, \\an)";
            this._createReferredToItems.UseVisualStyleBackColor = true;
            // 
            // _makeInferedRealBox
            // 
            this._makeInferedRealBox.AutoSize = true;
            this._makeInferedRealBox.Location = new System.Drawing.Point(34, 34);
            this._makeInferedRealBox.Name = "_makeInferedRealBox";
            this._makeInferedRealBox.Size = new System.Drawing.Size(266, 17);
            this._makeInferedRealBox.TabIndex = 0;
            this._makeInferedRealBox.Text = "Make all infered \\sn markers real (e.g. \\+sn --> \\sn)";
            this._makeInferedRealBox.UseVisualStyleBackColor = true;
            // 
            // QuickFixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 458);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickFixForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Quick Fixes";
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel _moveUpGoLink;
        private System.Windows.Forms.TextBox _moveUpMarkers;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel _executeRemoveEmpty;
        private System.Windows.Forms.TextBox _removeEmptyMarkers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _moveUpRoots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.LinkLabel _exectueFLExFixes;
        private System.Windows.Forms.CheckBox _createReferredToItems;
        private System.Windows.Forms.CheckBox _makeInferedRealBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
    }
}