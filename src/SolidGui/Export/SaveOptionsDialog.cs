using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolidGui.Model;

namespace SolidGui.Export
{
    public partial class SaveOptionsDialog : Form
    {
        public bool WarnAboutClosers = true;

        public static RecordFormatter ShortTermMemory;

        private void Memorize()
        {
            var rf = new RecordFormatter();
            rf.ShowIndented = this.checkBoxIndent.Checked;
            rf.IndentSpaces = (int)this.numSpaces.Value;
            rf.ShowInferred = this.checkBoxInferred.Checked;
            rf.ShowClosingTags = this.checkBoxClosers.Checked;
            rf.NewLine = (this.checkBoxLinuxNewline.Checked) ? "\n" : "\r\n";
            rf.Separator = (this.radioButtonTab.Checked) ? "\t" : " ";
            ShortTermMemory = rf;
        }

        private void RecallSettings()
        {
            RecordFormatter rf = ShortTermMemory;
            if (rf != null)
            {
                checkBoxIndent.Checked = rf.ShowIndented;
                numSpaces.Value = (decimal)rf.IndentSpaces;
                checkBoxInferred.Checked = rf.ShowInferred;
                checkBoxClosers.Checked = rf.ShowClosingTags;
                checkBoxLinuxNewline.Checked = (rf.NewLine == "\n");
                bool spc = (rf.Separator == " ");
                radioButtonSpace.Checked = spc;
                radioButtonTab.Checked = !spc;
            }
            else
            {
                ShortTermMemory = new RecordFormatter();
                ShortTermMemory.SetDefaultsDisk();
            }

        }

        public SaveOptionsDialog()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Memorize();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveOptionsDialog_Load(object sender, EventArgs e)
        {
            numSpaces.Value = RecordFormatter.IndentWidth;
            labelClosersWarning.Visible = false;
            RecallSettings();
            ShowWarningMaybe();
        }

        private void OptionsChanged(object sender, EventArgs e)
        {
            ShowWarningMaybe();
        }

        private void ShowWarningMaybe()
        {
            bool problematic = checkBoxClosers.Checked || checkBoxIndent.Checked || checkBoxInferred.Checked;
            labelClosersWarning.Visible = WarnAboutClosers && problematic;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            //Close();

        }

    }
}
