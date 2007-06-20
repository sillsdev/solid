using System.Collections.Generic;

namespace SolidGui
{
    /// <summary>
    /// Presentation Model (ui-agnostic) half of MainWindow
    /// </summary>
    public class MainWindowPM
    {
        private List<RecordFilter> _recordFilters = new List<RecordFilter>();
        private List<string> _masterRecordList = new List<string>();
        private RecordNavigatorPM _navigatorModel;
        private FilterChooserPM _filterListModel;

        public MainWindowPM()
        {
            _filterListModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();

            MasterRecordList.Add("something0");
            MasterRecordList.Add("something1");
            MasterRecordList.Add("something2");
            MasterRecordList.Add("something3");

            _recordFilters.Add(new RecordFilter("First Filter","These are some problems"));
            _recordFilters.Add(new RecordFilter("Second Filter","This is another problem"));
            _recordFilters.Add(new RecordFilter("Third Filter","So is this one"));

            FilterChooserModel.RecordFilters = _recordFilters;

            _navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = _recordFilters[0];
        }

        /// <summary>
        /// A list containing every lexical record in the dictionary
        /// </summary>
        public List<string> MasterRecordList
        {
            get
            {
                return _masterRecordList;
            }
        }

        public List<RecordFilter> RecordFilters
        {
            get
            {
                return _recordFilters;
            }
        }

        public RecordNavigatorPM NavigatorModel
        {
            get
            {
                return _navigatorModel;
            }
        }


        public FilterChooserPM FilterChooserModel
        {
            get
            {
                return _filterListModel;
            }
        }
    }
}
