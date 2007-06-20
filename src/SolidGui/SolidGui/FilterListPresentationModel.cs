using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class FilterListPresentationModel
    {
        private IEnumerable<RecordFilter> _recordFilters;
        private RecordFilter _activeRecordFilter;

        public class RecordFilterChangedEventArgs : System.EventArgs
        {
            public RecordFilter _recordFilter;

            public RecordFilterChangedEventArgs(RecordFilter recordFilter)
            {
                _recordFilter = recordFilter;
            }
        }

        public event EventHandler<RecordFilterChangedEventArgs> RecordFilterChanged;

        public FilterListPresentationModel()
        {

        }

        public IEnumerable<RecordFilter> RecordFilters
        {
            get
            {
                return _recordFilters;
            }
            set
            {
                _recordFilters = value;
            }
        }

        public RecordFilter ActiveRecordFilter
        {
            get
            {
                return _activeRecordFilter;
            }
            set
            {
                _activeRecordFilter = value;
                if (RecordFilterChanged != null)
                {
                    RecordFilterChanged.Invoke(this, new RecordFilterChangedEventArgs(_activeRecordFilter));
                }
            }
        }
    }
}
