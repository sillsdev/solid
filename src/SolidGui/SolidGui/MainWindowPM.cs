using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    /// <summary>
    /// Presentation Model component of MainWindow
    /// </summary>
    public class MainWindowPM
    {
        private List<RecordFilter> _recordFilters = new List<RecordFilter>();
        private List<string> _masterRecordList = new List<string>();
        private RecordNavigatorPresentationModel _navigator;
        private FilterListPresentationModel _filterListPM;

        public MainWindowPM()
        {
            _filterListPM = new FilterListPresentationModel();
            _navigator = new RecordNavigatorPresentationModel();

            MasterRecordList.Add("something0");
            MasterRecordList.Add("something1");
            MasterRecordList.Add("something2");
            MasterRecordList.Add("something3");

            _recordFilters.Add(new RecordFilter("First Filter","These are some problems"));
            _recordFilters.Add(new RecordFilter("Second Filter","This is another problem"));
            _recordFilters.Add(new RecordFilter("Third Filter","So is this one"));

            FilterListPM.RecordFilters = _recordFilters;

            _navigator.MasterRecordList = MasterRecordList;
            _navigator.ActiveFilter = _recordFilters[0];
        }

        public List<RecordFilter> RecordFilters
        {
            get
            {
                return _recordFilters;
            }
        }

        public RecordNavigatorPresentationModel Navigator
        {
            get
            {
                return _navigator;
            }
        }

        public List<string> MasterRecordList
        {
            get
            {
                return _masterRecordList;
            }
        }

        public FilterListPresentationModel FilterListPM
        {
            get
            {
                return _filterListPM;
            }
        }
    }
}
