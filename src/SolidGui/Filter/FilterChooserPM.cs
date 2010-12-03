using System;
using System.Collections.Generic;

namespace SolidGui.Filter
{

    /// <summary>
    /// The filter chooser is the list of filters the user can click on.
    /// This class is the Presentation Model(ui-agnostic) half of this control
    /// </summary>
    public class FilterChooserPM
    {
        private IList<RecordFilter> _recordFilters;
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

        public FilterChooserPM()
        {

        }

        public IList<RecordFilter> RecordFilters
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

        public void OnDictionaryProcessed()
        {}

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
