using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Palaso.UI.WindowsForms.Progress;
using SolidEngine;

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
    	private readonly SfmDictionary _workingDictionary;
    	private List<Record> _masterRecordList;
    	private String _realDictionaryPath;
    	private SearchPM _searchModel;
    	private SolidSettings _solidSettings;


    	public MainWindowPM()
        {
            _recordFilters = new RecordFilterSet();
            _workingDictionary = new SfmDictionary();
            _markerSettingsModel = new MarkerSettingsPM();
            _tempDictionaryPath = Path.Combine(Path.GetTempPath(),"TempDictionary.db");
            _filterChooserModel = new FilterChooserPM();
            _navigatorModel = new RecordNavigatorPM();
            _sfmEditorModel = new SfmEditorPM(_navigatorModel);
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
    					if (_solidSettings != null && path != _solidSettings.FilePath)
    					{
    						paths.Add(path);
    					}
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
    			string path = DirectoryOfExecutingAssembly;

    			if (path.ToLower().IndexOf("output") > -1)
    			{
					// ReSharper disable PossibleNullReferenceException
					//go up to output
    				path = Directory.GetParent(path).FullName;
    				//go up to directory containing output
    				path = Directory.GetParent(path).FullName;
					// ReSharper restore PossibleNullReferenceException
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

    	public SfmDictionary WorkingDictionary
    	{
    		get
    		{
    			return _workingDictionary;
    		}
    	}

    	public event EventHandler DictionaryProcessed;

    	/// <summary>
        /// Called by the view to determine whether to ask the user for a starting template
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
					solidSettings = LoadSettingsFromExistingFile(solidFilePath);
				}
				catch (InvalidOperationException e)
				{
					Palaso.Reporting.ErrorReport.NotifyUserOfProblem(
						String.Format("There was a problem opening the solid file '{0:s}'\n", solidFilePath) + e.Message
					);
				}
				result = solidSettings == null;
			}
			return result;
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
			string solidFilePath = SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath);
			if (File.Exists(solidFilePath))
			{
				_solidSettings = LoadSettingsFromExistingFile(solidFilePath);
			}
			else
			{
				_solidSettings = LoadSettingsFromTemplate(templatePath);
			}
			GiveSolidSettingsToModels();
			_workingDictionary.Open(_realDictionaryPath, _solidSettings, _recordFilters);
            _filterChooserModel.OnDictionaryProcessed();

            if (DictionaryProcessed != null)
            {
                DictionaryProcessed.Invoke(this, null);
            }

        }

    	private void GiveSolidSettingsToModels()
        {
            _markerSettingsModel.MarkerSettings = _solidSettings.MarkerSettings;
            _markerSettingsModel.Root = _solidSettings.RecordMarker;
            _sfmEditorModel.Settings = _solidSettings;
        }

		private SolidSettings LoadSettingsFromTemplate(string templatePath)
        {
            Palaso.Reporting.Logger.WriteEvent("Loading Solid file from Template from {0}", templatePath);
            Debug.Assert(!string.IsNullOrEmpty(templatePath));
            return SolidSettings.CreateSolidFileFromTemplate(
				templatePath, 
				SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath)
			);
        }

		private SolidSettings LoadSettingsFromExistingFile(string solidFilePath)
        {
			Palaso.Reporting.Logger.WriteEvent("Loading Solid file from {0}", solidFilePath);
			return SolidSettings.OpenSolidFile(
				Path.Combine(WorkingDictionary.GetDirectoryPath(), solidFilePath)
			);
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
                Palaso.Reporting.Logger.WriteEvent("Saving settings");
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

        public void SolidSettingsSaveAs(string filePath)
        {
            _solidSettings.SaveAs(filePath);
        }

        public bool DictionaryAndSettingsSave()
        {
            _solidSettings.SaveAs(SolidSettings.GetSettingsFilePathFromDictionaryPath(_realDictionaryPath));
            _workingDictionary.SaveAs(_realDictionaryPath);
            return true; // Todo: can't fail.
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

        public void Export(int filterIndex, string destinationFilePath)
        {
            using (ProgressDialog dlg = new ProgressDialog())
            {
                ExportFactory f = ExportFactory.Singleton();
                IExporter exporter = f.CreateFromSettings(f.ExportSettings[filterIndex]);

                dlg.Overview = "Please wait...";
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += exporter.OnDoWork;
                dlg.BackgroundWorker = worker;
                dlg.CanCancel = true;

                _workingDictionary.SaveAs(_tempDictionaryPath);
                _solidSettings.SaveAs(SolidSettings.GetSettingsFilePathFromDictionaryPath(_tempDictionaryPath));
                string sourceFilePath = _tempDictionaryPath;

                ExportArguments exportArguments = new ExportArguments();
                exportArguments.inputFilePath = sourceFilePath;
                exportArguments.outputFilePath = destinationFilePath;
                exportArguments.countHint = _workingDictionary.Count;

                dlg.ProgressState.Arguments = exportArguments;
                dlg.ShowDialog();
                if (dlg.ProgressStateResult != null && dlg.ProgressStateResult.ExceptionThatWasEncountered != null)
                {
                    Palaso.Reporting.ErrorReport.ReportNonFatalException(dlg.ProgressStateResult.ExceptionThatWasEncountered);
                    return;
                }

                //exporter.Export(sourceFilePath, destinationFilePath);
            }
        }


    }


}
