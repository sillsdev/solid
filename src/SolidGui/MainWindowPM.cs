using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using SolidEngine;

namespace SolidGui
{
    /// <summary>
    /// Presentation Model (ui-agnostic) half of MainWindow
    /// </summary>
    public class MainWindowPM
    {
        private Dictionary _workingDictionary;
        private String _tempDictionaryPath;
        private String _realDictionaryPath;

        private SolidSettings _solidSettings;
        private MarkerSettingsPM _markerSettingsModel;
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


            _masterRecordList = WorkingDictionary.AllRecords;
            FilterChooserModel.RecordFilters = _recordFilters;
            _searchModel.Dictionary = _workingDictionary;
            //!!!_navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = new NullRecordFilter();
            _markerSettingsModel.AllMarkers = WorkingDictionary.AllMarkers;
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

            _realDictionaryPath = dictionaryPath;
            _solidSettings = CreateSolidSettings(templatePath);
            _workingDictionary.Open(_realDictionaryPath, _solidSettings, _recordFilters);
            _filterChooserModel.OnDictionaryProcessed();

            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }

        }

        private SolidSettings CreateSolidSettings(string templatePath)
        {
            if (File.Exists(SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath)))
            {
                LoadSettingsFromExistingFile();
            }
            else
            {
                LoadSettingsFromTemplate(templatePath);
            }            
            GiveSolidSettingsToModels();

            return _solidSettings;
        }

        private void GiveSolidSettingsToModels()
        {
            _markerSettingsModel.MarkerSettings = _solidSettings.MarkerSettings;
            _markerSettingsModel.Root = _solidSettings.RecordMarker;
            _sfmEditorModel.Settings = _solidSettings;
        }

        private void LoadSettingsFromTemplate(string templatePath)
        {
            Debug.Assert(!string.IsNullOrEmpty(templatePath));
            _solidSettings =
                SolidSettings.CreateSolidFileFromTemplate(templatePath, SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath));
        }

        private void LoadSettingsFromExistingFile()
        {
            _solidSettings =
                SolidSettings.OpenSolidFile(
                    Path.Combine(WorkingDictionary.GetDirectoryPath(),
                                 SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath)));
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
            WorkingDictionary.SaveAs(_tempDictionaryPath);

            _workingDictionary.Open(_tempDictionaryPath, _solidSettings, _recordFilters);
            _filterChooserModel.OnDictionaryProcessed();

            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }
        }

        public bool SaveDictionary()
        {
            _solidSettings.Save();
            WorkingDictionary.SaveAs(_realDictionaryPath);
            return true; // Todo: can't fail.
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

                foreach (string path in Directory.GetFiles(WorkingDictionary.GetDirectoryPath(), "*.solid"))
                {
                    if (_solidSettings != null && path != _solidSettings.FilePath)
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
                return WorkingDictionary.FilePath;
            }
        }

        public Dictionary WorkingDictionary
        {
            get
            {
                return _workingDictionary;
            }
        }

        public void UseSolidSettingsTemplate(string path)
        {
            _solidSettings.Save();
            LoadSettingsFromTemplate(path);
            GiveSolidSettingsToModels();
            ProcessLexicon();

            //???? do we replace these settings, or ask the settings to do the switch?
            //todo: copy over this set of settings
            //todo: reload settings UI
            //todo: clear out the report
        }
    }
}
