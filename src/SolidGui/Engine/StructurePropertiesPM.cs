// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SolidGui.Engine;
using SolidGui.MarkerSettings;

namespace Solid.Engine
{
    public class StructurePropertiesPM
    {
        private SolidMarkerSetting _markerSetting;
        //private List<string> _allValidMarkers;
        public MarkerSettingsPM MarkerSettingsPm;

        public StructurePropertiesPM(MarkerSettingsPM markerSettings)
        {
            MarkerSettingsPm = markerSettings;
        }

        public SolidMarkerSetting MarkerSetting
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

        public List<SolidStructureProperty> StructureProperties
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

        /*
        public IList<string> AllValidMarkers
        {
            private get { return _allValidMarkers; }
            set
            {
                _allValidMarkers = (List<string>)value;
            }
        }*/

        public event EventHandler StructureSettingChanged;

        private void MayNeedSave()
        {
            if (MarkerSettingsPm != null)
            {
                MarkerSettingsPm.WillNeedSave();
                if (StructureSettingChanged != null) StructureSettingChanged.Invoke(this, EventArgs.Empty);

            }
        }

        public void UpdateInferedParent(String comboBoxText)
        {
            if (_markerSetting != null)
            {
                if(comboBoxText == "Report Error")
                {
                    _markerSetting.InferedParent = "";
                }
                else
                {
                    int space = comboBoxText.LastIndexOf(' '); 
                    _markerSetting.InferedParent = comboBoxText.Substring(space+1);  // E.g. extract just the "sn" in "infer sn" or "infer an sn"
                }
            }
            MayNeedSave();
        }

        public string UpdateComment(string comment)
        {
            string safe = Sanitize(comment);
            if(_markerSetting.Comment != comment)
            {
                _markerSetting.Comment = safe;
                MayNeedSave();
            }
            return safe;
        }

        public static string Sanitize(string s)
        {
            string tmp = Regex.Replace(s, @"[^a-zA-Z0-9 ;:,.!?()\-=+_']", "");
            return tmp;
        }

        public void UpdateParentMarkers(ListView.ListViewItemCollection items)
        {
            _markerSetting.StructureProperties.Clear();

            foreach (ListViewItem item in items)
            {
                SolidStructureProperty property = (SolidStructureProperty) item.Tag;
                property.Parent = RemoveLeadingBackslash(item.Text);
                
                if (ValidParent(property.Parent))
                {
                    _markerSetting.StructureProperties.Add(property);
                }
            }
            MayNeedSave();
        }

        public bool ValidParent(string marker)
        {
            string markerWithoutLeadingBackslash = RemoveLeadingBackslash(marker);
            IList<string> markers = MarkerSettingsPm.GetAllMarkers();
            return (
                       markers.Contains(markerWithoutLeadingBackslash) &&
                       !_markerSetting.IsAnAllowedParent(markerWithoutLeadingBackslash) &&
                       _markerSetting.Marker != markerWithoutLeadingBackslash
                   );
        }

        public static string RemoveLeadingBackslash(string marker)
        {
            if (!String.IsNullOrEmpty(marker) && marker[0].Equals('\\'))
            {
                marker = marker.Substring(1);
            }
            return marker;
        }


        public void UpdateOptions(SolidStructureProperty selected, 
                                       bool onceChecked, 
                                       bool multipleApartChecked,
                                       bool multipleTogetherChecked, 
                                       bool requiredChecked)
        {
            MultiplicityAdjacency tmp;
            if (onceChecked)
            {
                tmp = MultiplicityAdjacency.Once;
            }
            else if (multipleApartChecked)
            {
                tmp = MultiplicityAdjacency.MultipleApart;
            }
            else
            {
                tmp = MultiplicityAdjacency.MultipleTogether;
            }
            // Or, the lines above could go in the UI. -JMC

            if (selected.Multiplicity != tmp)
            {
                selected.Multiplicity = tmp;
                MayNeedSave();
            }
            
            if (selected.Required != requiredChecked)
            {
                selected.Required = requiredChecked;
                MayNeedSave();
            }
        }

        public void RemoveStructureProperty(string marker)
        {
            foreach (SolidStructureProperty property in StructureProperties)
            {
                if(property.Parent == marker)
                {
                    StructureProperties.Remove(property);
                    break;
                }
            }
            MayNeedSave();
        }

    }
}