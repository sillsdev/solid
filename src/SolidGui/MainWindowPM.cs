// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
using SolidGui.Setup;


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
        private RecordFilterSet _recordFilters;  // TODO! redundant with the property in the RecordFilter model? -JMC
        private SfmEditorPM _sfmEditorModel;
        private SfmDictionary _workingDictionary;
        // private List<Record> _masterRecordList;
        private String _realDictionaryPath;
        private SearchViewModel _searchModel;
        public bool needsSave = false;

        private static Regex _cleanUpIndents;
        private static Regex _cleanUpClosers;
        private static Regex _cleanUpInferred;
        private static Regex _cleanUpSeparators;
        private static Regex _cleanUpNewlines;
        /*
        private static Regex _cleanUpSeparators;
        private static Regex _cleanUpNewlinesNonWindows;
        private static Regex _cleanUpNewlinesNonLinux;
        */


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

        private static String TempDictionaryPath()
        {
            return Path.Combine(Path.GetTempPath(), "TempDictionary.db");
        }

        public void Initialize(SfmDictionary dict) // , SolidSettings settings)
        {
            _recordFilters = new RecordFilterSet();
            _workingDictionary = dict;
            // Settings = settings;
            _markerSettingsModel = new MarkerSettingsPM(this);
            _warningFilterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM(this);
            EditorRecordFormatter = new RecordFormatter();
            EditorRecordFormatter.SetDefaultsUiTree();
            _sfmEditorModel = new SfmEditorPM(this);  // passing s.t. with access to the dict will help fix issue #173 etc. (adding/deleting entries) -JMC
            _searchModel = new SearchViewModel(this, EditorRecordFormatter);
            // _masterRecordList = WorkingDictionary.AllRecords;  // Got rid of this extra-step link because it made it harder to swap out the model. -JMC 2013-10
            WarningFilterChooserModel.RecordFilters = _recordFilters;  // JMC:!! get rid of this too? (i.e. use the main PM instead)
            _searchModel.Dictionary = _workingDictionary;
            //!!!_navigatorModel.MasterRecordList = MasterRecordList;
            _navigatorModel.ActiveFilter = new NullRecordFilter();  // TODO: remove the NullRecordFilter class? (try setting to null first and fixing what breaks) -JMC
            _markerSettingsModel.MarkersInDictionary = _workingDictionary.AllMarkers;   // TODO! get rid of this too? It's safer (e.g. after canceling File Open) to have fewer handles to manage. -JMC

            // Wiring: the nav should update its own filter whenever something (other than null) is selected in a chooser -JMC 2013-10
            WarningFilterChooserModel.WarningFilterChanged += NavigatorModel.OnFilterChanged;
            MarkerSettingsModel.MarkerFilterChanged += NavigatorModel.OnFilterChanged;
            // and the choosers should listen to the nav so they can clear themselves as needed
            NavigatorModel.NavFilterChanged += WarningFilterChooserModel.OnNavFilterChanged;
            NavigatorModel.NavFilterChanged += MarkerSettingsModel.OnNavFilterChanged;

            RegexOptions options = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Multiline;
            _cleanUpIndents = new Regex(@"^[ \t]+\\", options);  //any line-initial spaces or tabs
            _cleanUpClosers = new Regex(@" ?\\\w+\* ?", options);  //any closing tag, and possibly one space buffer around it
            _cleanUpInferred = new Regex(@"^[ \t]*\\\+.*(\r|\r?\n)", options);  //any inferred marker (\+mrkr) and its value and newline (any leading whitespace)
            _cleanUpSeparators = new Regex(@"\t", options);  // one tab
            _cleanUpNewlines = new Regex(@"(\r?\n|\r)", options);  // one newline (\r\n or \n or \r)
        }

        public MarkerSettingsPM MarkerSettingsModel
        {
            get
            {
                return _markerSettingsModel;
            }
        }

        // Remove this property? (Or replace it with pass-thru of _markerSettingsModel.SolidSettings) -JMC
        // Yes, done (pass-thru). -JMC
        public SolidSettings Settings
        {
            get
            {
                return _markerSettingsModel.SolidSettings;
            }  // was get {}
            private set { _markerSettingsModel.SolidSettings = value; }  // was private set {}
        }

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
            set { _realDictionaryPath = value; }
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
                        }
                        else
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
                return Path.Combine(TopAppDirectory, TemplatesFolder);
            }
        }


        public static string TopAppDirectory
        {
            get
            {
                string path = DirectoryOfExecutingAssembly;
                int outputIndex = path.ToLowerInvariant().IndexOf("output");
                if (outputIndex > -1)
                {
                    path = path.Substring(0, outputIndex);
                }
                return path;
            }
        }

        private RecordFormatter _editorRecordFormatter;

        public RecordFormatter EditorRecordFormatter
        {
            get { return _editorRecordFormatter; }

            private set
            {
                _editorRecordFormatter = value;                
            }
        }

        public void SyncFormat(RecordFormatter rf, bool updateSearchView)
        {
            if (_editorRecordFormatter.ShowIndented != rf.ShowIndented)
            {
                // We need to get in sync with the dialog's indentation
                _editorRecordFormatter = rf;
                var args = new RecordFormatterChangedEventArgs(rf);
                if (updateSearchView)  // this if is intended to block ping-ponging invokes -JMC
                    if (EditorRecordFormatterChanged != null) EditorRecordFormatterChanged.Invoke(this, args);

                /*
                _editorRecordFormatter = new RecordFormatter();  //JMC:! Need a rich text subclass here!
                if (dialogRF.Indented)
                {
                    _editorRecordFormatter.SetDefaultsUiTree();
                }
                else
                {
                    _editorRecordFormatter.SetDefaultsUiFlat();
                }
                 */
            }

        }

        // JMC:! We should install to %APPDATA% as non-administrator and modify/rename this method to point there
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

        public event EventHandler<RecordFormatterChangedEventArgs> EditorRecordFormatterChanged;
        private const string TemplatesFolder = "templates";

        /// <summary>
        /// Called by the view to determine whether to ask the user for a starting template. No significant side effects, except it notifies the user on error.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ShouldAskForTemplateBeforeOpening(string filePath)
        {
            string solidFilePath = SolidSettings.GetSettingsFilePathFromDictionaryPath(filePath);
            if (!File.Exists(solidFilePath)) return true;
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
                
            return solidSettings == null;
        }

        private static string CleanupOne(string input, string rw, Regex reg, StringBuilder report, string label)
        {
            int c = 0;
            string output = reg.Replace(input, 
                (match) =>
                {
                    c++;
                    return match.Result(rw);
                });
            if ((c > 0) && (output != input))
            {
                report.AppendLine(String.Format("Replaced {0} occurrences of {1}",c,label));
                return output;
            }
            return input;
        }

        // Copy the file at the source path to a new file, doing cleanup find/replace on it.
        // Returns a report of what was cleaned up; if nothing, returns an empty string.
        private static string CleanupToTempFile(string source, string tmp)
        {
            var report = new StringBuilder("");

            string writeText = File.ReadAllText(source, SolidSettings.LegacyEncoding);

            writeText = CleanupOne(writeText, "\\", _cleanUpIndents, report, "Indentation"); //potentially releases writeText's memory?
            writeText = CleanupOne(writeText, "", _cleanUpClosers, report, "Closers");     //ditto, and below too
            writeText = CleanupOne(writeText, "", _cleanUpInferred, report, "Inferred fields (and any values!)");
            writeText = CleanupOne(writeText, " ", _cleanUpSeparators, report, "Tabs (become single spaces)"); 
            writeText = CleanupOne(writeText, SolidSettings.NewLine, _cleanUpNewlines, report, "Newlines");

            File.WriteAllText(tmp, writeText, SolidSettings.LegacyEncoding);
            return report.ToString();
        }

        /// <summary>
        /// Caller should first call ShouldAskForTemplateBeforeOpening, and supply a non-null templatePath if that returns true
        /// </summary>
        /// <param name="dictionaryPath"></param>
        /// <param name="templatePath"></param>
        /// <returns>True if successful; false otherwise.</returns>
        public bool OpenDictionary(string dictionaryPath, string templatePath, bool forceUnicode)
        {
            bool saveSettings = false;
            _realDictionaryPath = dictionaryPath; 
            string solidFilePath = SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath);
            if (File.Exists(solidFilePath))
            {
                Settings = LoadSettingsFromExistingFile(solidFilePath); 
            }
            else
            {
                Settings = LoadSettingsFromTemplate(templatePath);
                if (forceUnicode)
                {
                    Settings.SetAllUnicodeTo(true); // implements #1259
                    saveSettings = true;
                }
            }
            GiveSolidSettingsToModels();

            //var dict = _workingDictionary.Open(_realDictionaryPath, Settings, _recordFilters);
            //var dict = new SfmDictionary();          

            
            string f;
            /*
            // TODO: #1274 Use regex to discard anything added by Save a Copy (leading spaces, tabs, closing tags). Disabled until more testing with encodings (esp. mixed ones) can be done. -JMC
            f = TempDictionaryPath();
            string report = CleanupToTempFile(_realDictionaryPath, f);
            if (report != "")
            {
                string msg = String.Format("Did the following cleanup: \n{0}\nChange(s) will become permanent if you save.\n", report);
                msg += @"Actually, so far this prototype feature only makes those changes here (please verify special character encodings): %appdata%\..\local\temp\TempDictionary.db"; //JMC:! delete this
                MessageBox.Show(msg, "Preprocessing made changes"); //TODO: move to UI? (my bad) -JMC
                Logger.WriteEvent(msg);
                needsSave = true;
            }
             */
            f = _realDictionaryPath;

            if (_workingDictionary.Open(f, Settings, _recordFilters))  
            {
                //if (DictionaryProcessed != null) DictionaryProcessed.Invoke(this, EventArgs.Empty);  //unnecessary? -JMC Sep 2014
                if (saveSettings) Settings.Save(); // Together with one other line, fixes bug #1260
                return true;
            }

            return false;
        }

        private void GiveSolidSettingsToModels()
        {
            _markerSettingsModel.SolidSettings = Settings;
            _markerSettingsModel.Root = Settings.RecordMarker;
        }

        // Copy and open a .solid file. Note that any migration done here is silent. The code calling this may report it
        // using the report object, but that's optional because the user hasn't tweaked settings yet. (Also, it's just a copy.) -JMC July 2014
        private SolidSettings LoadSettingsFromTemplate(string templatePath)
        {
            Palaso.Reporting.Logger.WriteEvent("Loading Solid file from template located at {0}", templatePath);
            Trace.Assert(!string.IsNullOrEmpty(templatePath), "Bug: no path provided for the templates folder.");
            SolidSettings s = SolidSettings.CreateSolidFileFromTemplate(
                templatePath, 
                SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath)
            );
            return s;
        }

        private SolidSettings LoadSettingsFromExistingFile(string solidFilePath)
        {
            Palaso.Reporting.Logger.WriteEvent("Loading Solid file from {0}", solidFilePath);
            return SolidSettings.OpenSolidFile(
                Path.Combine(WorkingDictionary.GetDirectoryPath(), solidFilePath) //The first half will only be used if solidFilePath is not absolute -JMC
            );
        }

        /// <summary>
        /// Called by Recheck. Also called after a quick fix, change template, etc. (Do this for Replace All too?) -JMC
        /// </summary>
        public void ProcessLexicon()
        {
            string newPath = TempDictionaryPath();
            WorkingDictionary.SaveAs(newPath, Settings);

            _workingDictionary.Open(newPath, Settings, _recordFilters);

            //if (DictionaryProcessed != null) DictionaryProcessed.Invoke(this, EventArgs.Empty);  //unnecessary? -JMC Sep 2014
        }

        public void SolidSettingsSaveAs(string filePath)
        {
            Settings.SaveAs(filePath);
        }

        // These methods seem redundant with SfmDictionary.SaveAs but they save both files. They are called by "Save" and "Save a Copy" buttons. -JMC
        // This is a REAL save--to a file the user knows about rather than a temp file. -JMC
        public bool DictionaryAndSettingsSaveAs(string dictionaryPath)
        {
            var rf = new RecordFormatter();
            rf.SetDefaultsDisk(); 
            return DictionaryAndSettingsSaveAs(dictionaryPath, rf);
        }
        public bool DictionaryAndSettingsSaveAs(string dictionaryPath, RecordFormatter rf)
        {
            string settingsPath = SolidSettings.GetSettingsFilePathFromDictionaryPath(dictionaryPath);
            bool success = Settings.SaveAs(settingsPath);
            return success && _workingDictionary.SaveAs(dictionaryPath, Settings, rf);
        }
        public bool DictionaryAndSettingsSave()
        {
            return DictionaryAndSettingsSaveAs(_realDictionaryPath);
            /*
            bool success = Settings.SaveAs(SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath));
            return success && _workingDictionary.SaveAs(_realDictionaryPath, Settings);
             */ 
        }

        public void SwitchSolidSettingsTemplate(string path)
        {
            if (Settings != null)
            {
                Settings.Save();
            }
            Settings = LoadSettingsFromTemplate(path);

            bool forceUnicode = EncodingChooser.UserWantsUnicode(PathToCurrentDictionary);
            if (forceUnicode) Settings.SetAllUnicodeTo(true); // implements #1259
            GiveSolidSettingsToModels();
            ProcessLexicon();
            Settings.Save(); // Together with one other line, fixes bug #1260
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

                _workingDictionary.SaveAs(TempDictionaryPath(), Settings);
                Settings.SaveAs(SolidSettings.GetSettingsFilePathFromDictionaryPath(TempDictionaryPath()));
                string sourceFilePath = TempDictionaryPath();

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
