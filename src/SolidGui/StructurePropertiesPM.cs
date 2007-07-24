using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
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

        public bool ValidParent(string parent)
        {
            return (
                _allValidMarkers.Contains(parent) &&
                !_markerSetting.ParentExists(parent) &&
                _markerSetting.Marker != parent
            );
        }

        public string RemoveLeadingBackslash(string parent)
        {
            if(parent[0].Equals('\\'))
            {
                parent = parent.Substring(1);
            }
            return parent;
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
