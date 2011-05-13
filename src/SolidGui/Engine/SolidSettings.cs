using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using SolidGui.Migration;

namespace SolidGui.Engine
{
    public class SolidSettings
    {
        private readonly List<SolidMarkerSetting> _markerSettings;
        private string _recordMarker = "lx";
    	public const int LatestVersion = 2;

        public SolidSettings()
        {
        	Version = LatestVersion.ToString();
			DefaultEncodingUnicode = false;
			_markerSettings = new List<SolidMarkerSetting>();
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
                Palaso.Reporting.ErrorReport.NotifyUserOfProblem(
                    "There was a problem opening that settings file.  The error was\r\n" + e.Message);
                return null;
            }

            bool defaultEncodingUnicode = String.Compare(templateFilePath, "unicode", true) == 0;
            return OpenSolidFile(outputFilePath, defaultEncodingUnicode);
        }

        /// <summary>
        /// Open existing file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Notifies user and returns null if file can't be opened for any reason</returns>
        public static SolidSettings OpenSolidFile(string filePath)
        {
            return OpenSolidFile(filePath, false);
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
            
			// Review: Why do we create a file if it doesn't exist? CP 2011-05
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

    }
}