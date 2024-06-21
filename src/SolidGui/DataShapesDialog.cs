using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SIL.Extensions;
using SIL.Reporting;
using SolidGui.Engine;
using SolidGui.Model;
using SolidGui.Search;

namespace SolidGui
{
    public partial class DataShapesDialog : Form
    {
        private FindReplaceDialog? _searchDialog;

        public DataShapesDialog(FindReplaceDialog searchDialog, MainWindowPM mwp)
        {
            InitializeComponent();
            if (DesignMode) return;

            _searchDialog = searchDialog;
/*
            _markerColumn.Width = 32;
            _countColumn.Width = 30;
            _shapeColumn.Width = 150;
*/
            _mainWindowPm = mwp;
        }

        private MainWindowPM? _mainWindowPm;

        private IEnumerable<SfmDictionary.DataShape>? _shapes;

        private void _closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _runButton_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void Run()
        {
            
            if (_mainWindowPm != null)
            {
                int before = (int)beforeNumericUpDown.Value;
                int after = (int)afterNumericUpDown.Value;
                
                var markers = new HashSet<string>();
                if (!String.IsNullOrEmpty(_markersTextBox.Text.Trim()))
                {
                    string[] splitOn = {", ", ",", " "};
                    string[] tmp = _markersTextBox.Text.Split(splitOn, StringSplitOptions.RemoveEmptyEntries);
                    markers.UnionWith(tmp);
                }
                
                _shapes = _mainWindowPm.WorkingDictionary.GetAllDataShapes(before, after, markers);
            }
            else
            {
                return;
/*
                //test data
                //String[] 
                _shapes = new List<string>();
                _shapes.Add("a");
                _shapes.Add("b");
                _shapes.Add("c");
                //string[] shapelabels = {"y", "x", "z"}; //new List<string>();
                //int[] counts = {3, 4, 2}; //new List<int>();
*/
            }

            _reportListView.BeginUpdate();
            _reportListView.Items.Clear();

            int i = 0;

            try{
                foreach (var shape in _shapes)
                {
                    var item = new ListViewItem(shape.FocusedMarker.ToString());
                    item.SubItems.Add(shape.Occurs.ToString());
                    item.SubItems.Add(shape.ToString());
                    item.Tag = shape;
                    _reportListView.Items.Add(item);
                    i++;
                }
                _reportListView.FullRowSelect = true;
                _reportListView.Focus();
                if (_reportListView.Items.Count >= 1)
                    _reportListView.Items[0].Selected = true;
                //_reportListView.FocusedItem = _reportListView.Items[0];
            }
            catch (Exception e)
            {
                ErrorReport.NotifyUserOfProblem(
                    "There was a problem with the marker(s) you typed in.  The error was\r\n" + e.Message);
            }
            _reportListView.EndUpdate();
        }

        private void _copyAllButton_Click(object sender, EventArgs e)
        {
            string s = string.Join(SolidSettings.NewLine, _reportListView.Items);
            Clipboard.SetText(s);
        }

        private void recipeButton_Click(object sender, EventArgs e)
        {
            makeRecipe();
        }
        
        private void makeRecipe()
        {
            var sel = _reportListView.SelectedItems;
            if (sel == null || sel.Count <= 0) return;

            var sbFind = new StringBuilder();
            var sbReplace = new StringBuilder();
            var tmp = (SfmDictionary.DataShape)sel[0].Tag;
            int i = 1;
            foreach(var mrk in tmp.ShapeMarkers())
            {
                string marker = mrk;
                if (marker == SfmDictionary.StartOfRecord)
                {
                    sbFind.Append(@"(?<!.)"); // negative look-behind that should match start-of-record
                    continue;
                }

                if (marker == SfmDictionary.EndOfRecord)
                {
                    sbFind.Append(@"(?!\\)"); // negative lookahead that should match end-of-record
                    break;
                }

                sbFind.Append(@"((?:^\\[+]?");  // the first parens makes sure we can capture multiple lines as \1, 
                // and (?: is non-capturing, to keep things simpler. The optional plus covers inferred: [+]?
                string plus = "";
                if (marker.EndsWith("+"))
                {
                    plus = @"+";
                    marker = marker.Substring(0, marker.Length - 1);
                }
                sbFind.Append(marker +@"\b.*[\r\n]+)" + plus + @")");
                sbReplace.Append(@"$" + i);
                i++;
            }
            
            string help = 
@"To reorder these fields, rearrange the captured pieces in the Replace With field.
This regex is designed to work in Notepad++ too, if you paste it there,
except that dollars ($1$2) would need to be backslashes (\1\2).
WARNING: This moves trailing newlines too, and it will not work 
if any of the fields have hard-wrapped data.";

            RegexItem r = RegexItem.GetCustomRegex(sbFind.ToString(), sbReplace.ToString(), help, true);
            _searchDialog?.LaunchSearch(r);

        }

        private void _reportListView_DoubleClick(object sender, EventArgs e)
        {
            makeRecipe();
        }

        private void _markersTextBox_Validating(object sender, CancelEventArgs e)
        {
            return; // not sure we need validation after all; will try/catch instead.
            //_markersTextBox.Text = Regex.Replace(_markersTextBox.Text, @"[^ a-zA-Z_0-9]", "");
        }
    }
}
