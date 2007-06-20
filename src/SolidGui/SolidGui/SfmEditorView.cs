using System.Windows.Forms;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        public SfmEditorView()
        {
            InitializeComponent();
        }

        public void OnRecordChanged(object sender, RecordNavigatorPresentationModel.RecordChangedEventArgs e)
        {
            _contentsBox.Text = e.Record;
        }
    }
}
