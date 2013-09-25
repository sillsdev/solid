using SolidGui.Model;

namespace SolidGui.Filter
{
    public sealed class AllRecordFilter : RecordFilter
    {
        public static AllRecordFilter CreateAllRecordFilter(RecordManager rm)
        {
            return new AllRecordFilter(rm);
        }

        private AllRecordFilter(RecordManager rm) :
            base(rm, "No issues found - All Records")
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