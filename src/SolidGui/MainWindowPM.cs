using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using SolidEngine;
using SolidGui.Properties;

namespace SolidGui
{
    /// <summary>
    /// Presentation Model (ui-agnostic) half of MainWindow
    /// </summary>
    public class MainWindowPM
    {
        private Dictionary _workingDictionary;
        private Dictionary _tempDictionary;
        private SolidSettings _solidSettings;
        private MarkerSettingsPM _markerSettingsModel;
        private List<string> _allMarkers;
        private String _tempDictionaryPath;
        private List<RecordFilter> _recordFilters;
        private List<Record> _masterRecordList;
        private RecordNavigatorPM _navigatorModel;
        private FilterChooserPM _filterChooserModel;
        private SfmEditorPM _sfmEditorModel;
        private SearchPM _searchModel;

        public event EventHandler DictionaryProcessed;

        public MainWindowPM()
        {
            _recordFilters = new List<RecordFilter>();
            _workingDictionary = new Dictionary();
            _markerSettingsModel = new MarkerSettingsPM();
            _tempDictionaryPath = Path.Combine(Path.GetTempPath(),"TempDictionary.db");
            _filterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();
            _sfmEditorModel = new SfmEditorPM();
            _searchModel = new SearchPM();


            _allMarkers = _workingDictionary.AllMarkers;
            _masterRecordList = _workingDictionary.AllRecords;
            FilterChooserModel.RecordFilters = _recordFilters;
            _searchModel.MasterRecordList = MasterRecordList;
            _navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = new RecordFilter();
            _markerSettingsModel.AllMarkers = _allMarkers;


            DictionaryProcessed += _filterChooserModel.OnDictionaryProcessed;
        }

        public MarkerSettingsPM MarkerSettingsModel
        {
            get
            {
                return _markerSettingsModel;
            }
        }

        public SolidSettings SolidSettings
        {
            get { return _solidSettings; }
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
            if(_solidSettings!=null)
            {
                _solidSettings.Save();
            }

            _workingDictionary.Open(path);
            _solidSettings =
                SolidSettings.OpenSolidFile(
                    Path.Combine(_workingDictionary.GetDirectoryPath(), _workingDictionary.GetFileNameNoExtension() + ".solid"));

            _markerSettingsModel.MarkerSettings = _solidSettings.MarkerSettings;
            _markerSettingsModel.Root = _solidSettings.RecordMarker;
            ProcessLexicon();
        }

        public void ProcessLexicon()
        {
            _tempDictionary = _workingDictionary.SaveAs(_tempDictionaryPath);
            
            //proccess temporary dictionary
            //_tempDictionaryPath

            UpdateRecordFilters(Path.Combine(_workingDictionary.GetDirectoryPath(),"report.xml"));

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
        }

        public bool SaveDictionary()
        {
            _solidSettings.Save();
            return _workingDictionary.Save();
        }

        public Dictionary SaveDictionaryAs(string path)
        {
            return _workingDictionary.SaveAs(path);
        }

        public List<string> GetTemplatePaths()
        {
            List<string> paths = new List<string>();

            foreach (string path in Directory.GetFiles(TopAppDirectory, "*.solid"))
            {
                paths.Add(path);
            }

            foreach (string path in Directory.GetFiles(_workingDictionary.GetDirectoryPath(), "*.solid"))
            {
                paths.Add(path);
            }
            return paths;
        }


        private static string TopAppDirectory
        {
            get
            {
                string path;

                path = DirectoryOfExecutingAssembly;

                if (path.ToLower().IndexOf("output") > -1)
                {
                    //go up to output
                    path = Directory.GetParent(path).FullName;
                    //go up to directory containing output
                    path = Directory.GetParent(path).FullName;
                }
                return path;
            }
        }

        private static string DirectoryOfExecutingAssembly
        {
            get
            {
                string path;
                bool unitTesting = Assembly.GetEntryAssembly() == null;
                if (unitTesting)
                {
                    path = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
                    path = Uri.UnescapeDataString(path);
                }
                else
                {
                    path = Assembly.GetExecutingAssembly().Location;
                }
                return Directory.GetParent(path).FullName;
            }
        }

        public string PathToCurrentSolidSettingsFile
        {
            get
            {
                return _solidSettings.FilePath;
            }
        }
    }
}
