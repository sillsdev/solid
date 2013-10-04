using SolidGui.Filter;

namespace SolidGui
{
    public class RecordFilterChangedEventArgs : System.EventArgs 
    {
        public RecordFilter RecordFilter;

        public RecordFilterChangedEventArgs(RecordFilter recordFilter)
        {
            RecordFilter = recordFilter;
        }
    }
}