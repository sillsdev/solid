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
using Palaso.UI.WindowsForms.Progress;

namespace SolidGui.Export
{
    public partial class ExportLogDialog : Form
    {
        public ExportLogDialog()
        {
            InitializeComponent();
            _close.Enabled = false;
        }

        public LogBox LogBox { get { return _logBox; } }

        public ExportArguments Arguments { get; set; }

        private void _close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExportLogDialog_Load(object sender, EventArgs e)
        {

            BackgroundWorker.RunWorkerAsync(Arguments);
        }

        private void _updateDisplayTimer_Tick(object sender, EventArgs e)
        {
            _close.Enabled = !BackgroundWorker.IsBusy;
        }
    }
}
