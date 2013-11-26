using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Palaso.Reporting;
using Palaso.UI.WindowsForms.Progress;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.Filter;
using SolidGui.MarkerSettings;
using SolidGui.Model;
using SolidGui.Properties;
using SolidGui.Search;


namespace SolidGui
{
    /// <summary>
    /// Presentation Model (ui-agnostic) half of MainWindow
    /// </summary>
    public class MainWindowPM :IDisposable
    {
        // most of the following were readonly, but I think this s/b hot-swappable (e.g. so we can roll back after cancelling a File Open, issue #1205) -JMC 2013-10
        private FilterChooserPM _warningFilterChooserModel;
        private MarkerSettingsPM _markerSettingsModel;
        private RecordNavigatorPM _navigatorModel;
        private RecordFilterSet _recordFilters;  // JMC: redundant with the property in the RecordFilter model?
        private SfmEditorPM _sfmEditorModel;
        private String _tempDictionaryPath;
        private SfmDictionary _workingDictionary;
        // private List<Record> _masterRecordList;
        private String _realDictionaryPath;
        private SearchViewModel _searchModel;
        public bool needsSave = false;

        public MainWindowPM() 
        {
            Initialize(new SfmDictionary()); // , new SolidSettings());
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6}",
                _workingDictionary, _markerSettingsModel, _recordFilters, _warningFilterChooserModel, 
                _navigatorModel, _sfmEditorModel, GetHashCode());
        }

        public void Initialize(SfmDictionary dict) // , SolidSettings settings)
        {
            _recordFilters = new RecordFilterSet();
            _workingDictionary = dict;
            // Settings = settings;
            _markerSettingsModel = new MarkerSettingsPM();
            _tempDictionaryPath = Path.Combine(Path.GetTempPath(), "TempDictionary.db");
            _warningFilterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();
            _sfmEditorModel = new SfmEditorPM(this);  // passing s.t. with access to the dict will help fix issue #173 etc. (adding/deleting entries) -JMC
            _searchModel = new SearchViewModel(this);
            // _masterRecordList = WorkingDictionary.AllRecords;  // Got rid of this extra-step link because it made it harder to swap out the model. -JMC 2013-10
            WarningFilterChooserModel.RecordFilters = _recordFilters;  // JMC:!! get rid of this too? (i.e. use the main PM instead)
            _searchModel.Dictionary = _workingDictionary;
            //!!!_navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = new NullRecordFilter();  // JMC: remove? (try setting to null first and fixing what breaks)
            _markerSettingsModel.MarkersInDictionary = _workingDictionary.AllMarkers;   // JMC:!! get rid of this too?

            // Wiring: the nav should update its own filter whenever something (other than null) is selected in a chooser -JMC 2013-10
            WarningFilterChooserModel.WarningFilterChanged += NavigatorModel.OnFilterChanged;
            MarkerSettingsModel.MarkerFilterChanged += NavigatorModel.OnFilterChanged;
            // and the choosers should listen to the nav so they can clear themselves as needed
            NavigatorModel.NavFilterChanged += WarningFilterChooserModel.OnNavFilterChanged;
            NavigatorModel.NavFilterChanged += MarkerSettingsModel.OnNavFilterChanged;

        }

        public MarkerSettingsPM MarkerSettingsModel
        {
            get
            {
                return _markerSettingsModel;
            }
        }

        // JMC:! Remove the following or replace it with pass-thru of _markerSettingsModel.SolidSettings ??
        public SolidSettings Settings { get; private set; }
            // get { return _markerSettingsModel.SolidSettings; }

        public SearchViewModel SearchModel
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

        public FilterChooserPM WarningFilterChooserModel
        {
            get
            {
                return _warningFilterChooserModel;
            }
        }

        public bool CanProcessLexicon
        {
            get
            {
                return _workingDictionary.AllRecords.Count > 0; // return _masterRecordList.Count > 0;
            }
        }

        public string DictionaryRealFilePath
        {
            get { return _realDictionaryPath; }
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

                if (DictionaryRealFilePath != null)
                {
                    foreach (string path in Directory.GetFiles(Path.GetDirectoryName(DictionaryRealFilePath), "*.solid"))
                    {
                        if (Settings != null && path != Settings.FilePath)
                        {
                            paths.Add(path);
                        }
                    }
                }
                return paths;
            }
        }

        public static string PathToFactoryTemplatesDirectory
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
                string path = DirectoryOfExecutingAssembly;
                int outputIndex = path.ToLower().IndexOf("output");
                if (outputIndex > -1)
                {
                    path = path.Substring(0, outputIndex);
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
                if (Settings == null)
                {
                    return null;
                }
                return Settings.FilePath;
            }
        }

        public string PathToCurrentDictionary
        {
            get
            {
                return WorkingDictionary.FilePath;
            }
        }

        public SfmDictionary WorkingDictionary
        {
            get
            {
                return _workingDictionary;
            }
        }

        public event EventHandler DictionaryProcessed;

        /// <summary>
        /// Called by the view to determine whether to ask the user for a starting template. No significant side effects, except it notifies the user on error.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ShouldAskForTemplateBeforeOpening(string filePath)
        {
            bool result = true;
            string solidFilePath = SolidSettings.GetSettingsFilePathFromDictionaryPath(filePath);
            if (File.Exists(solidFilePath))
            {
                // Check also that the setting file is valid.  If it's not allow true to be returned to pop up
                // the template chooser.
                // See http://projects.palaso.org/issues/show/180
                SolidSettings solidSettings = null;
                try
                {
                    solidSettings = LoadSettingsFromExistingFile(solidFilePath);  // A dry run. If it succeeds, we'll reload later. -JMC
                }
                catch (InvalidOperationException e)
                {
                    ErrorReport.NotifyUserOfProblem(
                        String.Format("There was a problem opening the solid file '{0:s}'\n", solidFilePath) + e.Message
                    );
                }
                catch (XmlException e)
                {
                    ErrorReport.NotifyUserOfProblem(
                        String.Format("There was a problem opening the solid file '{0:s}'\n", solidFilePath) + e.Message
                    );
                    
                }
                catch (ApplicationException e) // Added because MigratorBase.cs has changed (Palaso lib, changeset 2103 (51be8122f653) ~Mar 2013). -JMC
                {
                    ErrorReport.NotifyUserOfProblem(
                        String.Format("There was a problem opening the solid file '{0:s}'\n", solidFilePath) + e.Message
                    );

                }
                
                result = solidSettings == null;
            }
            return result;
        }

        /// <summary>
        /// Caller should first call ShouldAskForTemplateBeforeOpening, and supply a non-null templatePath iff that returns true
        /// </summary>
        /// <param name="dictionaryPath"></param>
        /// <param name="templatePath"></param>
        /// <returns>True if successful; false otherwise.</returns>
        public bool OpenDictionary(string dictionaryPath, string templatePath)
        {
            _realDictionaryPath = dictionaryPath; 
            string solidFilePath = SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath);
            if (File.Exists(solidFilePath))
            {
                Settings = LoadSettingsFromExistingFile(solidFilePath); 
            }
            else
            {
                Settings = LoadSettingsFromTemplate(templatePath); 
            }
            GiveSolidSettingsToModels();

            //var dict = _workingDictionary.Open(_realDictionaryPath, Settings, _recordFilters);
            //var dict = new SfmDictionary();
            var dict = _workingDictionary;
            if (dict.Open(_realDictionaryPath, Settings, _recordFilters))  
            {
                if (DictionaryProcessed != null)
                {
                    DictionaryProcessed.Invoke(this, null);
                }
                return true;
            }

            return false;
        }

        private void GiveSolidSettingsToModels()
        {
            _markerSettingsModel.SolidSettings = Settings;
            _markerSettingsModel.Root = Settings.RecordMarker;
            // _sfmEditorModel.SolidSettings = Settings;  // JMC: hopefully unnecessary now
        }

        private SolidSettings LoadSettingsFromTemplate(string templatePath)
        {
            Palaso.Reporting.Logger.WriteEvent("Loading Solid file from Template from {0}", templatePath);
            Trace.Assert(!string.IsNullOrEmpty(templatePath), "Bug: no path provided for the templates folder.");
            return SolidSettings.CreateSolidFileFromTemplate(
                templatePath, 
                SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath)
            );
        }

        private SolidSettings LoadSettingsFromExistingFile(string solidFilePath)
        {
            Palaso.Reporting.Logger.WriteEvent("Loading Solid file from {0}", solidFilePath);
            return SolidSettings.OpenSolidFile(
                Path.Combine(WorkingDictionary.GetDirectoryPath(), solidFilePath) //The first half will only be used if solidFilePath is not absolute -JMC
            );
        }

        // Called by Recheck. Also called after a quick fix, change template, etc. (Do this for Replace All too?) -JMC
        public void ProcessLexicon()
        {
            WorkingDictionary.SaveAs(_tempDictionaryPath);

            _workingDictionary.Open(_tempDictionaryPath, Settings, _recordFilters);

            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }
        }

        public void SolidSettingsSaveAs(string filePath)
        {
            Settings.SaveAs(filePath);
        }

        public bool DictionaryAndSettingsSave()
        {
            bool success = Settings.SaveAs(SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath));
            return success && _workingDictionary.SaveAs(_realDictionaryPath);
        }

        public void UseSolidSettingsTemplate(string path)
        {
            Settings.Save();
            LoadSettingsFromTemplate(path);
            GiveSolidSettingsToModels();
            ProcessLexicon();  

            //???? do we replace these settings, or ask the settings to do the switch?
            // TODO: copy over this set of settings
            // TODO: reload settings UI
            // TODO: clear out the report
        }

        public void Export(int filterIndex, string destinationFilePath)
        {
            using (var dlg = new ExportLogDialog())
            {
                ExportFactory f = ExportFactory.Singleton();
                IExporter exporter = f.CreateFromSettings(f.ExportSettings[filterIndex]);

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += exporter.ExportAsync;
                dlg.BackgroundWorker = worker;
               // dlg.CanCancel = true;

                destinationFilePath = exporter.ModifyDestinationIfNeeded(destinationFilePath);

                _workingDictionary.SaveAs(_tempDictionaryPath);
                Settings.SaveAs(SolidSettings.GetSettingsFilePathFromDictionaryPath(_tempDictionaryPath));
                string sourceFilePath = _tempDictionaryPath;

                ExportArguments exportArguments = new ExportArguments();
                exportArguments.inputFilePath = sourceFilePath;
                exportArguments.outputFilePath = destinationFilePath;
                exportArguments.countHint = _workingDictionary.Count;
                exportArguments.markerSettings = _markerSettingsModel;
                exportArguments.progress = dlg.LogBox;

                dlg.Arguments = exportArguments;
                dlg.ShowDialog();

            }
        }

        // Probably not proper, but I'm hoping this will flush out existing/new bugs related to pieces of Solid 
        // pointing to the old file's stuff after a second file is opened. -JMC 2013-10
        public void Dispose()
        {
            if (_warningFilterChooserModel != null) _warningFilterChooserModel.Dispose();
            if (_navigatorModel != null) _navigatorModel.Dispose();
            if (_recordFilters != null) _recordFilters.Dispose();
            if (_workingDictionary != null) _workingDictionary.Clear();

            //quick and dirty, for now
            if (_markerSettingsModel != null) _markerSettingsModel.SolidSettings = null;
            _markerSettingsModel = null;
            // if (_sfmEditorModel != null) _sfmEditorModel.SolidSettings = null;  // delete this
            _sfmEditorModel = null;
            _searchModel = null;

        }
    }


}
