// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT


// JMC: Shouldn't this be moved into the MarkerSettings folder/namespace?

using System;
using Palaso.Reporting;

using System.Windows.Forms;
using Palaso.UI.WindowsForms.Reporting;
using Solid.Engine;
using SolidGui.Engine;
using SolidGui.MarkerSettings;

namespace SolidGui
{
    public partial class StructurePropertiesView : UserControl
    {
        private StructurePropertiesPM _model;
        private bool _isProcessing = true;
        private string _cachedMarker = "";
        private const string InferLabel = "Infer ";
        private const string NewLabel = "(New)";

        public StructurePropertiesPM Model
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

        public StructurePropertiesView()
        {
            InitializeComponent();
        }

        public void UpdateDisplay()
        {
            _isProcessing = true;
            UpdateParentMarkerListAndComboBox();
            UpdateRadioButtonsAndExplanation();
           
            //Workaround so that we don't lose our row highlight. (Not needed when debugging with breakpoints!) -JMC Feb 2014
            
            _parentListView.Hide();
            _parentListView.Show();
            if (_parentListView.SelectedItems.Count > 0)
            {
                _parentListView.FocusedItem = _parentListView.SelectedItems[0];
            }
            else
            {
                //_parentListView.FocusedItem = _parentListView.Items[0];
            }
            _isProcessing = false;
        }

        public static string GetSelectedText(ListView parentListView)
        {
            string selected = "";
            if (parentListView.SelectedItems.Count > 0)
            {
                selected = parentListView.SelectedItems[0].Text;
            }
            return selected;
        }

        private void UpdateParentMarkerListAndComboBox()
        {
            string selected = GetSelectedText(_parentListView);

            _parentListView.Items.Clear(); // was _parentListView.Clear();
            _InferComboBox.Items.Clear();
            _InferComboBox.Text = "";

            ListViewItem item = new ListViewItem(NewLabel);
            item.Tag = new SolidStructureProperty();
            if (selected == NewLabel) item.Selected = true;
            item.SubItems.Add("--");
            _parentListView.Items.Add(item);

            _InferComboBox.Items.Add("Report Error");

            foreach (SolidStructureProperty property in _model.StructureProperties)
            {
                item = new ListViewItem(property.Parent);
                item.Tag = property;
                _parentListView.Items.Add(item);
                item.SubItems.Add(property.Multiplicity.Abbr());

                _InferComboBox.Items.Add(InferLabel + property.Parent);

                if (selected == "") 
                {
                    //The dialog just loaded; prepare to select the first listed parent.  // Added for convenience. -JMC Feb 2014
                    selected = item.Text;
                }

                if (item.Text == selected)
                {
                    item.Selected = true;
                }
            }
            if (Model.InferedParent == "")
            {
                _InferComboBox.SelectedIndex = 0;
            }
            else
            {
                _InferComboBox.SelectedIndex = _InferComboBox.Items.IndexOf(InferLabel + _model.InferedParent);
            }
        }
        
        private void UpdateRadioButtonsAndExplanation()
        {
            if (_parentListView.SelectedItems.Count > 0)
            {
                SolidStructureProperty property = (SolidStructureProperty)_parentListView.SelectedItems[0].Tag;
                switch (property.Multiplicity)
                {
                    case MultiplicityAdjacency.Once:
                        {
                            _onceRadioButton.Checked = true;
                            break;
                        }
                    case MultiplicityAdjacency.MultipleApart:
                        {
                            _multipleApartRadioButton.Checked = true;
                            break;
                        }
                    case MultiplicityAdjacency.MultipleTogether:
                        {
                            _multipleTogetherRadioButton.Checked = true;
                            break;
                        }
                }
                _requiredCheckBox.Checked = property.Required;
                setStructEnabled(true);
            }
            else
            {
                setStructEnabled(false);
            }

            string selected = GetSelectedText(_parentListView);
            _explanationLabel.Text = string.Format("Under {1}, {0} can occur...", _model.MarkerSetting.Marker, selected);
            _summaryTextBox.Text = "Summary: " + MarkerSettingsPM.MakeStructureLinkLabel(_model.MarkerSetting.StructureProperties, _model.MarkerSetting);
            _commentTextBox.Text = _model.MarkerSetting.Comment;
        }

        private void setStructEnabled(bool value)
        {
            flowLayoutPanelOccurs.Enabled = value;
            _InferComboBox.Enabled = value;
        }

        public void setLxEnabled(bool value)
        {
            flowLayoutPanelOccurs.Enabled = value;
            _parentListView.Enabled = value;
            _InferComboBox.Enabled = value;
        }

        private void _parentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Alas, this always fires twice, once to deselect, then once correctly. -JMC Feb 2014
            if (_isProcessing || _parentListView.SelectedItems.Count <= 0) return; //ignore the first firing
            UpdateDisplay();
        }

        private void _radioButton_Click(object sender, EventArgs e)
        {
            if (!_isProcessing && _parentListView.SelectedItems.Count > 0)
            {
                SolidStructureProperty selected = (SolidStructureProperty)_parentListView.SelectedItems[0].Tag;
                if (!_isProcessing)
                {
                    _model.UpdateMultiplicity(selected,
                                                _onceRadioButton.Checked,
                                                _multipleApartRadioButton.Checked,
                                                _multipleTogetherRadioButton.Checked,
                                                _requiredCheckBox.Checked);
                }
                UpdateDisplay();
            }
        }

        private void _InferComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isProcessing)
            {
                _model.UpdateInferedParent(_InferComboBox.Text);
            }
        }

        public void CommentTextBoxNeedsCheck(object sender, EventArgs e)
        {
            if (!_isProcessing)
            {
                _model.UpdateComment(_commentTextBox.Text);
            }
        }

        private void _parentListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            _cachedMarker = _parentListView.Items[e.Item].Text; // can't use e.Label because it's null at this point -JMC
        }

        private void _parentListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // Note that clicking twice and hitting Enter will give null; retyping it precisely will give _cachedLabel. -JMC
            if (e.Label == null || e.Label == _cachedMarker)
            {
                e.CancelEdit = true;
                return;
            }

            if (e.Label == "" || e.Label == NewLabel)
            {
                if (e.Item > 0)
                {
                    var d = new ProblemNotificationDialog("The parent marker cannot be empty. Try a valid marker like \\sn or \\lx. To delete, click just once and press Delete.", "Solid Error");
                    d.ShowDialog();
                }
                e.CancelEdit = true;
                return;
            }

            if (!_model.ValidParent(e.Label))
            {
                var d = new ProblemNotificationDialog(
                    String.Format(
                        "'{0}' isn't a valid parent marker. It must be a valid marker, such as \\sn or \\lx .",
                        e.Label
                    ),
                    "Solid Error"
                );
                d.ShowDialog();
                e.CancelEdit = true;
                return;
            }
            _parentListView.Items[e.Item].Text = StructurePropertiesPM.RemoveLeadingBackslash(e.Label);
            _model.UpdateParentMarkers(_parentListView.Items);
            UpdateDisplay();
            e.CancelEdit = true;
        }

        private void _parentListView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string selectedMarker = GetSelectedText(_parentListView);
                _model.RemoveStructureProperty(selectedMarker);
                if (_model.InferedParent == selectedMarker)
                {
                    _InferComboBox.SelectedIndex = 0;
                }
            }
        }

        private void _parentListView_MouseUp(object sender, MouseEventArgs e)
        {
            if(_parentListView.SelectedItems.Count > 0)
            {
                if (GetSelectedText(_parentListView) == NewLabel)
                {
                    _parentListView.SelectedItems[0].BeginEdit();
                }
            }
        }

    }
}
