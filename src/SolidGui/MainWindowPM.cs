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
        private ReportReader _reportReader;
        private String _TempDictionaryPath;
        private List<RecordFilter> _recordFilters = new List<RecordFilter>();
        private List<Record> _masterRecordList = new List<Record>();
        private RecordNavigatorPM _navigatorModel;
        private FilterChooserPM _filterChooserModel;
        private SfmEditorPM _sfmEditorModel;

        public event EventHandler DictionaryProcessed;

        public MainWindowPM()
        {
            _reportReader = new ReportReader();
            _TempDictionaryPath = Path.Combine(Path.GetTempPath(),"TempDictionary.txt");
            _filterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();
            _sfmEditorModel = new SfmEditorPM();

            FilterChooserModel.RecordFilters = new List<RecordFilter>();

            _navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = new RecordFilter();

            this.DictionaryProcessed += _filterChooserModel.OnDictionaryProcessed;
        }

        /// <summary>
        /// A list containing every lexical record in the dictionary
        /// </summary>
        public List<Record> MasterRecordList
        {
            get
            {
                return _masterRecordList;
            }
            set
            {
                _masterRecordList = value;
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

        public SfmEditorPM SfmEditorModel
        {
            get
            {
                return _sfmEditorModel;
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
                _masterRecordList.Add(new Record(recordContents.ToString()));
            }

            ProcessLexicon();
        }

        public void ProcessLexicon()
        {

            SaveDictionary(_TempDictionaryPath);
            
            //proccess temporary dictionary
            //_TempDictionaryPath

            UpdateRecordFilters(@"C:\Documents and Settings\WeSay\Desktop\Solid\trunk\data\report.xml");

            FilterChooserModel.RecordFilters = _recordFilters;

            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }
        }

        private void UpdateRecordFilters(string reportPath)
        {
            _recordFilters.Clear();

            _recordFilters.Add(new AllRecordFilter(_masterRecordList));
            _recordFilters.Add(new NullRecordFilter());
            _recordFilters.Add(new RegExRecordFilter("Has Note", @"\\nt\s\w",_masterRecordList));
            _recordFilters.Add(new RegExRecordFilter("Missing N Gloss", @"\\gn\s\w", true,_masterRecordList));
            _recordFilters.Add(new RegExRecordFilter("Missing ps", @"\\ps\s\w", true,_masterRecordList));

            _reportReader.Load(reportPath);
            while (_reportReader.NextRecordFilter())
            {
                _recordFilters.Add(new RecordFilter(_reportReader.Name,
                                                    _reportReader.Description,
                                                    _reportReader.Indexes));
            }

            FilterChooserModel.RecordFilters = _recordFilters;
        }

        public void SaveDictionary(string path)
        {
            if(_masterRecordList != null)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                for(int i = 0 ; i < _masterRecordList.Count; i++)
                {
                    builder.Append(_masterRecordList[i].Value);
                }
                File.WriteAllText(path, builder.ToString());
            }
        }
    }
}
