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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._multipleTogetherRadioButton = new System.Windows.Forms.RadioButton();
            this._multipleApartRadioButton = new System.Windows.Forms.RadioButton();
            this._onceRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            this.flowLayoutPanelBottom.SuspendLayout();
            this.flowLayoutPanelTop.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
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
            this._explanationLabel.Location = new System.Drawing.Point(149, 0);
            this._explanationLabel.Name = "_explanationLabel";
            this._explanationLabel.Size = new System.Drawing.Size(121, 13);
            this._explanationLabel.TabIndex = 3;
            this._explanationLabel.Text = "Under lx, ge can appear";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "When no valid parent is present,  ";
            // 
            // _InferComboBox
            // 
            this._InferComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._InferComboBox.FormattingEnabled = true;
            this._InferComboBox.Items.AddRange(new object[] {
            "Report Error"});
            this._InferComboBox.Location = new System.Drawing.Point(175, 3);
            this._InferComboBox.Name = "_InferComboBox";
            this._InferComboBox.Size = new System.Drawing.Size(149, 21);
            this._InferComboBox.TabIndex = 7;
            this._InferComboBox.SelectedIndexChanged += new System.EventHandler(this._InferComboBox_SelectedIndexChanged);
            // 
            // _parentListView
            // 
            this._parentListView.HideSelection = false;
            this._parentListView.LabelEdit = true;
            this._parentListView.Location = new System.Drawing.Point(3, 23);
            this._parentListView.Name = "_parentListView";
            this._parentListView.Size = new System.Drawing.Size(61, 89);
            this._parentListView.TabIndex = 8;
            this._parentListView.UseCompatibleStateImageBehavior = false;
            this._parentListView.View = System.Windows.Forms.View.SmallIcon;
            this._parentListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this._parentListView_AfterLabelEdit);
            this._parentListView.SelectedIndexChanged += new System.EventHandler(this._parentListBox_SelectedIndexChanged);
            this._parentListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this._parentListView_KeyUp);
            this._parentListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this._parentListView_MouseUp);
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
            this._onceRadioButton.CheckedChanged += new System.EventHandler(this._aRadioButton_CheckedChanged);
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
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(445, 364);
            this.tableLayoutPanelMain.TabIndex = 12;
            // 
            // flowLayoutPanelBottom
            // 
            this.flowLayoutPanelBottom.Controls.Add(this.label2);
            this.flowLayoutPanelBottom.Controls.Add(this._InferComboBox);
            this.flowLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelBottom.Location = new System.Drawing.Point(3, 324);
            this.flowLayoutPanelBottom.Name = "flowLayoutPanelBottom";
            this.flowLayoutPanelBottom.Size = new System.Drawing.Size(439, 37);
            this.flowLayoutPanelBottom.TabIndex = 0;
            // 
            // flowLayoutPanelTop
            // 
            this.flowLayoutPanelTop.Controls.Add(this.pictureBox1);
            this.flowLayoutPanelTop.Controls.Add(this.label3);
            this.flowLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTop.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelTop.Name = "flowLayoutPanelTop";
            this.flowLayoutPanelTop.Size = new System.Drawing.Size(439, 54);
            this.flowLayoutPanelTop.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._parentListView, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this._explanationLabel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 63);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(439, 255);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this._onceRadioButton);
            this.flowLayoutPanel3.Controls.Add(this._multipleTogetherRadioButton);
            this.flowLayoutPanel3.Controls.Add(this._multipleApartRadioButton);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(149, 23);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(287, 229);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // StructurePropertiesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "StructurePropertiesView";
            this.Size = new System.Drawing.Size(445, 364);
            this.Load += new System.EventHandler(this.StructurePropertiesView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.flowLayoutPanelBottom.ResumeLayout(false);
            this.flowLayoutPanelBottom.PerformLayout();
            this.flowLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
    }
}
