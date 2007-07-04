using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class TemplateChooser : Form
    {
        private List<string> _templatePaths;
        private string _pathToChosenTemplate="";
        private bool _wouldBeReplacingExistingSettings=false;

        public TemplateChooser()
        {
            InitializeComponent();
            _instructionsLabelForReplacement.Location = _instructionsLabel.Location;
        }


        public string CustomizedSolidDestinationName
        {
            set
            {
                _instructionsLabel.Text = string.Format(_instructionsLabel.Text, value);
            }
        }

        private void UpdateDisplay()
        {
            _okButton.Enabled = _templateChooser.SelectedItems.Count == 1;
        }

        public List<string> TemplatePaths
        {
            get
            {
                return _templatePaths;
            }
            set
            {
                _templatePaths = value;
            }
        }

        public string PathToChosenTemplate
        {
            get
            {
                return _pathToChosenTemplate;
            }
        }

        public bool WouldBeReplacingExistingSettings
        {
            get
            {
                return _wouldBeReplacingExistingSettings;
            }
            set
            {
                _wouldBeReplacingExistingSettings = value;
            }
        }

        private void TemplateChooser_Load(object sender, EventArgs e)
        {
            _templateChooser.SuspendLayout();
            _templateChooser.Items.Clear();
            foreach (string path in _templatePaths)
            {
                ListViewItem item = new ListViewItem(Path.GetFileNameWithoutExtension(path));
                item.Tag = path;
                item.ToolTipText = path;
                _templateChooser.Items.Add(item);

                _labelSaveFirst1.Visible = _labelSaveFirst2.Visible = _warningImage.Visible = _saveCurrentFirst.Visible = _wouldBeReplacingExistingSettings;
                _instructionsLabelForReplacement.Visible = _wouldBeReplacingExistingSettings;
                _instructionsLabel.Visible = !_wouldBeReplacingExistingSettings;

                if (_wouldBeReplacingExistingSettings)
                {
                    _templateChooser.Top = 64;
                }
                else
                {
                    if (path.ToLower().Contains("mdf.solid"))
                    {
                        item.Selected = true;
                    }
                }
            }
            _templateChooser.ResumeLayout();

            UpdateDisplay();
        }

        private void OnOKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void _templateChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_templateChooser.SelectedItems.Count == 1)
            {
                _pathToChosenTemplate =(string) _templateChooser.SelectedItems[0].Tag;
            }
            UpdateDisplay();
        }
    }
}