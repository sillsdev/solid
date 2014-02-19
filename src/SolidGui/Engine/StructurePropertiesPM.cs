// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SolidGui.Engine;
using SolidGui.MarkerSettings;

namespace Solid.Engine
{
    public class StructurePropertiesPM
    {
        private SolidMarkerSetting _markerSetting;
        private List<string> _allValidMarkers;
        private MarkerSettingsPM _markerSettingsPm;

        public StructurePropertiesPM(MarkerSettingsPM markerSettings)
        {
            _markerSettingsPm = markerSettings;
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

        public IEnumerable<string> AllValidMarkers
        {
            set
            {
                _allValidMarkers = (List<string>)value;
            }
        }

        private void MayNeedSave()
        {
            if (_markerSettingsPm != null) _markerSettingsPm.WillNeedSave();
        }

        public void UpdateInferedParent(String comboBoxText)
        {
            MayNeedSave();
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
        }

        public void UpdateParentMarkers(ListView.ListViewItemCollection items)
        {
            MayNeedSave();
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
        }

        public bool ValidParent(string marker)
        {
            var markerWithoutLeadingBackslash = RemoveLeadingBackslash(marker);
            return (
                       _allValidMarkers.Contains(markerWithoutLeadingBackslash) &&
                       !_markerSetting.ParentExists(markerWithoutLeadingBackslash) &&
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


        public void UpdateMultiplicity(SolidStructureProperty selected, 
                                       bool onceChecked, 
                                       bool multipleApartChecked, 
                                       bool multipleTogetherChecked)
        {
            MayNeedSave();
            if (onceChecked)
            {
                selected.Multiplicity = MultiplicityAdjacency.Once;
            }
            else if (multipleApartChecked)
            {
                selected.Multiplicity = MultiplicityAdjacency.MultipleApart;
            }
            else
            {
                selected.Multiplicity = MultiplicityAdjacency.MultipleTogether;
            }
        }

        public string GetSelectedText(ListView parentListView)
        {
            string selected;

            if (parentListView.SelectedItems.Count > 0)
            {
                selected = parentListView.SelectedItems[0].Text;
            }
            else
            {
                selected = "";
            }

            return selected;
        }

        public void RemoveStructureProperty(string marker)
        {
            MayNeedSave();
            foreach (SolidStructureProperty property in StructureProperties)
            {
                if(property.Parent == marker)
                {
                    StructureProperties.Remove(property);
                    break;
                }
            }

        }
    }
}