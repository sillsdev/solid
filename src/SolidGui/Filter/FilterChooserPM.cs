using System;
using System.Collections.Generic;

namespace SolidGui.Filter
{

    /// <summary>
    /// The filter chooser is the list of error/warning filters the user can click on.
    /// This class is the Presentation Model(ui-agnostic) half of this control
    /// See also MarkerDetails, since the user can only select an item from one list at a time.
    /// </summary>
    public class FilterChooserPM
    {
        private IList<RecordFilter> _recordFilters;
        private RecordFilter _activeWarningFilter;

        public class RecordFilterChangedEventArgs : System.EventArgs
        {
            public RecordFilter RecordFilter;

            public RecordFilterChangedEventArgs(RecordFilter recordFilter)
            {
                RecordFilter = recordFilter;
            }
        }

        public event EventHandler<RecordFilterChangedEventArgs> WarningFilterChanged;

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

        public RecordFilter ActiveWarningFilter
        {
            get
            {
                return _activeWarningFilter;
            }
            set
            {
                _activeWarningFilter = value;
                if (WarningFilterChanged != null)
                {
                    WarningFilterChanged.Invoke(this, new RecordFilterChangedEventArgs(_activeWarningFilter));
                }
            }
        }
    }
}
