// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

// TODO: Consider switching this MVP approach to the MVP approach used in WritingSystemsConfigPresenter
// That MVP is a nice middle ground; seems easier to understand/manage than all this event/listener wiring. -JMC Jan 2014

using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Text;
using SIL.IO;
using SIL.Reporting;
using SIL.Windows.Forms.Miscellaneous;
using Solid.Engine;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.MarkerSettings;
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
        private FindReplaceDialog _searchDialog;
        private DataShapesDialog _dataShapesDialog;

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

        /// <summary>
        /// Simulate getting a fresh UI, as if using new MainWindowView(_mainWindowPM); 
        /// 
        /// </summary>
        /// <param name="mainWindowPM"></param>
        private void ReInit(MainWindowPM mainWindowPM)
        {
            //InitializeComponent();  //this will break everything unless it all can be rewired
            Initialize(mainWindowPM); 

            BindModels(mainWindowPM);
        }

        public MainWindowView(MainWindowPM mainWindowPM)
        {
            
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }

            Initialize(mainWindowPM);

        }

        private void SetEnabledGeneral(bool enable)
        {
            splitContainerLeftRight.Panel1.Enabled = enable;
            splitContainerUpDown.Panel1.Enabled = enable;
            splitContainerUpDown.Panel2.Enabled = enable;
            //_sfmEditorView.Enabled = enable;
            _sfmEditorView.ContentsBox.ReadOnly = !enable;
        }

        private void SetEnabledSpecific(bool enable)
        {
            _filterChooserView.Enabled = enable;
            _changeWritingSystems.Enabled = enable;
            _changeTemplate.Enabled = enable;
            _exportButton.Enabled = enable;
            _quickFixButton.Enabled = enable;
            if (!enable)
            {
                setSaveEnabled(false);
            }
            else
            {
                setSaveEnabled(_mainWindowPM.needsSave);
            }
        }

        //Start fresh. (Helps the Find dialog see the active nav, anyway.)
        private void Initialize(MainWindowPM mainWindowPM)
        {
            this.KeyPreview = true;
            SetEnabledGeneral(false);

            _mainWindowPM = mainWindowPM;
            GetFindReplaceDialog();
            _filterIndex = 0;

            this.Top = 10;
            Screen scr = Screen.FromControl(this);
            this.Left = scr.WorkingArea.Width - this.Width;  // nearly top-right
            _searchDialog.Top = scr.WorkingArea.Height - (_searchDialog.Height);
            _searchDialog.Left = 0; //bottom left
            //see also _markerSettingsListView._xOfDialog etc. (top left)
        }

        // ASSUMES _sfmEditorView, _mainWindowPM are up-to-date and not null
        private FindReplaceDialog GetFindReplaceDialog()
        {
            if (_searchDialog == null)
                _searchDialog = new FindReplaceDialog(_sfmEditorView, _mainWindowPM);

            _searchDialog.WordFound -= OnWordFound;
            _searchDialog.WordFound += OnWordFound;
            return _searchDialog;
        }

        public void BindModels(MainWindowPM mainWindowPM) 
        {
            // cut any old wires first, just in case
            if (_mainWindowPM != null)
            {
                _mainWindowPM.SearchModel.SearchRecordFormatterChanged -= OnRecordFormatterChanged;
                _mainWindowPM.NavigatorModel.RecordChanged -= _sfmEditorView.OnRecordChanged;
                if (_searchDialog != null)
                {
                    _searchDialog.WordFound -= OnWordFound;
                    _mainWindowPM.EditorRecordFormatterChanged -= _searchDialog.OnEditorRecordFormatterChanged;
                } 

            }

            _mainWindowPM = mainWindowPM;
            GetFindReplaceDialog();

            _mainWindowPM.SearchModel.SearchRecordFormatterChanged -= OnRecordFormatterChanged;
            _mainWindowPM.SearchModel.SearchRecordFormatterChanged += OnRecordFormatterChanged;
            _mainWindowPM.EditorRecordFormatterChanged -= _searchDialog.OnEditorRecordFormatterChanged;
            _mainWindowPM.EditorRecordFormatterChanged += _searchDialog.OnEditorRecordFormatterChanged;
            _mainWindowPM.NavigatorModel.RecordChanged -= _sfmEditorView.OnRecordChanged;
            _mainWindowPM.NavigatorModel.RecordChanged += _sfmEditorView.OnRecordChanged;

            _sfmEditorView.BindModel(_mainWindowPM);
            _recordNavigatorView.BindModel(_mainWindowPM.NavigatorModel);
            _filterChooserView.BindModel(_mainWindowPM.WarningFilterChooserModel);
            _markerSettingsListView.BindModel(_mainWindowPM.MarkerSettingsModel, _mainWindowPM.WorkingDictionary);  // added -JMC
            // _searchView.BindModel(_mainWindowPM);  // Started to add this, but it'll crash; better to do on first use -JMC
            // Currently no need to also run MarkerSettingsDialog.BindModel(), since those get created/disposed each time.
            //   Or, we could create a private property for it, such as _markerSettingsDialog
            //   var test = new MarkerSettings.MarkerSettingsDialog(_mainWindowPM.MarkerSettingsModel, "");

            // _mainWindowPM.NavigatorModel.NavFilterChanged += _sfmEditorView.OnNavFilterChanged; //can be disabled? (redundant?) -JMC

            // Some of the following += were eventually piling up. That's why here and elsewhere I've put a -= before each += -JMC
            _recordNavigatorView.RefreshButton.Click -= _sfmEditorView.OnRefreshClicked;
            _recordNavigatorView.RefreshButton.Click += _sfmEditorView.OnRefreshClicked;
            _refreshMenuItem.Click -= _sfmEditorView.OnRefreshClicked;
            _refreshMenuItem.Click += _sfmEditorView.OnRefreshClicked;
            _recordNavigatorView.SearchButtonClicked -= OnSearchClick;
            _recordNavigatorView.SearchButtonClicked += OnSearchClick;
            _markerSettingsListView.MarkerSettingPossiblyChanged -= OnMarkerSettingPossiblyChanged;
            _markerSettingsListView.MarkerSettingPossiblyChanged += OnMarkerSettingPossiblyChanged;
            _sfmEditorView.RecordTextChanged -= OnRecordTextChanged;
            _sfmEditorView.RecordTextChanged += OnRecordTextChanged;
            _sfmEditorView.RecheckKeystroke -= OnRecheckKeystroke;
            _sfmEditorView.RecheckKeystroke += OnRecheckKeystroke;
            return;
        }

        private bool ReturnFalse()
        {
            return false;
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
            Cleanup();

            string initialDirectory = GetInitialDirectory();
            bool forceUnicode = false;

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
                forceUnicode = EncodingChooser.UserWantsUnicode(dlg.FileName); // issue #1259 Reduce number of templates by half
                templatePath = RequestTemplatePath(dlg.FileName, false);
                if (string.IsNullOrEmpty(templatePath))
                {
                    return; //they cancelled
                }
            }
            // At this point we s/b guaranteed to have a template file with a matching name. -JMC

            // Removed this extraneous save method, to clean up code and fix issue #1149 "Using Open Lexicon to switch files always saves currently open settings, even after choosing No." -JMC 2013-10
            // _mainWindowPM.SaveSettings(); 

            Open(dlg.FileName, templatePath, forceUnicode);
        }

        private void Open(string fileName, string templatePath, bool forceUnicode)
        {
            Cursor = Cursors.WaitCursor;

            // Starting here, we need to be able to roll the following back if the user cancels the File Open.
            // E.g. if they have an open file with unsaved data, its state should remain the same. That is, simply reloading 
            // the previous file from disk is not an adequate "rollback", unless we were to save current edits to a temp file first. -JMC Mar 2012
            MainWindowPM origPm = _mainWindowPM;
            var newPm = new MainWindowPM();
            ReInit(newPm);
            //TODO: consider calling origPm.WorkingDictionary.Reset() here -JMC

            if (_mainWindowPM.OpenDictionary(fileName, templatePath, forceUnicode))
            {
                //BindModels(_mainWindowPM);
                OnFileLoaded(fileName);

                origPm.Dispose();  // in case any variables are still referencing the old file's PM, this might block those bugs. -JMC
                setSaveEnabled(newPm.needsSave); // This fixes issue #1213 (bogus "needs save" right after opening a second file, if the first file was not saved)

                // JMC:! ? need to call something that's in _mainWindowPM.ProcessLexicon();
            }
            else
            {
                // File Open was cancelled or didn't succeed. Roll back.
                //BindModels(origPm); // less destructive than Initialize(origPm);  -JMC
                ReInit(origPm);
                newPm.Dispose();
                SetEnabledGeneral(_mainWindowPM.CanProcessLexicon);
                setSaveEnabled(origPm.needsSave);
            }

            Cursor = Cursors.Default;
        }

        public void OnFileLoaded(string filename)
        {
            Settings.Default.PreviousPathToDictionary = filename;
            Settings.Default.Save(); //we want to remember this even if we don't get a clean shutdown later on. -JMC

            /*
            _sfmEditorView.Enabled = false;  //We need to clearly toggle this off before on, or else the background may be gray -JMC
            _sfmEditorView.Enabled = true;   // (This issue only appeared after setting ShowSelectionMargin to True.)
             */

            string ext = Path.GetExtension(filename);
            if (!SolidSettings.FileExtensions.Contains(ext))
            {
                SolidSettings.FileExtensions.Add(ext);
                // adaptive: makes the next File Open dialog friendlier (during this run; lost on exit) -JMC
            }

            Show(); // needed so that other commands won't be ignored (e.g. so Ctrl+F5 will work, and for #1200) -JMC

            CheckAndNotify();

            //if (_mainWindowPM.NavigatorModel.ActiveFilter.MoveToFirst()) _sfmEditorView.UpdateViewFromModel();
            _mainWindowPM.NavigatorModel.MoveToFirst(); // fixes issue #1200 (right pane's top labels empty on command-line launch) -JMC
            UpdateDisplay(true);

        }

        public void CheckAndNotify()
        {
            if (_mainWindowPM.Settings != null)
            {
                string migrationReport = _mainWindowPM.Settings.FileStatusReport.GetReport();
                if (migrationReport != "")
                {
                    MessageBox.Show(migrationReport, "Settings Migrated or Corrected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _mainWindowPM.Settings.NotifyIfNewMarkers(false);
                string msg = _mainWindowPM.Settings.NotifyIfMixedEncodings();
                if (msg != "")
                {
                    MessageBox.Show(msg, "Mixed Encodings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void UpdateDisplay(bool fullRefresh)  //I'm hoping this parameter allowing a "light refresh" is gentler (set to false). -JMC Sep 2014
        {
            bool canProcess = _mainWindowPM.CanProcessLexicon;

            Text = "Solid: ";
            if (!String.IsNullOrEmpty(_mainWindowPM.DictionaryRealFilePath))
            {
                Text += _mainWindowPM.DictionaryRealFilePath;
            }

            SetEnabledSpecific(canProcess);

            //_recordNavigatorView.Enabled = _mainWindowPM.WorkingDictionary.Count > 0;  
            _recordNavigatorView.UpdateDisplay();  //Switched to this; might be overkill. -JMC

            SetEnabledGeneral(canProcess);

            if (fullRefresh)
            {
                _mainWindowPM.SfmEditorModel.MoveToFirst(); // Cheap way to "fix" #616 (and #274) -JMC 2013-09
                _mainWindowPM.NavigatorModel.StartupOrReset();
            }
            _markerSettingsListView.UpdateDisplay(fullRefresh);
            if (fullRefresh)
            {
                _filterChooserView.Model.Reset(); // _filterChooserView.Model.ActiveWarningFilter = _filterChooserView.Model.RecordFilters[0];  // Choose the "All Records" filter -JMC 2013-09
                _filterChooserView.UpdateDisplay(); // adding this helps the lower left pane...
                _sfmEditorView.Focus();
            }
            _sfmEditorView.UpdateViewFromModel();  // JMC:! ...but this doesn't help with the right pane
            //Hide(); Show();
            if (fullRefresh) _sfmEditorView.ContentsBox.Focus(); // possibly redundant -JMC
        
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
            //_mainWindowPM.needsSave = val;
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
                UpdateDisplay(false);
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

        private void Recheck()
        {
            Cleanup();

            Cursor = Cursors.WaitCursor;
            _sfmEditorView.UpdateModelFromView();
            _mainWindowPM.ProcessLexicon();
            UpdateDisplay(true);
            _sfmEditorView.HighlightMarkers = _mainWindowPM.NavigatorModel.ActiveFilter.HighlightMarkers;

            //_mainWindowPM.NavigatorModel.SendNavFilterChangedEvent();  // Added this so the left panes' selection would reset -JMC 2013-10
            //_sfmEditorView.UpdateViewFromModel();  //now unnecessary? -JMC Sep 2014
            Cursor = Cursors.Default;
        }


        private void OnSaveClick(object sender, EventArgs e) // (this works for Ctrl+S too) -JMC
        {
            _sfmEditorView.UpdateModelFromView();
            if (_mainWindowPM.DictionaryAndSettingsSave())
            {
                setSaveEnabled(false);
            }
            _sfmEditorView.UpdateViewFromModel();  // we want to see the data (indentation) the way Solid does -JMC
            _sfmEditorView.ContentsBox.Focus();  // just in case; probably redundant -JMC
        }

        // Currently is a "Save Copy As..." rather than a true "Save As..." -JMC
        private void OnSaveAsClick(object sender, EventArgs e)
        {
            SaveACopy();
        }

        private void SaveACopy()
        {
            // Present various save options before (or just after?) showing the file chooser dialog.
            // The options dialog will return a RecordFormatter with the selected options.
            // We'll pass that and the filename as arguments.

            var optionsDialog = new SaveOptionsDialog();
            // optionsDialog.WarnAboutClosers = false; //only do this if there are no structural errors detected. -JMC
            var result = optionsDialog.ShowDialog(this);
            if (DialogResult.OK != result)
            {
                return; // user cancelled
            }
            RecordFormatter rf = SaveOptionsDialog.ShortTermMemory;

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

            //TODO: If we save A.txt as B.txt and B.solid already exists, the following will silently overwrite B.solid -JMC
            // That's usually the right thing to do, but we could check, and provide a confirmation dialog here if needed, and bail on Cancel.

            string remember = _mainWindowPM.WorkingDictionary.FilePath;  // Not really a Save As, but a Save a Copy -JMC Apr 2014
            _mainWindowPM.DictionaryAndSettingsSaveAs(dlg.FileName, rf);
            /*
            if (_mainWindowPM.DictionaryAndSettingsSaveAs(dlg.FileName, rf))
            {
                setSaveEnabled(false);
                //For true Save As (which I don't prefer), we'd need to switch over to the other file. And to be safe, just reload? -JMC
            }
            */
            _mainWindowPM.WorkingDictionary.FilePath = remember;
            _sfmEditorView.UpdateViewFromModel();  // we want to see the data (indentation) the way Solid does -JMC
            _sfmEditorView.ContentsBox.Focus();  // just in case; probably redundant -JMC
        }


        private void OnRecordFormatterChanged(object sender, RecordFormatterChangedEventArgs e)
        {
            _mainWindowPM.SyncFormat(e.NewFormatter, false);
            UpdateDisplay(false);
        }


        private void OnWordFound(object sender, FindReplaceDialog.SearchResultEventArgs e)
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
            using (SILAboutBox box = new SILAboutBox(FileLocator.GetFileDistributedWithApplication("aboutBox.htm")))
            {
                box.ShowDialog();
            }
        }

        private void MainWindowView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cleanup();
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
                        OnSaveClick(this, null);
                        Cleanup();
                    }
                    else if (answer == System.Windows.Forms.DialogResult.No)
                    {
                        Cleanup();
                        //do nothing (allow close to happen)
                    }
                }
            }
        }

        private void Cleanup()

        // Simplifies dealing with Open, and with Open-then-Cancel
        // Wouldn't want those non-modal dialogs to stick around with handles to the wrong file's objects.
        {
            _markerSettingsListView.CloseSettingsDialog();
            CloseDialog(_searchDialog); // this helps now that cancel Find only hides rather than closing. -JMC
            _searchDialog = null;
            CloseDialog(_dataShapesDialog);
            _dataShapesDialog = null;
            CloseDialog(_dataValuesDialog);
            _dataValuesDialog = null;
        }
        private static void CloseDialog(Form d)
        {
            if (d != null && !d.IsDisposed)
            {
                d.Close();
                d.Dispose();
            }
        }

        private void OnChangeTemplate_Click(object sender, EventArgs e)
        {
            string path = RequestTemplatePath(_mainWindowPM.PathToCurrentDictionary, true);
            if(!String.IsNullOrEmpty(path))
            {
                _sfmEditorView.UpdateModelFromView();
                _mainWindowPM.SwitchSolidSettingsTemplate(path);
                UpdateDisplay(true);
                CheckAndNotify();
            }
        }

        public string RequestTemplatePath(string dictionaryPath, bool wouldBeReplacingExistingSettings)  //Made public for the sake of Program.cs -JMC
        {
            TemplateChooser chooser = new TemplateChooser(_mainWindowPM.Settings);
            chooser.CustomizedSolidDestinationName = Path.GetFileName(SolidSettings.GetSettingsFilePathFromDictionaryPath(dictionaryPath));

            string tmp = _mainWindowPM.DictionaryRealFilePath; //quick hack to enforce consistent behavior. -JMC Apr 2014
            _mainWindowPM.DictionaryRealFilePath = dictionaryPath;  
            chooser.TemplatePaths = _mainWindowPM.TemplatePaths;
            _mainWindowPM.DictionaryRealFilePath = tmp;

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

            //New dialog: -JMC Feb 2014
            GetFindReplaceDialog().LaunchSearch();
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
            _markerSettingsListView.OpenSettingsDialog("", "");
        }

        private void OnQuickFix(object sender, EventArgs e)
        {
            QuickFixer fixer = new QuickFixer(_mainWindowPM.WorkingDictionary);
            var dlg = new QuickFixForm(fixer);
            if (dlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            if (fixer.ChangesMade > 0)
            {
                _mainWindowPM.ProcessLexicon();
                UpdateDisplay(true);
                _sfmEditorView.UpdateViewFromModel(); //unnecessary? -JMC Sep 2014
                setSaveEnabled(true);
                    // Issue #1243 : temporary; really, the QuickFixForm ought to do this--only when it changes stuff. -JMC
                MessageBox.Show("Made " + fixer.ChangesMade + " changes.", "Done Applying Fix", MessageBoxButtons.OK);
            }
            else
            {
                // Enable this once all quick fixes return int for real
                //MessageBox.Show("Nothing was changed.", "Done Running Fix", MessageBoxButtons.OK);
            }
        }

        private void OnChangeWritingSystems_Click(object sender, EventArgs e)
        {
            var dialog = new WritingSystemsConfigDialog();
            var presenter = new WritingSystemsConfigPresenter(_mainWindowPM, AppWritingSystems.WritingSystems, dialog.WritingSystemsConfigView);
            DialogResult result = dialog.ShowDialog(this);
            UpdateDisplay(false);
        }

        private void getSupportMenuItem_Click(object sender, EventArgs e)
        {
            // (#249) I added this ability to trigger the yellow (non-fatal) Palaso error report. -JMC 2013-10
            // var tmp = new Palaso.UI.WindowsForms.Reporting.WinFormsErrorReporter();
            //tmp.ReportNonFatalException(new Exception("I would like to make a suggestion."), new ShowAlwaysPolicy());

            //Updating (JH) in 2017. I could not figure out where emails go, so to be safe, I'm changing to point to our standard website
            //which we can then keep up to date.
            Process.Start("http://software.sil.org/solid/support/");
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
            ShutDown();
        }

        private void ShutDown()
        {
            Cleanup();
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
                _sfmEditorView.UpdateModelAndView();  // Do we really need to do this now? (I think it's here because things like Alt-Tab may not trigger RichTextBox.Leave) -JMC
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
. ps (2)
. . sn (2)
. . . ge (+sn, 1)
. . . de (+sn, 1)
. . . re (2)
. se (2..2)
. . ps (2) .
. . et (1)
. . . ec (1)
. seco (2..2)
. . ps (2) .
. . et (1) .
. et (1) .
. dt

Notes:
- fields that can infer a parent (ge, de above) should probably be listed first. 
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
            GetFindReplaceDialog().SetFields(r);
            Search();
        }

        private void trimSpacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegexItem r = RegexItem.GetTrim();
            GetFindReplaceDialog().SetFields(r);
            Search();
        }

        private void unwrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegexItem reg = RegexItem.GetUnwrap();
            GetFindReplaceDialog().SetFields(reg);
            Search();
        }


        private void _globallyDeleteFieldsMenuItem_Click(object sender, EventArgs e)
        {
            RegexItem reg = RegexItem.GetDeleteFields();
            GetFindReplaceDialog().SetFields(reg);
            Search();
        }

        private void _switchlToUnicodeMenuItem_Click(object sender, EventArgs e)  // implements #1303
        {
            _mainWindowPM.Settings.SetAllUnicodeTo(true);
            _mainWindowPM.needsSave = true;
            UpdateDisplay(false);
        }

        private void _switchlToLegacyMenuItem_Click(object sender, EventArgs e)  // implements #1303
        {
            _mainWindowPM.Settings.SetAllUnicodeTo(false);
            _mainWindowPM.needsSave = true;
            UpdateDisplay(false);
        }

        private void _moveUpMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataShapeInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseDialog(_dataShapesDialog);
            _dataShapesDialog = new DataShapesDialog(GetFindReplaceDialog(), _mainWindowPM);
            _dataShapesDialog.Show();
        }

        private DataValuesDialog _dataValuesDialog;
        private void dataValueInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseDialog(_dataValuesDialog);
            _dataValuesDialog = new DataValuesDialog(_mainWindowPM);
            _dataValuesDialog.Show();
        }

    }
}
