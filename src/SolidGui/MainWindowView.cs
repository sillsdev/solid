using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using SolidEngine;
using SolidGui.Properties;

namespace SolidGui
{
    /// <summary>
    /// View component of MainWindow. The logic is in the MainWindowPM.
    /// </summary>
    public partial class MainWindowView : Form
    {
        private MainWindowPM _mainWindowPM;

        public MainWindowView(MainWindowPM mainWindowPM)
        {
            
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }

            _mainWindowPM = mainWindowPM;
            _sfmEditorView.BindModel(_mainWindowPM.SfmEditorModel);
            _recordNavigatorView.BindModel(_mainWindowPM.NavigatorModel);
            _filterChooserView.BindModel(_mainWindowPM.FilterChooserModel);

            this.KeyPreview = true;
            
            _mainWindowPM.DictionaryProcessed += this.OnDictionaryProcessed;
            _mainWindowPM.NavigatorModel.RecordChanged += _sfmEditorView.OnRecordChanged;
            _mainWindowPM.NavigatorModel.FilterChanged += _recordNavigatorView.OnFilterChanged;
            _mainWindowPM.FilterChooserModel.RecordFilterChanged += _mainWindowPM.NavigatorModel.OnFilterChanged;
            _mainWindowPM.FilterChooserModel.RecordFilterChanged += _filterChooserView.OnFilterChanged;
            _mainWindowPM.FilterChooserModel.RecordFilterChanged += _markerDetails.OnFilterChanged;
            _mainWindowPM.SearchModel.wordFound += OnWordFound;
            _recordNavigatorView._recheckButton.Click += _sfmEditorView.OnRecheckClicked;

            //_markerDetails.RecordFilterChanged += _mainWindowPM.NavigatorModel.OnFilterChanged;

            _sfmEditorView.RecordTextChanged += this.OnRecordTextChanged;
            _recordNavigatorView.SearchButtonClicked += OnSearchClick;

        }

        private bool ReturnFalse()
        {
            return false;
        }

        public void OnDictionaryProcessed(object sender, EventArgs e)
        {
            //wire up the change of record event to our record display widget
            _mainWindowPM.NavigatorModel.StartupOrReset();           
            _markerDetails.BindModel(
                _mainWindowPM.MarkerSettingsModel,
                _mainWindowPM.FilterChooserModel,
                _mainWindowPM.WorkingDictionary,
                _mainWindowPM.SolidSettings
            );
            _filterChooserView.UpdateDisplay();
            _markerDetails.UpdateDisplay();
            UpdateDisplay();
        }

        private void _openButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ChooseProject();
            Cursor = Cursors.Default;
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
            dlg.Filter = "Dictionary(*.*)|*.*";
            dlg.Multiselect = false;
            dlg.InitialDirectory = initialDirectory;
            if (DialogResult.OK != dlg.ShowDialog(this))
            {
                return;
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
            _mainWindowPM.OpenDictionary(dlg.FileName, templatePath );
            Text = "SOLID " + dlg.FileName;
            splitContainer1.Panel1.Enabled = true;
            splitContainer2.Panel1.Enabled = true;
            splitContainer2.Panel2.Enabled = true;
            Cursor = Cursors.WaitCursor;
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
            _saveButton.Enabled = true;
        }

        private void OnProcessButtonClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _mainWindowPM.ProcessLexicon();
            Cursor = Cursors.Default;
        }

        private void UpdateDisplay()
        {
            _filterChooserView.Enabled = _mainWindowPM.CanProcessLexicon;
            _changeTemplate.Enabled = _mainWindowPM.CanProcessLexicon;
            _exportButton.Enabled = _mainWindowPM.CanProcessLexicon;
            _recordNavigatorView.Enabled = _mainWindowPM.WorkingDictionary.Count > 0;
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            _sfmEditorView.SaveContentsOfTextBox();
            _mainWindowPM.SaveDictionary();
            _saveButton.Enabled = false;
        }

        private void OnWordFound(object sender, SearchPM.SearchResultEventArgs e)
        {
            _mainWindowPM.FilterChooserModel.ActiveRecordFilter = e.SearchResult.Filter;
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
            if(_mainWindowPM.SolidSettings != null)
                _mainWindowPM.SolidSettings.Save();
        }

        private void OnChangeTemplate_Click(object sender, EventArgs e)
        {
            string path =RequestTemplatePath(_mainWindowPM.PathToCurrentDictionary, true);
            if(!String.IsNullOrEmpty(path))
            {
                _mainWindowPM.UseSolidSettingsTemplate(path);
            }
        }

        private string RequestTemplatePath(string dictionaryPath, bool wouldBeReplacingExistingSettings)
        {
            TemplateChooser chooser = new TemplateChooser();
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
            _searchView = SearchView.CreatSearchView(_mainWindowPM.NavigatorModel, _sfmEditorView);
            _searchView.SearchModel = _mainWindowPM.SearchModel;
            _searchView.Show();
            _searchView.Focus();
        }

        private void MainWindowView_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MainWindowView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.F)
            {
                OnSearchClick(this, new EventArgs());
            }
            if (e.Control == true && e.KeyCode == Keys.O)
            {
                _openButton_Click(this, new EventArgs());
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

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Export As";
            saveDialog.AddExtension = true;
            saveDialog.Filter = ExportFilterString();
            saveDialog.FileName = _mainWindowPM.WorkingDictionary.GetFileNameNoExtension();
            if (DialogResult.OK != saveDialog.ShowDialog(this))
            {
                return;
            }

            //string sourceFilePath = Path.Combine(Path.GetTempPath(), "TempDict.dic");           
            //_mainWindowPM.WorkingDictionary.SaveAs(sourceFilePath);
            _mainWindowPM.WorkingDictionary.Save();
            string sourceFilePath = _mainWindowPM.WorkingDictionary.FilePath;

            string destinationFilePath = saveDialog.FileName;

            ExportFactory f = ExportFactory.Singleton();
            IExporter exporter = f.CreateFromSettings(f.ExportSettings[saveDialog.FilterIndex - 1]);
            try
            {
                exporter.Export(sourceFilePath, destinationFilePath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.InnerException.Message, "Solid Export Error");
            }

        }
    }
}
