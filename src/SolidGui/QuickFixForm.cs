// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Palaso.Reporting;

using Palaso.Extensions;
using Solid.Engine;
using SolidGui.Model;

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

        private void OnExecuteAddGuids(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                UsageReporter.SendNavigationNotice("QuickFix/AddGuids");
                _fixer.AddGuids();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                Palaso.Reporting.ErrorReport.ReportNonFatalException(error);
            }
        }

        private void OnExecuteMoveUp(object sender, LinkLabelLinkClickedEventArgs e)
        {
                
            try
            {
                UsageReporter.SendNavigationNotice("QuickFix/MoveUp");
                List<string> moveMarkers = _moveUpMarkers.Text.SplitTrimmed(',');
                List<string> rootMarkers = _moveUpRoots.Text.SplitTrimmed(',');
                if (moveMarkers.Count > 0 && rootMarkers.Count > 0)
                {
                    _fixer.MoveCommonItemsUp(rootMarkers, moveMarkers, _minimalCheckBox.Checked);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    ErrorReport.NotifyUserOfProblem("Is one of the fields empty?");
                }
            }
            catch(Exception error)
            {
                string msg = "An unexpected error occurred:\r\n" + error.Message;
                Palaso.Reporting.ErrorReport.ReportFatalMessageWithStackTrace(msg, error); // since quick fixes modify data, I would think this s/b fatal (data in unknown state) -JMC Feb 2014 
            }
        }

        private void OnExecuteRemoveEmpty(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<string> markers = _removeEmptyMarkers.Text.SplitTrimmed(',');
            try
            {
                UsageReporter.SendNavigationNotice("QuickFix/RemoveEmtpy");
                _fixer.RemoveEmptyFields(markers);
            }
            catch (Exception error)
            {
                string msg = "An unexpected error occurred:\r\n" + error.Message;
                Palaso.Reporting.ErrorReport.ReportFatalMessageWithStackTrace(msg, error); // since quick fixes modify data, I would think this s/b fatal (data in unknown state) -JMC Feb 2014 
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void OnExecuteSaveInferred_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string s = _tbMakeRealMarkers.Text;
            List<string> tokens = s.SplitTrimmed(',');
            _fixer.MakeInferedMarkersReal(tokens);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }


        private void OnExecuteFLExFixes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(_makeInferedRealBox.Checked)
            {
                _fixer.MakeInferedMarkersReal(new List<string>(new[] { "sn"}));
            }
            if (_createReferredToItems.Checked)
            {
                UsageReporter.SendNavigationNotice("QuickFix/CreateReferredToItems");
                string log = _fixer.MakeEntriesForReferredItems(new List<string>(new []{"cf","sy","an"}));
                log = SfmFieldModel.AsUtf8(log);  //quick hack to work around #1231 -JMC Nov 2014
                string path = Path.GetTempFileName()+".txt";
                File.WriteAllText(path, log, Encoding.UTF8);
                log = _fixer.MakeEntriesForReferredItemsOfLv();
                File.AppendAllText(path, log, Encoding.UTF8);
                Process.Start(path);
                
            }

            if (_pushPsDownToSns.Checked)
            {
                UsageReporter.SendNavigationNotice("QuickFix/PushPOSDOwnToSense");
                string log = _fixer.PropagatePartOfSpeech();
                string path = Path.GetTempFileName() + ".txt";
                File.WriteAllText(path, log);
                Process.Start(path);

            } 
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();

        }

        private void OnPremadeLabelClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Label l = (Label)sender;
            string x = l.Text.Replace(") to under (", "|");
            x = x.Replace("(", "");
            x = x.Replace(")", "");
            string[] z = x.Split(new char[] { '|' });
            _moveUpMarkers.Text = z[0];
            _moveUpRoots.Text = z[1];
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                @"This will create new records as needed. They will have a \CheckMe field, so they are easy to find.  Solid will guess at a \ps for the new entry based on the current one, or use 'FIXME' if it hasn't encountered a ps by the time it hits the referring field. (FLEx importer will only connect the entry if some sense exists on the new entry).

\lf's which are followed by more than one \lv will be copied to the position just before each \lv, as required by FLEx.

FLEx will not do the connection based on \lx if the target also has an \lc.  Therefore, this fix will also change the refering field to match the \lc in the case where it matches an \lx but no \lc.

FLEx cannot handle more than one reference per line.  Therefore this fix will split them up,  such that \sy one,two  becomes \sy one followed by \sy two.

With out this FLEx  just generates errors, and it takes a lot of work to create each one by hand.");
        }

        private void _showPushPSInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(@"Many dictionary entries have a single \ps, followed by multiple senses which share that part of speech. FLEx import does not handle this.  This fix attempts to move \ps down under all subsequent \sn's which are lacking their own \ps.  Your Solid structure should have ps as a child of sn.");
        }

    }
}
