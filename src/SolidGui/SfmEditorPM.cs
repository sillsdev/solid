// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SIL.Reporting;
using SIL.Windows.Forms.WritingSystems;
using SIL.WritingSystems;
using SolidGui.Engine;
using SolidGui.Model;


namespace SolidGui
{
    public class SfmEditorPM
    {
        private MainWindowPM _model;
        //private RecordFormatter _recordFormatter;
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
      
        // Take whatever was in the view's rich text box and update the underlying model to match it.
        public void UpdateCurrentRecord(Record record, string newContents)
        {
            Filter.RecordFilter f = _model.NavigatorModel.ActiveFilter;
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


            // Usually there'll only be one, but the readers lets us handle multiple \lx in a single "record"--the result of recent user edits (issue #173)
            var reader = SfmRecordReader.CreateFromText(newContents);
            reader.AllowLeadingWhiteSpace = true; // in case the UI added any; I currently have it using indentation instead. -JMC

            int i = -1;
            //foreach (var r in records)
            while (reader.ReadRecord())  // the loop will only run once, unless the user messed with lx
            {
                i++;
                SfmRecord sfmRecord = reader.Record;
                // Remove the inferred markers from the text
                RemoveInferredFields(sfmRecord);

                // At this point, this one record is stored in one clean string, ostensibly all UTF-16  -?

                // Encode the value correctly as per the solid marker settings (either utf-8 or SolidSettings.LegacyEncoding),
                // just as if we were already writing to disk? Is each utf-8 byte simply dumped into a two-byte char? -JMC

                //string old = AsString(sfmRecord);
                var rf = new RecordFormatter();
                rf.SetDefaultsDisk();
                rf.EncodeSomeUtf8 = false;
                Record.DecodeUtf8(sfmRecord, _model.Settings);
                string s = rf.FormatPlain(sfmRecord, _model.Settings);

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
                    var ef = f; // as SolidErrorRecordFilter;  // does an "is" check and a cast
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

        /*
        private string AsString(SfmRecord sfmRecord)
        {
            var sb = new StringBuilder();
            foreach (SfmField field in sfmRecord)
            {
                string s = SfmFieldModel.ValueAsLatin1(field.Marker, field.Value, _model.Settings);
                // field.Value = s; // JMC: Do we really want this side effect? Couldn't running this method twice on a record double decode the unicode values?
                // JMC: I've added a local variable s so we can test without it
                sb.Append("\\");
                sb.Append(field.Marker);
                if (s != "") 
                {
                    sb.Append(" ");
                    sb.Append(s);
                }
                sb.Append(field.Trailing);
            }
            return sb.ToString();
        } 
         */

        private static void RemoveInferredFields(List<SfmField> sfmRecord)
        {
            sfmRecord.RemoveAll(rhs => rhs.Marker.StartsWith("+"));
        }

        public Font FontForMarker(string marker)
        {
            string writingSystemId = _model.Settings.FindOrCreateMarkerSetting(marker).WritingSystemRfc4646;

            float fontSize = 12;

            // Get the default font information from the writing system.
            if (!String.IsNullOrEmpty(writingSystemId))
            {
                IWritingSystemRepository repository = AppWritingSystems.WritingSystems;
                if (repository.Contains(writingSystemId))
                {
                    WritingSystemDefinition definition = repository.Get(writingSystemId);
                    fontSize = (definition.DefaultFontSize < 10) ? 10 : definition.DefaultFontSize;
                    if (definition.DefaultFont != null)
                        return new Font(definition.DefaultFont.Name, fontSize);
                }
            }
            // Failing that use Doulos if it's installed.
            if (FontIsInstalled("Doulos SIL"))
            {
                return new Font("Doulos SIL", fontSize);
            }
            // Failing that use the default system font.
            return new Font(FontFamily.GenericSansSerif, fontSize);
        }


        private static bool FontIsInstalled(string name)
        {
            return FontFamily.Families.Any(family => family.Name == name);
        }
    }
}
