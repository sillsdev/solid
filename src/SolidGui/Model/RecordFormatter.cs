using System.Collections.Generic;
using System.Windows.Forms;

namespace SolidGui.Model
{
    // A sort of adapter for formatting the Record class. To replace ye olde:
    // - Record.ToStructuredString() 
    // - most of SfmDictionary.SaveAs(),
    // - SfmEditorPM.AsString()
    // - SfmEditorView.DisplayEachFieldInCurrentRecord() 
    // Also, it will allow for more save options, like closing tags. 
    // And a non-indented editing mode, which is helpful with regex find/replace  -JMC Feb 2014
    public class RecordFormatter
    {
        // defaults (for saving to disk on Windows)
        public bool Indented = false;
        public string Separator = " ";
        public string NewLine = "\r\n";  // use \n everywhere except when saving to disk; doing find/replace on RichTextBox basically mandates this. -JMC
        public bool ClosingTags = false;

        public RecordFormatter()
        {
        }

        // A convenience method for overwriting one object with another without affecting references (pointers).
        // The idea is to make it easy to make a copy, then swap these settings out, then put back the original settings. -JMC Feb 2014
        public void fill_from(RecordFormatter source)
        {
            Indented = source.Indented;
            Separator = source.Separator;
            NewLine = source.NewLine;
            ClosingTags = source.ClosingTags;
        }

        // Using my settings, format the record that is passed in.
        public string format(Record rec) //, MarkerSettings settings)
        {
            return "";
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