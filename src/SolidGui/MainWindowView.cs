using System;
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
        }

        public void OnDictionaryProcessed(object sender, EventArgs e)
        {
 
            //wire up the change of record event to our record display widget
            _mainWindowPM.NavigatorModel.StartupOrReset();
            UpdateDisplay();
         }

        private void _openButton_Click(object sender, EventArgs e)
        {
            ChooseProject();
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
            _mainWindowPM.OpenDictionary(dlg.FileName);
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

            UpdateDisplay();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            _mainWindowPM.ProcessLexicon();
        }

        private void UpdateDisplay()
        {
            _processButton.Enabled = _mainWindowPM.CanProcessLexicon;
            _filterChooserView.Enabled = _mainWindowPM.CanProcessLexicon;
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _mainWindowPM.SaveDictionary(Settings.Default.PreviousPathToDictionary);
        }
    }
}