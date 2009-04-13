using System;
using System.Collections.Generic;
using System.Text;
using SolidEngine;

namespace SolidGui
{

    public class SolidErrorRecordFilter : RecordFilter
    {
        SolidReport.EntryType _errorType;

        List<string> _errorMessages = new List<string>();

        public SolidErrorRecordFilter(SfmDictionary d, SolidReport.EntryType errorType, string name) :
            base(d, name)
        {
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

        public override string Description(int index)
        {
            return _errorMessages[index];
        }


    }
    /*
    public class RegExRecordFilter : RecordFilter
    {
        private readonly bool _matchWhenNotFound;
        private string _pattern;
        public RegExRecordFilter(string name, string pattern, bool matchWhenNotFound,List<Record> records)
        {
            _descriptions = new List<string>();
            _matchWhenNotFound = matchWhenNotFound;
            _name = name;
            _pattern = pattern;
            _indexesOfRecords = GetIndicesOfMatchingRecords(records);
            
        }
        public RegExRecordFilter(string name, string pattern, List<Record> records):this(name,pattern,false,records)
        {
            _name = name;
            _pattern = pattern;
        }

        protected override List<int> GetIndicesOfMatchingRecords(List<Record> records)
        {
            _indexesOfRecords.Clear();

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(_pattern, 
                System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Singleline);

            for (int i = 0; i < records.Count; i++)
            {
                bool match = regex.IsMatch(records[i].Value);
                if(match == !_matchWhenNotFound)
                {
                    _indexesOfRecords.Add(i);
                    _descriptions.Add(String.Format("Records that match '{0}'", _pattern));
                }
            }
            return _indexesOfRecords;
        }

        public override List<string> Descriptions
        {
            get
            {
                return _descriptions;
            }
        }
    }
    */

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
            for (int i = 0; i < _d.Count; i++)
            {
                _indexesOfRecords.Add(i);
            }
        }
        
        public override string Description(int index)
        {
            return "All " + _d.Count + " records";
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
            for (int i = 0; i < _d.Count; i++)
            {
                _d.MoveTo(i);
                if (_d.Current.IsMarkerNotEmpty(_marker))
                {
                    _indexesOfRecords.Add(i);
                }
            }
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
            _d = d;
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
                    _d.MoveTo(_indexesOfRecords[_currentIndex]);
                    return _d.Current;
                }
                return new Record(0);

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
                return _d.GetRecord(_indexesOfRecords[index]);

            return null;
        }

    }
}
