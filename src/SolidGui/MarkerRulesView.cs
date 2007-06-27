using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class MarkerRulesView : UserControl
    {
        private MarkerRulesPM _markerRulesModel;

        public MarkerRulesView()
        {
            InitializeComponent();
        }

        public MarkerRulesPM MarkerRulesModel
        {
            get
            {
                return _markerRulesModel;
            }
            set
            {
                _markerRulesModel = value;
            }
        }
        
        public void UpdateDisplay()
        {
            foreach( String marker in _markerRulesModel.AllMarkers)
            {
                _markerComboBox.Items.Add(marker);
            }
            Rule currentRule = _markerRulesModel.GetRule(_markerComboBox.Text);

            _noRadioButton.Checked = !currentRule.Required;
            _yesRadioButton.Checked = currentRule.Required;
            _ruleNameTextBox.Text = currentRule.Name;

        }

        private void _markerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            string name = _ruleNameTextBox.Text;
            string marker = _markerComboBox.Text;
            bool required = _yesRadioButton.Checked;

            _markerRulesModel.AddRule(name, marker, required);
            _markerRulesModel.WriteRulesToXml();
        }

    }
}
