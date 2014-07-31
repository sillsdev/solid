// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolidGui.Setup
{

    public partial class EncodingChooser : Form
    {
        public readonly string DictionaryPath;

        public EncodingChooser(string dictionaryPath)
        {
            DictionaryPath = dictionaryPath;
            InitializeComponent();
        }

        public void setAnalysis(bool utf8, bool cp1252)
        {
            textBoxUtf8Label.Text = String.Format(textBoxUtf8Label.Text, utf8);
            textBoxCp1252Label.Text = String.Format(textBoxCp1252Label.Text, cp1252);
            if (utf8)
            {
                radioButtonUtf8.Checked = true;
            }
            else
            {
                radioButtonLegacy.Checked = true;
            }
        }

        public void setPreviews(string utf8, string cp1252)
        {
            textBoxUtf8.Text = utf8;
            textBoxCp1252.Text = cp1252;
        }


        public bool choseUnicode()
        {
            return radioButtonUtf8.Checked;
        } 

        private void _okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;  // JMC: redundant?
            Close();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // JMC: redundant?
            Close();
        }

        // Utility function: pops up an instance of the dialog and gets the info
        public static bool UserWantsUnicode(string dictionaryPath)
        {
            bool cp1252 = EncodingChecker.CanBeReadAs(dictionaryPath, Encoding.GetEncoding(1252));
            bool utf8 = EncodingChecker.CanBeReadAs(dictionaryPath, Encoding.UTF8);
            string cp1252text = EncodingChecker.ReadLines(dictionaryPath, Encoding.GetEncoding(1252), 40);
            string utf8text = EncodingChecker.ReadLines(dictionaryPath, Encoding.UTF8, 40);

            var encChooser = new EncodingChooser(dictionaryPath);
            encChooser.setAnalysis(utf8, cp1252);
            encChooser.setPreviews(utf8text, cp1252text);
            encChooser.ShowDialog();
            if (encChooser.DialogResult == DialogResult.Cancel) return false;
            bool uni = encChooser.choseUnicode();
            return uni;
        }

    }
    

}
