using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Palaso.Reporting;
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
                
            try
            {
                var moveMarkers = _moveUpMarkers.Text.SplitTrimmed(',');
                var rootMarkers = _moveUpRoots.Text.SplitTrimmed(',');
                if (moveMarkers.Count > 0 && rootMarkers.Count > 0)
                {
                    _fixer.MoveCommonItemsUp(rootMarkers, moveMarkers);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    ErrorReport.NotifyUserOfProblem("Is a one of the fields empty?");
                }
            }
            catch(Exception error)
            {
                Palaso.Reporting.ErrorReport.ReportNonFatalException(error);
            }
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



        private void OnPremadeLabelClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Label l = (Label) sender;
            var x = l.Text.Replace(") to under (", "|");
            x = x.Replace("(", "");
            x = x.Replace(")", "");
            var z = x.Split(new char[] {'|'});
            _moveUpMarkers.Text = z[0];
            _moveUpRoots.Text = z[1];
        }
    }
}
