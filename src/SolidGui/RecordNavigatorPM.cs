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
        //!!!private IList<Record> _masterRecordList;
        private RecordFilter _recordFilter;
        //!!!private IList<int> _indexesOfFilteredRecords;
        //!!!private Record _currentRecord;

        public class RecordChangedEventArgs : System.EventArgs 
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
            //!!!_currentIndexIntoFilteredRecords = -1;
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
                _recordFilter.MoveToFirst();
                if (FilterChanged != null)
                {

                    FilterChanged.Invoke(
                        this,
                        new FilterChooserPM.RecordFilterChangedEventArgs(_recordFilter)
                    );
                }
                if (RecordChanged != null)
                {
                    RecordChanged.Invoke(
                        this,
                        new RecordChangedEventArgs(_recordFilter.Current)
                    );
                }
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

        public void MoveToFirst()
        {
            if (_recordFilter.MoveToFirst())
            {
                if (RecordChanged != null)
                    RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord));
            }
        }

        public void MoveToLast()
        {
            if (_recordFilter.MoveToLast())
            {
                if (RecordChanged != null)
                    RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord));
            }
        }

        public void MoveToPrevious()
        {
            if (_recordFilter.MoveToPrevious())
            {
                if(RecordChanged != null)
                    RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord));
            }
        }

        public void MoveToNext()
        {
            if (_recordFilter.MoveToNext())
            {
                if(RecordChanged != null)
                    RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord));     
            }
        }

        public bool CanGoPrev()
        {
            return _recordFilter.HasPrevious();
        }

        public bool CanGoNext()
        {
            return _recordFilter.HasNext();
        }

        public int Count
        {
            get
            {
                return _recordFilter.Count;
            }
        }
        
        public int CurrentIndexIntoFilteredRecords
        {
            get
            {
                return _recordFilter.CurrentIndex;
            }
            set
            {
                _recordFilter.MoveTo(value);
                if (RecordChanged != null)
                {
                    RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord));
                }
            }
        }
        
        public int CurrentRecordID
        {
            set
            {
                if(_recordFilter.MoveToByID(value))
                {
                    RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord));
                }
            }
            get
            {
                return _recordFilter.Current.ID;
            }
        }
        
        
        public Record CurrentRecord
        {
            get
            {
                return _recordFilter.Current;
            }
            /*
            set
            {
                _currentRecord = value;
            }
             */ 
        }


        public void StartupOrReset()
        {
            _recordFilter.MoveToFirst();
        }

        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            ActiveFilter = e._recordFilter;
        }
    }
}    

