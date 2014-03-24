using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public const int IndentWidth = 4; // a multiplier for pixels; use this when adding spaces too
        public bool ShowIndented;
        public int IndentSpaces;
        public bool ShowInferred;
        public string Separator;  // space or tab
        public bool EncodeForDisk; 
        public bool ShowClosingTags;
        public string NewLine;  // use \n everywhere except when saving to disk; doing find/replace on RichTextBox basically mandates \n and not \r\n. -JMC

        private Regex _regexOneNewline;
        private readonly Color _errorTextColor = Color.Red;
        private readonly Color _inferredTextColor = Color.Blue;
        private readonly Color _defaultTextColor = Color.DarkGreen; // .Black;
        private readonly Color _reminderTextColor = Color.DarkRed;
        private readonly Color _legacyTextColor = Color.DarkMagenta;


        public RecordFormatter()
        {
            SetDefaultsDisk();
            _regexOneNewline = new Regex(@"\r?\n|\r", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        }

        // E.g. "RF: Tree + NoClosers \r\n" would represent the normal editing format
        public override string ToString()
        {
            string ind = ShowIndented ? "Tree" : "Flat;";
            string inf = ShowInferred ? "+;" : "No+;";
            string enc = EncodeForDisk ? " encode;" : " no encode;";
            string clos = ShowClosingTags ? "Closers;" : "NoClosers;";
            return "RF: " + ind + IndentSpaces + inf + Separator + enc + clos + NewLine;
        }

        // A convenience method for overwriting one RecordFormatter with another without affecting existing references (pointers).
        // The idea is to make it easy to clone to a new one, swap these settings out, then put back the original settings back. -JMC Feb 2014
        public void FillFrom(RecordFormatter source)
        {
            ShowIndented = source.ShowIndented;
            IndentSpaces = source.IndentSpaces;
            ShowInferred = source.ShowInferred;
            Separator = source.Separator;
            EncodeForDisk = source.EncodeForDisk;
            NewLine = source.NewLine;
            ShowClosingTags = source.ShowClosingTags;
        }

        // Default formatting for Save As (Windows newlines, no indents or closers)
        public void SetDefaultsDisk()
        {
            ShowIndented = false;
            IndentSpaces = 0;
            ShowInferred = false;
            Separator = " ";
            EncodeForDisk = true;
            NewLine = SolidSettings.NewLine; // was "\r\n"; 
            ShowClosingTags = false;
        }

        // Default UI formatting, showing marker hierarchy
        public void SetDefaultsUiTree()
        {
            ShowIndented = true;
            IndentSpaces = 0;  // now that we have a better indent
            ShowInferred = true;
            Separator = "\t";
            EncodeForDisk = false;
            NewLine = "\n";
            ShowClosingTags = false;
        }

        // Flat UI formatting; also required for regex Find/Replace (needs to force UI into this mode)
        public void SetDefaultsUiFlat()
        {
            ShowIndented = false;
            IndentSpaces = 0;
            ShowInferred = true;
            Separator = " ";
            EncodeForDisk = false;
            NewLine = "\n";
            ShowClosingTags = false;
        }


        private int getIndentPixels(SfmFieldModel field)
        {
            return field.Depth*IndentWidth*3;
        }

        private string getIndent(SfmFieldModel field)
        {           
            string indentation = "";
            if (ShowIndented && IndentSpaces > 0)
            {
                indentation = new string(' ', field.Depth*IndentSpaces);
            }
            return indentation;
        }

        private string getSlash(SfmFieldModel field)
        {
            if (ShowInferred)
            {
                return (field.Inferred) ? "\\+" : "\\";
            }
            return "\\";
        }

        private string getMarker(SfmFieldModel field)
        {
            //JMC: bring up that Trim stuff that's in the rich text method ??
            return field.Marker;
/*
            if ((!ShowInferred) && field.Inferred)
            {
                return "";
            }
            return field.Marker;  
 */ 
        }

        private string getSeparator(SfmFieldModel field)
        {
            if (field.Value != "")
            {
                return ShowIndented ? "\t" : " ";
            }
            return "";
        }

        private string getValue(SfmFieldModel field, SolidSettings solidSettings)
        {
            if (EncodeForDisk)
            {
                
                return SfmFieldModel.ValueAsLatin1(field.Marker, field.Value, solidSettings);
                // return field.Value; //JMC: Is this ever needed?
            }
            else
            {
                return field.ValueForDisplay(solidSettings);
/*
                string retval;
                SolidMarkerSetting setting = solidSettings.FindOrCreateMarkerSetting(field.Marker);
                if (setting != null && setting.Unicode)
                {
                    retval = field.ValueAsUnicode();
                }
                else
                {
                    retval = field.Value;
                }
                return retval; // mainly for display
*/                
            }
        }

        private string getClosers(SfmFieldModel field)
        {
            string s = "";
            if (ShowClosingTags)
            {
                List<string> list = field.Closers;

                //JMC:! Dummy value for now. Parser (or something similar to LIFT adapter) needs to provide actual closers.
                if (list == null)
                {
                    list = new List<string> { "stub", "zz" }; // new List<string>;}
                }

                foreach (string c in list)
                {
                    s += " \\" + c + "*";
                }
            }
            return s;
        }

        private string replaceNewlines(string s)
        {
            string s2 = _regexOneNewline.Replace(s, NewLine); //force all newlines to be the same, typically \n or \r\n (\r is unlikely)
            return s2;
        }

        public string FormatPlain(IEnumerable<SfmField> rec, SolidSettings solidSettings)
        {
            var entry = SfmLexEntry.CreateFromReaderFields(rec);
            var record = new Record(entry, null);
            return FormatPlain(record, solidSettings);
        }

        // Using current settings, format the record that is passed in.
        public string FormatPlain(Record rec, SolidSettings solidSettings)
        {
            // One option here would be to just call FormatRich, then grab its plain text, since they
            // *must* match. But that's a lot of overhead when searching, saving, etc.
            // Implementing in parallel instead, with shared methods like getSeparator. -JMC Mar 2014
 
            StringBuilder sb = new StringBuilder();

            if (solidSettings == null && (ShowIndented || ShowClosingTags))
                throw new Exception("Programming error: non-flat output requested but solidSettings is null.");

            foreach (SfmFieldModel field in rec.Fields)
            {
                if (field == null) break; //does this happen?
                if (!ShowInferred && field.Inferred) continue;
                string indentation = getIndent(field);
                string slash = getSlash(field);
                string sep = getSeparator(field);
                string val = getValue(field, solidSettings);
                string closers = getClosers(field);
                sb.Append(indentation + slash + getMarker(field) + sep + val + closers + field.Trailing);
            }
            string s = sb.ToString();
            return replaceNewlines(s);
        }


        private readonly Font _defaultFont = new Font(FontFamily.GenericSansSerif, 13);
        private readonly Font _highlightMarkerFont = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
        private readonly Font _underlined = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Underline);

        // Using current settings, format the record that is passed in AS RICH TEXT.
        // Effect is the side effects on all but the first two parameters.
        // Warning: this APPENDS, so you should typically pass in an empty rb.
        public void FormatRich(Record rec, MainWindowPM model, RichTextBox rb, IEnumerable<string> highlightMarkers, MarkerTip markerTip)
        {

            //var report = new Dictionary<int, string>();
            highlightMarkers = highlightMarkers ?? new List<string>(); //don't want null

            rb.SelectionFont = _defaultFont;
            rb.SelectionColor = _defaultTextColor;

            if (ShowIndented)
            {
                SfmEditorView.TabPosition = SfmEditorView.TabPositionFar;
            }
            else
            {
                SfmEditorView.TabPosition = SfmEditorView.TabPositionNear;
                    //but might be better/more reassuring to not use tabs at all in flat mode. -JMC
            }

            int lineNumber = 0;
            foreach (SfmFieldModel field in rec.Fields)
            {
                if (field == null) break; //does this happen?
                if (!ShowInferred && field.Inferred) continue;
                bool isUnicode = (SfmFieldModel.IsUnicode(field.Marker, model.Settings));

                rb.SelectionFont = _defaultFont;
                rb.SelectionColor = _defaultTextColor;

                // 1) Indentation
                if (ShowIndented)
                {
                    /* // add leading spaces
                    string indentation = "";
                    indentation = getIndent(field);
                    rb.AppendText(indentation);
                    */

                    // Add indent. This gets rid of the problematic leading spaces. -JMC Mar 2014
                    rb.SelectionIndent = getIndentPixels(field);
                }

                string markerPrefix = getSlash(field);
                string marker = getMarker(field);

                // 2) Marker
                marker = marker.Trim(new[] {'_'});
                //JMC:! Remove this Trim? It can misalign find/replace, and don't think \se_ is the same as \se  -JMC Mar 2014

                if (highlightMarkers.Contains(field.Marker))
                {
                    rb.SelectionFont = _highlightMarkerFont;
                }

                //Color the markers, and add super tooltip messages. Also check for encoding issues.
                if (field == null) break;
                if (field.Inferred)
                {
                    markerTip.AddLineMessage(lineNumber, "Inferred");
                    rb.SelectionColor = _inferredTextColor;
                }

                bool encodingIssue = false;
                foreach (ReportEntry reportEntry in field.ReportEntries)
                {
                    markerTip.AddLineMessage(lineNumber, reportEntry.Description);

                    if (SolidReport.IsDataWarning(reportEntry.EntryType))
                    {
                        encodingIssue = true;
                    }
                    else
                    {
                        rb.SelectionColor = _errorTextColor;
                    }
                }
                rb.AppendText(markerPrefix + marker);
                rb.SelectionColor = _defaultTextColor;

                // 3) separator, value, closers, and Trailing Whitespace 
                string sep = getSeparator(field);
                // rb.SelectionFont = _underlined;  // just for grins (might look ok dashed/dotted; probably not. -JMC
                rb.AppendText(sep);

                rb.SelectionFont = model.SfmEditorModel.FontForMarker(field.Marker) ?? _defaultFont;
                if (encodingIssue)
                {
                    rb.SelectionColor = isUnicode ? _errorTextColor : _reminderTextColor;
                }
                else
                {
                    rb.SelectionColor = isUnicode ? _defaultTextColor : _legacyTextColor;
                }
                string displayValue = getValue(field, model.Settings);
                rb.AppendText(displayValue);  
                rb.SelectionColor = _defaultTextColor;

                string closers = getClosers(field);
                rb.AppendText(closers);
                rb.AppendText(field.Trailing);

                int inc = (sep + displayValue + field.Trailing).Count(x => x == '\n');
                lineNumber += inc;

                // Unlike FormatPlain, there's no need to try calling replaceNewlines(); 
                // the rich textbox will force \n regardless. -JMC

            }
        }


    }
}