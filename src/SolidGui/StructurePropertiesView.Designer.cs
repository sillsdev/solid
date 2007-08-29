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
            this._onceRadioButton = new System.Windows.Forms.RadioButton();
            this._multipleTogetherRadioButton = new System.Windows.Forms.RadioButton();
            this._multipleApartRadioButton = new System.Windows.Forms.RadioButton();
            this._parentListView = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parent Marker";
            // 
            // _explanationLabel
            // 
            this._explanationLabel.AutoSize = true;
            this._explanationLabel.Location = new System.Drawing.Point(122, 66);
            this._explanationLabel.Name = "_explanationLabel";
            this._explanationLabel.Size = new System.Drawing.Size(121, 13);
            this._explanationLabel.TabIndex = 3;
            this._explanationLabel.Text = "Under lx, ge can appear";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "When no valid parent is present, ";
            // 
            // _InferComboBox
            // 
            this._InferComboBox.FormattingEnabled = true;
            this._InferComboBox.Items.AddRange(new object[] {
            "Report Error"});
            this._InferComboBox.Location = new System.Drawing.Point(192, 202);
            this._InferComboBox.Name = "_InferComboBox";
            this._InferComboBox.Size = new System.Drawing.Size(149, 21);
            this._InferComboBox.TabIndex = 7;
            this._InferComboBox.SelectedIndexChanged += new System.EventHandler(this._InferComboBox_SelectedIndexChanged);
            // 
            // _onceRadioButton
            // 
            this._onceRadioButton.AutoSize = true;
            this._onceRadioButton.Location = new System.Drawing.Point(125, 96);
            this._onceRadioButton.Name = "_onceRadioButton";
            this._onceRadioButton.Size = new System.Drawing.Size(51, 17);
            this._onceRadioButton.TabIndex = 2;
            this._onceRadioButton.TabStop = true;
            this._onceRadioButton.Text = "Once";
            this._onceRadioButton.UseVisualStyleBackColor = true;
            this._onceRadioButton.CheckedChanged += new System.EventHandler(this._aRadioButton_CheckedChanged);
            // 
            // _multipleTogetherRadioButton
            // 
            this._multipleTogetherRadioButton.AutoSize = true;
            this._multipleTogetherRadioButton.Location = new System.Drawing.Point(125, 118);
            this._multipleTogetherRadioButton.Name = "_multipleTogetherRadioButton";
            this._multipleTogetherRadioButton.Size = new System.Drawing.Size(152, 17);
            this._multipleTogetherRadioButton.TabIndex = 4;
            this._multipleTogetherRadioButton.TabStop = true;
            this._multipleTogetherRadioButton.Text = "One or more times together";
            this._multipleTogetherRadioButton.UseVisualStyleBackColor = true;
            this._multipleTogetherRadioButton.CheckedChanged += new System.EventHandler(this._aRadioButton_CheckedChanged);
            // 
            // _multipleApartRadioButton
            // 
            this._multipleApartRadioButton.AutoSize = true;
            this._multipleApartRadioButton.Location = new System.Drawing.Point(125, 141);
            this._multipleApartRadioButton.Name = "_multipleApartRadioButton";
            this._multipleApartRadioButton.Size = new System.Drawing.Size(227, 17);
            this._multipleApartRadioButton.TabIndex = 5;
            this._multipleApartRadioButton.TabStop = true;
            this._multipleApartRadioButton.Text = "One or more times with intervening markers";
            this._multipleApartRadioButton.UseVisualStyleBackColor = true;
            this._multipleApartRadioButton.CheckedChanged += new System.EventHandler(this._aRadioButton_CheckedChanged);
            // 
            // _parentListView
            // 
            this._parentListView.HideSelection = false;
            this._parentListView.LabelEdit = true;
            this._parentListView.Location = new System.Drawing.Point(20, 82);
            this._parentListView.Name = "_parentListView";
            this._parentListView.Size = new System.Drawing.Size(61, 89);
            this._parentListView.TabIndex = 8;
            this._parentListView.UseCompatibleStateImageBehavior = false;
            this._parentListView.View = System.Windows.Forms.View.SmallIcon;
            this._parentListView.SelectedIndexChanged += new System.EventHandler(this._parentListBox_SelectedIndexChanged);
            this._parentListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this._parentListView_MouseUp);
            this._parentListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this._parentListView_AfterLabelEdit);
            this._parentListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this._parentListView_KeyUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SolidGui.Properties.Resources.info2;
            this.pictureBox1.Location = new System.Drawing.Point(20, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(58, 9);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(293, 54);
            this.textBox1.TabIndex = 10;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Add parents of this marker in the \'Parent Marker\' box.  For each marker select ad" +
                "ditional constraints using the radio buttons.";
            // 
            // StructurePropertiesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._parentListView);
            this.Controls.Add(this._InferComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._multipleApartRadioButton);
            this.Controls.Add(this._multipleTogetherRadioButton);
            this.Controls.Add(this._explanationLabel);
            this.Controls.Add(this._onceRadioButton);
            this.Controls.Add(this.label1);
            this.Name = "StructurePropertiesView";
            this.Size = new System.Drawing.Size(356, 229);
            this.Load += new System.EventHandler(this.StructurePropertiesView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _explanationLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _InferComboBox;
        private System.Windows.Forms.RadioButton _onceRadioButton;
        private System.Windows.Forms.RadioButton _multipleTogetherRadioButton;
        private System.Windows.Forms.RadioButton _multipleApartRadioButton;
        private System.Windows.Forms.ListView _parentListView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}
