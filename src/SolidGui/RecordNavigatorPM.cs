// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using SolidGui.Filter;
using SolidGui.MarkerSettings;
using SolidGui.Model;

namespace SolidGui
{
    /// <summary>
    /// The record navigator, the control that shows the user the description of the current
    /// filter and lets them say "next" and "previous".
    /// This class is the Presentation Model(ui-agnostic) half of this control
    /// </summary>
    public class RecordNavigatorPM :IDisposable
    {
        private RecordFilter _recordFilter;

        public class RecordChangedEventArgs : System.EventArgs 
        {
            public Record Record{ get; set;}

            public RecordChangedEventArgs(Record record, IEnumerable<string> _highlightMarkers)
            {
                Record = record;
                HighlightMarkers = _highlightMarkers;
            }

            public IEnumerable<string> HighlightMarkers
            {
                get; set;
            }
        }

        public event EventHandler<RecordChangedEventArgs> RecordChanged;
        public event EventHandler<RecordFilterChangedEventArgs> NavFilterChanged;

        public readonly MainWindowPM Model; 
        public RecordNavigatorPM(MainWindowPM model)
        {
            Model = model;
        }

        public override string ToString()
        {
            return string.Format("{{nav: Active: {0}}}", ActiveFilter);
        }

        public RecordFilter ActiveFilter
        {
            get
            {
                return _recordFilter;
            }
            set
            {
                int currentRecordId = 0;
                if (_recordFilter != null && _recordFilter.Current != null)
                {
                    currentRecordId = _recordFilter.Current.ID;
                }

                _recordFilter = value;
                
                if ( currentRecordId == 0 || !_recordFilter.MoveToByID(currentRecordId) )
                {
                    if (_recordFilter != null) _recordFilter.MoveToFirst();
                }

                // SendRecordChangedEvent();  // Currently redundant with the next event; disabled. -JMC 2013-10
                SendNavFilterChangedEvent();
            }
        }

        public string Description
        {
            get
            {
                if (CurrentRecordIndex != -1)
                {
                    return _recordFilter.Name;
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
                SendRecordChangedEvent();
            }
        }

        private void SendRecordChangedEvent()
        {
            if (RecordChanged != null)
                RecordChanged.Invoke(this, new RecordChangedEventArgs(CurrentRecord, _recordFilter.HighlightMarkers));
        }

        public void SendNavFilterChangedEvent()
        {
            if (NavFilterChanged != null)
            {
                NavFilterChanged.Invoke(this, new RecordFilterChangedEventArgs(_recordFilter));
            }
        }

        public void MoveToLast()
        {
            if (_recordFilter.MoveToLast())
            {
                SendRecordChangedEvent();
            }
        }

        public void MoveToPrevious()
        {
            if (_recordFilter.MoveToPrevious())
            {
                SendRecordChangedEvent();
            }
        }

        public void MoveToNext()
        {
            if (_recordFilter.MoveToNext())
            {
                SendRecordChangedEvent();
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
        
        public int CurrentRecordIndex
        {
            get
            {
                return _recordFilter.CurrentIndex;
            }
            set
            {
                _recordFilter.MoveTo(value);
                SendRecordChangedEvent();
            }
        }
        
        public int CurrentRecordID
        {
            set
            {
                if(_recordFilter.MoveToByID(value))
                {
                    SendRecordChangedEvent();
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

        public void OnFilterChanged(object sender, RecordFilterChangedEventArgs e)
        {
            RecordFilter filter = e.RecordFilter;
            if (filter == null)
            {
                ;
            }
            else
            {
                ActiveFilter = filter;
            }
        }

        public void Dispose()
        {
            // cut off any listeners
            NavFilterChanged = null;
            RecordChanged = null;
            // dispose
            _recordFilter = null;
            ActiveFilter = null;
        }
    }
}    

