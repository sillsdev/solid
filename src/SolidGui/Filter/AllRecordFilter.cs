using SolidGui.Model;

namespace SolidGui.Filter
{
    public sealed class AllRecordFilter : RecordFilter
    {
        public static AllRecordFilter CreateAllRecordFilter(RecordManager rm, string label)
        {
            label = (label == null || label == "") ? "All Records" : label;
            return new AllRecordFilter(rm, label);
        }

        private AllRecordFilter(RecordManager rm, string label) :
            base(rm, label)
        {
            UpdateFilter();
        }
       
        public override void UpdateFilter()
        {
            _indexesOfRecords.Clear();
            for (int i = 0; i < _recordManager.Count; i++)
            {
                _indexesOfRecords.Add(i);
            }
        }
        
        public override string Description(int index)
        {
            return "All " + _recordManager.Count + " records";
        }

    }
}