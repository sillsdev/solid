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
        private int _currentIndexIntoFilteredRecords;
        private IList<Record> _masterRecordList;
        private RecordFilter _recordFilter;
        private IList<int> _indexesOfFilteredRecords;
        private Record _currentRecord;

        public class RecordChangedEventArgs:System.EventArgs 
        {
            public Record _record;

            public RecordChangedEventArgs(Record record)
            {
                _record = record;
            }
        }

        public event EventHandler<RecordChangedEventArgs> RecordChanged;
        public event EventHandler<FilterChooserPM.RecordFilterChangedEventArgs> FilterChanged;
        

        public RecordNavigatorPM()
        {
            _currentIndexIntoFilteredRecords = -1;
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
                _indexesOfFilteredRecords = new List<int>(_recordFilter.IndexesOfRecords);

                if (_indexesOfFilteredRecords.Count == 0)
                {
                    CurrentIndexIntoFilteredRecords = -1;
                }
                else
                {
                    CurrentIndexIntoFilteredRecords = 0;
                }
                if (FilterChanged != null)
                {
                    FilterChanged.Invoke(this,
                                         new FilterChooserPM.RecordFilterChangedEventArgs(_recordFilter));
                }
            }
        }

        public IList<Record> MasterRecordList
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
                if (CurrentIndexIntoFilteredRecords != -1)
                {
                    return _recordFilter.Description(CurrentIndexIntoFilteredRecords);
                }
                else 
                {
                    return "";
                }
            }
        }

        public void Previous()
        {
            if (CanGoPrev())
            {
                CurrentIndexIntoFilteredRecords--;
            }
        }

        public void Next()
        {
            if (CanGoNext())
            {
                CurrentIndexIntoFilteredRecords++;
            }
        }

        public bool CanGoPrev()
        {
            return CurrentIndexIntoFilteredRecords > 0;
        }

        public bool CanGoNext()
        {
            return CurrentIndexIntoFilteredRecords < Count - 1;
        }

        public int Count
        {
            get
            {
                return _indexesOfFilteredRecords.Count;
            }
        }
        
        public int CurrentIndexIntoFilteredRecords
        {
            get
            {
                return _currentIndexIntoFilteredRecords;
            }
            set
            {
                _currentIndexIntoFilteredRecords = value;
                if (RecordChanged != null)
                {
                    RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord));
                }
            }
        }

        public int CurrentRecordIndex
        {
            set
            {
                if(_indexesOfFilteredRecords.Contains(value))
                {
                   CurrentIndexIntoFilteredRecords = _indexesOfFilteredRecords.IndexOf(value);
                }
            }
            get
            {
                if(CurrentIndexIntoFilteredRecords!=-1)
                {
                        return _indexesOfFilteredRecords[CurrentIndexIntoFilteredRecords];
                }
                else
                {
                    return -1;
                } 
            }
        }

        public Record CurrentRecord
        {
            get
            {
                if (_currentIndexIntoFilteredRecords <0)
                {
                    return null;
                }
                return _masterRecordList[CurrentRecordIndex];
            }
            set
            {
                _currentRecord = value;
            }
        }


        public void StartupOrReset()
        {
            if (_indexesOfFilteredRecords.Count > 0)
            {
                CurrentIndexIntoFilteredRecords = 0;
            }
        }

        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            ActiveFilter = e._recordFilter;
        }

        public void OnRecordEdited(object sender, SfmEditorPM.RecordEditedEventArgs e)
        {
            _masterRecordList[CurrentRecordIndex].Value = e._record;
       }
    
    }
}    

