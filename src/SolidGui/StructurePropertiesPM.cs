using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public class StructurePropertiesPM
    {
        private SolidMarkerSetting _markerSetting;

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

        public void UpdateInferedParent(string parent)
        {
            _markerSetting.InferedParent = parent;
        }

        public void UpdateParentMarkers(ListView.ListViewItemCollection items)
        {

            _markerSetting.StructureProperties.Clear();

            foreach (ListViewItem item in items)
            {
                SolidStructureProperty property = (SolidStructureProperty) item.Tag;
                property.Parent = item.Text;
                
                if (property.Parent != "(New)" && property.Parent != "")
                {
                    _markerSetting.StructureProperties.Add(property);
                }
            }
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

        public void RemoveStructureProperty(string selected)
        {
            SolidStructureProperty removeThis = null;

            foreach (SolidStructureProperty property in StructureProperties)
            {
                if(property.Parent == selected)
                {
                    removeThis = property;
                }
            }

            StructureProperties.Remove(removeThis);
        }
    }
}