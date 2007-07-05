using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        private Record _currentRecord;
        
        public SfmEditorView()
        {
            _currentRecord = null;
            InitializeComponent();
        }

        public void Highlight(int startIndex, int length)
        {
            _contentsBox.Select(startIndex, length);
        }

        public void OnRecordChanged(object sender, RecordNavigatorPM.RecordChangedEventArgs e)
        {
            if (e._record == null)
            {
                _currentRecord = null;
                _contentsBox.Text = "";
            }
            else
            {
                _currentRecord = e._record;
             
                for(int i = 0 ; i < _currentRecord.Count ; i++)
                {
                    string fieldText = _currentRecord.GetFieldStructured(i);
                    _contentsBox.AppendText(fieldText);
                    if(_currentRecord.GetFieldInferred(i))
                    {
                        _contentsBox.Find(fieldText);
                        _contentsBox.SelectionColor = Color.Blue;
                    }
                }
            }
        }

        private void OnTextChanged(object sender, System.EventArgs e)
        {
            if (_currentRecord!=null)
            {
                //somehow update the contents of the record
            }
        }
    }
}
