// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

namespace SolidGui
{
    partial class StructurePropertiesView
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
            this.label1 = new System.Windows.Forms.Label();
            this._explanationLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._InferComboBox = new System.Windows.Forms.ComboBox();
            this._parentListView = new System.Windows.Forms.ListView();
            this.columnHeaderMarker = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOccurs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._multipleTogetherRadioButton = new System.Windows.Forms.RadioButton();
            this._multipleApartRadioButton = new System.Windows.Forms.RadioButton();
            this._onceRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutTwoCol = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelOccurs = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            this.flowLayoutPanelBottom.SuspendLayout();
            this.flowLayoutPanelTop.SuspendLayout();
            this.tableLayoutTwoCol.SuspendLayout();
            this.flowLayoutPanelOccurs.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parent Marker";
            // 
            // _explanationLabel
            // 
            this._explanationLabel.AutoSize = true;
            this._explanationLabel.Location = new System.Drawing.Point(184, 0);
            this._explanationLabel.Name = "_explanationLabel";
            this._explanationLabel.Size = new System.Drawing.Size(124, 13);
            this._explanationLabel.TabIndex = 3;
            this._explanationLabel.Text = "Under lx, ge can occur...";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "When no valid parent is present,";
            // 
            // _InferComboBox
            // 
            this._InferComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._InferComboBox.FormattingEnabled = true;
            this._InferComboBox.Items.AddRange(new object[] {
            "Report Error"});
            this._InferComboBox.Location = new System.Drawing.Point(169, 3);
            this._InferComboBox.Name = "_InferComboBox";
            this._InferComboBox.Size = new System.Drawing.Size(149, 21);
            this._InferComboBox.TabIndex = 7;
            this._InferComboBox.SelectedIndexChanged += new System.EventHandler(this._InferComboBox_SelectedIndexChanged);
            // 
            // _parentListView
            // 
            this._parentListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMarker,
            this.columnHeaderOccurs});
            this._parentListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._parentListView.FullRowSelect = true;
            this._parentListView.HideSelection = false;
            this._parentListView.LabelEdit = true;
            this._parentListView.Location = new System.Drawing.Point(3, 23);
            this._parentListView.MultiSelect = false;
            this._parentListView.Name = "_parentListView";
            this._parentListView.Size = new System.Drawing.Size(175, 198);
            this._parentListView.TabIndex = 8;
            this._parentListView.UseCompatibleStateImageBehavior = false;
            this._parentListView.View = System.Windows.Forms.View.Details;
            this._parentListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this._parentListView_AfterLabelEdit);
            this._parentListView.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this._parentListView_BeforeLabelEdit);
            this._parentListView.SelectedIndexChanged += new System.EventHandler(this._parentListView_SelectedIndexChanged);
            this._parentListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this._parentListView_KeyUp);
            this._parentListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this._parentListView_MouseUp);
            // 
            // columnHeaderMarker
            // 
            this.columnHeaderMarker.Text = "Marker";
            // 
            // columnHeaderOccurs
            // 
            this.columnHeaderOccurs.Text = "Occurs";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SolidGui.Properties.Resources.info2;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // _multipleTogetherRadioButton
            // 
            this._multipleTogetherRadioButton.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this._multipleTogetherRadioButton.Location = new System.Drawing.Point(3, 26);
            this._multipleTogetherRadioButton.Name = "_multipleTogetherRadioButton";
            this._multipleTogetherRadioButton.Size = new System.Drawing.Size(152, 17);
            this._multipleTogetherRadioButton.TabIndex = 4;
            this._multipleTogetherRadioButton.TabStop = true;
            this._multipleTogetherRadioButton.Text = "One or more times together";
            this._multipleTogetherRadioButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this._multipleTogetherRadioButton.UseVisualStyleBackColor = true;
            this._multipleTogetherRadioButton.CheckedChanged += new System.EventHandler(this._aRadioButton_CheckedChanged);
            this._multipleTogetherRadioButton.Click += new System.EventHandler(this._radioButton_Click);
            // 
            // _multipleApartRadioButton
            // 
            this._multipleApartRadioButton.AutoEllipsis = true;
            this._multipleApartRadioButton.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this._multipleApartRadioButton.Location = new System.Drawing.Point(3, 49);
            this._multipleApartRadioButton.Name = "_multipleApartRadioButton";
            this._multipleApartRadioButton.Size = new System.Drawing.Size(183, 33);
            this._multipleApartRadioButton.TabIndex = 5;
            this._multipleApartRadioButton.TabStop = true;
            this._multipleApartRadioButton.Text = "One or more times with intervening markers";
            this._multipleApartRadioButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this._multipleApartRadioButton.UseVisualStyleBackColor = true;
            this._multipleApartRadioButton.CheckedChanged += new System.EventHandler(this._aRadioButton_CheckedChanged);
            this._multipleApartRadioButton.Click += new System.EventHandler(this._radioButton_Click);
            // 
            // _onceRadioButton
            // 
            this._onceRadioButton.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this._onceRadioButton.Location = new System.Drawing.Point(3, 3);
            this._onceRadioButton.Name = "_onceRadioButton";
            this._onceRadioButton.Size = new System.Drawing.Size(51, 17);
            this._onceRadioButton.TabIndex = 2;
            this._onceRadioButton.TabStop = true;
            this._onceRadioButton.Text = "Once";
            this._onceRadioButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this._onceRadioButton.UseVisualStyleBackColor = true;
            this._onceRadioButton.CheckedChanged += new System.EventHandler(this._radioButton_Click);
            this._onceRadioButton.Click += new System.EventHandler(this._radioButton_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(41, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(4, 4, 0, 0);
            this.label3.Size = new System.Drawing.Size(327, 48);
            this.label3.TabIndex = 11;
            this.label3.Text = "Add parents of this marker in the \'Parent Marker\' box.  For each marker select ad" +
    "ditional constraints using the radio buttons.";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.flowLayoutPanelBottom, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.flowLayoutPanelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutTwoCol, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(459, 323);
            this.tableLayoutPanelMain.TabIndex = 12;
            // 
            // flowLayoutPanelBottom
            // 
            this.flowLayoutPanelBottom.Controls.Add(this.label2);
            this.flowLayoutPanelBottom.Controls.Add(this._InferComboBox);
            this.flowLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelBottom.Location = new System.Drawing.Point(3, 283);
            this.flowLayoutPanelBottom.Name = "flowLayoutPanelBottom";
            this.flowLayoutPanelBottom.Size = new System.Drawing.Size(453, 37);
            this.flowLayoutPanelBottom.TabIndex = 0;
            // 
            // flowLayoutPanelTop
            // 
            this.flowLayoutPanelTop.Controls.Add(this.pictureBox1);
            this.flowLayoutPanelTop.Controls.Add(this.label3);
            this.flowLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTop.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelTop.Name = "flowLayoutPanelTop";
            this.flowLayoutPanelTop.Size = new System.Drawing.Size(453, 44);
            this.flowLayoutPanelTop.TabIndex = 0;
            // 
            // tableLayoutTwoCol
            // 
            this.tableLayoutTwoCol.ColumnCount = 2;
            this.tableLayoutTwoCol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutTwoCol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutTwoCol.Controls.Add(this.label1, 0, 0);
            this.tableLayoutTwoCol.Controls.Add(this._parentListView, 0, 1);
            this.tableLayoutTwoCol.Controls.Add(this.flowLayoutPanelOccurs, 1, 1);
            this.tableLayoutTwoCol.Controls.Add(this._explanationLabel, 1, 0);
            this.tableLayoutTwoCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutTwoCol.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutTwoCol.Name = "tableLayoutTwoCol";
            this.tableLayoutTwoCol.RowCount = 2;
            this.tableLayoutTwoCol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutTwoCol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutTwoCol.Size = new System.Drawing.Size(453, 224);
            this.tableLayoutTwoCol.TabIndex = 1;
            // 
            // flowLayoutPanelOccurs
            // 
            this.flowLayoutPanelOccurs.Controls.Add(this._onceRadioButton);
            this.flowLayoutPanelOccurs.Controls.Add(this._multipleTogetherRadioButton);
            this.flowLayoutPanelOccurs.Controls.Add(this._multipleApartRadioButton);
            this.flowLayoutPanelOccurs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelOccurs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelOccurs.Location = new System.Drawing.Point(184, 23);
            this.flowLayoutPanelOccurs.Name = "flowLayoutPanelOccurs";
            this.flowLayoutPanelOccurs.Size = new System.Drawing.Size(266, 198);
            this.flowLayoutPanelOccurs.TabIndex = 1;
            // 
            // StructurePropertiesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MinimumSize = new System.Drawing.Size(377, 224);
            this.Name = "StructurePropertiesView";
            this.Size = new System.Drawing.Size(459, 323);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.flowLayoutPanelBottom.ResumeLayout(false);
            this.flowLayoutPanelBottom.PerformLayout();
            this.flowLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutTwoCol.ResumeLayout(false);
            this.tableLayoutTwoCol.PerformLayout();
            this.flowLayoutPanelOccurs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _explanationLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _InferComboBox;
        private System.Windows.Forms.ListView _parentListView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton _onceRadioButton;
        private System.Windows.Forms.RadioButton _multipleApartRadioButton;
        private System.Windows.Forms.RadioButton _multipleTogetherRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBottom;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutTwoCol;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOccurs;
        private System.Windows.Forms.ColumnHeader columnHeaderMarker;
        private System.Windows.Forms.ColumnHeader columnHeaderOccurs;
    }
}
