using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
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
            _recordNavigatorView.Model = _mainWindowPM.NavigatorModel;
            _filterChooserView.Model = _mainWindowPM.FilterChooserModel;
            _markerSettingsView.Model = _mainWindowPM.MarkerSettingsModel;
            _searchButton.Image =
                _searchButton.Image.GetThumbnailImage(_searchButton.Width-8, _searchButton.Height-8, ReturnFalse,
                                                      System.IntPtr.Zero);
        }

        private bool ReturnFalse()
        {
            return false;
        }

        public void OnDictionaryProcessed(object sender, EventArgs e)
        {
 
            //wire up the change of record event to our record display widget
            _mainWindowPM.NavigatorModel.StartupOrReset();
            _filterChooserView.UpdateDisplay();
            _markerSettingsView.UpdateDisplay();
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
                templatePath = RequestTemplatePath(dlg.FileName);
            }
            _mainWindowPM.OpenDictionary(dlg.FileName, templatePath );
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

            _mainWindowPM.NavigatorModel.RecordChanged += _sfmEditorView.OnRecordChanged;
            _filterChooserView.Model.RecordFilterChanged += _mainWindowPM.NavigatorModel.OnFilterChanged;
            _filterChooserView.Model.RecordFilterChanged += _filterChooserView.OnFilterChanged;
            _mainWindowPM.NavigatorModel.FilterChanged += _recordNavigatorView.OnFilterChanged;
            _mainWindowPM.SearchModel.wordFound += OnWordFound;
            Record.RecordTextChanged += OnRecordTextChanged;

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
            _processButton.Enabled = _mainWindowPM.CanProcessLexicon;
            _filterChooserView.Enabled = _mainWindowPM.CanProcessLexicon;
            _changeTemplate.Enabled = _mainWindowPM.CanProcessLexicon;
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            if(_mainWindowPM.SaveDictionary())
            {
                _saveButton.Enabled = false;
            }
        }

        public static void EnableSave()
        {
            
        }

        private void OnSearchClick(object sender, EventArgs e)
        {
            //_searchView hides itself when closed
            //if (_searchView == null)
            {
                _searchView = new SearchView(_recordNavigatorView,_sfmEditorView);
                _searchView.SearchModel = _mainWindowPM.SearchModel;
            }
            _searchView.Show();
        }

        private void OnWordFound(object sender, SearchPM.SearchResultEventArgs e)
        {
            _mainWindowPM.FilterChooserModel.ActiveRecordFilter = _mainWindowPM.RecordFilters[0];
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
            _mainWindowPM.SolidSettings.Save();
        }

        private void OnChangeTemplate_Click(object sender, EventArgs e)
        {
            string path =RequestTemplatePath(_mainWindowPM.PathToCurrentDictionary);
            if(!String.IsNullOrEmpty(path))
            {
                _mainWindowPM.UseSolidSettingsTemplate(path);
            }
        }

        private string RequestTemplatePath(string dictionaryPath)
        {
            TemplateChooser chooser = new TemplateChooser();
            chooser.CustomizedSolidDestinationName = Path.GetFileName(_mainWindowPM.GetSettingsPathFromDictionaryPath(dictionaryPath));
            chooser.TemplatePaths = _mainWindowPM.TemplatePaths;
            chooser.SelectedToSettingsFileInUse = _mainWindowPM.PathToCurrentSolidSettingsFile;
            chooser.HighlightADefaultChoice = false;
            chooser.ShowDialog();
           if (chooser.DialogResult == DialogResult.OK && chooser.SelectedToSettingsFileInUse != _mainWindowPM.PathToCurrentSolidSettingsFile)
           {
               return chooser.SelectedToSettingsFileInUse;
           }
            return null;
        }
    }
}