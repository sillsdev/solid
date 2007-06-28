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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._ruleNameComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(120, 217);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(77, 29);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "&Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _markerComboBox
            // 
            this._markerComboBox.FormattingEnabled = true;
            this._markerComboBox.Location = new System.Drawing.Point(76, 50);
            this._markerComboBox.Name = "_markerComboBox";
            this._markerComboBox.Size = new System.Drawing.Size(116, 21);
            this._markerComboBox.TabIndex = 1;
            this._markerComboBox.SelectedIndexChanged += new System.EventHandler(this._markerComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Marker";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Rule Name";
            // 
            // _ruleNameTextBox
            // 
            this._ruleNameTextBox.Location = new System.Drawing.Point(76, 24);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 47);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Marker Required";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._ruleNameTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this._markerComboBox);
            this.groupBox2.Location = new System.Drawing.Point(5, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 153);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit Rule";
            // 
            // _ruleNameComboBox
            // 
            this._ruleNameComboBox.FormattingEnabled = true;
            this._ruleNameComboBox.Location = new System.Drawing.Point(82, 8);
            this._ruleNameComboBox.Name = "_ruleNameComboBox";
            this._ruleNameComboBox.Size = new System.Drawing.Size(130, 21);
            this._ruleNameComboBox.TabIndex = 7;
            this._ruleNameComboBox.SelectedIndexChanged += new System.EventHandler(this._ruleNameComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Rule";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 29);
            this.button1.TabIndex = 9;
            this.button1.Text = "&Remove";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MarkerRulesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._ruleNameComboBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._saveButton);
            this.Name = "MarkerRulesView";
            this.Size = new System.Drawing.Size(224, 264);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox _ruleNameComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}
