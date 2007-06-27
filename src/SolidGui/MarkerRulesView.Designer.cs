namespace SolidGui
{
    partial class MarkerRulesView
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
            this._saveButton = new System.Windows.Forms.Button();
            this._markerComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._ruleNameTextBox = new System.Windows.Forms.TextBox();
            this._yesRadioButton = new System.Windows.Forms.RadioButton();
            this._noRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(120, 217);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(77, 29);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _markerComboBox
            // 
            this._markerComboBox.FormattingEnabled = true;
            this._markerComboBox.Location = new System.Drawing.Point(81, 75);
            this._markerComboBox.Name = "_markerComboBox";
            this._markerComboBox.Size = new System.Drawing.Size(116, 21);
            this._markerComboBox.TabIndex = 1;
            this._markerComboBox.SelectedIndexChanged += new System.EventHandler(this._markerComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Marker";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Rule Name";
            // 
            // _ruleNameTextBox
            // 
            this._ruleNameTextBox.Location = new System.Drawing.Point(81, 49);
            this._ruleNameTextBox.Name = "_ruleNameTextBox";
            this._ruleNameTextBox.Size = new System.Drawing.Size(116, 20);
            this._ruleNameTextBox.TabIndex = 5;
            // 
            // _yesRadioButton
            // 
            this._yesRadioButton.AutoSize = true;
            this._yesRadioButton.Location = new System.Drawing.Point(27, 20);
            this._yesRadioButton.Name = "_yesRadioButton";
            this._yesRadioButton.Size = new System.Drawing.Size(43, 17);
            this._yesRadioButton.TabIndex = 0;
            this._yesRadioButton.TabStop = true;
            this._yesRadioButton.Text = "Yes";
            this._yesRadioButton.UseVisualStyleBackColor = true;
            // 
            // _noRadioButton
            // 
            this._noRadioButton.AutoSize = true;
            this._noRadioButton.Checked = true;
            this._noRadioButton.Location = new System.Drawing.Point(105, 20);
            this._noRadioButton.Name = "_noRadioButton";
            this._noRadioButton.Size = new System.Drawing.Size(39, 17);
            this._noRadioButton.TabIndex = 1;
            this._noRadioButton.TabStop = true;
            this._noRadioButton.Text = "No";
            this._noRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._noRadioButton);
            this.groupBox1.Controls.Add(this._yesRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(17, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 47);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Marker Required";
            // 
            // MarkerRulesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._ruleNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._markerComboBox);
            this.Name = "MarkerRulesView";
            this.Size = new System.Drawing.Size(224, 264);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.ComboBox _markerComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _ruleNameTextBox;
        private System.Windows.Forms.RadioButton _yesRadioButton;
        private System.Windows.Forms.RadioButton _noRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
