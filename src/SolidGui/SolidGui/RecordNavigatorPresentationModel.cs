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
                _currentIndex--;
            }
        }

        public void Next()
        {
            if (CanGoNext())
            {
                _currentIndex++;
            }
        }

        public bool CanGoPrev()
        {
            return _currentIndex > 0;
        }

        public bool CanGoNext()
        {
            return _currentIndex < Count-1;
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
        }

        public string CurrentRecord
        {
            get
            {
                return _masterRecordList[_currentIndex];
            }
        }




    }    
}
