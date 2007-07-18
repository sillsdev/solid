using System;
using SolidEngine;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class StructurePropertiesView : UserControl
    {
        private StructurePropertiesPM _model;

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
            UpdateParentMarkerListAndComboBox();
            UpdateRadioButtonsAndExplanation();
        }

        private void UpdateParentMarkerListAndComboBox()
        {
            string selected = _model.GetSelectedText(_parentListView);
            _parentListView.Clear();
            _InferComboBox.Items.Clear();
            _InferComboBox.Text = "";

            ListViewItem newItem = new ListViewItem("(New)");
            newItem.Tag = new SolidStructureProperty();
            _parentListView.Items.Add(newItem);

            _InferComboBox.Items.Add("Report Error");

            foreach (SolidStructureProperty property in _model.StructureProperties)
            {
                ListViewItem item = new ListViewItem(property.Parent);
                item.Tag = property;
                _parentListView.Items.Add(item);
                _InferComboBox.Items.Add("Infer " + property.Parent);

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
                _InferComboBox.SelectedIndex = _InferComboBox.Items.IndexOf("Infer " + _model.InferedParent);
            }
        }
        
        private void UpdateRadioButtonsAndExplanation()
        {
            if (_parentListView.SelectedItems.Count > 0)
            {
                SolidStructureProperty property = (SolidStructureProperty)_parentListView.SelectedItems[0].Tag;
                switch (property.MultipleAdjacent)
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
                _onceRadioButton.Enabled = true;
                _multipleTogetherRadioButton.Enabled = true;
                _multipleApartRadioButton.Enabled = true;
            }
            else
            {
                _onceRadioButton.Enabled = false;
                _multipleTogetherRadioButton.Enabled = false;
                _multipleApartRadioButton.Enabled = false;
            }

            string selected = _model.GetSelectedText(_parentListView);
            _explanationLabel.Text = string.Format("{0} can appear under {1} ", _model.MarkerSetting.Marker, selected);
        }

        private void _parentListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRadioButtonsAndExplanation();
        }

        private void _aRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_parentListView.SelectedItems.Count > 0)
            {
                SolidStructureProperty selected = (SolidStructureProperty) _parentListView.SelectedItems[0].Tag;

                _model.UpdateMultiplicity(selected,
                                          _onceRadioButton.Checked,
                                          _multipleApartRadioButton.Checked,
                                          _multipleTogetherRadioButton.Checked);
            }
        }

        private void _InferComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.UpdateInferedParent(_InferComboBox.Text);
        }

        private void _parentListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (_model.ValidParent(e.Label))
            {
                _parentListView.Items[e.Item].Text = e.Label;
                _model.UpdateParentMarkers(_parentListView.Items);
                UpdateDisplay();
            }
            e.CancelEdit = true;
        }

        private void _parentListView_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                string selectedMarker = _model.GetSelectedText(_parentListView);
                _model.RemoveStructureProperty(selectedMarker);
                if (_model.InferedParent == selectedMarker)
                {
                    _InferComboBox.SelectedIndex = 0;
                }
                UpdateDisplay();
            }
        }

        private void StructurePropertiesView_Load(object sender, EventArgs e)
        {

        }

        private void _parentListView_MouseUp(object sender, MouseEventArgs e)
        {
            if(_parentListView.SelectedItems.Count >0)
            {
                if (_model.GetSelectedText(_parentListView) == "(New)")
                {
                    _parentListView.SelectedItems[0].BeginEdit();
                }
            }
        }
    }
}
