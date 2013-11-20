using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Palaso.Reporting;
using SolidGui.Engine;
using SolidGui.Model;


namespace SolidGui
{
    public class SfmEditorPM
    {
        // private SolidSettings _solidSettings;
        private MainWindowPM _model;
        //private readonly RecordNavigatorPM _navigatorModel;  // JMC: delete this
        // private SfmDictionary _dictionary;   // working toward fixing #173 etc. (adding/deleting entries) -JMC 2013-09
        public class RecordEditedEventArgs:EventArgs
        {
            public string Record;

            public RecordEditedEventArgs(string record)
            {
                Record = record;
            }
        }

        public SfmEditorPM (MainWindowPM m)  //JMC: was (RecordNavigatorPM navigatorModel, SfmDictionary dict)  
        {
            _model = m;
/*
            _navigatorModel = navigatorModel;
            _dictionary = dict;
*/
        }

        public override string ToString()
        {
            return string.Format("{{edit: {0}}}", _model.WorkingDictionary);
        }

/*
        // JMC:!! Remove this property altogether? Access it via MainWindowPM instead (easier to swap it out then)
        public SolidSettings SolidSettings
        {
            set
            { _solidSettings = value; }
            get  // added it; missing get seemed like a simple oversight, and I needed it. -JMC 2013-09
            { return _solidSettings; }
        }
*/

        public void MoveToFirst()
        {
            _model.NavigatorModel.MoveToFirst();
        }

        public void MoveToLast()
        {
            _model.NavigatorModel.MoveToLast();
        }

        public void MoveToPrevious()
        {
            _model.NavigatorModel.MoveToPrevious();
        }

        public void MoveToNext()
        {
            _model.NavigatorModel.MoveToNext();
        }

        private static Regex ReggieLeading = new Regex(
            @"^\s+", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static Regex ReggieTab = new Regex(
            @"\t", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static Regex ReggieLx = new Regex(
            @"^[ \t]*\\" + SolidSettings.NewLine + @"\b", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        // Take whatever was in the view's rich text box and update the underlying model to match it.
        public void UpdateCurrentRecord(Record record, string newContents)
        {
            var f = _model.NavigatorModel.ActiveFilter;
            newContents = newContents.TrimStart(null);
            if (newContents.TrimEnd(null) == "") // the DELETE case (issue #174)
            {
                // user cleared it; delete the record that was there, then return -JMC
                if (_model.WorkingDictionary.DeleteRecord(record))
                {
                    record.SetRecordContents("", _model.Settings);
                    // Prevents phantoms from reappearing on Refresh etc. Too bad we can't dispose the object (or all references to it). -JMC 2013-10
                    // It also signals SfmEditorView to hide the textbox so the user won't enter data there (which would be lost).

                    f.Remove(); // JMC:! test the effects of this, and of add. (unit tests too)
                    if (f.HasPrevious())
                    {
                        f.MoveToPrevious();
                    }
                    /*
                                        else if (navf.HasNext())
                                        {
                                            navf.MoveToNext();
                                            navf.MoveToPrevious();
                                        }
                    */
                    f.UpdateFilter();

                }
                else
                {
                    // JMC: Hmm, we should at least notify here, but do we want to crash harder? Shouldn't really happen anyway, though.
                    ErrorReport.NotifyUserOfProblem(
                        String.Format("There was a problem deleting this record (ID {0}).\n Record not found.",
                                      record.ID));
                }
                return;
            }
            else if (!newContents.StartsWith("\\" + _model.Settings.RecordMarker))
                // the FRAGMENT ABOVE case (an edge case under issue #173)
            {
                // user edits resulted in an initial fragment; insert an "\\lx FRAGMENT line" -JMC 2013-09
                // JMC: It might be nice to create an additional warning filter that finds these fragments; for that, we would want frag below to become a global setting.
                string frag = "FRAGMENT!";
                newContents = "\\" + _model.Settings.RecordMarker + " " + frag + SolidSettings.NewLine +
                              newContents;

                ErrorReport.NotifyUserOfProblem("Record fragment detected! Adding a new record for it.", record.ID);
                // Ideally we'd show this after displaying "FRAGMENT!" in the rich edit box, but before applying the effects (updating the filter and right pane).
                // That would probably involve triggering a (new) event, though, and having the view listen to it. Not worth it, for now? -JMC 2013-10
            }


            // regex cleanup to remove tabs, and leading spaces -JMC 2013-09

            // JMC: Could also paste these two lines into a toolbar button method that does "plain-text copy" (includes inferred like \+sn but no formatting).
            //   Toolbar button and/or add Ctrl-C to SfmEditorView, _contentsBox_KeyDown .
            newContents = ReggieLeading.Replace(newContents, "");
            newContents = ReggieTab.Replace(newContents, " ");

            // Check for multiple \lx in a single "record"--the result of recent user edits (issue #173)
            /*
                        // JMC: make newContents into a string[] and put the following in a loop.
                        var splitAt = ReggieLx.Matches(newContents);
                        var records = ReggieLx.Split(newContents);
            */
            var reader = SfmRecordReader.CreateFromText(newContents);
            reader.AllowLeadingWhiteSpace = true;

            int i = -1;
            //foreach (var r in records)
            while (reader.ReadRecord())
            {
                i++;
                SfmRecord sfmRecord = reader.Record;
                // Remove the inferred markers from the text
                RemoveInferredFields(sfmRecord);
                // Encode the value correctly as per the solid marker settings (either utf-8 or iso-8859-1)
                string s = AsString(sfmRecord);

                if (i == 0)
                {
                    // We get one freebie; the first record is simply kept and sent to the UI
                    record.SetRecordContents(s, _model.Settings);

                }
                else
                {
                    // Additional \lx found; insert a new record into the lexicon, and into the current filter
                    SfmLexEntry lexEntry = SfmLexEntry.CreateFromReaderFields(reader.Fields);
                    var tmp = new Record(lexEntry, null);

                    // JMC:! The following (append) works, but it would be nicer to insert into our current position in the file and filter.
                    _model.WorkingDictionary.AddRecord(tmp);
                    // JMC:! Update the filter; make sure this works for SolidErrorRecordFilter too 
                    var ef = f as SolidErrorRecordFilter;  // does an "is" check and a cast
                    if (ef != null)
                    {
                        ef.AddEntry(_model.WorkingDictionary.Count-1); // JMC:! append; insert w/b better
                    }
                    else
                    {
                        _model.NavigatorModel.ActiveFilter.UpdateFilter();                        
                    }
                }
                 
            }

            // JMC: the UI will now update the right pane display (e.g. if the user edited leading spaces, replace those with the current interpretation).
            // Again, should we explicitly trigger this by invoking an event, for clarity?
        }

        private string AsString(SfmRecord sfmRecord)
        {
            var sb = new StringBuilder();
            foreach (SfmField field in sfmRecord)
            {
                string s = GetLatin1ValueFromUnicode(field.Marker, field.Value);
                field.Value = s; // JMC:! Do we really want this side effect? Couldn't running this method twice on a record double decode the unicode values?
                // JMC: I've added a local variable so we can test without it
                sb.Append("\\");
                sb.Append(field.Marker);
                if (s != "") // (issue #1206) don't insert trailing spaces that weren't in the file. -JMC 2013-09
                {
                    sb.Append(" ");
                    sb.Append(s);
                }
                sb.Append(field.Trailing);
            }
            return sb.ToString();
        }

        private static void RemoveInferredFields(List<SfmField> sfmRecord)
        {
            sfmRecord.RemoveAll(rhs => rhs.Marker.StartsWith("+"));
        }

        public Font FontForMarker(string marker)
        {
            string writingSystemId = _model.Settings.FindOrCreateMarkerSetting(marker).WritingSystemRfc4646;

            // Get the default font information from the writing system.
            if (!String.IsNullOrEmpty(writingSystemId))
            {
                var repository = AppWritingSystems.WritingSystems;
                if (repository.Contains(writingSystemId))
                {
                    var definition = repository.Get(writingSystemId);
                    var fontSize = (definition.DefaultFontSize < 10) ? 10 : definition.DefaultFontSize;
                    return new Font(definition.DefaultFontName, fontSize);
                }
            }
            // Failing that use Doulos if it's installed.
            if (FontIsInstalled("Doulos SIL"))
            {
                return new Font("Doulos SIL", 12);
            }
            // Failing that use the default system font.
            return new Font(FontFamily.GenericSansSerif, 12);
        }

        public string GetUnicodeValueFromLatin1(SfmFieldModel field)
        {
            string retval;
            SolidMarkerSetting setting = _model.Settings.FindOrCreateMarkerSetting(field.Marker);
            if (setting != null && setting.Unicode)
            {
                retval = field.ValueAsUnicode();
            }
            else
            {
                retval = field.Value;
            }
            return retval;
        }

        private string GetLatin1ValueFromUnicode(string marker, string value)
        {
            SolidMarkerSetting setting = _model.Settings.FindOrCreateMarkerSetting(marker);
            if (setting != null && setting.Unicode)
            {
                Encoding stringEncoding = Encoding.UTF8;
                byte[] valueAsBytes = stringEncoding.GetBytes(value);
                Encoding byteEncoding = Encoding.GetEncoding("iso-8859-1");
                return byteEncoding.GetString(valueAsBytes);
            }

            return value;
        }

        private static bool FontIsInstalled(string name)
        {
            return FontFamily.Families.Any(family => family.Name == name);
        }
    }
}
