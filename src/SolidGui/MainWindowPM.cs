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

        public event EventHandler DictionaryProcessed;

        public MainWindowPM()
        {
            _filterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();

            _recordFilters.Add(new AllRecordFilter());
            _recordFilters.Add(new NullRecordFilter());
            _recordFilters.Add(new RegExRecordFilter("Has Note", @"\\nt\s\w"));
            _recordFilters.Add(new RegExRecordFilter("Missing N Gloss", @"\\gn\s\w", true));
            _recordFilters.Add(new RegExRecordFilter("Missing ps", @"\\ps\s\w", true));

            FilterChooserModel.RecordFilters = _recordFilters;

            _navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = _recordFilters[0];

            this.DictionaryProcessed += _filterChooserModel.OnDictionaryProcessed;
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

        public bool CanProcessLexicon
        {
            get
            {
                return _masterRecordList.Count > 0;
            }
        }

        public void OpenDictionary(string path)
        {
            _masterRecordList.Clear();
           TextReader dictReader= File.OpenText(path);
           SolidConsole.SfmRecordReader reader = new SolidConsole.SfmRecordReader(dictReader, 10000);
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

            ProcessLexicon();
        }

        public void ProcessLexicon()
        {
            //later, we'll do the actual work at this point

            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }
        }
    }
}
