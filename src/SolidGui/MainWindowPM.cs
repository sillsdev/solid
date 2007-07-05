using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
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
        class SolidObserver : Solidifier.Observer
        {
            private Dictionary _dictionary;

            public SolidObserver(Dictionary dictionary)
            {
                _dictionary = dictionary;
            }

            public override void OnRecordProcess(XmlNode structure, SolidReport report)
            {
                _dictionary.AddRecord(structure, report);
            }
        }

        private Dictionary _workingDictionary;
        private SolidSettings _solidSettings;
        private MarkerSettingsPM _markerSettingsModel;
        private List<string> _allMarkers;
        private String _tempDictionaryPath;
        private RecordFilterSet _recordFilters;
        private List<Record> _masterRecordList;
        private RecordNavigatorPM _navigatorModel;
        private FilterChooserPM _filterChooserModel;
        private SfmEditorPM _sfmEditorModel;
        private SearchPM _searchModel;

        public event EventHandler DictionaryProcessed;

        public MainWindowPM()
        {
            _recordFilters = new RecordFilterSet();
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
            //!!!_navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = new NullRecordFilter();
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

        public RecordFilterSet RecordFilters
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

        /// <summary>
        /// Called by the view to determine whether to ask the user for a starting template
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ShouldAskForTemplateBeforeOpening(string path)
        {
            return !File.Exists(SolidSettings.GetSettingsFilePathFromDictionaryPath(path));
        }

        /// <summary>
        /// Caller should first call ShouldAskForTemplateBeforeOpening, and supply a templatePath iff that returns true
        /// </summary>
        /// <param name="dictionaryPath"></param>
        /// <param name="templatePath"></param>
        public void OpenDictionary(string dictionaryPath, string templatePath)
        {
            if (!SaveOffOpenModifiedStuff())
            {
                return;
            }

            _workingDictionary.Open(dictionaryPath);

            if (File.Exists(SolidSettings.GetSettingsFilePathFromDictionaryPath(_workingDictionary.FilePath)))
            {
                _solidSettings =
                    SolidSettings.OpenSolidFile(
                        Path.Combine(_workingDictionary.GetDirectoryPath(),
                                     SolidSettings.GetSettingsFilePathFromDictionaryPath(_workingDictionary.FilePath)));
            }
            else
            {
                Debug.Assert(!string.IsNullOrEmpty(templatePath));
                _solidSettings =
                    SolidSettings.CreateSolidFileFromTemplate(templatePath, SolidSettings.GetSettingsFilePathFromDictionaryPath(_workingDictionary.FilePath));
            }
            _markerSettingsModel.MarkerSettings = _solidSettings.MarkerSettings;
            _markerSettingsModel.Root = _solidSettings.RecordMarker;
            ProcessLexicon();
        }

        /// <summary>
        /// Call this before switching dictionaries or quitting
        /// </summary>
        /// <returns>false if user cancelled</returns>
        private bool SaveOffOpenModifiedStuff()
        {
            //review Mark(JH): do we need to save an existing, open dictionary at this point (and let the user cancel)?

            if(_solidSettings!=null)
            {
                _solidSettings.Save();
            }

            return true; //todo: let the user cancel if the dictionary was changed
        }

        public void ProcessLexicon()
        {
            _workingDictionary.CopyTo(_tempDictionaryPath);

            _workingDictionary.Clear();
            SolidObserver observer = new SolidObserver(_workingDictionary);
            Solidifier solid = new Solidifier();
            solid.Attach(observer);
            solid.Process(_tempDictionaryPath, _solidSettings);
            
            //proccess temporary dictionary
            //_tempDictionaryPath
            //!!!_recordFilters.OnSolidReportChange(report);
            //!!!UpdateRecordFilters(report);

            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }
        }

        private void UpdateRecordFilters(SolidReport report)
        {
            /*
            _recordFilters.Clear();

            _recordFilters.Add(new AllRecordFilter(_masterRecordList));
            _recordFilters.Add(new SolidReportRecordFilter(report));
            _recordFilters.Add(new NullRecordFilter());
            _recordFilters.Add(new RegExRecordFilter("Has Note", @"\\nt\s\w",_masterRecordList));
            _recordFilters.Add(new RegExRecordFilter("Missing N Gloss", @"\\gn\s\w", true,_masterRecordList));
            _recordFilters.Add(new RegExRecordFilter("Missing ps", @"\\ps\s\w", true,_masterRecordList));
            */
        }

        public bool SaveDictionary()
        {
            _solidSettings.Save();
            return _workingDictionary.Save();
        }

        public Dictionary SaveDictionaryAs(string path)
        {
            return _workingDictionary.CopyTo(path);
        }

        /// <summary>
        /// Note: omits the currently in-use settings
        /// </summary>
        public List<string> TemplatePaths
        {
            get
            {
                List<string> paths = new List<string>();

                foreach (string path in Directory.GetFiles(PathToFactoryTemplatesDirectory, "*.solid"))
                {
                    paths.Add(path);
                }

                foreach (string path in Directory.GetFiles(_workingDictionary.GetDirectoryPath(), "*.solid"))
                {
                    if (path != _solidSettings.FilePath)
                    {
                        paths.Add(path);
                    }
                }
                return paths;
            }
        }

        public string PathToFactoryTemplatesDirectory
        {
            get
            {
                return Path.Combine(TopAppDirectory, "templates");
            }
        }


        public static string TopAppDirectory
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
                if (_solidSettings == null)
                {
                    return null;
                }
                return _solidSettings.FilePath;
            }
        }

        public string PathToCurrentDictionary
        {
            get
            {
                return _workingDictionary.FilePath;
            }
        }

        public void UseSolidSettingsTemplate(string path)
        {
            _solidSettings.Save();
           
            //???? do we replace these settings, or ask the settings to do the switch?
            //todo: copy over this set of settings
            //todo: reload settings UI
            //todo: clear out the report
        }
    }
}
