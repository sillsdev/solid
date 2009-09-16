using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Solid.Engine
{
    public class StructurePropertiesPM
    {
        private SolidMarkerSetting _markerSetting;
        private List<string> _allValidMarkers;

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
                    _markerSetting.InferedParent = comboBoxText.Substring(6);
                }
            }
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
            if (onceChecked)
            {
                selected.MultipleAdjacent = MultiplicityAdjacency.Once;
            }
            else if (multipleApartChecked)
            {
                selected.MultipleAdjacent = MultiplicityAdjacency.MultipleApart;
            }
            else
            {
                selected.MultipleAdjacent = MultiplicityAdjacency.MultipleTogether;
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