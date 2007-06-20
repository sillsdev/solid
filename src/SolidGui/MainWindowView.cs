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

        public void OnDictionaryLoaded(object sender, EventArgs e)
        {
 
            //wire up the change of record event to our record display widget
            _mainWindowPM.NavigatorModel.Startup();
         }

        private void _openButton_Click(object sender, EventArgs e)
        {
            _mainWindowPM.OpenDictionary(@"C:\Documents and Settings\John\My Documents\Language Data\Lahu Si\LahuDict2.txt");
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


        }

    }
}