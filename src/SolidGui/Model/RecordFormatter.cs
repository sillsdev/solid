using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SolidGui.Engine;

namespace SolidGui.Model
{

    public class RecordFormatterChangedEventArgs : System.EventArgs
    {
        public RecordFormatter NewFormatter;

        public RecordFormatterChangedEventArgs(RecordFormatter newFormatter)
        {
            NewFormatter = newFormatter;
        }
    }


    // A sort of adapter for formatting the Record class. Created to replace ye olde:
    // - Record.ToStructuredString() + SfmFieldModel.ToStructuredString()

    // - Record.ToString() ??
    // - most of SfmDictionary.SaveAs(),
    // - SfmEditorPM.AsString()
    // - SfmEditorView.DisplayEachFieldInCurrentRecord() 
    // Also, it will allow for more save options, like closing tags. 
    // And a non-indented editing mode, which is helpful with regex find/replace  -JMC Feb 2014
    public class RecordFormatter
    {

        // One option here might be a private inner interface IFormatter 
        // with private inner classers PlainTextFormatter and RichTextFormatter implementing it.
        // Heavy, but it might make the main formatting method cleaner...? -JMC 

        public const int SpacesInIndentation = 4;
        public bool Indented;
        public bool Inferred;
        public string Separator;
        public string NewLine;  // use \n everywhere except when saving to disk; doing find/replace on RichTextBox basically mandates \n and not \r\n. -JMC
        public bool ClosingTags;
        private Regex _regexOneNewline;

        public RecordFormatter()
        {
            SetDefaultsDisk();
            _regexOneNewline = new Regex(@"\r?\n|\r", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        }

        // E.g. "RF: Tree + NoClosers \r\n" would represent the normal editing format
        public override string ToString()
        {
            string ind = Indented ? "Tree " : "Flat ";
            string inf = Inferred ? "+ " : "No+ ";
            string clos = ClosingTags ? "Closers " : "NoClosers ";
            return "RF: " + ind + inf + Separator + clos + NewLine;
        }

        // A convenience method for overwriting one RecordFormatter with another without affecting existing references (pointers).
        // The idea is to make it easy to clone to a new one, swap these settings out, then put back the original settings back. -JMC Feb 2014
        public void FillFrom(RecordFormatter source)
        {
            Indented = source.Indented;
            Inferred = source.Inferred;
            Separator = source.Separator;
            NewLine = source.NewLine;
            ClosingTags = source.ClosingTags;
        }

        // Default formatting for Save As (Windows newlines, no indents or closers)
        public void SetDefaultsDisk()
        {
            Indented = false;
            Inferred = false;
            Separator = " ";
            NewLine = SolidSettings.NewLine; // was "\r\n"; 
            ClosingTags = false;
        }

        // Default UI formatting, showing marker hierarchy
        public void SetDefaultsUiTree()
        {
            Indented = true;
            Inferred = true;
            Separator = "\t";
            NewLine = "\n";
            ClosingTags = false;
        }

        // Flat UI formatting; also required for regex Find/Replace (needs to force UI into this mode)
        public void SetDefaultsUiFlat()
        {
            Indented = false;
            Inferred = true;
            Separator = " ";
            NewLine = "\n";
            ClosingTags = false;
        }


        private string getClosers(SfmFieldModel field)
        {
            string s = "";
            if (ClosingTags)
            {
                List<string> list = field.Closers;

                //JMC:! Dummy value for now. Parser (or something similar to LIFT adapter) needs to provide actual closers.
                if (list == null)
                {
                    list = new List<string> {"stub", "zz"}; // new List<string>;}
                }

                foreach(string c in list)
                {
                    s += " \\" + c + "*";
                }
            }
            return s;
        }

        private string getIndent(SfmFieldModel field)
        {           
            string indentation = "";
            if (Indented)
            {
                indentation = new string(' ', field.Depth * SpacesInIndentation);
            }
            return indentation;
        }

        private string getSlash(SfmFieldModel field)
        {
            if (Inferred)
            {
                return (field.Inferred) ? "\\+" : "\\";
            }
            return "\\";
        }

        private string getMarker(SfmFieldModel field)
        {
            return field.Marker;  //JMC: Or, bring up that Trim stuff ??
        }



        // Using current settings, format the record that is passed in.
        public string FormatPlain(Record rec, SolidSettings solidSettings)
        {


/*            return Format(rec, solidSettings, null);
        }

        // Using my settings, format the record that is passed in.
        private string Format(Record rec, SolidSettings solidSettings, Dictionary<int, string> report) //, MarkerSettings settings)
        {
 */ 
  
            StringBuilder record = new StringBuilder();

            if (solidSettings == null && (Indented || ClosingTags))
                throw new Exception("Programming error: non-flat output requested but solidSettings is null.");

            foreach (SfmFieldModel field in rec.LexEntry.Fields)
            {
                
                string indentation = getIndent(field);
                string slash = getSlash(field);
                string closers = getClosers(field);

                string val = (field.Value == "") ? "" : " " + field.DecodedValue(solidSettings);  // + field.Value;
                record.Append(indentation + slash + field.Marker + val + closers + field.Trailing);
            }
            string s = record.ToString();
            string s2 = _regexOneNewline.Replace(s, NewLine); //force all newlines to be the same, typically \n or \r\n (\r is unlikely)
            return s2;
        }


        private readonly Font _defaultFont = new Font(FontFamily.GenericSansSerif, 13);
        private readonly Font _highlightMarkerFont = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);

        // Using current settings, format the record that is passed in AS RICH TEXT.
        // The main goal is the side effect, but we also return a list of messages indexed to line numbers.
        // Warning: this APPENDS, so you should typically pass in an empty rb.
        public void FormatRich(Record rec, RichTextBox rb, MainWindowPM model)
        {

            var report = new Dictionary<int, string>();

            rb.SelectionFont = _defaultFont;
            rb.SelectionColor = SfmEditorView.DefaultTextColor;

            if (Indented)
            {
                SfmEditorView.Indent = SfmEditorView.IndentLarge;
            }
            else
            {
                SfmEditorView.Indent = SfmEditorView.IndentSmall;
            }

            foreach (SfmFieldModel field in rec.Fields)
            {
                if (field == null) break;

                string indentation = getIndent(field);
                string markerPrefix = getSlash(field);
                string closers = getClosers(field);
                string marker = getMarker(field);

                // 1) Indentation
                rb.AppendText(indentation);

                // 2) Marker
                marker = marker.Trim(new[] {'_'});
                    //JMC:! Remove this Trim? It can misalign find/replace, and don't think \se_ is the same as \se  -JMC Mar 2014
                rb.SelectionFont = _defaultFont;
/*
                if (HighlightMarkers!=null && HighlightMarkers.Contains(marker))
                {
                    rb.SelectionFont = _highlightMarkerFont;
                }
                else
                {
                    rb.SelectionFont = _defaultFont;
                }
 */
                rb.AppendText(markerPrefix + marker);
                rb.SelectionColor = SfmEditorView.DefaultTextColor;

                // 3) (tab + Value) + Trailing Whitespace 
                rb.SelectionFont = model.SfmEditorModel.FontForMarker(field.Marker) ?? _defaultFont;
                string displayValue = model.SfmEditorModel.GetUnicodeValueFromLatin1(field);
                if (displayValue != "")
                {
                    rb.AppendText("\t" + displayValue);
                }
                rb.AppendText(field.Trailing);
            }

        }



    }
}