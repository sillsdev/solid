namespace SolidGui.Export
{
    partial class SaveOptionsDialog
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
            this.checkBoxIndent = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.flowLayoutForIndent = new System.Windows.Forms.FlowLayoutPanel();
            this.numSpaces = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxClosers = new System.Windows.Forms.CheckBox();
            this.flowLayoutMain = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxInferred = new System.Windows.Forms.CheckBox();
            this.checkBoxLinuxNewline = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonTab = new System.Windows.Forms.RadioButton();
            this.radioButtonSpace = new System.Windows.Forms.RadioButton();
            this.labelClosersWarning = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutForIndent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpaces)).BeginInit();
            this.flowLayoutMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxIndent
            // 
            this.checkBoxIndent.AutoSize = true;
            this.checkBoxIndent.Location = new System.Drawing.Point(3, 3);
            this.checkBoxIndent.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.checkBoxIndent.Name = "checkBoxIndent";
            this.checkBoxIndent.Size = new System.Drawing.Size(122, 17);
            this.checkBoxIndent.TabIndex = 0;
            this.checkBoxIndent.Text = "&Indent each level by";
            this.checkBoxIndent.UseVisualStyleBackColor = true;
            this.checkBoxIndent.CheckedChanged += new System.EventHandler(this.OptionsChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(89, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(170, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // flowLayoutForIndent
            // 
            this.flowLayoutForIndent.Controls.Add(this.checkBoxIndent);
            this.flowLayoutForIndent.Controls.Add(this.numSpaces);
            this.flowLayoutForIndent.Controls.Add(this.label1);
            this.flowLayoutForIndent.Location = new System.Drawing.Point(0, 3);
            this.flowLayoutForIndent.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.flowLayoutForIndent.Name = "flowLayoutForIndent";
            this.flowLayoutForIndent.Size = new System.Drawing.Size(225, 22);
            this.flowLayoutForIndent.TabIndex = 3;
            // 
            // numSpaces
            // 
            this.numSpaces.Location = new System.Drawing.Point(125, 1);
            this.numSpaces.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.numSpaces.Name = "numSpaces";
            this.numSpaces.Size = new System.Drawing.Size(28, 20);
            this.numSpaces.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "spaces";
            // 
            // checkBoxClosers
            // 
            this.checkBoxClosers.AutoSize = true;
            this.checkBoxClosers.Location = new System.Drawing.Point(3, 59);
            this.checkBoxClosers.Name = "checkBoxClosers";
            this.checkBoxClosers.Padding = new System.Windows.Forms.Padding(0, 2, 0, 3);
            this.checkBoxClosers.Size = new System.Drawing.Size(60, 22);
            this.checkBoxClosers.TabIndex = 4;
            this.checkBoxClosers.Text = "C&losers";
            this.checkBoxClosers.UseVisualStyleBackColor = true;
            this.checkBoxClosers.CheckedChanged += new System.EventHandler(this.OptionsChanged);
            // 
            // flowLayoutMain
            // 
            this.flowLayoutMain.Controls.Add(this.flowLayoutForIndent);
            this.flowLayoutMain.Controls.Add(this.checkBoxInferred);
            this.flowLayoutMain.Controls.Add(this.checkBoxClosers);
            this.flowLayoutMain.Controls.Add(this.checkBoxLinuxNewline);
            this.flowLayoutMain.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutMain.Controls.Add(this.labelClosersWarning);
            this.flowLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutMain.Name = "flowLayoutMain";
            this.flowLayoutMain.Size = new System.Drawing.Size(248, 187);
            this.flowLayoutMain.TabIndex = 5;
            // 
            // checkBoxInferred
            // 
            this.checkBoxInferred.AutoSize = true;
            this.checkBoxInferred.Location = new System.Drawing.Point(3, 31);
            this.checkBoxInferred.Name = "checkBoxInferred";
            this.checkBoxInferred.Padding = new System.Windows.Forms.Padding(0, 2, 0, 3);
            this.checkBoxInferred.Size = new System.Drawing.Size(102, 22);
            this.checkBoxInferred.TabIndex = 5;
            this.checkBoxInferred.Text = "In&ferred markers";
            this.checkBoxInferred.UseVisualStyleBackColor = true;
            this.checkBoxInferred.CheckedChanged += new System.EventHandler(this.OptionsChanged);
            // 
            // checkBoxLinuxNewline
            // 
            this.checkBoxLinuxNewline.AutoSize = true;
            this.checkBoxLinuxNewline.Location = new System.Drawing.Point(3, 87);
            this.checkBoxLinuxNewline.Name = "checkBoxLinuxNewline";
            this.checkBoxLinuxNewline.Padding = new System.Windows.Forms.Padding(0, 2, 0, 3);
            this.checkBoxLinuxNewline.Size = new System.Drawing.Size(123, 22);
            this.checkBoxLinuxNewline.TabIndex = 6;
            this.checkBoxLinuxNewline.Text = "Linux/Mac &Newlines";
            this.checkBoxLinuxNewline.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonTab);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonSpace);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 115);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(197, 22);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Separator:";
            // 
            // radioButtonTab
            // 
            this.radioButtonTab.AutoSize = true;
            this.radioButtonTab.Location = new System.Drawing.Point(59, 3);
            this.radioButtonTab.Name = "radioButtonTab";
            this.radioButtonTab.Size = new System.Drawing.Size(40, 17);
            this.radioButtonTab.TabIndex = 6;
            this.radioButtonTab.Text = "tab";
            this.radioButtonTab.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpace
            // 
            this.radioButtonSpace.AutoSize = true;
            this.radioButtonSpace.Checked = true;
            this.radioButtonSpace.Location = new System.Drawing.Point(105, 3);
            this.radioButtonSpace.Name = "radioButtonSpace";
            this.radioButtonSpace.Size = new System.Drawing.Size(54, 17);
            this.radioButtonSpace.TabIndex = 7;
            this.radioButtonSpace.TabStop = true;
            this.radioButtonSpace.Text = "space";
            this.radioButtonSpace.UseVisualStyleBackColor = true;
            // 
            // labelClosersWarning
            // 
            this.labelClosersWarning.AutoSize = true;
            this.labelClosersWarning.ForeColor = System.Drawing.Color.DarkRed;
            this.labelClosersWarning.Location = new System.Drawing.Point(3, 140);
            this.labelClosersWarning.Name = "labelClosersWarning";
            this.labelClosersWarning.Size = new System.Drawing.Size(241, 13);
            this.labelClosersWarning.TabIndex = 6;
            this.labelClosersWarning.Text = "Warning: bad data can make formatting incorrect.";
            this.labelClosersWarning.Visible = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonCancel);
            this.flowLayoutPanel2.Controls.Add(this.buttonOk);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 157);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(248, 30);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // SaveOptionsDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(248, 187);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutMain);
            this.Location = new System.Drawing.Point(500, 500);
            this.MaximumSize = new System.Drawing.Size(310, 268);
            this.MinimumSize = new System.Drawing.Size(233, 218);
            this.Name = "SaveOptionsDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Formatting Options";
            this.Load += new System.EventHandler(this.SaveOptionsDialog_Load);
            this.flowLayoutForIndent.ResumeLayout(false);
            this.flowLayoutForIndent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpaces)).EndInit();
            this.flowLayoutMain.ResumeLayout(false);
            this.flowLayoutMain.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.CheckBox checkBoxIndent;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutForIndent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox checkBoxClosers;
        public System.Windows.Forms.CheckBox checkBoxInferred;
        public System.Windows.Forms.CheckBox checkBoxLinuxNewline;
        public System.Windows.Forms.RadioButton radioButtonTab;
        public System.Windows.Forms.RadioButton radioButtonSpace;
        public System.Windows.Forms.NumericUpDown numSpaces;
        private System.Windows.Forms.Label labelClosersWarning;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}