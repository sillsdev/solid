namespace SolidGui
{
    partial class DataValuesDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._closeButton = new System.Windows.Forms.Button();
            this._copyAllButton = new System.Windows.Forms.Button();
            this._runButton = new System.Windows.Forms.Button();
            this._markersTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._maxLabel = new System.Windows.Forms.Label();
            this.maxNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._reportTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._markersTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._maxLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.maxNumericUpDown, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._reportTextBox, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 431);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Controls.Add(this._copyAllButton);
            this.flowLayoutPanel1.Controls.Add(this._runButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 399);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(354, 29);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _closeButton
            // 
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._closeButton.Location = new System.Drawing.Point(276, 3);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 9;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _copyAllButton
            // 
            this._copyAllButton.Location = new System.Drawing.Point(195, 3);
            this._copyAllButton.Name = "_copyAllButton";
            this._copyAllButton.Size = new System.Drawing.Size(75, 23);
            this._copyAllButton.TabIndex = 8;
            this._copyAllButton.Text = "&Copy All";
            this._copyAllButton.UseVisualStyleBackColor = true;
            this._copyAllButton.Click += new System.EventHandler(this._copyAllButton_Click);
            // 
            // _runButton
            // 
            this._runButton.Location = new System.Drawing.Point(114, 3);
            this._runButton.Name = "_runButton";
            this._runButton.Size = new System.Drawing.Size(75, 23);
            this._runButton.TabIndex = 7;
            this._runButton.Text = "&Run";
            this._runButton.UseVisualStyleBackColor = true;
            this._runButton.Click += new System.EventHandler(this._runButton_Click);
            // 
            // _markersTextBox
            // 
            this._markersTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._markersTextBox.Location = new System.Drawing.Point(212, 13);
            this._markersTextBox.Name = "_markersTextBox";
            this._markersTextBox.Size = new System.Drawing.Size(145, 20);
            this._markersTextBox.TabIndex = 3;
            this._markersTextBox.Text = "ps pn lf sn hm pdl un ue";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Restricted-value &field(s) to tally up";
            // 
            // _maxLabel
            // 
            this._maxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._maxLabel.AutoSize = true;
            this._maxLabel.Location = new System.Drawing.Point(73, 45);
            this._maxLabel.Name = "_maxLabel";
            this._maxLabel.Size = new System.Drawing.Size(133, 13);
            this._maxLabel.TabIndex = 4;
            this._maxLabel.Text = "&Maximum to report per field";
            // 
            // maxNumericUpDown
            // 
            this.maxNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.maxNumericUpDown.Location = new System.Drawing.Point(212, 48);
            this.maxNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.maxNumericUpDown.Name = "maxNumericUpDown";
            this.maxNumericUpDown.Size = new System.Drawing.Size(145, 20);
            this.maxNumericUpDown.TabIndex = 5;
            this.maxNumericUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // _reportTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._reportTextBox, 2);
            this._reportTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reportTextBox.Location = new System.Drawing.Point(3, 83);
            this._reportTextBox.Multiline = true;
            this._reportTextBox.Name = "_reportTextBox";
            this._reportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._reportTextBox.Size = new System.Drawing.Size(354, 310);
            this._reportTextBox.TabIndex = 6;
            // 
            // DataValuesDialog
            // 
            this.AcceptButton = this._runButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(360, 431);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DataValuesDialog";
            this.ShowIcon = false;
            this.Text = "Tallies of values in list-type fields";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maxNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.Button _copyAllButton;
        private System.Windows.Forms.Button _runButton;
        private System.Windows.Forms.TextBox _markersTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown maxNumericUpDown;
        private System.Windows.Forms.Label _maxLabel;
        private System.Windows.Forms.TextBox _reportTextBox;
    }
}