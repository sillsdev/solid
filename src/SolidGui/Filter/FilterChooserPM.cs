using System;
using System.Collections.Generic;

namespace SolidGui.Filter
{

    /// <summary>
    /// The filter chooser is the list of error/warning filters the user can click on.
    /// This class is the Presentation Model(ui-agnostic) half of this control
    /// See also MarkerSettingsListView, since the user can only select an item from one list at a time.
    /// </summary>
    public class FilterChooserPM :IDisposable
    {
        private IList<RecordFilter> _recordFilters;
        private RecordFilter _activeWarningFilter;

        public event EventHandler<RecordFilterChangedEventArgs> WarningFilterChanged;

        public void OnNavFilterChanged(object sender, RecordFilterChangedEventArgs e)  // added -JMC 2013-10
        {
            var filter = e.RecordFilter;
            if (filter != ActiveWarningFilter)
            {
                // The nav filter just changed, and it wasn't me.
                ActiveWarningFilter = null;
            }
        }

        public FilterChooserPM()
        {

        }

        public override string ToString()
        {
            return string.Format("{{filts: Active: {0}; All: {1}; {2}}}", 
                _activeWarningFilter, _recordFilters, GetHashCode());
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

        public RecordFilter ActiveWarningFilter
        {
            get
            {
                return _activeWarningFilter;
            }
            set
            {
                if (_activeWarningFilter == value) return;
                _activeWarningFilter = value;
                if (WarningFilterChanged != null)
                {
                    WarningFilterChanged.Invoke(this, new RecordFilterChangedEventArgs(_activeWarningFilter));
                }
            }
        }

        public void Reset()
        {
            if (RecordFilters != null && RecordFilters.Count > 0)
            {
                ActiveWarningFilter = RecordFilters[0];                
            }
            else
            {
                ActiveWarningFilter = null;
            }
        }

        public void Dispose()
        {
            _recordFilters = null;
            _activeWarningFilter = null;
        }
    }
}
