namespace SolidGui
{
    partial class DataShapesDialog
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
            this.recipeButton = new System.Windows.Forms.Button();
            this._runButton = new System.Windows.Forms.Button();
            this._markersTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._reportListView = new System.Windows.Forms.ListView();
            this._markerColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._countColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._shapeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._RadiusLabel = new System.Windows.Forms.Label();
            this.beforeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.afterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beforeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.69287F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.30713F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._markersTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._reportListView, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._RadiusLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.beforeNumericUpDown, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.afterNumericUpDown, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(359, 353);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Controls.Add(this._copyAllButton);
            this.flowLayoutPanel1.Controls.Add(this.recipeButton);
            this.flowLayoutPanel1.Controls.Add(this._runButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 321);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(353, 29);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _closeButton
            // 
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._closeButton.Location = new System.Drawing.Point(275, 3);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 9;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _copyAllButton
            // 
            this._copyAllButton.Location = new System.Drawing.Point(194, 3);
            this._copyAllButton.Name = "_copyAllButton";
            this._copyAllButton.Size = new System.Drawing.Size(75, 23);
            this._copyAllButton.TabIndex = 8;
            this._copyAllButton.Text = "&Copy All";
            this._copyAllButton.UseVisualStyleBackColor = true;
            this._copyAllButton.Click += new System.EventHandler(this._copyAllButton_Click);
            // 
            // recipeButton
            // 
            this.recipeButton.Location = new System.Drawing.Point(113, 3);
            this.recipeButton.Name = "recipeButton";
            this.recipeButton.Size = new System.Drawing.Size(75, 23);
            this.recipeButton.TabIndex = 10;
            this.recipeButton.Text = "Reci&pe...";
            this.recipeButton.UseVisualStyleBackColor = true;
            this.recipeButton.Click += new System.EventHandler(this.recipeButton_Click);
            // 
            // _runButton
            // 
            this._runButton.Location = new System.Drawing.Point(32, 3);
            this._runButton.Name = "_runButton";
            this._runButton.Size = new System.Drawing.Size(75, 23);
            this._runButton.TabIndex = 7;
            this._runButton.Text = "&Run";
            this._runButton.UseVisualStyleBackColor = true;
            this._runButton.Click += new System.EventHandler(this._runButton_Click);
            // 
            // _markersTextBox
            // 
            this._markersTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._markersTextBox.Location = new System.Drawing.Point(271, 13);
            this._markersTextBox.Name = "_markersTextBox";
            this._markersTextBox.Size = new System.Drawing.Size(85, 20);
            this._markersTextBox.TabIndex = 3;
            this._markersTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._markersTextBox_Validating);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Marker(s) to analyze (leave blank for All)";
            // 
            // _reportListView
            // 
            this._reportListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._markerColumn,
            this._countColumn,
            this._shapeColumn});
            this.tableLayoutPanel1.SetColumnSpan(this._reportListView, 2);
            this._reportListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reportListView.Location = new System.Drawing.Point(3, 118);
            this._reportListView.MultiSelect = false;
            this._reportListView.Name = "_reportListView";
            this._reportListView.Size = new System.Drawing.Size(353, 197);
            this._reportListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._reportListView.TabIndex = 6;
            this._reportListView.UseCompatibleStateImageBehavior = false;
            this._reportListView.View = System.Windows.Forms.View.Details;
            this._reportListView.DoubleClick += new System.EventHandler(this._reportListView_DoubleClick);
            // 
            // _markerColumn
            // 
            this._markerColumn.Text = "Mkr";
            this._markerColumn.Width = 32;
            // 
            // _countColumn
            // 
            this._countColumn.Text = "Count";
            this._countColumn.Width = 30;
            // 
            // _shapeColumn
            // 
            this._shapeColumn.Text = "Shape";
            this._shapeColumn.Width = 277;
            // 
            // _RadiusLabel
            // 
            this._RadiusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._RadiusLabel.AutoSize = true;
            this._RadiusLabel.Location = new System.Drawing.Point(99, 45);
            this._RadiusLabel.Name = "_RadiusLabel";
            this._RadiusLabel.Size = new System.Drawing.Size(166, 13);
            this._RadiusLabel.TabIndex = 4;
            this._RadiusLabel.Text = "Ra&dius (lines above/below target)";
            // 
            // beforeNumericUpDown
            // 
            this.beforeNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.beforeNumericUpDown.Location = new System.Drawing.Point(271, 48);
            this.beforeNumericUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.beforeNumericUpDown.Name = "beforeNumericUpDown";
            this.beforeNumericUpDown.Size = new System.Drawing.Size(85, 20);
            this.beforeNumericUpDown.TabIndex = 5;
            this.beforeNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "For a custom find/replace recipe, double-click a row.";
            // 
            // afterNumericUpDown
            // 
            this.afterNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.afterNumericUpDown.Location = new System.Drawing.Point(271, 83);
            this.afterNumericUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.afterNumericUpDown.Name = "afterNumericUpDown";
            this.afterNumericUpDown.Size = new System.Drawing.Size(85, 20);
            this.afterNumericUpDown.TabIndex = 5;
            this.afterNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // DataShapesDialog
            // 
            this.AcceptButton = this._runButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(359, 353);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DataShapesDialog";
            this.ShowIcon = false;
            this.Text = "Data Shapes";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.beforeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button _copyAllButton;
        private System.Windows.Forms.Button _runButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.TextBox _markersTextBox;
        private System.Windows.Forms.Label _RadiusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown beforeNumericUpDown;
        private System.Windows.Forms.ListView _reportListView;
        private System.Windows.Forms.ColumnHeader _shapeColumn;
        private System.Windows.Forms.ColumnHeader _countColumn;
        private System.Windows.Forms.Button recipeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader _markerColumn;
        private System.Windows.Forms.NumericUpDown afterNumericUpDown;
    }
}