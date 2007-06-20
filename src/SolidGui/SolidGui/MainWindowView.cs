using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SolidGui
{
    /// <summary>
    /// view component of MainWindow. The logic is in the MainWindowPM.
    /// </summary>
    public partial class MainWindowView : Form
    {
        private MainWindowPM _mainWindowPM;

        public MainWindowView(MainWindowPM mainWindowPM)
        {
            InitializeComponent();
            _mainWindowPM = mainWindowPM;
            _recordNavigatorView1.Model = _mainWindowPM.Navigator;
            _filterList.Model = _mainWindowPM.FilterListPM;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
 
            //wire up the change of record event to our record display widget
            _mainWindowPM.Navigator.RecordChanged += _sfmEditorView.OnRecordChanged;
            _filterList.Model.RecordFilterChanged += _mainWindowPM.Navigator.OnFilterChanged;
            _mainWindowPM.Navigator.FilterChanged += _recordNavigatorView1.OnFilterChanged;

            _mainWindowPM.Navigator.Startup();
         }

    }
}