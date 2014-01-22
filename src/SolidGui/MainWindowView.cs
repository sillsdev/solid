// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using Palaso.Reporting;
using Solid.Engine;
using SolidGui.Engine;
using SolidGui.Export;
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

            splitContainer1.Panel1.Enabled = false;
            splitContainer2.Panel1.Enabled = false;
            splitContainer2.Panel2.Enabled = false;
            _sfmEditorView.Enabled = false;

            _mainWindowPM = mainWindowPM;
            _filterIndex = 0;

        }

        public void BindModels(MainWindowPM mainWindowPM) 
        {
            // cut any old wires first
            if (_mainWindowPM != null)
            {
                _mainWindowPM.DictionaryProcessed -= OnDictionaryProcessed;
                _mainWindowPM.SearchModel.WordFound -= OnWordFound;
                _mainWindowPM.NavigatorModel.RecordChanged -= _sfmEditorView.OnRecordChanged;
            }

            _mainWindowPM = mainWindowPM;

            _mainWindowPM.DictionaryProcessed += OnDictionaryProcessed;
            _mainWindowPM.SearchModel.WordFound += OnWordFound;
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
            }

        }

        private void ChooseAndOpenProject()
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

            OpenFileDialog dlg = new OpenFileDialog();
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
            var origPm = _mainWindowPM;
            _mainWindowPM = new MainWindowPM();

            if (_mainWindowPM.OpenDictionary(fileName, templatePath))
            {
                Settings.Default.PreviousPathToDictionary = fileName;
                Settings.Default.Save(); //we want to remember this even if we don't get a clean shutdown later on. -JMC

                BindModels(_mainWindowPM);
                OnFileLoaded(fileName);
                setSaveEnabled(false); // This fixes issue #1213 (bogus "needs save" right after opening a second file, if the first file was not saved)
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
                // Initialize(origPm);  // JMC:!! probably need something less destructive
                BindModels(origPm);
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

        private void UpdateDisplay()
        {

            bool canProcess = _mainWindowPM.CanProcessLexicon;
            _filterChooserView.Enabled = canProcess;
            _changeWritingSystems.Enabled = canProcess;
            _changeTemplate.Enabled = canProcess;
            _exportButton.Enabled = canProcess;
            _recordNavigatorView.Enabled = _mainWindowPM.WorkingDictionary.Count > 0;
            _quickFixButton.Enabled = canProcess;
            setSaveEnabled(_mainWindowPM.needsSave);

            splitContainer1.Panel1.Enabled = canProcess;
            splitContainer2.Panel1.Enabled = canProcess;
            splitContainer2.Panel2.Enabled = canProcess;
            _sfmEditorView.Enabled = canProcess;

            _mainWindowPM.SfmEditorModel.MoveToFirst(); // This helps fix #616 (and #274) -JMC 2013-09
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

            UpdateDisplay();
        }

        private void setSaveEnabled(bool val)
        {
            _saveMenuItem.Enabled = _saveButton.Enabled = _mainWindowPM.needsSave = val;
        }

        private void OnRecordTextChanged(object sender, EventArgs e)
        {
            setSaveEnabled(true);
        }

        private void OnMarkerSettingPossiblyChanged(object sender, EventArgs e)
        {
            setSaveEnabled(true); // JMC:! It would be much better to know for sure whether a save is needed or not. 
            _sfmEditorView.OnSolidSettingsChange();
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

        private void OnWordFound(object sender, SearchViewModel.SearchResultEventArgs e)
        {
            if (e.SearchResult.Filter != _mainWindowPM.WarningFilterChooserModel.ActiveWarningFilter)
            {
                _mainWindowPM.WarningFilterChooserModel.ActiveWarningFilter = e.SearchResult.Filter;
            }
            _mainWindowPM.NavigatorModel.CurrentRecordIndex = e.SearchResult.RecordIndex;
            _recordNavigatorView.UpdateDisplay();
            _sfmEditorView.Highlight(e.SearchResult.TextIndex, e.SearchResult.ResultLength);
        }

        private void OnAboutBoxButton_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
            box.Dispose();
        }

        private void MainWindowView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_searchView != null) {_searchView.Dispose();}  // this may be helpful, now that cancel Find only hides rather than closing. -JMC
            if (_mainWindowPM.needsSave) 
            {
                var answer = MessageBox.Show("Save changes before quitting?", "Solid: Save first?", MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question);
                switch (answer)
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case System.Windows.Forms.DialogResult.Yes:
                        OnSaveClick(this,null);
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        break;

                }
            }
        }

        private void OnChangeTemplate_Click(object sender, EventArgs e)
        {
            string path = RequestTemplatePath(_mainWindowPM.PathToCurrentDictionary, true);
            if(!String.IsNullOrEmpty(path))
            {
                //JMC:!! Do we need to save first? I.e. prob need to call SfmEditorView.UpdateModel() to be safe
                _sfmEditorView.UpdateModel();
                _mainWindowPM.UseSolidSettingsTemplate(path);
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
            _searchView = SearchView.CreateSearchView(_mainWindowPM, _sfmEditorView);
            _searchView.TopMost = true; // means that this form should always be in front of all others
            // _searchView.SearchModel = _mainWindowPM.SearchModel; // JMC: redundant now?
            _searchView.Show();
            _searchView.Focus();
        }

        private void MainWindowView_KeyDown(object sender, KeyEventArgs e)
        {

        }

        // These keystrokes are mostly redundant now that I've underlined button letters, but I'm leaving them in in case anyone's used to them. -JMC 2013-10
        private void MainWindowView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.F)
            {
                OnSearchClick(this, new EventArgs());
            }
            if (e.Control == true && e.KeyCode == Keys.O)
            {
                OnOpenClick(this, new EventArgs());
            }
            if (e.Control == true && e.KeyCode == Keys.S)
            {
                if (e.Shift)
                {
                    // JMC:! Insert code here for calling OnSaveClick() with saveClosingTags = true
                    return;
                }
                if (_mainWindowPM.needsSave)
                {
                    OnSaveClick(this, new EventArgs());
                }
            }
            if (e.Control == true && e.KeyCode == Keys.R)
            {
                OnRecheckButtonClick(this, new EventArgs());
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
            var destinationFilePath = saveDialog.FileName;

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
            _markerSettingsListView.OpenSettingsDialog(null);
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
            var presenter = new WritingSystemsConfigPresenter(_mainWindowPM.Settings, AppWritingSystems.WritingSystems, dialog.WritingSystemsConfigView);
            DialogResult result = dialog.ShowDialog(this);
            _markerSettingsListView.UpdateDisplay(); // TODO this is quite heavy handed. Make an UpdateWritingSystems, or notify off solid settings better. CP 2012-02
            _markerSettingsListView.Refresh();
            if (result != DialogResult.Cancel)  // fixes issue #1213 (bogus "needs save")
            {
                OnMarkerSettingPossiblyChanged(null, null);
            }

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
            var x = _sfmEditorView.ContentsBox.SelectedText;
           
        }

    }
}
