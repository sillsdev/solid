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

        public void UpdateRuleNameComboBox(string newRuleName)
        {
            _ruleNameComboBox.Items.Clear();
            _ruleNameComboBox.Items.Add("(New Rule)");

            foreach (string ruleName in _markerRulesModel.GetAllRuleNames())
            {
                _ruleNameComboBox.Items.Add(ruleName);
            }

            _ruleNameComboBox.Text = newRuleName;
        }
        
        public void UpdateEditRuleDisplay()
        {

            _markerComboBox.Items.Clear();
            foreach( String marker in _markerRulesModel.AllMarkers)
            {
                _markerComboBox.Items.Add(marker);
            }

            Rule currentRule = _markerRulesModel.GetRule(_ruleNameComboBox.Text);

            _noRadioButton.Checked = !currentRule.Required;
            _yesRadioButton.Checked = currentRule.Required;
            _ruleNameTextBox.Text = currentRule.Name;
            _markerComboBox.SelectedIndex = _markerComboBox.Items.IndexOf(currentRule.Marker);

        }

        private void _markerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateDisplay();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            string name = _ruleNameTextBox.Text;
            string marker = _markerComboBox.Text;
            bool required = _yesRadioButton.Checked;

            _markerRulesModel.AddRule(name, marker, required);
            _markerRulesModel.WriteRulesToXml();

            UpdateRuleNameComboBox(_ruleNameTextBox.Text);
        }

        private void _ruleNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEditRuleDisplay();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _markerRulesModel.RemoveRule(_ruleNameComboBox.Text);
            _markerRulesModel.WriteRulesToXml();
            UpdateRuleNameComboBox("(New Rule)");
        }

    }
}
