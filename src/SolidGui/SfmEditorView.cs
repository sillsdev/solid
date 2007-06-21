using System.Windows.Forms;

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
                _contentsBox.Text = e._record.Value;
            }
        }

        private void OnTextChanged(object sender, System.EventArgs e)
        {
            if (_currentRecord!=null && _currentRecord.Value!=_contentsBox.Text)
            {
                _currentRecord.Value = _contentsBox.Text;
            }

        }
    }
}
