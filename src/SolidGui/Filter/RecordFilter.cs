using System;
using System.Collections.Generic;
using SolidGui.Model;

namespace SolidGui.Filter
{
    public class NullRecordFilter : RecordFilter  // JMC: if this really serves a purpose, I should document it
    {
        public NullRecordFilter()
            : base(null, "None")
        {
        }

        public override void UpdateFilter()
        {
            return;
        }
    
    }

    public abstract class RecordFilter : RecordManagerDecorator  // Decided this class could be declared abstract. (E.g. Update() wasn't really implemented.) -JMC
    {
        protected string _name;
        //  protected List<string> _descriptions;
        protected List<int> _indexesOfRecords = new List<int>();
        private int _currentIndex;

        public override bool Remove()
        {
            _indexesOfRecords.RemoveAt(CurrentIndex);
            return true;
        }

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
            else
            {
                MoveToLast();
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
            else
            {
                MoveToFirst();
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
            if (index > Count - 1) index = Count - 1; // JMC: Trying to add robustness; is this a good idea?
            if (index < 0) index = 0;                 // Note that now we'll only return false if the filter is empty.

            if (index >= 0 && index < Count)
            {
                _currentIndex = index;
                return true;
            }
            return false;
        }

        public override bool MoveToByID(int id)
        {
            if (_indexesOfRecords.Contains(id))
            {
                _currentIndex = _indexesOfRecords.IndexOf(id);
                return true;
            }
            return false;
        }

        public override Record GetRecord(int index)
        {
            if (index >= 0 && index < _indexesOfRecords.Count)
                return _recordManager.GetRecord(_indexesOfRecords[index]);

            return null;
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
            get { return new[] {""}; }
        }

        public virtual string Description(int index)
        {
            return "unknown description";
        }

        public abstract void UpdateFilter();

    }
}