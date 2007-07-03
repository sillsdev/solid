using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SolidEngine;

namespace SolidGui
{
    /// <summary>
    /// Presentation Model (ui-agnostic) half of MainWindow
    /// </summary>
    public class MainWindowPM
    {
        private SolidSettings _solidSettings;
        private MarkerSettingsPM _markerSettingsModel;
        private DummyProcessor _dummyProcessor;
        private string _rulesXmlPath;
        private DateTime _lastWrittenTo;
        private List<string> _allMarkers;
        private String _TempDictionaryPath;
        private List<RecordFilter> _recordFilters = new List<RecordFilter>();
        private List<Record> _masterRecordList = new List<Record>();
        private RecordNavigatorPM _navigatorModel;
        private FilterChooserPM _filterChooserModel;
        private SfmEditorPM _sfmEditorModel;
        private SearchPM _searchModel;

        public event EventHandler DictionaryProcessed;

        public MainWindowPM()
        {
            _rulesXmlPath = @"C:\Documents and Settings\WeSay\Desktop\Solid\trunk\data\rules.xml";

            _solidSettings = new SolidSettings();
            _markerSettingsModel = new MarkerSettingsPM();
            _dummyProcessor = new DummyProcessor();
            _allMarkers = new List<string>();
            _lastWrittenTo = DateTime.Now;
            _TempDictionaryPath = Path.Combine(Path.GetTempPath(),"TempDictionary.txt");
            _filterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();
            _sfmEditorModel = new SfmEditorPM();
            _searchModel = new SearchPM();

            FilterChooserModel.RecordFilters = new List<RecordFilter>();

            _searchModel.MasterRecordList = MasterRecordList;
            _navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = new RecordFilter();
            _markerSettingsModel.AllMarkers = _allMarkers;
            _markerSettingsModel.MarkerSettings = _solidSettings.MarkerSettings;
            _markerSettingsModel.Root = _solidSettings.RecordMarker;

            DictionaryProcessed += _filterChooserModel.OnDictionaryProcessed;
        }

        public MarkerSettingsPM MarkerSettingsModel
        {
            get
            {
                return _markerSettingsModel;
            }
        }

        public SearchPM SearchModel
        {
            get
            {
                return _searchModel;
            }
            set
            {
                _searchModel = value;
            }
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
            _lastWrittenTo = File.GetLastWriteTime(path);
            _masterRecordList.Clear();
            using (TextReader dictReader = File.OpenText(path))
            {
                SolidEngine.SfmRecordReader reader = new SolidEngine.SfmRecordReader(dictReader, 10000);
                while (reader.Read())
                {
                    if (reader.FieldCount == 0)
                        continue;

                    StringBuilder recordContents = new StringBuilder();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        recordContents.AppendLine("\\" + reader.Key(i) + " " + reader.Value(i));
                        if (!_allMarkers.Contains(reader.Key(i)))
                        {
                            _allMarkers.Add(reader.Key(i));
                        }
                    }
                    _masterRecordList.Add(new Record(recordContents.ToString()));
                }
            }
            ProcessLexicon();
        }

        public void ProcessLexicon()
        {

            SaveDictionary(_TempDictionaryPath, false);
            
            //proccess temporary dictionary
            //_TempDictionaryPath
            _dummyProcessor.ReadRules(_rulesXmlPath);
            _dummyProcessor.ProcessDictionary(MasterRecordList);

            UpdateRecordFilters(@"C:\Documents and Settings\WeSay\Desktop\Solid\trunk\data\report.xml");

            FilterChooserModel.RecordFilters = _recordFilters;

            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }
        }

        private void UpdateRecordFilters(string reportPath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<RecordFilter>));
            

            _recordFilters.Clear();

            _recordFilters.Add(new AllRecordFilter(_masterRecordList));
            _recordFilters.Add(new NullRecordFilter());
            _recordFilters.Add(new RegExRecordFilter("Has Note", @"\\nt\s\w",_masterRecordList));
            _recordFilters.Add(new RegExRecordFilter("Missing N Gloss", @"\\gn\s\w", true,_masterRecordList));
            _recordFilters.Add(new RegExRecordFilter("Missing ps", @"\\ps\s\w", true,_masterRecordList));

            try
            {
                if (File.Exists(reportPath))
                {
                    using (StreamReader reader = new StreamReader(reportPath))
                    {
                        _recordFilters.AddRange((List<RecordFilter>) xs.Deserialize(reader));
                    }
                }
            }
            catch
            {
                
            }

            FilterChooserModel.RecordFilters = _recordFilters;
        }

        public bool SaveDictionary(string path, bool checkWriteTime)
        {
            if (_lastWrittenTo == File.GetLastWriteTime(path) || 
                !checkWriteTime || 
                !File.Exists(path))
            {
                if (_masterRecordList != null)
                {
                    System.Text.StringBuilder builder = new System.Text.StringBuilder();
                    for (int i = 0; i < _masterRecordList.Count; i++)
                    {
                        builder.Append(_masterRecordList[i].Value);
                    }
                    File.WriteAllText(path, builder.ToString());
                    if(checkWriteTime)
                    {
                        _lastWrittenTo = File.GetLastWriteTime(path);
                    }
                    return true;
                }
            }

            System.Windows.Forms.MessageBox.Show("The file has been altered outside of Solid",
                                                 "Error Message",
                                                 System.Windows.Forms.MessageBoxButtons.OK,
                                                 System.Windows.Forms.MessageBoxIcon.Error);
            return false;
            
            
        }
    }
}
