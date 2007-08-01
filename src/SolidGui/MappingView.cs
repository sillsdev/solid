using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SolidEngine;

namespace SolidGui
{
    /// <summary>
    /// The mapping control allows the user to connect markers to concepts in other
    /// formats/applications.
    /// This class is the View (ui-specific) half of this control
    /// </summary>
    public partial class MappingView : UserControl
    {
        private MappingPM _model;
        
        public MappingView()
        {
            InitializeComponent();
        }


        public MappingPM Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
             }
        }
        private void OnLoad(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            _targetCombo.SelectedIndex = (int)_model.Type;
            //LoadConceptList();
            //LoadInformationPane();
        }
        
        public void InitializeDisplay()
        {
            if (_targetCombo.SelectedIndex != (int)_model.Type)
            {
                _targetCombo.SelectedIndex = (int)_model.Type;
            }
            else
            {
                _targetCombo_SelectedIndexChanged(this, new EventArgs());
            }
        }

        private void HighlightPreviouslySelectedConcept()
        {
           // _model.SelectedConcept = (MappingPM.Concept)_conceptList.Items[0].Tag;
           // _conceptList.Items[0].Selected = true;

            foreach(ListViewItem item in _conceptList.Items)
            {
                MappingPM.Concept concept = (MappingPM.Concept) item.Tag;
                string conceptId = concept.GetId();
                string storedConceptId = _model.MarkerSetting.GetMappingConceptId(CurrentMappingType());
                if (conceptId == storedConceptId)
                {
                    _conceptList.SelectedIndexChanged -= _conceptList_SelectedIndexChanged;
                    item.Selected = true;
                    _conceptList.TopItem = item;
                    _model.SelectedConcept = concept;
                    _conceptList.SelectedIndexChanged += _conceptList_SelectedIndexChanged;
                }
            }
        }

        private void LoadConceptList()
        {
            _conceptList.Items.Clear();
            ListViewItem none = new ListViewItem("(None)");
            none.Tag = new MappingPM.Concept(null);
            _conceptList.Items.Add(none);

            foreach (MappingPM.Concept concept in _model.TargetSystem.Concepts)
            {
                ListViewItem item = new ListViewItem(concept.ToString());
                item.Tag = concept;
                _conceptList.Items.Add(item);
            }
        }

        private void _conceptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_conceptList.SelectedItems.Count == 0)
            {
                return;
            }
            _model.SelectedConcept = (MappingPM.Concept) _conceptList.SelectedItems[0].Tag;
            _model.MarkerSetting.SetMappingConcept(CurrentMappingType(),_model.SelectedConcept.GetId());
            LoadInformationPane();
        }

        private SolidMarkerSetting.MappingType CurrentMappingType()
        {
            if (_targetCombo.SelectedIndex == 0) return SolidMarkerSetting.MappingType.Flex;
            if (_targetCombo.SelectedIndex == 1) return SolidMarkerSetting.MappingType.Lift;

            return new SolidMarkerSetting.MappingType();
        }

        private void LoadInformationPane()
        {
            if (_model.SelectedConcept == null)
            {
                return;
            }
            string html = _model.TransformInformationToHtml(_model.SelectedConcept.InformationAsXml);
             _htmlViewer.DocumentText = html;
        }

        private void _conceptList_SizeChanged(object sender, EventArgs e)
        {
            columnHeader1.Width = _conceptList.Width-40;
        }

        private void _targetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.TargetSystem = _model.TargetChoices[_targetCombo.SelectedIndex];
            
            LoadConceptList();
            HighlightPreviouslySelectedConcept(); 
            LoadInformationPane();
        }
    }
}
