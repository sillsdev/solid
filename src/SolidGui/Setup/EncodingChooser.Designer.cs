namespace SolidGui.Setup
{
    partial class EncodingChooser
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
            this.tableLayoutDialog = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutEncoding = new System.Windows.Forms.TableLayoutPanel();
            this.labelChooseEncoding = new System.Windows.Forms.Label();
            this.labelEncodingDetectedLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonUtf8 = new System.Windows.Forms.RadioButton();
            this.radioButtonLegacy = new System.Windows.Forms.RadioButton();
            this.textBoxUtf8Label = new System.Windows.Forms.TextBox();
            this.textBoxCp1252Label = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this.textBoxUtf8 = new System.Windows.Forms.TextBox();
            this.textBoxCp1252 = new System.Windows.Forms.TextBox();
            this.tableLayoutDialog.SuspendLayout();
            this.tableLayoutEncoding.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutDialog
            // 
            this.tableLayoutDialog.ColumnCount = 1;
            this.tableLayoutDialog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDialog.Controls.Add(this.tableLayoutEncoding, 0, 0);
            this.tableLayoutDialog.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutDialog.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutDialog.Name = "tableLayoutDialog";
            this.tableLayoutDialog.RowCount = 2;
            this.tableLayoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutDialog.Size = new System.Drawing.Size(567, 327);
            this.tableLayoutDialog.TabIndex = 0;
            // 
            // tableLayoutEncoding
            // 
            this.tableLayoutEncoding.AutoSize = true;
            this.tableLayoutEncoding.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutEncoding.ColumnCount = 2;
            this.tableLayoutEncoding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutEncoding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutEncoding.Controls.Add(this.labelChooseEncoding, 0, 0);
            this.tableLayoutEncoding.Controls.Add(this.labelEncodingDetectedLabel, 0, 1);
            this.tableLayoutEncoding.Controls.Add(this.flowLayoutPanel3, 1, 0);
            this.tableLayoutEncoding.Controls.Add(this.textBoxUtf8Label, 0, 2);
            this.tableLayoutEncoding.Controls.Add(this.textBoxCp1252Label, 1, 2);
            this.tableLayoutEncoding.Controls.Add(this.textBoxUtf8, 0, 3);
            this.tableLayoutEncoding.Controls.Add(this.textBoxCp1252, 1, 3);
            this.tableLayoutEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutEncoding.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutEncoding.Name = "tableLayoutEncoding";
            this.tableLayoutEncoding.RowCount = 4;
            this.tableLayoutEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutEncoding.Size = new System.Drawing.Size(561, 286);
            this.tableLayoutEncoding.TabIndex = 13;
            // 
            // labelChooseEncoding
            // 
            this.labelChooseEncoding.AutoSize = true;
            this.labelChooseEncoding.Location = new System.Drawing.Point(3, 3);
            this.labelChooseEncoding.Margin = new System.Windows.Forms.Padding(3);
            this.labelChooseEncoding.Name = "labelChooseEncoding";
            this.labelChooseEncoding.Size = new System.Drawing.Size(178, 13);
            this.labelChooseEncoding.TabIndex = 0;
            this.labelChooseEncoding.Text = "What encoding is the file already in?";
            // 
            // labelEncodingDetectedLabel
            // 
            this.labelEncodingDetectedLabel.AutoSize = true;
            this.labelEncodingDetectedLabel.Location = new System.Drawing.Point(3, 32);
            this.labelEncodingDetectedLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelEncodingDetectedLabel.Name = "labelEncodingDetectedLabel";
            this.labelEncodingDetectedLabel.Size = new System.Drawing.Size(272, 13);
            this.labelEncodingDetectedLabel.TabIndex = 1;
            this.labelEncodingDetectedLabel.Text = "Analysis: Can whole file be read in with no data loss as...";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.radioButtonUtf8);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonLegacy);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(281, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(277, 23);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // radioButtonUtf8
            // 
            this.radioButtonUtf8.AutoSize = true;
            this.radioButtonUtf8.Location = new System.Drawing.Point(1, 3);
            this.radioButtonUtf8.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.radioButtonUtf8.Name = "radioButtonUtf8";
            this.radioButtonUtf8.Size = new System.Drawing.Size(46, 17);
            this.radioButtonUtf8.TabIndex = 2;
            this.radioButtonUtf8.Text = "utf-8";
            this.radioButtonUtf8.UseVisualStyleBackColor = true;
            // 
            // radioButtonLegacy
            // 
            this.radioButtonLegacy.AutoSize = true;
            this.radioButtonLegacy.Checked = true;
            this.radioButtonLegacy.Location = new System.Drawing.Point(49, 3);
            this.radioButtonLegacy.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.radioButtonLegacy.Name = "radioButtonLegacy";
            this.radioButtonLegacy.Size = new System.Drawing.Size(148, 17);
            this.radioButtonLegacy.TabIndex = 1;
            this.radioButtonLegacy.TabStop = true;
            this.radioButtonLegacy.Text = "legacy, mixed or unknown";
            this.radioButtonLegacy.UseVisualStyleBackColor = true;
            // 
            // textBoxUtf8Label
            // 
            this.textBoxUtf8Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUtf8Label.Location = new System.Drawing.Point(3, 51);
            this.textBoxUtf8Label.Name = "textBoxUtf8Label";
            this.textBoxUtf8Label.ReadOnly = true;
            this.textBoxUtf8Label.Size = new System.Drawing.Size(272, 20);
            this.textBoxUtf8Label.TabIndex = 3;
            this.textBoxUtf8Label.Text = "As utf-8: {0}";
            // 
            // textBoxCp1252Label
            // 
            this.textBoxCp1252Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCp1252Label.Location = new System.Drawing.Point(281, 51);
            this.textBoxCp1252Label.Name = "textBoxCp1252Label";
            this.textBoxCp1252Label.ReadOnly = true;
            this.textBoxCp1252Label.Size = new System.Drawing.Size(277, 20);
            this.textBoxCp1252Label.TabIndex = 4;
            this.textBoxCp1252Label.Text = "As cp1252: {0}";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._cancelButton);
            this.flowLayoutPanel1.Controls.Add(this._okButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 295);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(561, 29);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Enabled = false;
            this._cancelButton.Location = new System.Drawing.Point(483, 3);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 0;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(402, 3);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "&OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // textBoxUtf8
            // 
            this.textBoxUtf8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUtf8.Location = new System.Drawing.Point(3, 77);
            this.textBoxUtf8.Multiline = true;
            this.textBoxUtf8.Name = "textBoxUtf8";
            this.textBoxUtf8.ReadOnly = true;
            this.textBoxUtf8.Size = new System.Drawing.Size(272, 206);
            this.textBoxUtf8.TabIndex = 5;
            // 
            // textBoxCp1252
            // 
            this.textBoxCp1252.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCp1252.Location = new System.Drawing.Point(281, 77);
            this.textBoxCp1252.Multiline = true;
            this.textBoxCp1252.Name = "textBoxCp1252";
            this.textBoxCp1252.ReadOnly = true;
            this.textBoxCp1252.Size = new System.Drawing.Size(277, 206);
            this.textBoxCp1252.TabIndex = 6;
            // 
            // EncodingChooser
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(567, 327);
            this.Controls.Add(this.tableLayoutDialog);
            this.Name = "EncodingChooser";
            this.Text = "EncodingChooser";
            this.tableLayoutDialog.ResumeLayout(false);
            this.tableLayoutDialog.PerformLayout();
            this.tableLayoutEncoding.ResumeLayout(false);
            this.tableLayoutEncoding.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutEncoding;
        private System.Windows.Forms.Label labelChooseEncoding;
        private System.Windows.Forms.Label labelEncodingDetectedLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.RadioButton radioButtonUtf8;
        private System.Windows.Forms.RadioButton radioButtonLegacy;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.TextBox textBoxUtf8Label;
        private System.Windows.Forms.TextBox textBoxCp1252Label;
        private System.Windows.Forms.TextBox textBoxUtf8;
        private System.Windows.Forms.TextBox textBoxCp1252;

    }
}