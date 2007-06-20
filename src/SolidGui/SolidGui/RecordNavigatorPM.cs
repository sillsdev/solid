using System;
using System.Collections.Generic;

namespace SolidGui
{
    /// <summary>
    /// The record navigator the control that shows the user the description of the current
    /// filter and lets them say "next" and "previous".
    /// This class is the Presentation Model(ui-agnostic) half of this control
    /// </summary>
    public class RecordNavigatorPM
    {
        private int _currentIndex;
        private IList<string> _masterRecordList;
        private RecordFilter _recordFilter;
        private IList<int> _indexesOfFilteredRecords;

        public class RecordChangedEventArgs:System.EventArgs 
        {
            public String Record;

            public RecordChangedEventArgs(string record)
            {
                Record = record;
            }
        }



        public event EventHandler<RecordChangedEventArgs> RecordChanged;
        public event EventHandler<FilterChooserPM.RecordFilterChangedEventArgs> FilterChanged;
        

        public RecordNavigatorPM()
        {
            _currentIndex = 0;
        }

        public RecordFilter ActiveFilter
        {
            get
            {
                return _recordFilter;
            }
            set
            {
                _recordFilter = value;
                _indexesOfFilteredRecords = new List<int>(_recordFilter.GetIndicesOfMatchingRecords());

                if (FilterChanged != null)
                {
                    FilterChanged.Invoke(this,
                                         new FilterChooserPM.RecordFilterChangedEventArgs(_recordFilter));
                }
            }
        }

        public IList<string> MasterRecordList
        {
            get
            {
                return _masterRecordList;
            }
            set
            {
                _masterRecordList = value;
            }
        }

        public string Description
        {
            get
            {
                return _recordFilter.Description;
            }
        }

        public void Previous()
        {
            if (CanGoPrev())
            {
                CurrentIndex--;
            }
        }

        public void Next()
        {
            if (CanGoNext())
            {
                CurrentIndex++;
            }
        }

        public bool CanGoPrev()
        {
            return CurrentIndex > 0;
        }

        public bool CanGoNext()
        {
            return CurrentIndex < Count - 1;
        }

        public int Count
        {
            get
            {
                return _indexesOfFilteredRecords.Count;
            }
        }
        
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
                if (RecordChanged != null)
                {
                    RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord));
                }
            }
        }

        public string CurrentRecord
        {
            get
            {
                return _masterRecordList[_currentIndex];
            }
        }


        public void Startup()
        {
            if (_indexesOfFilteredRecords.Count > 0)
            {
                CurrentIndex = 0;
            }
        }

        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            ActiveFilter = e._recordFilter;
        }
    }    
}
