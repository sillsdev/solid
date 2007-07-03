using System;
using System.Collections.Generic;
using System.Text;

namespace SolidEngine
{
    public class SolidMarkerOps
    {
        SolidMarkerSetting _markerSetting;

        public SolidMarkerOps(SolidMarkerSetting markerSetting)
        {
            _markerSetting = markerSetting;
        }

        public bool IsParent(string marker)
        {
            SolidStructureProperty retval = _markerSetting.StructureProperties.Find(
                delegate(SolidStructureProperty item) { return item.Parent == marker; }
            );
            return retval != null;
        }
    }
}
