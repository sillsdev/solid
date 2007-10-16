using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SolidEngine
{
    public class SolidSettings
    {
        private  string _filePath;
        private List<SolidMarkerSetting> _markerSettings;
        private string _recordMarker = "lx";
        private string _version = "1.0";
        private string _newestVersion = "1.0";

        public SolidSettings()
        {
            _markerSettings = new List<SolidMarkerSetting>();
        }

        public string RecordMarker
        {
            get { return _recordMarker; }
            set { _recordMarker = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public List<SolidMarkerSetting> MarkerSettings
        {
            get { return _markerSettings; }
        }

        [XmlIgnore]
        public string FilePath
        {
            get{ return _filePath ; }
            set { _filePath = value; }
        }

        public SolidMarkerSetting FindMarkerSetting(string marker)
        {
            // Search for the marker. If not found return default marker settings.
            SolidMarkerSetting result = _markerSettings.Find(delegate(SolidMarkerSetting item) { return item.Marker == marker; });
            if (result == null)
            {
                result = new SolidMarkerSetting(marker);
                _markerSettings.Add(result);
            }
            return result;
        }

        /// <summary>
        /// will overwrite existing file, if needed
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        public static SolidSettings CreateSolidFileFromTemplate(string templatePath, string outputPath)
        {
            try
            {
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
                File.Copy(templatePath, outputPath);
            }
            catch (Exception e)
            {
                Reporting.ErrorReporter.ReportNonFatalMessage(
                    "There was a problem opening that settings file.  The error was\r\n" + e.Message);
                return null;
            }
            return OpenSolidFile(outputPath);
        }

        /// <summary>
        /// Open existing file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Notifies user and returns null if file can't be opened for any reason</returns>
        public static SolidSettings OpenSolidFile(string path)
        {
            SolidSettings settings;
            XmlSerializer xs = new XmlSerializer(typeof(SolidSettings));
            
            if(!File.Exists(path))
            {
                using(File.Create(path))
                {}
            }
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    settings = (SolidSettings) xs.Deserialize(reader);
                }
            }
            catch(Exception e)
            {
                Reporting.ErrorReporter.ReportNonFatalMessage(
                    "There was a problem opening that settings file.  The error was\r\n" + e.Message);
                return null              ;
            }

            settings.FilePath = path;
            // Fix settings for the record marker.
            SolidMarkerSetting markerSetting = settings.FindMarkerSetting(settings.RecordMarker);
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
            SaveAs(_filePath);
        }

        public void SaveAs(string filePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(SolidSettings));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                xs.Serialize(writer, this);
            }
        }

        private void UpdateVersion()
        {
            if(_version != _newestVersion)
            {
                //do a transform from the old version to the new one
            }
        }

        public static string GetSettingsFilePathFromDictionaryPath(string dataFilePath)
        {
            int lastDot = dataFilePath.LastIndexOf('.');
            string retval = dataFilePath.Substring(0, lastDot) + ".solid";
            return retval;
        }

    }
}