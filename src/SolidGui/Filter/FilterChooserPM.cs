using System;
using System.Collections.Generic;

namespace SolidGui.Filter
{

    /// <summary>
    /// The filter chooser is the list of error/warning filters the user can click on.
    /// This class is the Presentation Model(ui-agnostic) half of this control
    /// See also MarkerSettingsListView, since the user can only select an item from one list at a time.
    /// </summary>
    public class FilterChooserPM
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
    }
}
