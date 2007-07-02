using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SolidEngine
{
    public class SolidSettings
    {
        private  string _filePath;
        private List<SolidMarkerSetting> _markerSettings;
        private string _recordMarker = "\\_lx";

        public SolidSettings()
        {
            _markerSettings = new List<SolidMarkerSetting>();
        }

        public string RecordMarker
        {
            get { return _recordMarker; }
            set { _recordMarker = value; }
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

        public SolidMarkerSetting Find(string marker)
        {
            // Search for the marker. If not found return default marker settings.
            SolidMarkerSetting result = _markerSettings.Find(delegate(SolidMarkerSetting item) { return item.Marker == marker; });
            if (result == null)
            {
                result = new SolidMarkerSetting(marker);
            }
            return result;
        }

        public static SolidSettings OpenSolidFile(string path)
        {
            SolidSettings settings;
            XmlSerializer xs = new XmlSerializer(typeof(SolidSettings));
            using (StreamReader reader = new StreamReader(path))
            {
                settings = (SolidSettings)xs.Deserialize(reader);
                settings.FilePath = path;
            }

            return settings;
        }

        public void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(SolidSettings));
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                xs.Serialize(writer, this);
            }
        }
    }
}