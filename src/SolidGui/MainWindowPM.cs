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
using SolidGui.Search;


namespace SolidGui
{
    /// <summary>
    /// Presentation Model (ui-agnostic) half of MainWindow
    /// </summary>
    public class MainWindowPM
    {
        private readonly FilterChooserPM _filterChooserModel;
        private readonly MarkerSettingsPM _markerSettingsModel;
        private readonly RecordNavigatorPM _navigatorModel;
        private readonly RecordFilterSet _recordFilters;
        private readonly SfmEditorPM _sfmEditorModel;
        private readonly String _tempDictionaryPath;
        private SfmDictionary _workingDictionary;
        // private List<Record> _masterRecordList;
        private String _realDictionaryPath;
        private SearchViewModel _searchModel;
        public bool needsSave = false;

        public MainWindowPM()
        {
            _recordFilters = new RecordFilterSet();
            _workingDictionary = new SfmDictionary();
            _markerSettingsModel = new MarkerSettingsPM();
            _tempDictionaryPath = Path.Combine(Path.GetTempPath(),"TempDictionary.db");
            _filterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();
            _sfmEditorModel = new SfmEditorPM(_navigatorModel, _workingDictionary);  // passing the dict will help fix issue #173 etc. (adding/deleting entries) -JMC
            _searchModel = new SearchViewModel();

            Initialize();
        }

        private void Initialize()
        {
            // _masterRecordList = WorkingDictionary.AllRecords;  // Disabled this extra-step link because it made it harder to swap out the model. -JMC 2013-10
            FilterChooserModel.RecordFilters = _recordFilters;
            _searchModel.Dictionary = _workingDictionary;
            //!!!_navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = new NullRecordFilter();  // JMC: remove? (try setting to null first and fixing what breaks)
            _markerSettingsModel.MarkersInDictionary = WorkingDictionary.AllMarkers;
        }

        public MarkerSettingsPM MarkerSettingsModel
        {
            get
            {
                return _markerSettingsModel;
            }
        }

        public SolidSettings Settings { get; private set; }

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

        /// <summary>
        /// A list containing every lexical record in the dictionary
        /// </summary>
        public List<Record> MasterRecordList
        {
            get
            {
                return _workingDictionary.AllRecords; // return _masterRecordList;
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
            // JMC:! starting here, we need to be able to roll the following back if the user cancels the File Open.
            // E.g. if they have an open file with unsaved data, its state should remain the same. (I.e. simply reloading the previous file is not an adequate "rollback".)
            // The approach used here isn't very elegant, but it involves less refactoring. (Making SfmDictionary and MainWindowPM implement ICloneable might help.)
            // http://stackoverflow.com/questions/11074381/deep-copy-of-a-c-sharp-object
            string origDictionaryPath = _realDictionaryPath;
            SolidSettings origSettings = Settings;
            SfmDictionary origDict = _workingDictionary;

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


            var dict = new SfmDictionary();
            if (dict.Open(_realDictionaryPath, Settings, _recordFilters))  
            {
                _workingDictionary = dict;
                if (DictionaryProcessed != null)
                {
                    DictionaryProcessed.Invoke(this, null);
                }
                return true;
            }

            // File Open was cancelled or didn't succeed. Roll back.
            _realDictionaryPath = origDictionaryPath;
            Settings = origSettings;
            GiveSolidSettingsToModels();
            _workingDictionary = origDict;
            Initialize();

            // JMC: need to also invoke DictionaryProcessed ? Hopefully not, since that would switch to the default filter.

/*
            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }
*/

            return false;
        }

        private void GiveSolidSettingsToModels()
        {
            _markerSettingsModel.SolidSettings = Settings;
            _markerSettingsModel.Root = Settings.RecordMarker;
            _sfmEditorModel.SolidSettings = Settings;
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

        /// <summary>
        /// Call this before switching dictionaries or quitting
        /// </summary>
        /// <returns>false if user cancelled</returns>
        public bool SaveOffOpenModifiedStuff()
        {
            //review Mark(JH): do we need to save an existing, open dictionary at this point (and let the user cancel)?

            if (Settings != null)
            {
                Palaso.Reporting.Logger.WriteEvent("Saving settings");
                Settings.Save();
            }

            return true; // TODO: let the user cancel if the dictionary was changed (JMC:! e.g. http://projects.palaso.org/issues/1149)
        }

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

    }


}
