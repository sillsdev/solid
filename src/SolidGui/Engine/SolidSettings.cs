using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Palaso.Reporting;
using SolidGui.Migration;

namespace SolidGui.Engine
{
    public class SolidSettings
    {

        private readonly List<SolidMarkerSetting> _markerSettings;
        private string _recordMarker = "lx";
        public const int LatestVersion = 2; // JMC: Safer to use readonly here?

        public SolidSettings()
        {
        	Version = LatestVersion.ToString();
			DefaultEncodingUnicode = false;
			_markerSettings = new List<SolidMarkerSetting>();
        }

        // Candidates that could be global 'constants' (or public static...): "lx", "entry", ".solid", "infer ", "Report Error"

        private static List<string> _fileExtensions = new List<string> { ".db", ".sfm", ".mdf", ".dic", ".txt", ".lex" };  // added by JMC 2013-09

        public static List<string> FileExtensions  // added by JMC 2013-09; note that it's not read-only
        {
            get { return _fileExtensions; }
        }

        public static string extsAsString(IEnumerable<string> exts) // added by JMC 2013-09
        {
            var x = new StringBuilder();
            foreach (var extension in exts)
            {
                x.Append(extension + " ");
            }
            return x.ToString().Trim();
        }

        // Returns the known dictionary extensions as a mask; baseFileName can simply be "*"
        public static string extsAsMask(IEnumerable<string> exts, string baseFileName) // added by JMC 2013-09
        {
            var x2 = new StringBuilder();
            foreach (var extension in exts)
            {
                x2.Append(baseFileName + extension + ";");
            }
            return x2.ToString();
        }

        public string RecordMarker
        {
            get { return _recordMarker; }
            set { _recordMarker = value; }
        }

    	public string Version { get; set; } // set needs to be public for XmlSerialize to work CP.

    	public IEnumerable<string> Markers
        {
            get 
            {
                return _markerSettings.Select(item => item.Marker);
            }
        }

        public List<SolidMarkerSetting> MarkerSettings // TODO review: this shouldn't be visible
        {
            get { return _markerSettings; }
        }

        [XmlIgnore]
        public string FilePath { get; set; }

        [XmlIgnore]
        public bool DefaultEncodingUnicode { get; private set; }

        public bool HasMarker(string marker)
        {
            return FindMarkerSetting(marker) != null;
        }

        private SolidMarkerSetting FindMarkerSetting(string marker)
        {
            return _markerSettings.Find(item => item.Marker == marker);
        }

        public SolidMarkerSetting FindOrCreateMarkerSetting(string marker)
        {
            // Search for the marker. If not found return default marker settings.
            SolidMarkerSetting result = FindMarkerSetting(marker);
            if (result == null)
            {
                // JMC: Hmm, in some cases, the calling code should notify the user of "new fields detected". Use an out parameter? (A list of strings, to add marker names to if non-null.)
                result = new SolidMarkerSetting(marker, DefaultEncodingUnicode);
                _markerSettings.Add(result);
            }
            return result;
        }

        /// <summary>
        /// will overwrite existing file, if needed
        /// </summary>
        /// <param name="templateFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <returns></returns>
        public static SolidSettings CreateSolidFileFromTemplate(string templateFilePath, string outputFilePath)
        {
            try
            {
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
                File.Copy(templateFilePath, outputFilePath);
            }
            catch (Exception e)
            {
                ErrorReport.NotifyUserOfProblem(
                    "There was a problem opening that settings file.  The error was\r\n" + e.Message);
                return null;
            }

            //JMC: The following is a side effect; move it out?
            bool defaultEncodingUnicode = String.Compare(templateFilePath, "unicode", true) == 0; //JMC: Won't this always be false?
            return OpenSolidFile(outputFilePath, defaultEncodingUnicode);
        }

        /// <summary>
        /// Open existing file. Defaults to legacy rather than unicode.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Notifies user and returns null if file can't be opened for any reason</returns>
        public static SolidSettings OpenSolidFile(string filePath)
        {
            // JMC: insert smart code here for determining default encoding (based on majority of markers?)

            return OpenSolidFile(filePath, false);  //Assumes legacy rather than unicode by default. -JMC:
        }

        /// <summary>
        /// Open existing file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="defaultEncodingUnicode"></param>
        /// <returns>Notifies user and returns null if file can't be opened for any reason</returns>
        public static SolidSettings OpenSolidFile(string filePath, bool defaultEncodingUnicode)
        {
			// Migrate before we load the model.
			var migrator = new SolidSettingsMigrator(filePath);
			if (migrator.NeedsMigration())
			{
				migrator.Migrate();
			}
            
			// Review: Why do we create a file if it doesn't exist? CP 2011-05 ; JMC: delete this?
            if(!File.Exists(filePath))
            {
                using(File.Create(filePath))
                {}
            }
			// Deserialize 
			SolidSettings settings;
			var settingsDataMapper = new XmlSerializer(typeof(SolidSettings));
			using (var reader = new StreamReader(filePath))
            {
                settings = (SolidSettings) settingsDataMapper.Deserialize(reader);
            }

			// Set properties that aren't serialized.
            settings.FilePath = filePath;
            settings.DefaultEncodingUnicode = defaultEncodingUnicode;
            // Fix settings for the record marker.
            SolidMarkerSetting markerSetting = settings.FindOrCreateMarkerSetting(settings.RecordMarker);
            //!!! Assert if null
            // Check that it has 'entry' as the parent.
            if (!markerSetting.ParentExists("entry"))
            {
                markerSetting.StructureProperties.Add(new SolidStructureProperty("entry"));
            }
            return settings;
        }

        public void Save()
        {
            SaveAs(FilePath);
        }

        /// <summary>
        /// Saves the .solid settings file. Overwrites without prompting if the file exists.
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveAs(string filePath)
        {
            var xs = new XmlSerializer(typeof(SolidSettings));
            using (var writer = new StreamWriter(filePath))
            {
                xs.Serialize(writer, this);
            }
        }

        public static string GetSettingsFilePathFromDictionaryPath(string dataFilePath)
        {
            int lastDot = dataFilePath.LastIndexOf('.');
            if (lastDot < 0)
            {
                lastDot = dataFilePath.Length;
            }
            string retval = dataFilePath.Substring(0, lastDot) + ".solid";
            return retval;
        }
        
        // Assuming some file.solid exists and was passed in, return any other file.* that exists; 
        // Return just one path, preferring certain extensions. Created by JMC 2013-09
        public static string GetDictionaryFilePathFromSettingsPath(string settingsFilePath)
        {

            var fileInfo = new FileInfo(settingsFilePath);
            var dirInfo = fileInfo.Directory;

            string f = fileInfo.Name;
            string ext = fileInfo.Extension; //Path.GetExtension(settingsFilePath);
            if (ext.Length > 0)
            {
                f = f.Substring(0, f.Length - ext.Length); //get the base filename without the extension
            }
            else
            {
                throw new ArgumentException("The settings file path ought to end in .solid");
            }

            var extensions = FileExtensions; // was: new string[]{".db", ".sfm", ".mdf", ".dic", ".txt"};
            string mask = extsAsMask(extensions, f);
            FileInfo[] matches = dirInfo.GetFiles(mask, SearchOption.TopDirectoryOnly); //find likely matches
            if (matches.Length < 1)
            {
                matches = dirInfo.GetFiles(f + ".*"); //nothing yet; find all possible matches

                if (matches.Length < 2)
                {
                    //still nothing beyond the .solid file; fail
                    var x = new StringBuilder();
                    x.AppendFormat("SOLID could not find a matching dictionary for {0}. ", settingsFilePath);
                    ErrorReport.NotifyUserOfProblem(x.ToString());
                    return ""; 
                }

            }

            foreach (var match in matches)   // pick the first (or only) match
            {
                if (match.Extension != ".solid")
                {
                    if (!extensions.Contains(match.Extension))
                    {
                        extensions.Add(match.Extension); // adaptive: makes the next File Open dialog friendlier (not saved) -JMC
                    }
                    return Path.Combine(dirInfo.ToString(), match.ToString());
                }
                
            }

            return ""; // fail
        }

    }
}