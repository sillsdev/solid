using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolidGui.Engine;

namespace SolidGui
{
    public partial class DataValuesDialog : Form
    {
        public DataValuesDialog(MainWindowPM mwp)
        {
            InitializeComponent();
            if (DesignMode) return;

            _mainWindowPm = mwp;
        }

        private MainWindowPM _mainWindowPm;

        private void _runButton_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void Run()
        {
            if (String.IsNullOrEmpty(_markersTextBox.Text.Trim())) return;

            int max = (int)maxNumericUpDown.Value;

            var markers = new HashSet<string>();
            if (!String.IsNullOrEmpty(_markersTextBox.Text))
            {
                markers.UnionWith(_markersTextBox.Text.Split(' '));
            }
            _reportTextBox.Text = "";

            var sb = new StringBuilder();

            foreach (var marker in markers)
            {
                sb.AppendLine(marker + " marker has these values:");
                var values = _mainWindowPm.WorkingDictionary.GetAllDataValues(marker, max, _mainWindowPm.Settings);
                int c = 0;
                foreach (var v in values)
                {
                    sb.AppendLine("  " + v);
                    if (++c >= max) break;
                }
                sb.AppendLine("");
            }
            _reportTextBox.Text = sb.ToString();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _copyAllButton_Click(object sender, EventArgs e)
        {
            string s = _reportTextBox.Text;
            Clipboard.SetText(s);
        }
    }
}
