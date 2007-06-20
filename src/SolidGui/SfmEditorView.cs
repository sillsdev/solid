using System.Windows.Forms;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        public SfmEditorView()
        {
            InitializeComponent();
        }

        public void OnRecordChanged(object sender, RecordNavigatorPM.RecordChangedEventArgs e)
        {
            if (e.Record == null)
            {
                _contentsBox.Text = "";
            }
            else
            {
                _contentsBox.Text = e.Record;
            }
        }
    }
}
