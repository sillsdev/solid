using System;
using System.Windows.Forms;

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
            _filterChooser.Model = _mainWindowPM.FilterChooserModel;            
        }

        public void OnDictionaryProcessed(object sender, EventArgs e)
        {
 
            //wire up the change of record event to our record display widget
            _mainWindowPM.NavigatorModel.StartupOrReset();
         }

        private void _openButton_Click(object sender, EventArgs e)
        {
            _mainWindowPM.OpenDictionary(@"C:\Documents and Settings\WeSay\Desktop\Solid\trunk\data\dict1.txt");
        }

        private void MainWindowView_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            _mainWindowPM.NavigatorModel.RecordChanged += _sfmEditorView.OnRecordChanged;
            _filterChooser.Model.RecordFilterChanged += _mainWindowPM.NavigatorModel.OnFilterChanged;
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
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _mainWindowPM.SaveDictionary(@"C:\Documents and Settings\WeSay\Desktop\Solid\trunk\data\save1.txt");
        }
    }
}