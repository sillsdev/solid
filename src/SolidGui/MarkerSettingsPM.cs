using System;
using System.Collections.Generic;
using System.Text;
using SolidEngine;

namespace SolidGui
{
    public class MarkerSettingsPM
    {
        private List<string> _allMarkers;
        private List<SolidMarkerSetting> _markerSettings;
        private string _root;

        public MarkerSettingsPM()
        {
            _allMarkers = new List<string>();
            _markerSettings = new List<SolidMarkerSetting>();
        }

        public SolidMarkerSetting GetMarkerSetting(string marker)
        {
            foreach (SolidMarkerSetting markerSetting in _markerSettings)
            {
                if(markerSetting.Marker == marker)
                {
                    return markerSetting;
                }
            }
            SolidMarkerSetting ms = new SolidMarkerSetting(marker);
            _markerSettings.Add(ms);
            return ms;
        }

        public List<string> AllMarkers
        {
            get
            {
                return _allMarkers;
            }
            set
            {
                _allMarkers = value;
            }
        }
        public List<SolidMarkerSetting>MarkerSettings
        {
            get
            {
                return _markerSettings;
            }
            set
            {
                _markerSettings = value;
            }
        }
        public string Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
            }
        }
    }
}
