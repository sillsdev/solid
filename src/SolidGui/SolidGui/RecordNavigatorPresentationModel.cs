using System;
using System.Collections.Generic;

namespace SolidGui
{
    public class RecordNavigatorPresentationModel
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
        public event EventHandler<FilterListPresentationModel.RecordFilterChangedEventArgs> FilterChanged;
        

        public RecordNavigatorPresentationModel()
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
                                         new FilterListPresentationModel.RecordFilterChangedEventArgs(_recordFilter));
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

        public void OnFilterChanged(object sender, FilterListPresentationModel.RecordFilterChangedEventArgs e)
        {
            ActiveFilter = e._recordFilter;
        }
    }    
}
