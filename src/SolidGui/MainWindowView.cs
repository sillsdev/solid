// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

// TODO: Consider switching this MVP approach to the MVP approach used in WritingSystemsConfigPresenter
// That MVP is a nice middle ground; seems easier to understand/manage than all this event/listener wiring. -JMC Jan 2014

using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using System.Text;
using Palaso.Reporting;
using Solid.Engine;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.Model;
using SolidGui.Properties;
using SolidGui.Search;
using SolidGui.Setup;

namespace SolidGui
{
    /// <summary>
    /// View component of MainWindow. The logic is in the MainWindowPM.
    /// </summary>
    public partial class MainWindowView : Form
    {
        private MainWindowPM _mainWindowPM;  // / was readonly, but I think it s/b be hot-swappable (e.g. so we can roll back after cancelling a File Open, issue #1205) -JMC 2013-10
        private int _filterIndex;
        private SearchView _searchView;
        private FindReplaceDialog _searchView2;


        // No longer needed because it needs to be wired up from the start now. -JMC Mar 2014
/*
        private FindReplaceDialog CreateSearchView(MainWindowPM model, SfmEditorView sfmEditorView)
        {
            // Only creates it the first time; otherwise just gets it (almost like the old singleton). -JMC Feb 2014
            if (_searchView2 == null || _searchView2.IsDisposed)
            {
                _searchView2 = new FindReplaceDialog(sfmEditorView, model);
            }
             
            return _searchView2;
        }
*/

        public MainWindowView(MainWindowPM mainWindowPM)
        {
            
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }

            Initialize(mainWindowPM);

        }

        private void Initialize(MainWindowPM mainWindowPM)
        {
            this.KeyPreview = true;

            splitContainerLeftRight.Panel1.Enabled = false;
            splitContainerUpDown.Panel1.Enabled = false;
            splitContainerUpDown.Panel2.Enabled = false;
            //_sfmEditorView.Enabled = false;

            _mainWindowPM = mainWindowPM;
            _searchView2 = new FindReplaceDialog(_sfmEditorView, _mainWindowPM);
            _filterIndex = 0;

        }

        public void BindModels(MainWindowPM mainWindowPM) 
        {
            // cut any old wires first (does doing this make sense?)
            if (_mainWindowPM != null)
            {
                _mainWindowPM.DictionaryProcessed -= OnDictionaryProcessed;
                _mainWindowPM.SearchModel.WordFound -= OnWordFound;
                _mainWindowPM.SearchModel.SearchRecordFormatterChanged -= OnRecordFormatterChanged;
                _mainWindowPM.EditorRecordFormatterChanged -= _searchView2.OnEditorRecordFormatterChanged;
                _mainWindowPM.NavigatorModel.RecordChanged -= _sfmEditorView.OnRecordChanged;
            }

            _mainWindowPM = mainWindowPM;

            _mainWindowPM.DictionaryProcessed += OnDictionaryProcessed;
            _mainWindowPM.SearchModel.WordFound += OnWordFound;
            _mainWindowPM.SearchModel.SearchRecordFormatterChanged += OnRecordFormatterChanged;
            _mainWindowPM.EditorRecordFormatterChanged += _searchView2.OnEditorRecordFormatterChanged;
            _mainWindowPM.NavigatorModel.RecordChanged += _sfmEditorView.OnRecordChanged;

            _sfmEditorView.BindModel(_mainWindowPM);
            _recordNavigatorView.BindModel(_mainWindowPM.NavigatorModel);
            _filterChooserView.BindModel(_mainWindowPM.WarningFilterChooserModel);
            _markerSettingsListView.BindModel(_mainWindowPM.MarkerSettingsModel, _mainWindowPM.WorkingDictionary);  // added -JMC
            // _searchView.BindModel(_mainWindowPM);  // Started to add this, but it'll crash; better to do on first use -JMC
            //JMC: should prob also run MarkerSettingsDialog.BindModel() 
            // var test = new MarkerSettings.MarkerSettingsDialog(_mainWindowPM.MarkerSettingsModel, "");
            // But first, we'll need a private property for it, such as _markerSettingsDialog

            // _mainWindowPM.NavigatorModel.NavFilterChanged += _sfmEditorView.OnNavFilterChanged;

            // JMC: verify that the following += don't stack up after several File Open.
            _recordNavigatorView.RefreshButton.Click += _sfmEditorView.OnRefreshClicked;
            _refreshMenuItem.Click += _sfmEditorView.OnRefreshClicked;
            _recordNavigatorView.SearchButtonClicked += OnSearchClick;
            _markerSettingsListView.MarkerSettingPossiblyChanged += OnMarkerSettingPossiblyChanged;
            _sfmEditorView.RecordTextChanged += OnRecordTextChanged;
            _sfmEditorView.RecheckKeystroke += OnRecheckKeystroke;

        }

        private bool ReturnFalse()
        {
            return false;
        }

        public void OnDictionaryProcessed(object sender, EventArgs e)
        {
/* Moved this into BindModels()  -JMC 2013-10
            //wire up the change of record event to our record display widget
            _markerSettingsList.BindModel(
                _mainWindowPM.MarkerSettingsModel,
                _mainWindowPM.WorkingDictionary
            );
*/

            UpdateDisplay();
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            if (_mainWindowPM.needsSave)
            {
                DialogResult result = MessageBox.Show(this, "Changes may have been made to your current work. Before opening a new file would you like to save your work?", "Solid", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    OnSaveClick(sender, e);
                }
            }
            ChooseAndOpenProject();
            if (_mainWindowPM.Settings != null)
            {
                _mainWindowPM.Settings.NotifyIfNewMarkers();
                string msg = _mainWindowPM.Settings.NotifyIfMixedEncodings();
                if (msg != "")
                {
                    MessageBox.Show(msg, "Mixed Encodings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private string GetInitialDirectory()
        {
            string initialDirectory = null;
            if (!String.IsNullOrEmpty(Settings.Default.PreviousPathToDictionary))
            {
                try
                {
                    if (File.Exists(Settings.Default.PreviousPathToDictionary))
                    {
                        initialDirectory = Path.GetDirectoryName(Settings.Default.PreviousPathToDictionary);
                    }
                }
                catch
                {
                    //swallow
                }
            }

            if (initialDirectory == null || initialDirectory == "")
            {
                initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            return initialDirectory;
        }

        private void ChooseAndOpenProject()
        {
            string initialDirectory = GetInitialDirectory();

            var dlg = new OpenFileDialog();
            dlg.Title = "Open Dictionary File...";
            dlg.DefaultExt = ".db";
            dlg.FileName = Settings.Default.PreviousPathToDictionary;
            string exts = SolidSettings.extsAsString(SolidSettings.FileExtensions);
            string mask = SolidSettings.extsAsMask(SolidSettings.FileExtensions, "*");
            dlg.Filter = "SFM Lexicon (" + exts + ")|" + mask + "|All Files (*.*)|*.*";
            // the above produces something like this: "SFM Lexicon (.db .txt .lex .sfm)|*.db;*.txt;*.lex;*.sfm|All Files (*.*)|*.*" -JMC
            dlg.Multiselect = false;
            dlg.InitialDirectory = initialDirectory;
            if (DialogResult.OK != dlg.ShowDialog(this))
            {
                return; //they cancelled
            }

            Cursor = Cursors.WaitCursor;
            string templatePath = null;
            if (_mainWindowPM.ShouldAskForTemplateBeforeOpening(dlg.FileName))
            {
                templatePath = RequestTemplatePath(dlg.FileName, false);
                if (string.IsNullOrEmpty(templatePath))
                {
                    return; //they cancelled
                }
            }
            // At this point we s/b guaranteed to have a template file with a matching name. -JMC

            // Removed this extraneous save method, to clean up code and fix issue #1149 "Using Open Lexicon to switch files always saves currently open settings, even after choosing No." -JMC 2013-10
            // _mainWindowPM.SaveSettings(); 

            Open(dlg.FileName, templatePath);
        }

        private void Open(string fileName, string templatePath)
        {
            Cursor = Cursors.WaitCursor;

            // JMC:! starting here, we need to be able to roll the following back if the user cancels the File Open.
            // E.g. if they have an open file with unsaved data, its state should remain the same. (I.e. simply reloading the previous file from disk is not an adequate "rollback".)
            MainWindowPM origPm = _mainWindowPM;
            _mainWindowPM = new MainWindowPM();

            if (_mainWindowPM.OpenDictionary(fileName, templatePath))
            {
                Settings.Default.PreviousPathToDictionary = fileName;
                Settings.Default.Save(); //we want to remember this even if we don't get a clean shutdown later on. -JMC

                BindModels(_mainWindowPM);
                OnFileLoaded(fileName);
                setSaveEnabled(false); // This fixes issue #1213 (bogus "needs save" right after opening a second file, if the first file was not saved)
                _sfmEditorView.Enabled = false;  //We need to clearly toggle this off before on, or else the background may be gray -JMC
                _sfmEditorView.Enabled = true;   // (This issue only appeared after setting ShowSelectionMargin to True.)

                string ext = Path.GetExtension(fileName);
                if (!SolidSettings.FileExtensions.Contains(ext))
                {
                    SolidSettings.FileExtensions.Add(ext);
                    // adaptive: makes the next File Open dialog friendlier (during this run; lost on exit) -JMC
                }
                origPm.Dispose();  // in case any variables are still referencing the old file's PM, this might block those bugs. -JMC

                // JMC:! need to call something that's in _mainWindowPM.ProcessLexicon();
            }
            else
            {
                // File Open was cancelled or didn't succeed. Roll back.
                BindModels(origPm); // less destructive than Initialize(origPm);  -JMC
            }

            Cursor = Cursors.Default;
        }

        public void OnFileLoaded(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                Text = "Solid: " + name;                
            }
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            bool canProcess = _mainWindowPM.CanProcessLexicon;

            _filterChooserView.Enabled = canProcess;
            _changeWritingSystems.Enabled = canProcess;
            _changeTemplate.Enabled = canProcess;
            _exportButton.Enabled = canProcess;
            _recordNavigatorView.UpdateDisplay();
            _quickFixButton.Enabled = canProcess;
            setSaveEnabled(_mainWindowPM.needsSave);

            splitContainerLeftRight.Panel1.Enabled = canProcess;
            splitContainerUpDown.Panel1.Enabled = canProcess;
            splitContainerUpDown.Panel2.Enabled = canProcess;
            _sfmEditorView.Enabled = canProcess;  //JMC: but consider doing the following instead (see Open() too):
            //_sfmEditorView.ContentsBox.ReadOnly = !canProcess;

            _mainWindowPM.SfmEditorModel.MoveToFirst(); // Cheap way to "fix" #616 (and #274) -JMC 2013-09
            _mainWindowPM.NavigatorModel.StartupOrReset();
            _markerSettingsListView.UpdateDisplay();
            _filterChooserView.Model.Reset(); // _filterChooserView.Model.ActiveWarningFilter = _filterChooserView.Model.RecordFilters[0];  // Choose the "All Records" filter -JMC 2013-09
            _filterChooserView.UpdateDisplay(); // adding this helps the lower left pane...
            _sfmEditorView.Focus();
            _sfmEditorView.Reload();  // JMC:! ...but this doesn't help with the right pane
            // Recheck(); // JMC:! ...so this is a temporary total hack (an extra recheck is expensive!)
            _sfmEditorView.ContentsBox.Focus(); // possibly redundant -JMC
        
        }

        private void MainWindowView_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

        }

        // just a convenience method
        private void setSaveEnabled(bool val)
        {
            _saveMenuItem.Enabled = _saveButton.Enabled = _mainWindowPM.needsSave = val;
            _mainWindowPM.needsSave = val;
        }

        private void OnRecordTextChanged(object sender, EventArgs e)
        {
            setSaveEnabled(true);
        }

        private void OnMarkerSettingPossiblyChanged(object sender, EventArgs e)
        {
            if (_mainWindowPM.needsSave)
            {
                setSaveEnabled(true);
                //_sfmEditorView.UpdateBoth(); // Using UpdateView() alone was losing edits. However, perhaps neither is necessary now thanks to ContentsBox_Leave() -JMC
                // _sfmEditorView.UpdateView(); //JMC: was .OnSolidSettingsChange();
                this.UpdateDisplay();
            }
        }

        private void OnRecheckButtonClick(object sender, EventArgs e)
        {
            Recheck();
        }

        private void OnRecheckKeystroke(object sender, EventArgs e)
        {
            Recheck();
        }

        public void Recheck()
        {
            Cursor = Cursors.WaitCursor;
            _sfmEditorView.UpdateModel();
            _mainWindowPM.ProcessLexicon();
            _sfmEditorView.HighlightMarkers = _mainWindowPM.NavigatorModel.ActiveFilter.HighlightMarkers;

            //_mainWindowPM.NavigatorModel.SendNavFilterChangedEvent();  // Added this so the left panes' selection would reset -JMC 2013-10
            _sfmEditorView.Reload();
            Cursor = Cursors.Default;
        }


        private void OnSaveClick(object sender, EventArgs e) // (this works for Ctrl+S too) -JMC
        {
            _sfmEditorView.UpdateModel();
            if (_mainWindowPM.DictionaryAndSettingsSave())
            {
                setSaveEnabled(false);
            }
            _sfmEditorView.Reload();  // we want to see the data (indentation) the way Solid does -JMC
            _sfmEditorView.ContentsBox.Focus();  // just in case; probably redundant -JMC
        }

        // Currently is a "Save Copy As..." rather than a true "Save As..." -JMC
        private void OnSaveAsClick(object sender, EventArgs e)
        {
            SaveACopy();
        }

        private void SaveACopy()
        {
            //JMC!: insert code here to present various save options first (or just after the file chooser?).
            // The options dialog will return a RecordFormatter with the selected options.
            // We'll pass that as an argument.
            var rf = new RecordFormatter();
            rf.SetDefaultsDisk();
            /*
            rf.IndentSpaces = 4;
            rf.ShowIndented = true;
            rf.ShowClosingTags = true;
             */ 

            var optionsDialog = new SaveOptionsDialog();
            // optionsDialog.WarnAboutClosers = false; //only do this if there are no structural errors detected. -JMC
            var result = optionsDialog.ShowDialog(this);
            if (DialogResult.OK != result)
            {
                return; // user cancelled
            }
            rf = SaveOptionsDialog.ShortTermMemory;

            string s = _mainWindowPM.DictionaryRealFilePath;
            string initialDirectory = Path.GetDirectoryName(s);
            string initialFilename = Path.GetFileName(s);
            string initialExt = Path.GetExtension(s);


            var dlg = new SaveFileDialog();
            dlg.Title = "Save As...";
            dlg.DefaultExt = initialExt;
            dlg.FileName = initialFilename;
            //dlg.Filter = "SFM Lexicon (" + exts + ")|" + mask + "|All Files (*.*)|*.*";
            dlg.InitialDirectory = initialDirectory;
            if (DialogResult.OK != dlg.ShowDialog(this))
            {
                return; // user cancelled
            }

            //JMC: If we save A.txt as B.txt and B.solid already exists, the following will silently overwrite B.solid
            // That's usually the right thing to do, but we could check, and provide a confirmation dialog here if needed, and bail on Cancel.

            if (_mainWindowPM.DictionaryAndSettingsSaveAs(dlg.FileName, rf));
            {
                setSaveEnabled(false);
                //JMC: For true Save As, we'd need to switch over to the other file. And to be safe, just reload?

            }
            _sfmEditorView.Reload();  // we want to see the data (indentation) the way Solid does -JMC
            _sfmEditorView.ContentsBox.Focus();  // just in case; probably redundant -JMC
        }


        private void OnRecordFormatterChanged(object sender, RecordFormatterChangedEventArgs e)
        {
            _mainWindowPM.SyncFormat(e.NewFormatter);
            UpdateDisplay();
        }


        private void OnWordFound(object sender, SearchViewModel.SearchResultEventArgs e)
        {
            if (e.SearchResult.Filter != _mainWindowPM.WarningFilterChooserModel.ActiveWarningFilter)
            {
                //From Find, the user switched from a specific filter to All Records, so update the UI to match
                _mainWindowPM.WarningFilterChooserModel.ActiveWarningFilter = e.SearchResult.Filter;
            }
            _mainWindowPM.NavigatorModel.CurrentRecordIndex = e.SearchResult.RecordIndex;
            _recordNavigatorView.UpdateDisplay();
            _sfmEditorView.Highlight(e.SearchResult.TextIndex, e.SearchResult.Found.Length);
        }

        private void OnAboutBoxButton_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
            box.Dispose();
        }

        private void MainWindowView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_mainWindowPM.needsSave) 
            {
                DialogResult answer = MessageBox.Show("Save changes before quitting?", "Solid: Save first?", MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question);
                if (answer == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (answer == System.Windows.Forms.DialogResult.Yes)
                    {
                        OnSaveClick(this,null);
                    }
                    else if (answer == System.Windows.Forms.DialogResult.No)
                    {
                        //do nothing
                    }
                    if (_searchView != null) { _searchView.Dispose(); }  // this may be helpful, now that cancel Find only hides rather than closing. -JMC
                    if (_searchView2 != null) { _searchView2.Dispose(); }  // this may be helpful, now that cancel Find only hides rather than closing. -JMC
                }
            }
        }

        private void OnChangeTemplate_Click(object sender, EventArgs e)
        {
            string path = RequestTemplatePath(_mainWindowPM.PathToCurrentDictionary, true);
            if(!String.IsNullOrEmpty(path))
            {
                //JMC:!! Don't we need to save first? I.e. prob need to call SfmEditorView.UpdateModel() to be safe
                _sfmEditorView.UpdateModel();
                _mainWindowPM.UseSolidSettingsTemplate(path);
                //JMC:!! Don't we need to do our on-Open checking now (i.e. for new markers and mixed encodings)?
            }
        }

        public string RequestTemplatePath(string dictionaryPath, bool wouldBeReplacingExistingSettings)  //Made public for the sake of Program.cs -JMC
        {
            TemplateChooser chooser = new TemplateChooser(_mainWindowPM.Settings);
            chooser.CustomizedSolidDestinationName = Path.GetFileName(SolidSettings.GetSettingsFilePathFromDictionaryPath(dictionaryPath));
            chooser.TemplatePaths = _mainWindowPM.TemplatePaths;
            chooser.WouldBeReplacingExistingSettings = wouldBeReplacingExistingSettings;
            chooser.ShowDialog();
            if (chooser.DialogResult == DialogResult.OK && chooser.PathToChosenTemplate != _mainWindowPM.PathToCurrentSolidSettingsFile)
            {
               return chooser.PathToChosenTemplate;
            }
            return null;
        }

        private void OnSearchClick(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            /* // Old dialog: 
            _searchView = SearchView.CreateSearchView(_mainWindowPM, _sfmEditorView);
            _searchView.TopMost = true; // means that this form should always be in front of all others
            _searchView.Show();
            _searchView.Focus();*/ 

            //JMC: New dialog: -JMC Feb 2014
            //_searchView2 = CreateSearchView(_mainWindowPM, _sfmEditorView);  //dialog is no longer a singleton
            _searchView2.TopMost = true; // means that this form should always be in front of all others
            _searchView2.SelectFind();
            _searchView2.Show();
            _searchView2.Focus();
            _searchView2.ShowHelp();
        }

        private void MainWindowView_KeyDown(object sender, KeyEventArgs e)
        {

        }

        // These keystrokes are mostly redundant now that I've underlined button letters, but I'm leaving them in in case anyone's used to them. -JMC 2013-10
        private void MainWindowView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)  //ctrl+f
            {
                OnSearchClick(this, EventArgs.Empty);
            }
            if (e.Control && e.KeyCode == Keys.O)  //ctrl+o
            {
                OnOpenClick(this, EventArgs.Empty);
            }
            if (e.Control && e.KeyCode == Keys.S)  //ctrl+s
            {
                if (_mainWindowPM.needsSave)
                {
                    OnSaveClick(this, EventArgs.Empty);
                }
            }
            if (e.Control && e.KeyCode == Keys.R)
            {
                OnRecheckButtonClick(this, EventArgs.Empty);
            }
        }

        public string ExportFilterString()
        {
            StringBuilder builder = new StringBuilder();
            ExportFactory f = ExportFactory.Singleton();
            foreach (ExportHeader header in f.ExportSettings)
            {
                if (builder.Length > 0)
                {
                    builder.Append("|");
                }
                builder.Append(header.FileNameFilter);
            }
            return builder.ToString();
        }

        private void OnExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Export As LIFT (Experimental)";
            saveDialog.AddExtension = true;
            saveDialog.Filter = ExportFilterString();
            saveDialog.FileName = Path.GetFileNameWithoutExtension(_mainWindowPM.DictionaryRealFilePath);
            saveDialog.FilterIndex = _filterIndex;
            if (DialogResult.OK != saveDialog.ShowDialog(this))
            {
                return;
            }
            _filterIndex = saveDialog.FilterIndex;
            string destinationFilePath = saveDialog.FileName;

            try
            {
                
                _mainWindowPM.Export(saveDialog.FilterIndex - 1, destinationFilePath);
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                if (exception.InnerException != null)
                {
                    message += exception.InnerException.Message;
                }
                MessageBox.Show(this, message, "Solid Export Error");
            }
        }

        private void OnEditMarkerPropertiesClick(object sender, EventArgs e)
        {
            if (_markerSettingsListView.OpenSettingsDialog(null))
            {
                UpdateDisplay();
            }
        }

        private void OnQuickFix(object sender, EventArgs e)
        {
            QuickFixer fixer = new QuickFixer(_mainWindowPM.WorkingDictionary);
            var dlg = new QuickFixForm(fixer);
            if (dlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            _mainWindowPM.ProcessLexicon(); 
            _sfmEditorView.Reload();
            setSaveEnabled(true);
        }

        private void OnChangeWritingSystems_Click(object sender, EventArgs e)
        {
            var dialog = new WritingSystemsConfigDialog();
            var presenter = new WritingSystemsConfigPresenter(_mainWindowPM, AppWritingSystems.WritingSystems, dialog.WritingSystemsConfigView);
            DialogResult result = dialog.ShowDialog(this);
            _markerSettingsListView.UpdateDisplay(); // TODO this is quite heavy handed. Make an UpdateWritingSystems, or notify off solid settings better. CP 2012-02
            _markerSettingsListView.Refresh();
            UpdateDisplay();
        }

        private void reportAProblemsuggestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // (#249) I added this ability to trigger the yellow (non-fatal) Palaso error report. -JMC 2013-10
            var tmp = new Palaso.UI.WindowsForms.Reporting.WinFormsErrorReporter();
            tmp.ReportNonFatalException(new Exception("I would like to make a suggestion."), new ShowAlwaysPolicy());
        }

        private void _goFirstMenuItem_Click(object sender, EventArgs e)
        {
            _mainWindowPM.NavigatorModel.MoveToFirst();
        }

        private void _goLastMenuItem_Click(object sender, EventArgs e)
        {
            _mainWindowPM.NavigatorModel.MoveToLast();
        }

        private void _goPreviousMenuItem_Click(object sender, EventArgs e)
        {
            _mainWindowPM.NavigatorModel.MoveToPrevious();
        }

        private void _goNextMenuItem_Click(object sender, EventArgs e)
        {
            _mainWindowPM.NavigatorModel.MoveToNext();
        }

        private void _findMenuItem_Click(object sender, EventArgs e)
        {
            _recordNavigatorView.Find();
        }

        private void _exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _copyMenuItem_Click(object sender, EventArgs e)
        {
            string x = _sfmEditorView.ContentsBox.SelectedText;
            // unfinished
           
        }

        private void _openHelpMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Solid Documentation.pdf");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "File not found");
            }
        }

        private void MainWindowView_Deactivate(object sender, EventArgs e)
        {
            if (_sfmEditorView.IsDirty)
            {
                _sfmEditorView.UpdateBoth();
            }
        }

        private void buttonTree_Click(object sender, EventArgs e)
        {
            //JMC: Stub for feature #255
            // Probably simplest to just generate a plain text report in a (copiable) text box,
            // or in a MessageBox and copied to the clipboard
            // Note: some markers would show up multiple times in the hierarchy (once per parent),
            /* 
Example (of plain-text report):
lx
. ps (ii)
. . sn (ii)
. . . ge (+sn, i)
. . . de (+sn, i)
. . . re (ii)
. se (i_i)
. . ps (ii) .
. . et (i)
. . . ec (i)
. seco (i_i)
. . ps (ii) .
. . et (i) .
. et (i) .
. dt

Notes:
- that fields that can infer a parent (ge, de above) should probably be listed first. 
- The second occurrence of a parent marker would not show all the children again. 
             */
        }

        private void toolStripSplitOnSemicolon(object sender, EventArgs e)
        {
            SplitOnSemicolon();
        }
        private void SplitOnSemicolon()
        {
            RegexItem r = RegexItem.GetSplitOnSemicolon();
            _searchView2.SetFields(r);
            Search();
        }

        private void trimSpacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unwrap();
        }
        private void Unwrap()
        {
            RegexItem reg = RegexItem.GetUnwrap();
            _searchView2.SetFields(reg);
            Search();
        }

        private void _globallyDeleteFieldsMenuItem_Click(object sender, EventArgs e)
        {
            RegexItem reg = RegexItem.GetDeleteFields();
            _searchView2.SetFields(reg);
            Search();
        }



    }
}
