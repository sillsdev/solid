using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
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
        private readonly MainWindowPM _mainWindowPM;
        private int _filterIndex;

        public MainWindowView(MainWindowPM mainWindowPM)
        {
            
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }

            _mainWindowPM = mainWindowPM;
            _filterIndex = 0;
            _sfmEditorView.BindModel(_mainWindowPM.SfmEditorModel);
            _recordNavigatorView.BindModel(_mainWindowPM.NavigatorModel);
            _filterChooserView.BindModel(_mainWindowPM.FilterChooserModel);

            this.KeyPreview = true;
            
            // Wire the views to listen to the models
            _mainWindowPM.DictionaryProcessed += OnDictionaryProcessed;
            _mainWindowPM.NavigatorModel.RecordChanged += _sfmEditorView.OnRecordChanged;
            _mainWindowPM.NavigatorModel.NavFilterChanged += _recordNavigatorView.OnNavFilterChanged;
            // _mainWindowPM.NavigatorModel.NavFilterChanged += _sfmEditorView.OnNavFilterChanged;
            _mainWindowPM.FilterChooserModel.WarningFilterChanged += _filterChooserView.OnWarningFilterChanged;
            _mainWindowPM.MarkerSettingsModel.MarkerFilterChanged += _markerSettingsList.OnMarkerFilterChanged;
            _mainWindowPM.SearchModel.WordFound += OnWordFound;

            // Event wiring for child views.
            _recordNavigatorView._refreshButton.Click += _sfmEditorView.OnRefreshClicked;

            // Event wiring for the main view.
            _markerSettingsList.MarkerSettingPossiblyChanged += OnMarkerSettingPossiblyChanged;
            _sfmEditorView.RecordTextChanged += OnRecordTextChanged;
            _sfmEditorView.RecheckKeystroke += OnRecheckKeystroke;
            _recordNavigatorView.SearchButtonClicked += OnSearchClick;


            splitContainer1.Panel1.Enabled = false;
            splitContainer2.Panel1.Enabled = false;
            splitContainer2.Panel2.Enabled = false;
            _sfmEditorView.Enabled = false;

        }

        private bool ReturnFalse()
        {
            return false;
        }

        public void OnDictionaryProcessed(object sender, EventArgs e)
        {
            //wire up the change of record event to our record display widget
            _markerSettingsList.BindModel(
                _mainWindowPM.MarkerSettingsModel,
                _mainWindowPM.WorkingDictionary
            );
            _markerSettingsList.UpdateDisplay();
            _filterChooserView.Model.ActiveWarningFilter = _filterChooserView.Model.RecordFilters[0];  // Choose the "All Records" filter -JMC 2013-09
            _mainWindowPM.SfmEditorModel.MoveToFirst(); // This helps fix #616 (and #274) -JMC 2013-09
            _filterChooserView.UpdateDisplay();
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
            ChooseProject();
            if (_mainWindowPM.Settings != null)
            {
                _mainWindowPM.Settings.NotifyIfNewMarkers();
            }

        }

        private void ChooseProject()
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

            Settings.Default.PreviousPathToDictionary = dlg.FileName;

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

            Cursor = Cursors.WaitCursor;
            if (_mainWindowPM.OpenDictionary(dlg.FileName, templatePath))
            {
                OnFileLoaded(dlg.FileName);
                Settings.Default.Save(); //we want to remember this even if we don't get a clean shutdown later on. -JMC
                _mainWindowPM.needsSave = false; // These two lines fix issue #1213 (bogus "needs save" right after opening a second file, if the first file was not saved)
                _saveButton.Enabled = false;
                string ext = Path.GetExtension(dlg.FileName);
                if (!SolidSettings.FileExtensions.Contains(ext))
                {
                    SolidSettings.FileExtensions.Add(ext);
                        // adaptive: makes the next File Open dialog friendlier (during this run; lost on exit) -JMC
                }
            }

            Cursor = Cursors.Default;
        }

        public void OnFileLoaded(string name)
        {
            Text = "Solid: " + name;
            splitContainer1.Panel1.Enabled = true;
            splitContainer2.Panel1.Enabled = true;
            splitContainer2.Panel2.Enabled = true;
            _sfmEditorView.Enabled = true;
            _mainWindowPM.NavigatorModel.StartupOrReset();
            _sfmEditorView.Focus();
            _sfmEditorView.Reload();
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

        private void OnRecordTextChanged(object sender, EventArgs e)
        {
            _saveButton.Enabled = _mainWindowPM.needsSave = true;
        }

        private void OnMarkerSettingPossiblyChanged(object sender, EventArgs e)
        {
            _saveButton.Enabled = _mainWindowPM.needsSave = true;
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

            _sfmEditorView.Reload();
            Cursor = Cursors.Default;
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
            _saveButton.Enabled = _mainWindowPM.needsSave;
        }

        private void OnSaveClick(object sender, EventArgs e) // (this works for Ctrl+S too) -JMC
        {
            _sfmEditorView.UpdateModel();
            if (_mainWindowPM.DictionaryAndSettingsSave())
            {
                _saveButton.Enabled = _mainWindowPM.needsSave = false;
            }
            _sfmEditorView.Reload();  // we want to see the data (indentation) the way Solid does -JMC
            _sfmEditorView.ContentsBox.Focus();  // just in case; probably redundant -JMC
        }

        private void OnWordFound(object sender, SearchViewModel.SearchResultEventArgs e)
        {
            if (e.SearchResult.Filter != _mainWindowPM.FilterChooserModel.ActiveWarningFilter)
            {
                _mainWindowPM.FilterChooserModel.ActiveWarningFilter = e.SearchResult.Filter;
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
            if (_searchView != null) {_searchView.Dispose();}  // may be helpful now that cancel Find only hides rather than closing. -JMC
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
            _searchView = SearchView.CreateSearchView(_mainWindowPM.NavigatorModel, _sfmEditorView);
            _searchView.TopMost = true; // means that this form should always be in front of all others
            _searchView.SearchModel = _mainWindowPM.SearchModel;
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
            saveDialog.Title = "Export As";
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
            _markerSettingsList.OpenSettingsDialog(null);
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
            _saveButton.Enabled = _mainWindowPM.needsSave = true;
        }

        private void OnChangeWritingSystems_Click(object sender, EventArgs e)
        {
            var dialog = new WritingSystemsConfigDialog();
            var presenter = new WritingSystemsConfigPresenter(_mainWindowPM.Settings, AppWritingSystems.WritingSystems, dialog.WritingSystemsConfigView);
            DialogResult result = dialog.ShowDialog(this);
            _markerSettingsList.UpdateDisplay(); // TODO this is quite heavy handed. Make an UpdateWritingSystems, or notify off solid settings better. CP 2012-02
            _markerSettingsList.Refresh();
            if (result != DialogResult.Cancel)  // fixes issue #1213 (bogus "needs save")
            {
                OnMarkerSettingPossiblyChanged(null, null);
            }

        }

    }
}
