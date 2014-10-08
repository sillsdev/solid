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
            this._nestedTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._parentListView = new System.Windows.Forms.ListView();
            this.columnHeaderMarker = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOccurs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.summaryLabel = new System.Windows.Forms.TextBox();
            this._commentsLabel = new System.Windows.Forms.TextBox();
            this._commentTextBox = new System.Windows.Forms.TextBox();
            this._summaryTextBox = new System.Windows.Forms.TextBox();
            this._parentLabel = new System.Windows.Forms.TextBox();
            this._explanationLabel = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelOccurs = new System.Windows.Forms.FlowLayoutPanel();
            this._onceRadioButton = new System.Windows.Forms.RadioButton();
            this._multipleTogetherRadioButton = new System.Windows.Forms.RadioButton();
            this._multipleApartRadioButton = new System.Windows.Forms.RadioButton();
            this._requiredCheckBox = new System.Windows.Forms.CheckBox();
            this._InferComboBox = new System.Windows.Forms.ComboBox();
            this._inferComboLabel = new System.Windows.Forms.TextBox();
            this._nestedTableLayoutPanel.SuspendLayout();
            this.flowLayoutPanelOccurs.SuspendLayout();
            this.SuspendLayout();
            // 
            // _nestedTableLayoutPanel
            // 
            this._nestedTableLayoutPanel.ColumnCount = 2;
            this._nestedTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._nestedTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this._nestedTableLayoutPanel.Controls.Add(this._parentListView, 0, 1);
            this._nestedTableLayoutPanel.Controls.Add(this.summaryLabel, 0, 3);
            this._nestedTableLayoutPanel.Controls.Add(this._commentsLabel, 0, 4);
            this._nestedTableLayoutPanel.Controls.Add(this._commentTextBox, 1, 4);
            this._nestedTableLayoutPanel.Controls.Add(this._summaryTextBox, 1, 3);
            this._nestedTableLayoutPanel.Controls.Add(this._parentLabel, 0, 0);
            this._nestedTableLayoutPanel.Controls.Add(this._explanationLabel, 1, 0);
            this._nestedTableLayoutPanel.Controls.Add(this.flowLayoutPanelOccurs, 1, 1);
            this._nestedTableLayoutPanel.Controls.Add(this._InferComboBox, 1, 2);
            this._nestedTableLayoutPanel.Controls.Add(this._inferComboLabel, 0, 2);
            this._nestedTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nestedTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._nestedTableLayoutPanel.Name = "_nestedTableLayoutPanel";
            this._nestedTableLayoutPanel.RowCount = 5;
            this._nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this._nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._nestedTableLayoutPanel.Size = new System.Drawing.Size(381, 266);
            this._nestedTableLayoutPanel.TabIndex = 1;
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
            this._parentListView.Location = new System.Drawing.Point(3, 33);
            this._parentListView.MultiSelect = false;
            this._parentListView.Name = "_parentListView";
            this._parentListView.Size = new System.Drawing.Size(89, 104);
            this._parentListView.TabIndex = 9;
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
            this.columnHeaderMarker.Width = 44;
            // 
            // columnHeaderOccurs
            // 
            this.columnHeaderOccurs.Text = "Occurs";
            this.columnHeaderOccurs.Width = 39;
            // 
            // summaryLabel
            // 
            this.summaryLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.summaryLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.summaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryLabel.Location = new System.Drawing.Point(3, 173);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(89, 13);
            this.summaryLabel.TabIndex = 0;
            this.summaryLabel.TabStop = false;
            this.summaryLabel.Text = "Summary:";
            // 
            // _commentsLabel
            // 
            this._commentsLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._commentsLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._commentsLabel.Location = new System.Drawing.Point(3, 203);
            this._commentsLabel.Name = "_commentsLabel";
            this._commentsLabel.Size = new System.Drawing.Size(89, 13);
            this._commentsLabel.TabIndex = 0;
            this._commentsLabel.TabStop = false;
            this._commentsLabel.Text = "Comments:";
            // 
            // _commentTextBox
            // 
            this._commentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._commentTextBox.Location = new System.Drawing.Point(98, 203);
            this._commentTextBox.Multiline = true;
            this._commentTextBox.Name = "_commentTextBox";
            this._commentTextBox.Size = new System.Drawing.Size(280, 60);
            this._commentTextBox.TabIndex = 3;
            this._commentTextBox.Leave += new System.EventHandler(this.CommentTextBoxNeedsCheck);
            this._commentTextBox.Validated += new System.EventHandler(this.CommentTextBoxNeedsCheck);
            // 
            // _summaryTextBox
            // 
            this._summaryTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._summaryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._summaryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summaryTextBox.Location = new System.Drawing.Point(98, 173);
            this._summaryTextBox.Name = "_summaryTextBox";
            this._summaryTextBox.Size = new System.Drawing.Size(280, 13);
            this._summaryTextBox.TabIndex = 0;
            this._summaryTextBox.TabStop = false;
            this._summaryTextBox.Text = "i";
            // 
            // _parentLabel
            // 
            this._parentLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._parentLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._parentLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._parentLabel.Location = new System.Drawing.Point(3, 3);
            this._parentLabel.Name = "_parentLabel";
            this._parentLabel.Size = new System.Drawing.Size(89, 13);
            this._parentLabel.TabIndex = 0;
            this._parentLabel.TabStop = false;
            this._parentLabel.Text = "Parent Marker(s):";
            // 
            // _explanationLabel
            // 
            this._explanationLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._explanationLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._explanationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._explanationLabel.Location = new System.Drawing.Point(98, 3);
            this._explanationLabel.Name = "_explanationLabel";
            this._explanationLabel.Size = new System.Drawing.Size(280, 13);
            this._explanationLabel.TabIndex = 0;
            this._explanationLabel.TabStop = false;
            this._explanationLabel.Text = "Under {0}, {1} can occur...";
            // 
            // flowLayoutPanelOccurs
            // 
            this.flowLayoutPanelOccurs.Controls.Add(this._onceRadioButton);
            this.flowLayoutPanelOccurs.Controls.Add(this._multipleTogetherRadioButton);
            this.flowLayoutPanelOccurs.Controls.Add(this._multipleApartRadioButton);
            this.flowLayoutPanelOccurs.Controls.Add(this._requiredCheckBox);
            this.flowLayoutPanelOccurs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelOccurs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelOccurs.Location = new System.Drawing.Point(98, 33);
            this.flowLayoutPanelOccurs.Name = "flowLayoutPanelOccurs";
            this.flowLayoutPanelOccurs.Size = new System.Drawing.Size(280, 104);
            this.flowLayoutPanelOccurs.TabIndex = 2;
            // 
            // _onceRadioButton
            // 
            this._onceRadioButton.AutoSize = true;
            this._onceRadioButton.Checked = true;
            this._onceRadioButton.Location = new System.Drawing.Point(3, 3);
            this._onceRadioButton.Name = "_onceRadioButton";
            this._onceRadioButton.Size = new System.Drawing.Size(51, 17);
            this._onceRadioButton.TabIndex = 0;
            this._onceRadioButton.TabStop = true;
            this._onceRadioButton.Text = "Once";
            this._onceRadioButton.UseVisualStyleBackColor = true;
            this._onceRadioButton.CheckedChanged += new System.EventHandler(this._radioButton_Click);
            // 
            // _multipleTogetherRadioButton
            // 
            this._multipleTogetherRadioButton.AutoSize = true;
            this._multipleTogetherRadioButton.Location = new System.Drawing.Point(3, 26);
            this._multipleTogetherRadioButton.Name = "_multipleTogetherRadioButton";
            this._multipleTogetherRadioButton.Size = new System.Drawing.Size(247, 17);
            this._multipleTogetherRadioButton.TabIndex = 0;
            this._multipleTogetherRadioButton.Text = "One or more times together (excepting children)";
            this._multipleTogetherRadioButton.UseVisualStyleBackColor = true;
            this._multipleTogetherRadioButton.CheckedChanged += new System.EventHandler(this._radioButton_Click);
            // 
            // _multipleApartRadioButton
            // 
            this._multipleApartRadioButton.AutoSize = true;
            this._multipleApartRadioButton.Location = new System.Drawing.Point(3, 49);
            this._multipleApartRadioButton.Name = "_multipleApartRadioButton";
            this._multipleApartRadioButton.Size = new System.Drawing.Size(250, 17);
            this._multipleApartRadioButton.TabIndex = 0;
            this._multipleApartRadioButton.Text = "One or more times (siblings may be interspersed)";
            this._multipleApartRadioButton.UseVisualStyleBackColor = true;
            this._multipleApartRadioButton.CheckedChanged += new System.EventHandler(this._radioButton_Click);
            // 
            // _requiredCheckBox
            // 
            this._requiredCheckBox.AutoSize = true;
            this._requiredCheckBox.Enabled = false;
            this._requiredCheckBox.Location = new System.Drawing.Point(3, 72);
            this._requiredCheckBox.Name = "_requiredCheckBox";
            this._requiredCheckBox.Size = new System.Drawing.Size(143, 17);
            this._requiredCheckBox.TabIndex = 1;
            this._requiredCheckBox.Text = "Must occur (i.e. required)";
            this._requiredCheckBox.UseVisualStyleBackColor = true;
            this._requiredCheckBox.CheckedChanged += new System.EventHandler(this._radioButton_Click);
            // 
            // _InferComboBox
            // 
            this._InferComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._InferComboBox.FormattingEnabled = true;
            this._InferComboBox.Items.AddRange(new object[] {
            "Report Error"});
            this._InferComboBox.Location = new System.Drawing.Point(98, 143);
            this._InferComboBox.Name = "_InferComboBox";
            this._InferComboBox.Size = new System.Drawing.Size(280, 21);
            this._InferComboBox.TabIndex = 2;
            this._InferComboBox.SelectedIndexChanged += new System.EventHandler(this._InferComboBox_SelectedIndexChanged);
            // 
            // _inferComboLabel
            // 
            this._inferComboLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._inferComboLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._inferComboLabel.Location = new System.Drawing.Point(3, 143);
            this._inferComboLabel.Name = "_inferComboLabel";
            this._inferComboLabel.Size = new System.Drawing.Size(89, 13);
            this._inferComboLabel.TabIndex = 0;
            this._inferComboLabel.TabStop = false;
            this._inferComboLabel.Text = "If no parent, then";
            // 
            // StructurePropertiesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this._nestedTableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(377, 239);
            this.Name = "StructurePropertiesView";
            this.Size = new System.Drawing.Size(381, 266);
            this.Leave += new System.EventHandler(this.CommentTextBoxNeedsCheck);
            this._nestedTableLayoutPanel.ResumeLayout(false);
            this._nestedTableLayoutPanel.PerformLayout();
            this.flowLayoutPanelOccurs.ResumeLayout(false);
            this.flowLayoutPanelOccurs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _nestedTableLayoutPanel;
        private System.Windows.Forms.ComboBox _InferComboBox;
        private System.Windows.Forms.ListView _parentListView;
        private System.Windows.Forms.ColumnHeader columnHeaderMarker;
        private System.Windows.Forms.ColumnHeader columnHeaderOccurs;
        private System.Windows.Forms.TextBox summaryLabel;
        private System.Windows.Forms.TextBox _commentsLabel;
        private System.Windows.Forms.TextBox _commentTextBox;
        private System.Windows.Forms.TextBox _summaryTextBox;
        private System.Windows.Forms.TextBox _parentLabel;
        private System.Windows.Forms.TextBox _explanationLabel;
        private System.Windows.Forms.TextBox _inferComboLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOccurs;
        private System.Windows.Forms.RadioButton _onceRadioButton;
        private System.Windows.Forms.RadioButton _multipleTogetherRadioButton;
        private System.Windows.Forms.RadioButton _multipleApartRadioButton;
        private System.Windows.Forms.CheckBox _requiredCheckBox;

    }
}
