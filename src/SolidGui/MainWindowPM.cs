using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        private FilterChooserPM _filterChooserModel;

        public event EventHandler DictionaryLoaded;

        public MainWindowPM()
        {
            _filterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();

            _recordFilters.Add(new AllRecordFilter());
            _recordFilters.Add(new NullRecordFilter());
            _recordFilters.Add(new RecordFilter("First Filter","These are some problems"));
            _recordFilters.Add(new RecordFilter("Second Filter","This is another problem"));
            _recordFilters.Add(new RecordFilter("Third Filter","So is this one"));

            FilterChooserModel.RecordFilters = _recordFilters;

            _navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = _recordFilters[0];

            this.DictionaryLoaded += _filterChooserModel.OnDictionaryLoaded;
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
                return _filterChooserModel;
            }
        }

        public void OpenDictionary(string path)
        {
            _masterRecordList.Clear();
           TextReader dictReader= File.OpenText(path);
            SolidConsole.SfmCollection reader = new SolidConsole.SfmCollection(dictReader, 10000);
            while (reader.Read())
            {
                if (reader.FieldCount == 0)
                    continue;

                StringBuilder recordContents = new StringBuilder();
                for(int i=0; i<reader.FieldCount;i++)
                {
                    recordContents.AppendLine("\\"+reader.Key(i)+" " + reader.Value(i));
                }
                _masterRecordList.Add(recordContents.ToString());
            }

            if (DictionaryLoaded != null)
            {
                DictionaryLoaded.Invoke(this, null);
            }
        }
    }
}
