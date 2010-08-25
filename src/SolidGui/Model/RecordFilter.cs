using System;
using System.Collections.Generic;
using SolidGui.Engine;

namespace SolidGui.Model
{
    public class SolidErrorRecordFilter : RecordFilter
    {
        private readonly string _marker;
        SolidReport.EntryType _errorType;

        List<string> _errorMessages = new List<string>();

        public SolidErrorRecordFilter(SfmDictionary d, string marker,SolidReport.EntryType errorType, string name) :
            base(d, name)
        {
            _marker = marker;
            _errorType = errorType;
            _name = name;
        }


        public void AddEntry(SolidReport.Entry entry)
        {
            if (!_indexesOfRecords.Contains(entry.RecordID))
            {
                _indexesOfRecords.Add(entry.RecordID);
            }
        }

        public override IEnumerable<string> HighlightMarkers
        {
            get { return new string[] { _marker }; }
        }

        public override string Description(int index)
        {
            return _errorMessages[index];
        }


    }

    public class AllRecordFilter : RecordFilter
    {
        private static AllRecordFilter _allRecordFilter;

        public static AllRecordFilter CreateAllRecordFilter(RecordManager rm)
        {
            if(_allRecordFilter == null)
            {
                _allRecordFilter = new AllRecordFilter(rm);
            }
            return _allRecordFilter;
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

    public class MarkerFilter : RecordFilter
    {
        private string _marker;

        public MarkerFilter(RecordManager recordManager, string marker) :
            base(recordManager, String.Format("Marker {0}", marker))
        {
            _marker = marker;
            UpdateFilter();
        }

        public override void UpdateFilter()
        {
            _indexesOfRecords.Clear();
            for (int i = 0; i < _recordManager.Count; i++)
            {
                _recordManager.MoveTo(i);
                if (_recordManager.Current.IsMarkerNotEmpty(_marker))
                {
                    _indexesOfRecords.Add(i);
                }
            }
        }

        public override IEnumerable<string> HighlightMarkers
        {
            get { return new string[] { _marker }; }
        }
        public override string Description(int index)
        {
            return string.Format("Records containing {0}", _marker);
        }
       

    
    }

    public class NullRecordFilter : RecordFilter
    {
        public NullRecordFilter()
            : base(null, "None")
        {
        }
    }

    public class RecordFilter : RecordManagerDecorator
    {
        protected string _name;
        //  protected List<string> _descriptions;
        protected List<int> _indexesOfRecords = new List<int>();
        private int _currentIndex;

        public RecordFilter(RecordManager d, string name) :
            base(d)
        {
            _recordManager = d;
            _name = name;
            _currentIndex = 0;
        }

        public override int Count
        {
            get { return _indexesOfRecords.Count; }
        }
        /*
        public override IEnumerator<Record> GetEnumerator()
        {
            return _indexesOfRecords.GetEnumerator();
        }
        */

        public override Record Current
        {
            get
            {
                if (_indexesOfRecords.Count > 0)
                {
                    _recordManager.MoveTo(_indexesOfRecords[_currentIndex]);
                    return _recordManager.Current;
                }
                return new Record();

            }
        }

        public override int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                MoveTo(value);
            }
        }

        public override bool HasPrevious()
        {
            return _currentIndex > 0;
        }

        public override bool HasNext()
        {
            return _currentIndex < _indexesOfRecords.Count - 1;
        }

        public override bool MoveToNext()
        {
            bool retval = HasNext();
            if (retval)
            {
                _currentIndex++;
            }
            return retval;
        }

        public override bool MoveToPrevious()
        {
            bool retval = HasPrevious();
            if (retval)
            {
                _currentIndex--;
            }
            return retval;
        }

        public override bool MoveToFirst()
        {
            return MoveTo(0);
        }

        public override bool MoveToLast()
        {
            bool retval = false;
            if (Count > 0)
            {
                retval = MoveTo(Count - 1);
            }
            return retval;
        }

        public override bool MoveTo(int index)
        {
            bool retval = false;
            if (index >= 0 && index < Count)
            {
                retval = true;
                _currentIndex = index;
            }
            return retval;
        }

        public override string ToString()
        {
            return _name + " (" + Count + ")";
        }

        public string Name
        {
            get { return _name; }
        }

        public virtual IEnumerable<string> HighlightMarkers
        {
            get { return new string[] {""}; }
        }

        public virtual string Description(int index)
        {
            return "unknown description";
        }
       
        public virtual void UpdateFilter()
        {
        }

        public override bool MoveToByID(int id)
        {
            if(_indexesOfRecords.Contains(id))
            {
                _currentIndex = _indexesOfRecords.IndexOf(id);
                return true;
            }
            return false;
        }
        
        public override Record GetRecord(int index)
        {
            if(index >= 0 && index < _indexesOfRecords.Count)
                return _recordManager.GetRecord(_indexesOfRecords[index]);

            return null;
        }

    }
}