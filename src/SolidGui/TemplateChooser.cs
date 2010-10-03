using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SolidGui.Engine;

using System.Drawing;

namespace SolidGui
{
    public partial class TemplateChooser : Form
    {
    	private readonly SolidSettings _solidSettings;

		public TemplateChooser (SolidSettings solidSettings)
        {
            InitializeComponent();
            _solidSettings = solidSettings;
        	PathToChosenTemplate = "";
        	WouldBeReplacingExistingSettings = false;
        }

		public List<string> TemplatePaths { get; set; }

		public string PathToChosenTemplate { get; private set; }

		public bool WouldBeReplacingExistingSettings { get; set; }

        public string CustomizedSolidDestinationName
        {
            set
            {
                _lblInstructions.Text = string.Format(_lblInstructions.Text, value);
            }
        }

        private void UpdateDisplay()
        {
            _okButton.Enabled = _templateChooser.SelectedItems.Count == 1;
        }

    	private void TemplateChooser_Load(object sender, EventArgs e)
        {
            _templateChooser.SuspendLayout();
            _templateChooser.Items.Clear();
            foreach (string path in TemplatePaths)
            {
                var item = new ListViewItem(Path.GetFileNameWithoutExtension(path));
                item.Tag = path;
                item.ToolTipText = path;
                _templateChooser.Items.Add(item);

            	int listViewBottom = _okButton.Location.Y - 8;
                if (WouldBeReplacingExistingSettings)
                {
                	_lblInstructions.Visible = false;
                	_pnlWarning.Visible = true;
                	_pnlListView.Location = new Point
                	{
						X = _pnlListView.Location.X,
                		Y = (_pnlWarning.Location.Y + _pnlWarning.Height)
                	};
                	_pnlListView.Height = listViewBottom - _pnlListView.Location.Y;
                }
                else
                {
					_lblInstructions.Visible = true;
					_pnlWarning.Visible = false;
					_pnlListView.Location = new Point
                	{
						X = _pnlListView.Location.X,
						Y = (_lblInstructions.Location.Y + _lblInstructions.Height)
                	};
					_pnlListView.Height = listViewBottom - _pnlListView.Location.Y;
					if (path.ToLower ().Contains ("mdf.solid"))
                    {
                        item.Selected = true;
                    }
                }
				this.SuspendLayout();
				this.ResumeLayout();
            }
            _templateChooser.ResumeLayout();

            UpdateDisplay();
        }

        private void OnOKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_templateChooser.SelectedItems.Count == 1)
            {
                PathToChosenTemplate =(string) _templateChooser.SelectedItems[0].Tag;
            }
            UpdateDisplay();
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnSaveCurrentSettingsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = ".solid";
            dlg.AddExtension = true;
            dlg.Filter = "Solid Settings File (*.solid)|*.solid";
            dlg.OverwritePrompt = true;
            dlg.Title = "Save Solid Settings File";
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                _solidSettings.SaveAs(dlg.FileName);
            }
		}

    }
}