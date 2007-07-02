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
    }
}
