// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Windows.Forms;
using Palaso.Reporting;
using SolidGui.Engine;

namespace SolidGui.Mapping
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
        }
        public void BindModel(MappingPM value)
            {
                _model = value;
                if (_targetCombo.SelectedIndex != (int)_model.Type)
                {
                    _targetCombo.SelectedIndex = (int)_model.Type;
                }
            }
        
        private void OnLoad(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            UsageReporter.SendNavigationNotice("MappingDialog");

            _targetCombo.SelectedIndex = (int)_model.Type;
            ApplyTarget();  //not sure why this is necessary, but without it the dialog's initial display of the right side isn't hooked up well. -JMC

            UpdateDisplay();
        }
        
        private void HighlightPreviouslySelectedConcept()
        {
            Model.IsProcessing = true;
            // _model.SelectedConcept = (MappingPM.Concept)_conceptList.Items[0].Tag;
            // _conceptList.Items[0].Selected = true;

            string storedConceptId = _model.MarkerSetting.GetMappingConceptId(CurrentMappingType());
            foreach (ListViewItem item in _conceptList.Items)
            {
                MappingPM.Concept concept = (MappingPM.Concept) item.Tag;
                string conceptId = concept.GetId();
                if (conceptId == storedConceptId)
                {
                    _conceptList.SelectedIndexChanged -= _conceptList_SelectedIndexChanged;
                    item.Selected = true;
                    _conceptList.TopItem = item;
                    _model.SelectedConcept = concept;
                    _conceptList.SelectedIndexChanged += _conceptList_SelectedIndexChanged;
                }
            }
            Model.IsProcessing = false;
        }

        // Clear and refill (from memory) the list of concepts (fields) for the current target system.
        private void FillConceptList()
        {
            _conceptList.Items.Clear();
            ListViewItem none = new ListViewItem("(None)");
            none.Tag = new MappingPM.Concept(null);
            _conceptList.Items.Add(none);

            foreach (MappingPM.Concept concept in _model.TargetSystem.Concepts)
            {
                ListViewItem item = new ListViewItem(concept.Label());
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
            if (!Model.IsProcessing)
            {
                _model.SelectedConcept = (MappingPM.Concept)_conceptList.SelectedItems[0].Tag;
            }
            _model.MarkerSetting.SetMappingConcept(CurrentMappingType(), _model.SelectedConcept.GetId());
            LoadInformationPane();
        }

        private SolidMarkerSetting.MappingType CurrentMappingType()
        {
            if (_targetCombo.SelectedIndex == 0) return SolidMarkerSetting.MappingType.FlexDefunct;
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

        public void UpdateDisplay()
        {
            FillConceptList();
            HighlightPreviouslySelectedConcept(); 
            LoadInformationPane();
            this.Hide();
            this.Show();
        }

        private void ApplyTarget()
        {
            _model.TargetSystem = _model.TargetChoices[_targetCombo.SelectedIndex];
        }

        private void _targetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyTarget();
            UpdateDisplay();
        }

        private void _conceptList_SizeChanged(object sender, EventArgs e)
        {
            columnHeader1.Width = _conceptList.Width - 40;
        }

    }
}