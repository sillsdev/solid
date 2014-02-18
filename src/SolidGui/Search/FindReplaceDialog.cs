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

namespace SolidGui.Search
{
    public partial class FindReplaceDialog : Form
    {
        public FindReplaceDialog()
        {
            InitializeComponent();
            _scopeComboBox.SelectedIndex = 0;
        }


        private void radioButtonMode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDoubleRegex.Checked)
            {
                groupBoxFindContext.Enabled = true;
            }
            else
            {
                groupBoxFindContext.Enabled = false;
            }
        }

        private void FindReplaceDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Added this so that no matter which way the user 'closes' the dialog, it only hides. A cheap way to remember field contents (#326). -JMC Feb 2014
            this.Hide();
            e.Cancel = true;
        }

    }
}
