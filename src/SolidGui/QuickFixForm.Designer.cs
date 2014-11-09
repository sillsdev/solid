// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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
            this._tbFixes = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._minimalCheckBox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this._executeMoveUp = new System.Windows.Forms.LinkLabel();
            this._moveUpRoots = new System.Windows.Forms.TextBox();
            this._moveUpMarkers = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._executeRemoveEmpty = new System.Windows.Forms.LinkLabel();
            this._removeEmptyMarkers = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._showPushPSInfo = new System.Windows.Forms.LinkLabel();
            this.linkFlexFixesInfo = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this._executeFLExFixes = new System.Windows.Forms.LinkLabel();
            this._pushPsDownToSns = new System.Windows.Forms.CheckBox();
            this._createReferredToItems = new System.Windows.Forms.CheckBox();
            this._makeInferedRealBox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._tbMakeRealMarkers = new System.Windows.Forms.TextBox();
            this._executeSaveInferred = new System.Windows.Forms.LinkLabel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this._executeAddGuids = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this._tbFixes.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(396, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "&Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _tbFixes
            // 
            this._tbFixes.Controls.Add(this.tabPage3);
            this._tbFixes.Controls.Add(this.tabPage4);
            this._tbFixes.Controls.Add(this.tabPage1);
            this._tbFixes.Controls.Add(this.tabPage2);
            this._tbFixes.Controls.Add(this.tabPage5);
            this._tbFixes.Location = new System.Drawing.Point(12, 9);
            this._tbFixes.Name = "_tbFixes";
            this._tbFixes.SelectedIndex = 0;
            this._tbFixes.Size = new System.Drawing.Size(463, 435);
            this._tbFixes.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._minimalCheckBox);
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Controls.Add(this.linkLabel3);
            this.tabPage3.Controls.Add(this.linkLabel2);
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this._executeMoveUp);
            this.tabPage3.Controls.Add(this._moveUpRoots);
            this.tabPage3.Controls.Add(this._moveUpMarkers);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(455, 409);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Move Up";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _minimalCheckBox
            // 
            this._minimalCheckBox.AutoSize = true;
            this._minimalCheckBox.Checked = true;
            this._minimalCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._minimalCheckBox.Location = new System.Drawing.Point(247, 158);
            this._minimalCheckBox.Name = "_minimalCheckBox";
            this._minimalCheckBox.Size = new System.Drawing.Size(42, 17);
            this._minimalCheckBox.TabIndex = 13;
            this._minimalCheckBox.Text = "yes";
            this._minimalCheckBox.UseVisualStyleBackColor = true;
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
            this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.Location = new System.Drawing.Point(42, 277);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(144, 16);
            this.linkLabel3.TabIndex = 13;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "(hm, lc, bw) to under (lx)";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnPremadeLabelClick);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(42, 296);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(108, 16);
            this.linkLabel2.TabIndex = 13;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "(sd) to under (ps)";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnPremadeLabelClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(42, 258);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(156, 16);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "(lt, ph) to under (lx, se, va)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnPremadeLabelClick);
            // 
            // _executeMoveUp
            // 
            this._executeMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._executeMoveUp.AutoSize = true;
            this._executeMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._executeMoveUp.Location = new System.Drawing.Point(15, 380);
            this._executeMoveUp.Name = "_executeMoveUp";
            this._executeMoveUp.Size = new System.Drawing.Size(258, 16);
            this._executeMoveUp.TabIndex = 14;
            this._executeMoveUp.TabStop = true;
            this._executeMoveUp.Text = "I know what I\'m doing and have backed up";
            this._executeMoveUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteMoveUp);
            // 
            // _moveUpRoots
            // 
            this._moveUpRoots.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._moveUpRoots.Location = new System.Drawing.Point(247, 128);
            this._moveUpRoots.Name = "_moveUpRoots";
            this._moveUpRoots.Size = new System.Drawing.Size(120, 22);
            this._moveUpRoots.TabIndex = 12;
            this._moveUpRoots.Text = "lx, hm, lc";
            // 
            // _moveUpMarkers
            // 
            this._moveUpMarkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._moveUpMarkers.Location = new System.Drawing.Point(247, 105);
            this._moveUpMarkers.Name = "_moveUpMarkers";
            this._moveUpMarkers.Size = new System.Drawing.Size(120, 22);
            this._moveUpMarkers.TabIndex = 11;
            this._moveUpMarkers.Text = "valx";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(106, 27);
            this.label6.MaximumSize = new System.Drawing.Size(300, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(294, 34);
            this.label6.TabIndex = 10;
            this.label6.Text = "Warning: this is very dangerous.  Back up before this, and check the results. ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(256, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Click one of these to fill in the fields above:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(42, 214);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(273, 16);
            this.label13.TabIndex = 9;
            this.label13.Text = "Works best with: single fields, error-free files.)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(42, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(319, 16);
            this.label12.TabIndex = 9;
            this.label12.Text = "and will still force markers up just above all nephews.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(42, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(278, 16);
            this.label11.TabIndex = 9;
            this.label11.Text = "(Will still tear parents away from their children, ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(21, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(212, 16);
            this.label10.TabIndex = 9;
            this.label10.Text = "Reduce move amount (a bit safer):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "To underneath these roots:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
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
            this.tabPage4.Size = new System.Drawing.Size(455, 409);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Remove empty";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // _executeRemoveEmpty
            // 
            this._executeRemoveEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._executeRemoveEmpty.AutoSize = true;
            this._executeRemoveEmpty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._executeRemoveEmpty.Location = new System.Drawing.Point(15, 380);
            this._executeRemoveEmpty.Name = "_executeRemoveEmpty";
            this._executeRemoveEmpty.Size = new System.Drawing.Size(258, 16);
            this._executeRemoveEmpty.TabIndex = 16;
            this._executeRemoveEmpty.TabStop = true;
            this._executeRemoveEmpty.Text = "I know what I\'m doing and have backed up";
            this._executeRemoveEmpty.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteRemoveEmpty);
            // 
            // _removeEmptyMarkers
            // 
            this._removeEmptyMarkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._removeEmptyMarkers.Location = new System.Drawing.Point(49, 53);
            this._removeEmptyMarkers.Name = "_removeEmptyMarkers";
            this._removeEmptyMarkers.Size = new System.Drawing.Size(288, 22);
            this._removeEmptyMarkers.TabIndex = 15;
            this._removeEmptyMarkers.Text = "lx, sn";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(235, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Fields to leave, even if they are empty:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._showPushPSInfo);
            this.tabPage1.Controls.Add(this.linkFlexFixesInfo);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this._executeFLExFixes);
            this.tabPage1.Controls.Add(this._pushPsDownToSns);
            this.tabPage1.Controls.Add(this._createReferredToItems);
            this.tabPage1.Controls.Add(this._makeInferedRealBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(455, 409);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "FLEx Import Fixes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _showPushPSInfo
            // 
            this._showPushPSInfo.AutoSize = true;
            this._showPushPSInfo.Location = new System.Drawing.Point(55, 184);
            this._showPushPSInfo.Name = "_showPushPSInfo";
            this._showPushPSInfo.Size = new System.Drawing.Size(106, 13);
            this._showPushPSInfo.TabIndex = 19;
            this._showPushPSInfo.TabStop = true;
            this._showPushPSInfo.Text = "Important Information";
            this._showPushPSInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._showPushPSInfo_LinkClicked);
            // 
            // linkFlexFixesInfo
            // 
            this.linkFlexFixesInfo.AutoSize = true;
            this.linkFlexFixesInfo.Location = new System.Drawing.Point(58, 122);
            this.linkFlexFixesInfo.Name = "linkFlexFixesInfo";
            this.linkFlexFixesInfo.Size = new System.Drawing.Size(106, 13);
            this.linkFlexFixesInfo.TabIndex = 19;
            this.linkFlexFixesInfo.TabStop = true;
            this.linkFlexFixesInfo.Text = "Important Information";
            this.linkFlexFixesInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 54);
            this.label5.MaximumSize = new System.Drawing.Size(250, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(250, 26);
            this.label5.TabIndex = 18;
            this.label5.Text = "Increases the chance that FLEx has the same idea about the structure as your SOLI" +
                "D rules.";
            // 
            // _executeFLExFixes
            // 
            this._executeFLExFixes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._executeFLExFixes.AutoSize = true;
            this._executeFLExFixes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._executeFLExFixes.Location = new System.Drawing.Point(15, 380);
            this._executeFLExFixes.Name = "_executeFLExFixes";
            this._executeFLExFixes.Size = new System.Drawing.Size(258, 16);
            this._executeFLExFixes.TabIndex = 17;
            this._executeFLExFixes.TabStop = true;
            this._executeFLExFixes.Text = "I know what I\'m doing and have backed up";
            this._executeFLExFixes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteFLExFixes_LinkClicked);
            // 
            // _pushPsDownToSns
            // 
            this._pushPsDownToSns.AutoSize = true;
            this._pushPsDownToSns.Location = new System.Drawing.Point(34, 154);
            this._pushPsDownToSns.Name = "_pushPsDownToSns";
            this._pushPsDownToSns.Size = new System.Drawing.Size(197, 17);
            this._pushPsDownToSns.TabIndex = 0;
            this._pushPsDownToSns.Text = "Push \\ps down to subsequent \\sn\'s.";
            this._pushPsDownToSns.UseVisualStyleBackColor = true;
            // 
            // _createReferredToItems
            // 
            this._createReferredToItems.AutoSize = true;
            this._createReferredToItems.Location = new System.Drawing.Point(34, 98);
            this._createReferredToItems.Name = "_createReferredToItems";
            this._createReferredToItems.Size = new System.Drawing.Size(292, 17);
            this._createReferredToItems.TabIndex = 0;
            this._createReferredToItems.Text = "Add entries for all referred to items (e.g. \\cf, \\sy, \\an, \\lv)";
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this._tbMakeRealMarkers);
            this.tabPage2.Controls.Add(this._executeSaveInferred);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(455, 409);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Make Markers Real";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(15, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(434, 73);
            this.label8.TabIndex = 21;
            this.label8.Text = "Enter a comma separated list of markers in the text box above.  Wherever these ma" +
                "rkers are \'inferred\', they will be made real. i.e. The + sign will be removed an" +
                "d they will be saved.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Markers to make real";
            // 
            // _tbMakeRealMarkers
            // 
            this._tbMakeRealMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbMakeRealMarkers.Location = new System.Drawing.Point(127, 15);
            this._tbMakeRealMarkers.Name = "_tbMakeRealMarkers";
            this._tbMakeRealMarkers.Size = new System.Drawing.Size(322, 20);
            this._tbMakeRealMarkers.TabIndex = 19;
            this._tbMakeRealMarkers.Text = "sn,ps,rf";
            // 
            // _executeSaveInferred
            // 
            this._executeSaveInferred.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._executeSaveInferred.AutoSize = true;
            this._executeSaveInferred.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._executeSaveInferred.Location = new System.Drawing.Point(15, 380);
            this._executeSaveInferred.Name = "_executeSaveInferred";
            this._executeSaveInferred.Size = new System.Drawing.Size(258, 16);
            this._executeSaveInferred.TabIndex = 18;
            this._executeSaveInferred.TabStop = true;
            this._executeSaveInferred.Text = "I know what I\'m doing and have backed up";
            this._executeSaveInferred.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteSaveInferred_LinkClicked);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this._executeAddGuids);
            this.tabPage5.Controls.Add(this.label9);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(455, 409);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Add Guids";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // _executeAddGuids
            // 
            this._executeAddGuids.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._executeAddGuids.AutoSize = true;
            this._executeAddGuids.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._executeAddGuids.Location = new System.Drawing.Point(15, 378);
            this._executeAddGuids.Name = "_executeAddGuids";
            this._executeAddGuids.Size = new System.Drawing.Size(258, 16);
            this._executeAddGuids.TabIndex = 23;
            this._executeAddGuids.TabStop = true;
            this._executeAddGuids.Text = "I know what I\'m doing and have backed up";
            this._executeAddGuids.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnExecuteAddGuids);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(15, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(434, 73);
            this.label9.TabIndex = 22;
            this.label9.Text = resources.GetString("label9.Text");
            // 
            // QuickFixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(487, 485);
            this.Controls.Add(this._tbFixes);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickFixForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Quick Fixes";
            this._tbFixes.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl _tbFixes;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel _executeMoveUp;
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
        private System.Windows.Forms.LinkLabel _executeFLExFixes;
        private System.Windows.Forms.CheckBox _createReferredToItems;
        private System.Windows.Forms.CheckBox _makeInferedRealBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkFlexFixesInfo;
        private System.Windows.Forms.CheckBox _pushPsDownToSns;
        private System.Windows.Forms.LinkLabel _showPushPSInfo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox _tbMakeRealMarkers;
        private System.Windows.Forms.LinkLabel _executeSaveInferred;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.LinkLabel _executeAddGuids;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox _minimalCheckBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
    }
}