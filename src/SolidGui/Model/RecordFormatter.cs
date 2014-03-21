using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SolidGui.Engine;

namespace SolidGui.Model
{
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


        // Using my settings, format the record that is passed in.
        public string Format(Record rec, SolidSettings solidSettings) //, MarkerSettings settings)
        {
            StringBuilder record = new StringBuilder();
            int spacesInIndentation = 4;

            if (solidSettings == null && (Indented || ClosingTags))
                throw new Exception("Programming error: non-flat output requested but solidSettings is null.");

            foreach (SfmFieldModel field in rec.LexEntry.Fields)
            {
                string indentation = "";
                if (Indented) indentation = new string(' ', field.Depth * spacesInIndentation);
                string slash = (field.Inferred) ? "\\+" : "\\";
                string closers = "";
                if (ClosingTags) closers = FormatClosers(field.Closers);
                string val = (field.Value == "") ? "" : " " + field.DecodedValue(solidSettings);  // + field.Value;
                record.Append(indentation + slash + field.Marker + val + closers + field.Trailing);
            }
            string s = record.ToString();
            string s2 = _regexOneNewline.Replace(s, NewLine); //force all newlines to be the same, typically \n or \r\n (\r is unlikely)
            return s2;
        }

        private string FormatClosers(List<string> list)
        {
            string s = "";

            //JMC:! Dummy value for now. Parser (or something similar to LIFT adapter) needs to provide actual closers.
            if (list == null) list = new List<string> {"stub", "zz"}; // new List<string>;}

            foreach(string c in list)
            {
                s += " \\" + c + "*";
            }

            return s;
        }

        // Using our current settings, format the record that is passed in AS RICH TEXT.
        // The main goal is the side effect, but we also return a list of messages indexed to line numbers.
        // Warning: this APPENDS, so you should typically pass in an empty rb.
        public Dictionary<int, string> formatRich(Record rec, RichTextBox rb)
        {
            // Should we return string instead?

            return null;
            // Should we simultaneously return string? Less clear, though then we only need one public method (with rb optionally null).
        }

    }
}