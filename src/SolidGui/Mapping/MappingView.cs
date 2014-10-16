// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
        private bool _isProcessing; //flag for avoiding UI ping-ponging
        
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
        public void BindModel(MappingPM value, SolidMarkerSetting markerSetting)
            {
                _model = value;
                _model.MarkerSetting = markerSetting;

                if (_targetCombo.SelectedIndex != (int)_model.Type)
                {
                    _targetCombo.SelectedIndex = (int)_model.Type;
                }

                _model.Init(markerSetting);
            }
        
        private void OnLoad(object sender, EventArgs e)
        {
            if (DesignMode) return;

            UsageReporter.SendNavigationNotice("MappingDialog");

            _targetCombo.SelectedIndex = (int)_model.Type;
            ApplyTarget();  //not sure why this is necessary, but without it the dialog's initial display of the right side isn't hooked up well. -JMC

            UpdateDisplay();
        }
        
        private void UpdateDisplayConcept()
        {
            // _model.SelectedConcept = (MappingPM.Concept)_conceptList.Items[0].Tag;
            // _conceptList.Items[0].Selected = true;

            string storedConceptId = _model.MarkerSetting.GetMappingConceptId(CurrentMappingType());
            foreach (ListViewItem item in _conceptList.Items)
            {
                MappingPM.Concept concept = (MappingPM.Concept) item.Tag;
                string conceptId = concept.GetId();
                if (conceptId == storedConceptId)
                {
                    item.Selected = true;
                    _conceptList.TopItem = item; // scroll down
                    //_model.SelectedConcept = concept;  //why is this here? Not a good way to set a default value in the model. -JMC
                }
            }
        }

        // Clear and refill (from memory) the list of concepts (fields) for the current target system.
        private void UpdateDisplayConceptList()
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
            if (DesignMode) return;
            UpdateModel();
            UpdateDisplayInformationPane();  //partial UpdateDisplay
        }

        private void UpdateModel()
        {
            if (_isProcessing || _conceptList.SelectedItems.Count == 0 || DesignMode) return;

            var tmp = (MappingPM.Concept)_conceptList.SelectedItems[0].Tag;
            _model.MarkerSetting.SetMappingConcept(CurrentMappingType(), tmp.GetId());
            _model.SelectedConcept = tmp;  // (if a change, sets Needsave to true)
        }

        private SolidMarkerSetting.MappingType CurrentMappingType()
        {
            if (_targetCombo.SelectedIndex == 0) return SolidMarkerSetting.MappingType.FlexDefunct;
            if (_targetCombo.SelectedIndex == 1) return SolidMarkerSetting.MappingType.Lift;

            return new SolidMarkerSetting.MappingType();
        }

        private void UpdateDisplayInformationPane()
        {
            string html = "";
            if (_model.SelectedConcept != null)
            {
                html = _model.TransformInformationToHtml(_model.SelectedConcept.InformationAsXml);
            }
            _htmlViewer.DocumentText = html;
        }

        public void UpdateDisplay()
        {
            if (_isProcessing || DesignMode) return;  //block ping-ponging
            _isProcessing = true;
            UpdateDisplayConceptList();
            UpdateDisplayConcept(); 
            UpdateDisplayInformationPane();
            this.Hide(); this.Show();  //necessary to preserve highlight after user clicks to change mapping -JMC
            _isProcessing = false;
        }

        private void ApplyTarget()
        {
            _model.TargetSystem = _model.TargetChoices[_targetCombo.SelectedIndex];
        }

        private void _targetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DesignMode) return;
            ApplyTarget();
            UpdateDisplay();
        }

        private void _conceptList_SizeChanged(object sender, EventArgs e)
        {
            columnHeader1.Width = _conceptList.Width - 40;
        }

    }
}