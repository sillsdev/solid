namespace SolidGui.Search
{
    partial class FindReplaceDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchView));
            this.groupBoxFindReplace = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelMode = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonModeBasic = new System.Windows.Forms.RadioButton();
            this.radioButtonDoubleRegex = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBoxFindContext = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelFindContext = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanelFindReplace = new System.Windows.Forms.TableLayoutPanel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxFindReplace.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.flowLayoutPanelSettings.SuspendLayout();
            this.flowLayoutPanelOptions.SuspendLayout();
            this.flowLayoutPanelMode.SuspendLayout();
            this.groupBoxFindContext.SuspendLayout();
            this.tableLayoutPanelFindContext.SuspendLayout();
            this.tableLayoutPanelFindReplace.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFindReplace
            // 
            this.groupBoxFindReplace.Controls.Add(this.tableLayoutPanelFindReplace);
            this.groupBoxFindReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFindReplace.Location = new System.Drawing.Point(3, 239);
            this.groupBoxFindReplace.Name = "groupBoxFindReplace";
            this.groupBoxFindReplace.Size = new System.Drawing.Size(520, 151);
            this.groupBoxFindReplace.TabIndex = 19;
            this.groupBoxFindReplace.TabStop = false;
            this.groupBoxFindReplace.Text = "Replace";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(68, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "&Find";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(65, 26);
            this.button5.TabIndex = 7;
            this.button5.Text = "&Replace";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(74, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(89, 26);
            this.button6.TabIndex = 8;
            this.button6.Text = "Replace/Fi&nd";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.AutoSize = true;
            this.tableLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.flowLayoutPanelSettings, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxFindContext, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxFindReplace, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(526, 393);
            this.tableLayoutPanelMain.TabIndex = 20;
            // 
            // flowLayoutPanelSettings
            // 
            this.flowLayoutPanelSettings.Controls.Add(this.flowLayoutPanelMode);
            this.flowLayoutPanelSettings.Controls.Add(this.flowLayoutPanelOptions);
            this.flowLayoutPanelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSettings.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelSettings.Name = "flowLayoutPanelSettings";
            this.flowLayoutPanelSettings.Size = new System.Drawing.Size(520, 74);
            this.flowLayoutPanelSettings.TabIndex = 22;
            // 
            // flowLayoutPanelOptions
            // 
            this.flowLayoutPanelOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelOptions.Controls.Add(this.checkBox1);
            this.flowLayoutPanelOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelOptions.Location = new System.Drawing.Point(169, 3);
            this.flowLayoutPanelOptions.Name = "flowLayoutPanelOptions";
            this.flowLayoutPanelOptions.Size = new System.Drawing.Size(132, 66);
            this.flowLayoutPanelOptions.TabIndex = 23;
            // 
            // flowLayoutPanelMode
            // 
            this.flowLayoutPanelMode.Controls.Add(this.radioButtonModeBasic);
            this.flowLayoutPanelMode.Controls.Add(this.radioButtonDoubleRegex);
            this.flowLayoutPanelMode.Controls.Add(this.radioButton1);
            this.flowLayoutPanelMode.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelMode.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelMode.Name = "flowLayoutPanelMode";
            this.flowLayoutPanelMode.Size = new System.Drawing.Size(160, 76);
            this.flowLayoutPanelMode.TabIndex = 24;
            // 
            // radioButtonModeBasic
            // 
            this.radioButtonModeBasic.AutoSize = true;
            this.radioButtonModeBasic.Checked = true;
            this.radioButtonModeBasic.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonModeBasic.Location = new System.Drawing.Point(3, 3);
            this.radioButtonModeBasic.Name = "radioButtonModeBasic";
            this.radioButtonModeBasic.Size = new System.Drawing.Size(152, 17);
            this.radioButtonModeBasic.TabIndex = 1;
            this.radioButtonModeBasic.Text = "Basic mode";
            this.radioButtonModeBasic.UseVisualStyleBackColor = true;
            // 
            // radioButtonDoubleRegex
            // 
            this.radioButtonDoubleRegex.AutoSize = true;
            this.radioButtonDoubleRegex.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonDoubleRegex.Location = new System.Drawing.Point(3, 26);
            this.radioButtonDoubleRegex.Name = "radioButtonDoubleRegex";
            this.radioButtonDoubleRegex.Size = new System.Drawing.Size(152, 17);
            this.radioButtonDoubleRegex.TabIndex = 3;
            this.radioButtonDoubleRegex.Text = "Double Regular expression";
            this.radioButtonDoubleRegex.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButton1.Location = new System.Drawing.Point(3, 49);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(152, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Text = "Regular expression";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Case sensitive";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBoxFindContext
            // 
            this.groupBoxFindContext.Controls.Add(this.tableLayoutPanelFindContext);
            this.groupBoxFindContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFindContext.Location = new System.Drawing.Point(3, 83);
            this.groupBoxFindContext.Name = "groupBoxFindContext";
            this.groupBoxFindContext.Size = new System.Drawing.Size(520, 150);
            this.groupBoxFindContext.TabIndex = 23;
            this.groupBoxFindContext.TabStop = false;
            this.groupBoxFindContext.Text = "Step 1 (create context):";
            // 
            // tableLayoutPanelFindContext
            // 
            this.tableLayoutPanelFindContext.ColumnCount = 3;
            this.tableLayoutPanelFindContext.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelFindContext.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFindContext.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFindContext.Controls.Add(this.textBox2, 1, 1);
            this.tableLayoutPanelFindContext.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanelFindContext.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanelFindContext.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanelFindContext.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanelFindContext.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanelFindContext.Controls.Add(this.textBox5, 1, 2);
            this.tableLayoutPanelFindContext.Controls.Add(this.button2, 2, 1);
            this.tableLayoutPanelFindContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFindContext.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelFindContext.Name = "tableLayoutPanelFindContext";
            this.tableLayoutPanelFindContext.RowCount = 3;
            this.tableLayoutPanelFindContext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindContext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindContext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindContext.Size = new System.Drawing.Size(514, 131);
            this.tableLayoutPanelFindContext.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Replace with:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Find:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Preview (feeds step 2):";
            // 
            // textBox5
            // 
            this.textBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox5.Location = new System.Drawing.Point(203, 89);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox5.Size = new System.Drawing.Size(288, 39);
            this.textBox5.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(497, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(14, 20);
            this.button1.TabIndex = 27;
            this.button1.Text = ">";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(203, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(288, 37);
            this.textBox1.TabIndex = 28;
            this.textBox1.Text = "^\\\\(re) (.+)$  a";
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(203, 46);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(288, 37);
            this.textBox2.TabIndex = 29;
            this.textBox2.Text = "\\\\\\1 \\2";
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.Location = new System.Drawing.Point(497, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(14, 20);
            this.button2.TabIndex = 27;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelFindReplace
            // 
            this.tableLayoutPanelFindReplace.ColumnCount = 3;
            this.tableLayoutPanelFindReplace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelFindReplace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFindReplace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFindReplace.Controls.Add(this.textBox3, 1, 1);
            this.tableLayoutPanelFindReplace.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanelFindReplace.Controls.Add(this.button4, 0, 0);
            this.tableLayoutPanelFindReplace.Controls.Add(this.textBox4, 1, 0);
            this.tableLayoutPanelFindReplace.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanelFindReplace.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanelFindReplace.Controls.Add(this.textBox6, 1, 2);
            this.tableLayoutPanelFindReplace.Controls.Add(this.button7, 2, 1);
            this.tableLayoutPanelFindReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFindReplace.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelFindReplace.Name = "tableLayoutPanelFindReplace";
            this.tableLayoutPanelFindReplace.RowCount = 3;
            this.tableLayoutPanelFindReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindReplace.Size = new System.Drawing.Size(514, 132);
            this.tableLayoutPanelFindReplace.TabIndex = 18;
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(203, 46);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(288, 37);
            this.textBox3.TabIndex = 29;
            this.textBox3.Text = "\\\\\\1 \\2";
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button3.Location = new System.Drawing.Point(497, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(14, 20);
            this.button3.TabIndex = 27;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(203, 3);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox4.Size = new System.Drawing.Size(288, 37);
            this.textBox4.TabIndex = 28;
            this.textBox4.Text = "^\\\\(re) (.+)$  a";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Preview (result):";
            // 
            // textBox6
            // 
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox6.Location = new System.Drawing.Point(203, 89);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox6.Size = new System.Drawing.Size(288, 40);
            this.textBox6.TabIndex = 24;
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button7.Location = new System.Drawing.Point(497, 54);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(14, 20);
            this.button7.TabIndex = 27;
            this.button7.Text = ">";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button5);
            this.flowLayoutPanel1.Controls.Add(this.button6);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 43);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 43);
            this.flowLayoutPanel1.TabIndex = 25;
            // 
            // FindReplaceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 393);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MinimumSize = new System.Drawing.Size(403, 343);
            this.Name = "FindReplaceDialog";
            this.Text = "Find and Replace";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.groupBoxFindReplace.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.flowLayoutPanelSettings.ResumeLayout(false);
            this.flowLayoutPanelOptions.ResumeLayout(false);
            this.flowLayoutPanelOptions.PerformLayout();
            this.flowLayoutPanelMode.ResumeLayout(false);
            this.flowLayoutPanelMode.PerformLayout();
            this.groupBoxFindContext.ResumeLayout(false);
            this.tableLayoutPanelFindContext.ResumeLayout(false);
            this.tableLayoutPanelFindContext.PerformLayout();
            this.tableLayoutPanelFindReplace.ResumeLayout(false);
            this.tableLayoutPanelFindReplace.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFindReplace;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.GroupBox groupBoxFindContext;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFindContext;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSettings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMode;
        private System.Windows.Forms.RadioButton radioButtonModeBasic;
        private System.Windows.Forms.RadioButton radioButtonDoubleRegex;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOptions;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFindReplace;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
    }
}