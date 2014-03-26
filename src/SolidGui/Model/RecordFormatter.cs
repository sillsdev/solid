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
    // - SfmEditorPM.AsString()
    // - most of SfmDictionary.SaveAs(),
    // - SfmEditorView.DisplayEachFieldInCurrentRecord() 
    // (That last one being rich text). Also, it will allow for more save options, like closing tags. 
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
        public bool EncodeSomeUtf8; 
        public bool ShowClosingTags;
        public string NewLine;  // use \n everywhere except when saving to disk; doing find/replace on RichTextBox basically mandates \n and not \r\n. -JMC

        private Regex _regexOneNewline;
        private readonly Color _errorTextColor = Color.Red;
        private readonly Color _inferredTextColor = Color.Blue;
        private readonly Color _defaultTextColor = Color.Black; // DarkGreen; // 
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
            string ind = ShowIndented ? "Tree;" : "Flat;";
            string inf = ShowInferred ? "+;" : "No+;";
            string enc = EncodeSomeUtf8 ? " some utf8;" : " no utf8;";
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
            EncodeSomeUtf8 = source.EncodeSomeUtf8;
            NewLine = source.NewLine;
            ShowClosingTags = source.ShowClosingTags;
        }

        // Default formatting for Save As (Windows newlines, no indents or closers)
        public void SetDefaultsDisk()
        {
            ShowIndented = false;
            IndentSpaces = IndentWidth;
            ShowInferred = false;
            Separator = " ";
            EncodeSomeUtf8 = false;  // because our unicode data is (unofficially) already utf-8 sitting in memory strings.
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
            EncodeSomeUtf8 = true;
            NewLine = "\n";
            ShowClosingTags = false;
        }

        // Flat UI formatting; also required for regex Find/Replace (needs to force UI into this mode)
        public void SetDefaultsUiFlat()
        {
            ShowIndented = false;
            IndentSpaces = IndentWidth;
            ShowInferred = true;
            Separator = " ";
            EncodeSomeUtf8 = true;
            NewLine = "\n";
            ShowClosingTags = false;
        }


        private int GetIndentPixels(SfmFieldModel field)
        {
            return field.Depth*IndentWidth*3;
        }

        private string GetIndent(SfmFieldModel field)
        {           
            string indentation = "";
            if (ShowIndented && IndentSpaces > 0)
            {
                indentation = new string(' ', field.Depth*IndentSpaces);
            }
            return indentation;
        }

        private string GetSlash(SfmFieldModel field)
        {
            if (ShowInferred)
            {
                return (field.Inferred) ? "\\+" : "\\";
            }
            return "\\";
        }

        private string GetMarker(SfmFieldModel field)
        {
            return field.Marker;
/*
            if ((!ShowInferred) && field.Inferred)
            {
                return "";
            }
            return field.Marker;  
 */ 
        }

        private string GetSeparator(SfmFieldModel field)
        {
            if (field.Value != "")  // (issue #1206) don't insert trailing spaces that weren't in the file. -JMC 2013-09
            {
                return Separator;
            }
            return "";
        }

        /* Analysis of original design of encoding/decoding: (JMC)
        SfmEditorPM.UpdateCurrentRecord 
          called GetLatin1ValueFromUnicode
            if field marker is "unicode", convert to ISO:
                byte[] valueAsBytes = Encoding.UTF8;.GetBytes(value);
                return Encoding.GetEncoding("iso-8859-1").GetString(valueAsBytes);
            else plain value (ISO)
          Conclusion: RichTextBox returns some UTF8, but Everything is stored as ISO in memory.

        SfmDictionary.SaveAs called update, then used a streamwriter set to ISO and wrote straight from memory
          Conclusion: Everything is stored as ISO to disk.
  
        DisplayEachFieldInCurrentRecord()
          called GetUnicodeValueFromLatin1(field) and appended that to the richtextbox
            if field marker is "unicode", convert to utf8
                byte[] valueAsBytes = Encoding.GetEncoding("iso-8859-1").GetBytes(value);
                return Encoding.UTF8.GetString(valueAsBytes);
            else plain value (ISO)
          Conclusion: The only place we officially write UTF8 is to the UI! 
            Example: presumably a character that's 3-byte in UTF8 gets stored in memory as 3 UTF16 chars (6 bytes).
            Hopefully it all gets passed through ok, and 1- to 4-byte characters work identically?
            (Presumably 4 is no harder than 2-3, so no need to explicitly handle surrogate pairs?)
         */

        private string GetValue(SfmFieldModel field, SolidSettings solidSettings)
        {
            if (EncodeSomeUtf8)
            {
                return field.ValueMaybeUtf8(solidSettings);
            }
            return field.Value;
        }

        private string GetClosers(SfmFieldModel field)
        {
            string s = "";
            if (ShowClosingTags)
            {
                List<string> list = field.Closers;
                if (list == null)
                {
                    // For error fields, have them close themselves. Not ideal, but at least it balances. -JMC
                    return "\\" + field.Marker + "*";
                }

                foreach (string c in list)
                {
                    s += " \\" + c + "*";
                }
            }
            return s;
        }

        private string ReplaceNewlines(string s)
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
                string indentation = GetIndent(field);
                string slash = GetSlash(field);
                string marker = GetMarker(field);
                string sep = GetSeparator(field);
                string val = GetValue(field, solidSettings);
                string closers = GetClosers(field);
                sb.Append(indentation + slash + marker + sep + val + closers + field.Trailing);
            }
            string s = sb.ToString();
            return ReplaceNewlines(s);
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
                    rb.SelectionIndent = GetIndentPixels(field);
                }

                string markerPrefix = GetSlash(field);
                string marker = GetMarker(field);

                // 2) Marker
                //marker = marker.Trim(new[] { '_' });  //I think this was being trimmed off because the old parser detected the header based on leading underscores. -JMC 2013-10
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
                string sep = GetSeparator(field);
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
                string displayValue = GetValue(field, model.Settings);
                rb.AppendText(displayValue);  
                rb.SelectionColor = _defaultTextColor;

                string closers = GetClosers(field);
                rb.AppendText(closers);
                rb.AppendText(field.Trailing);

                int inc = (sep + displayValue + field.Trailing).Count(x => x == '\n'); //count the number of newlines in this field
                lineNumber += inc;

                // Unlike FormatPlain, there's no need to try calling replaceNewlines(); 
                // the richtextbox will force \n regardless. -JMC

            }
        }


    }
}