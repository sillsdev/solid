using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        private Record _currentRecord;
        private Color _inferredTextColor = Color.Blue;
        private Color _defaultTextColor = Color.Black;
        
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
                ClearContentsOfTextBox();
            }
            else
            {
                _currentRecord = e._record;

                ClearContentsOfTextBox();
                DisplayEachFieldInRecord();
            }
        }

        private void ClearContentsOfTextBox()
        {
            _contentsBox.Text = "";
        }

        private void DisplayEachFieldInRecord()
        {
            for (int i = 0; i < _currentRecord.Count; i++)
            {
                string fieldText = _currentRecord.GetFieldStructured(i);
                if (_currentRecord.GetFieldInferred(i))
                {
                    _contentsBox.SelectionColor = _inferredTextColor;
                }
                else
                {
                    _contentsBox.SelectionColor = _defaultTextColor;
                }
                _contentsBox.AppendText(fieldText + "\n");
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
