using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SolidEngine;
using Palaso.Extensions;

namespace SolidGui
{
    public partial class QuickFixForm : Form
    {
        private readonly QuickFixer _fixer;

        public QuickFixForm(QuickFixer fixer)
        {
            _fixer = fixer;
            InitializeComponent();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void OnExecuteMoveUp(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var markers = _moveUpMarkers.Text.SplitTrimmed(',');
            try
            {
                _fixer.MoveCommonItemsUp(markers);
            }
            catch(Exception error)
            {
                Palaso.Reporting.ErrorReport.ReportNonFatalException(error);
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

 
        private void OnExecuteRemoveEmpty(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var markers = _removeEmptyMarkers.Text.SplitTrimmed(',');
            try
            {
                _fixer.RemoveEmptyFields(markers);
            }
            catch (Exception error)
            {
                Palaso.Reporting.ErrorReport.ReportNonFatalException(error);
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
