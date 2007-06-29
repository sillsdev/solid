using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class StructurePropertiesPM
    {
        private SolidConsole.SolidMarkerSetting _markerSetting;

        public SolidConsole.SolidMarkerSetting MarkerSetting
        {
            get
            {
                return _markerSetting;
            }
            set
            {
                _markerSetting = value;
            }
        }

        public List<SolidConsole.SolidStructureProperty> StructureProperties
        {
            get
            {
                return _markerSetting.StructureProperties;
            }
        }

        public string InferedParent
        {
            get
            {
                return _markerSetting.InferedParent;
            }
        }
    }
}
