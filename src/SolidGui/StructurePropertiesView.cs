using System;
using SolidConsole;
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

            if (selected == "(New)")
            {
                newItem.Selected = true;
            }

            foreach (SolidStructureProperty property in _model.StructureProperties)
            {
                ListViewItem item = new ListViewItem(property.Parent);
                item.Tag = property;
                _parentListView.Items.Add(item);
                _InferComboBox.Items.Add(property.Parent);

                if (item.Text == selected)
                {
                    item.Selected = true;
                }
            }

            _InferComboBox.SelectedIndex = _InferComboBox.Items.IndexOf(_model.InferedParent);
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

                string selected = _model.GetSelectedText(_parentListView);
                _explanationLabel.Text = string.Format("{0} can appear under {1} ", _model.MarkerSetting.Marker, selected);
            }
            else
            {
                _onceRadioButton.Enabled = false;
                _multipleTogetherRadioButton.Enabled = false;
                _multipleApartRadioButton.Enabled = false;
            }
        }

        private void _parentListView_SelectedIndexChanged(object sender, EventArgs e)
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
            if (e.Label != "")
            {
                _parentListView.Items[e.Item].Text = e.Label;
                _model.UpdateParentMarkers(_parentListView.Items);
                UpdateDisplay();
            }
            e.CancelEdit = true;
        }

        private void _parentListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.Equals(Keys.Delete))
            {
                _model.RemoveStructureProperty(_model.GetSelectedText(_parentListView));
                UpdateDisplay();
            }
        }

        private void _parentListView_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                _model.RemoveStructureProperty(_model.GetSelectedText(_parentListView));
                UpdateDisplay();
            }
        }
    }
}
