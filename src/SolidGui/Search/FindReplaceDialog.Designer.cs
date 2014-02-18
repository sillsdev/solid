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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindReplaceDialog));
            this.groupBoxFindReplace = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelFindReplace = new System.Windows.Forms.TableLayoutPanel();
            this.labelWarning = new System.Windows.Forms.Label();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.buttonHintReplace1 = new System.Windows.Forms.Button();
            this.buttonFind = new System.Windows.Forms.Button();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelReplaceButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonReplaceFind = new System.Windows.Forms.Button();
            this.textBoxReplacePreview = new System.Windows.Forms.TextBox();
            this.buttonHintReplace2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelPreview2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelMode = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonModeBasic = new System.Windows.Forms.RadioButton();
            this.radioButtonDoubleRegex = new System.Windows.Forms.RadioButton();
            this.radioButtonRegex = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanelOptions = new System.Windows.Forms.FlowLayoutPanel();
            this._scopeComboBox = new System.Windows.Forms.ComboBox();
            this.checkBoxCaseSensitive = new System.Windows.Forms.CheckBox();
            this.groupBoxFindContext = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelFindContext = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxContextReplace = new System.Windows.Forms.TextBox();
            this.buttonHintContext1 = new System.Windows.Forms.Button();
            this.labelReplace = new System.Windows.Forms.Label();
            this.textBoxContextFind = new System.Windows.Forms.TextBox();
            this.labelFind = new System.Windows.Forms.Label();
            this.labelPreview1 = new System.Windows.Forms.Label();
            this.textBoxContextPreview = new System.Windows.Forms.TextBox();
            this.buttonHintContext2 = new System.Windows.Forms.Button();
            this.groupBoxFindReplace.SuspendLayout();
            this.tableLayoutPanelFindReplace.SuspendLayout();
            this.flowLayoutPanelReplaceButtons.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.flowLayoutPanelSettings.SuspendLayout();
            this.flowLayoutPanelMode.SuspendLayout();
            this.flowLayoutPanelOptions.SuspendLayout();
            this.groupBoxFindContext.SuspendLayout();
            this.tableLayoutPanelFindContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFindReplace
            // 
            this.groupBoxFindReplace.Controls.Add(this.tableLayoutPanelFindReplace);
            this.groupBoxFindReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFindReplace.Location = new System.Drawing.Point(3, 231);
            this.groupBoxFindReplace.Name = "groupBoxFindReplace";
            this.groupBoxFindReplace.Size = new System.Drawing.Size(529, 177);
            this.groupBoxFindReplace.TabIndex = 19;
            this.groupBoxFindReplace.TabStop = false;
            this.groupBoxFindReplace.Text = "Replace";
            // 
            // tableLayoutPanelFindReplace
            // 
            this.tableLayoutPanelFindReplace.ColumnCount = 3;
            this.tableLayoutPanelFindReplace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelFindReplace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFindReplace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFindReplace.Controls.Add(this.labelWarning, 1, 3);
            this.tableLayoutPanelFindReplace.Controls.Add(this.textBoxReplace, 1, 1);
            this.tableLayoutPanelFindReplace.Controls.Add(this.buttonHintReplace1, 2, 0);
            this.tableLayoutPanelFindReplace.Controls.Add(this.buttonFind, 0, 0);
            this.tableLayoutPanelFindReplace.Controls.Add(this.textBoxFind, 1, 0);
            this.tableLayoutPanelFindReplace.Controls.Add(this.flowLayoutPanelReplaceButtons, 0, 1);
            this.tableLayoutPanelFindReplace.Controls.Add(this.textBoxReplacePreview, 1, 2);
            this.tableLayoutPanelFindReplace.Controls.Add(this.buttonHintReplace2, 2, 1);
            this.tableLayoutPanelFindReplace.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanelFindReplace.Controls.Add(this.button1, 0, 3);
            this.tableLayoutPanelFindReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFindReplace.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelFindReplace.Name = "tableLayoutPanelFindReplace";
            this.tableLayoutPanelFindReplace.RowCount = 4;
            this.tableLayoutPanelFindReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelFindReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelFindReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelFindReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelFindReplace.Size = new System.Drawing.Size(523, 158);
            this.tableLayoutPanelFindReplace.TabIndex = 18;
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.Location = new System.Drawing.Point(203, 120);
            this.labelWarning.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(292, 26);
            this.labelWarning.TabIndex = 32;
            this.labelWarning.Text = "Instead of applying per-record, this runs on the whole file (including inferred m" +
    "arkers) and can therefore merge entries. ";
            // 
            // textBoxReplace
            // 
            this.textBoxReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxReplace.Location = new System.Drawing.Point(203, 42);
            this.textBoxReplace.Multiline = true;
            this.textBoxReplace.Name = "textBoxReplace";
            this.textBoxReplace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReplace.Size = new System.Drawing.Size(297, 33);
            this.textBoxReplace.TabIndex = 11;
            this.textBoxReplace.Text = "\\r\\n\\\\re ";
            // 
            // buttonHintReplace1
            // 
            this.buttonHintReplace1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonHintReplace1.Location = new System.Drawing.Point(506, 9);
            this.buttonHintReplace1.Name = "buttonHintReplace1";
            this.buttonHintReplace1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.buttonHintReplace1.Size = new System.Drawing.Size(14, 20);
            this.buttonHintReplace1.TabIndex = 8;
            this.buttonHintReplace1.Text = ">";
            this.buttonHintReplace1.UseVisualStyleBackColor = true;
            // 
            // buttonFind
            // 
            this.buttonFind.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonFind.Location = new System.Drawing.Point(3, 8);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(68, 23);
            this.buttonFind.TabIndex = 6;
            this.buttonFind.Text = "&Find";
            this.buttonFind.UseVisualStyleBackColor = true;
            // 
            // textBoxFind
            // 
            this.textBoxFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFind.Location = new System.Drawing.Point(203, 3);
            this.textBoxFind.Multiline = true;
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFind.Size = new System.Drawing.Size(297, 33);
            this.textBoxFind.TabIndex = 7;
            this.textBoxFind.Text = "\\s*;\\s*";
            // 
            // flowLayoutPanelReplaceButtons
            // 
            this.flowLayoutPanelReplaceButtons.Controls.Add(this.buttonReplace);
            this.flowLayoutPanelReplaceButtons.Controls.Add(this.buttonReplaceFind);
            this.flowLayoutPanelReplaceButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelReplaceButtons.Location = new System.Drawing.Point(0, 39);
            this.flowLayoutPanelReplaceButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelReplaceButtons.Name = "flowLayoutPanelReplaceButtons";
            this.flowLayoutPanelReplaceButtons.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.flowLayoutPanelReplaceButtons.Size = new System.Drawing.Size(200, 39);
            this.flowLayoutPanelReplaceButtons.TabIndex = 25;
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(3, 6);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(65, 23);
            this.buttonReplace.TabIndex = 9;
            this.buttonReplace.Text = "&Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            // 
            // buttonReplaceFind
            // 
            this.buttonReplaceFind.Location = new System.Drawing.Point(74, 6);
            this.buttonReplaceFind.Name = "buttonReplaceFind";
            this.buttonReplaceFind.Size = new System.Drawing.Size(93, 23);
            this.buttonReplaceFind.TabIndex = 10;
            this.buttonReplaceFind.Text = "Replace + Fi&nd";
            this.buttonReplaceFind.UseVisualStyleBackColor = true;
            // 
            // textBoxReplacePreview
            // 
            this.textBoxReplacePreview.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxReplacePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxReplacePreview.Location = new System.Drawing.Point(203, 81);
            this.textBoxReplacePreview.Multiline = true;
            this.textBoxReplacePreview.Name = "textBoxReplacePreview";
            this.textBoxReplacePreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReplacePreview.Size = new System.Drawing.Size(297, 33);
            this.textBoxReplacePreview.TabIndex = 24;
            this.textBoxReplacePreview.Text = "\\re hum, to\r\n\\re sing, to\r\n\\re croon, to";
            // 
            // buttonHintReplace2
            // 
            this.buttonHintReplace2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonHintReplace2.Location = new System.Drawing.Point(506, 48);
            this.buttonHintReplace2.Name = "buttonHintReplace2";
            this.buttonHintReplace2.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.buttonHintReplace2.Size = new System.Drawing.Size(14, 20);
            this.buttonHintReplace2.TabIndex = 12;
            this.buttonHintReplace2.Text = ">";
            this.buttonHintReplace2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelPreview2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 81);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(194, 33);
            this.flowLayoutPanel1.TabIndex = 30;
            // 
            // labelPreview2
            // 
            this.labelPreview2.AutoSize = true;
            this.labelPreview2.Location = new System.Drawing.Point(3, 5);
            this.labelPreview2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelPreview2.Name = "labelPreview2";
            this.labelPreview2.Size = new System.Drawing.Size(82, 13);
            this.labelPreview2.TabIndex = 17;
            this.labelPreview2.Text = "Preview (result):";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Replace &All + Recheck";
            this.button1.UseVisualStyleBackColor = true;
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
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(535, 411);
            this.tableLayoutPanelMain.TabIndex = 20;
            // 
            // flowLayoutPanelSettings
            // 
            this.flowLayoutPanelSettings.Controls.Add(this.flowLayoutPanelMode);
            this.flowLayoutPanelSettings.Controls.Add(this.flowLayoutPanelOptions);
            this.flowLayoutPanelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSettings.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelSettings.Name = "flowLayoutPanelSettings";
            this.flowLayoutPanelSettings.Size = new System.Drawing.Size(529, 74);
            this.flowLayoutPanelSettings.TabIndex = 22;
            // 
            // flowLayoutPanelMode
            // 
            this.flowLayoutPanelMode.Controls.Add(this.radioButtonModeBasic);
            this.flowLayoutPanelMode.Controls.Add(this.radioButtonDoubleRegex);
            this.flowLayoutPanelMode.Controls.Add(this.radioButtonRegex);
            this.flowLayoutPanelMode.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelMode.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelMode.Name = "flowLayoutPanelMode";
            this.flowLayoutPanelMode.Size = new System.Drawing.Size(160, 76);
            this.flowLayoutPanelMode.TabIndex = 24;
            // 
            // radioButtonModeBasic
            // 
            this.radioButtonModeBasic.AutoSize = true;
            this.radioButtonModeBasic.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonModeBasic.Location = new System.Drawing.Point(3, 3);
            this.radioButtonModeBasic.Name = "radioButtonModeBasic";
            this.radioButtonModeBasic.Size = new System.Drawing.Size(152, 17);
            this.radioButtonModeBasic.TabIndex = 0;
            this.radioButtonModeBasic.Text = "Basic mode";
            this.radioButtonModeBasic.UseVisualStyleBackColor = true;
            this.radioButtonModeBasic.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonDoubleRegex
            // 
            this.radioButtonDoubleRegex.AutoSize = true;
            this.radioButtonDoubleRegex.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonDoubleRegex.Location = new System.Drawing.Point(3, 26);
            this.radioButtonDoubleRegex.Name = "radioButtonDoubleRegex";
            this.radioButtonDoubleRegex.Size = new System.Drawing.Size(152, 17);
            this.radioButtonDoubleRegex.TabIndex = 1;
            this.radioButtonDoubleRegex.Text = "Double Regular expression";
            this.radioButtonDoubleRegex.UseVisualStyleBackColor = true;
            this.radioButtonDoubleRegex.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonRegex
            // 
            this.radioButtonRegex.AutoSize = true;
            this.radioButtonRegex.Checked = true;
            this.radioButtonRegex.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonRegex.Location = new System.Drawing.Point(3, 49);
            this.radioButtonRegex.Name = "radioButtonRegex";
            this.radioButtonRegex.Size = new System.Drawing.Size(152, 17);
            this.radioButtonRegex.TabIndex = 2;
            this.radioButtonRegex.TabStop = true;
            this.radioButtonRegex.Text = "Regular expression";
            this.radioButtonRegex.UseVisualStyleBackColor = true;
            this.radioButtonRegex.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // flowLayoutPanelOptions
            // 
            this.flowLayoutPanelOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelOptions.AutoSize = true;
            this.flowLayoutPanelOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelOptions.Controls.Add(this._scopeComboBox);
            this.flowLayoutPanelOptions.Controls.Add(this.checkBoxCaseSensitive);
            this.flowLayoutPanelOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelOptions.Location = new System.Drawing.Point(169, 3);
            this.flowLayoutPanelOptions.MinimumSize = new System.Drawing.Size(132, 66);
            this.flowLayoutPanelOptions.Name = "flowLayoutPanelOptions";
            this.flowLayoutPanelOptions.Size = new System.Drawing.Size(132, 66);
            this.flowLayoutPanelOptions.TabIndex = 23;
            // 
            // _scopeComboBox
            // 
            this._scopeComboBox.FormattingEnabled = true;
            this._scopeComboBox.Items.AddRange(new object[] {
            "Current Filter",
            "Entire Dictionary"});
            this._scopeComboBox.Location = new System.Drawing.Point(3, 3);
            this._scopeComboBox.Name = "_scopeComboBox";
            this._scopeComboBox.Size = new System.Drawing.Size(116, 21);
            this._scopeComboBox.TabIndex = 3;
            // 
            // checkBoxCaseSensitive
            // 
            this.checkBoxCaseSensitive.AutoSize = true;
            this.checkBoxCaseSensitive.Location = new System.Drawing.Point(3, 30);
            this.checkBoxCaseSensitive.Name = "checkBoxCaseSensitive";
            this.checkBoxCaseSensitive.Size = new System.Drawing.Size(94, 17);
            this.checkBoxCaseSensitive.TabIndex = 4;
            this.checkBoxCaseSensitive.Text = "Case sensitive";
            this.checkBoxCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // groupBoxFindContext
            // 
            this.groupBoxFindContext.Controls.Add(this.tableLayoutPanelFindContext);
            this.groupBoxFindContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFindContext.Location = new System.Drawing.Point(3, 83);
            this.groupBoxFindContext.Name = "groupBoxFindContext";
            this.groupBoxFindContext.Size = new System.Drawing.Size(529, 142);
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
            this.tableLayoutPanelFindContext.Controls.Add(this.textBoxContextReplace, 1, 1);
            this.tableLayoutPanelFindContext.Controls.Add(this.buttonHintContext1, 2, 0);
            this.tableLayoutPanelFindContext.Controls.Add(this.labelReplace, 0, 1);
            this.tableLayoutPanelFindContext.Controls.Add(this.textBoxContextFind, 1, 0);
            this.tableLayoutPanelFindContext.Controls.Add(this.labelFind, 0, 0);
            this.tableLayoutPanelFindContext.Controls.Add(this.labelPreview1, 0, 2);
            this.tableLayoutPanelFindContext.Controls.Add(this.textBoxContextPreview, 1, 2);
            this.tableLayoutPanelFindContext.Controls.Add(this.buttonHintContext2, 2, 1);
            this.tableLayoutPanelFindContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFindContext.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelFindContext.Name = "tableLayoutPanelFindContext";
            this.tableLayoutPanelFindContext.RowCount = 3;
            this.tableLayoutPanelFindContext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindContext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindContext.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFindContext.Size = new System.Drawing.Size(523, 123);
            this.tableLayoutPanelFindContext.TabIndex = 99;
            // 
            // textBoxContextReplace
            // 
            this.textBoxContextReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxContextReplace.Location = new System.Drawing.Point(203, 44);
            this.textBoxContextReplace.Multiline = true;
            this.textBoxContextReplace.Name = "textBoxContextReplace";
            this.textBoxContextReplace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContextReplace.Size = new System.Drawing.Size(297, 35);
            this.textBoxContextReplace.TabIndex = 4;
            this.textBoxContextReplace.Text = "\\\\\\1 \\2";
            // 
            // buttonHintContext1
            // 
            this.buttonHintContext1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonHintContext1.Location = new System.Drawing.Point(506, 10);
            this.buttonHintContext1.Name = "buttonHintContext1";
            this.buttonHintContext1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.buttonHintContext1.Size = new System.Drawing.Size(14, 20);
            this.buttonHintContext1.TabIndex = 2;
            this.buttonHintContext1.Text = ">";
            this.buttonHintContext1.UseVisualStyleBackColor = true;
            // 
            // labelReplace
            // 
            this.labelReplace.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelReplace.AutoSize = true;
            this.labelReplace.Location = new System.Drawing.Point(3, 55);
            this.labelReplace.Name = "labelReplace";
            this.labelReplace.Size = new System.Drawing.Size(72, 13);
            this.labelReplace.TabIndex = 3;
            this.labelReplace.Text = "Replace &with:";
            // 
            // textBoxContextFind
            // 
            this.textBoxContextFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxContextFind.Location = new System.Drawing.Point(203, 3);
            this.textBoxContextFind.Multiline = true;
            this.textBoxContextFind.Name = "textBoxContextFind";
            this.textBoxContextFind.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContextFind.Size = new System.Drawing.Size(297, 35);
            this.textBoxContextFind.TabIndex = 1;
            this.textBoxContextFind.Text = "^\\\\(re) (.+)$";
            // 
            // labelFind
            // 
            this.labelFind.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFind.AutoSize = true;
            this.labelFind.Location = new System.Drawing.Point(3, 14);
            this.labelFind.Name = "labelFind";
            this.labelFind.Size = new System.Drawing.Size(30, 13);
            this.labelFind.TabIndex = 0;
            this.labelFind.Text = "Fin&d:";
            // 
            // labelPreview1
            // 
            this.labelPreview1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPreview1.AutoSize = true;
            this.labelPreview1.Location = new System.Drawing.Point(3, 96);
            this.labelPreview1.Name = "labelPreview1";
            this.labelPreview1.Size = new System.Drawing.Size(115, 13);
            this.labelPreview1.TabIndex = 17;
            this.labelPreview1.Text = "Preview (feeds step 2):";
            // 
            // textBoxContextPreview
            // 
            this.textBoxContextPreview.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxContextPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxContextPreview.Location = new System.Drawing.Point(203, 85);
            this.textBoxContextPreview.Multiline = true;
            this.textBoxContextPreview.Name = "textBoxContextPreview";
            this.textBoxContextPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContextPreview.Size = new System.Drawing.Size(297, 35);
            this.textBoxContextPreview.TabIndex = 24;
            this.textBoxContextPreview.Text = "\\re hum, to; sing, to; croon, to";
            // 
            // buttonHintContext2
            // 
            this.buttonHintContext2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonHintContext2.Location = new System.Drawing.Point(506, 51);
            this.buttonHintContext2.Name = "buttonHintContext2";
            this.buttonHintContext2.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.buttonHintContext2.Size = new System.Drawing.Size(14, 20);
            this.buttonHintContext2.TabIndex = 5;
            this.buttonHintContext2.Text = ">";
            this.buttonHintContext2.UseVisualStyleBackColor = true;
            // 
            // FindReplaceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 411);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(403, 383);
            this.Name = "FindReplaceDialog";
            this.Text = "Find and Replace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindReplaceDialog_FormClosing);
            this.groupBoxFindReplace.ResumeLayout(false);
            this.tableLayoutPanelFindReplace.ResumeLayout(false);
            this.tableLayoutPanelFindReplace.PerformLayout();
            this.flowLayoutPanelReplaceButtons.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.flowLayoutPanelSettings.ResumeLayout(false);
            this.flowLayoutPanelSettings.PerformLayout();
            this.flowLayoutPanelMode.ResumeLayout(false);
            this.flowLayoutPanelMode.PerformLayout();
            this.flowLayoutPanelOptions.ResumeLayout(false);
            this.flowLayoutPanelOptions.PerformLayout();
            this.groupBoxFindContext.ResumeLayout(false);
            this.tableLayoutPanelFindContext.ResumeLayout(false);
            this.tableLayoutPanelFindContext.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFindReplace;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonReplaceFind;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.GroupBox groupBoxFindContext;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFindContext;
        private System.Windows.Forms.Label labelReplace;
        private System.Windows.Forms.Label labelFind;
        private System.Windows.Forms.Label labelPreview1;
        private System.Windows.Forms.TextBox textBoxContextPreview;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSettings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMode;
        private System.Windows.Forms.RadioButton radioButtonModeBasic;
        private System.Windows.Forms.RadioButton radioButtonDoubleRegex;
        private System.Windows.Forms.RadioButton radioButtonRegex;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOptions;
        private System.Windows.Forms.CheckBox checkBoxCaseSensitive;
        private System.Windows.Forms.Button buttonHintContext1;
        private System.Windows.Forms.TextBox textBoxContextFind;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFindReplace;
        private System.Windows.Forms.TextBox textBoxReplace;
        private System.Windows.Forms.Button buttonHintReplace1;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelReplaceButtons;
        private System.Windows.Forms.Label labelPreview2;
        private System.Windows.Forms.TextBox textBoxReplacePreview;
        private System.Windows.Forms.Button buttonHintReplace2;
        private System.Windows.Forms.TextBox textBoxContextReplace;
        private System.Windows.Forms.Button buttonHintContext2;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox _scopeComboBox;
    }
}