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
        private SolidSettings _solidSettings;
        private readonly RecordNavigatorPM _navigatorModel;
        private SfmDictionary _dictionary;   // JMC:! working toward fixing #173 etc.
        public class RecordEditedEventArgs:EventArgs
        {
            public string Record;

            public RecordEditedEventArgs(string record)
            {
                Record = record;
            }
        }

        public SfmEditorPM(RecordNavigatorPM navigatorModel, SfmDictionary dict)  
        {
            _navigatorModel = navigatorModel;
            _dictionary = dict;
        }

        public SolidSettings SolidSettings
        {
            set
            { _solidSettings = value; }
            get  // added it; missing get seemed like a simple oversight, and I needed it. -JMC 2013-09
            { return _solidSettings; }
        }

        public void MoveToFirst()
        {
            _navigatorModel.MoveToFirst();
        }

        public void MoveToLast()
        {
            _navigatorModel.MoveToLast();
        }

        public void MoveToPrevious()
        {
            _navigatorModel.MoveToPrevious();
        }

        public void MoveToNext()
        {
            _navigatorModel.MoveToNext();
        }

        private static Regex ReggieLeading = new Regex(
            @"^\s+", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static Regex ReggieTab = new Regex(
            @"\t", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public void UpdateCurrentRecord(Record record, string newContents)
        {
            newContents = newContents.TrimStart(null);
            if (newContents.TrimEnd(null) == "")  // the DELETE case
            {
                // user cleared it; delete the record that was there, then return -JMC
                if (_dictionary.DeleteRecord(record))
                {
                    // JMC:! Success. Need to also delete it from the current filter, or reload the filter and jump to index i-1 or so (or failover to AllRecords)
                    // JMC:! Verify that this prevents a phantom display after clearing and pressing Refresh
                }
                else
                {
                    // JMC: Hmm, we should notify here, but do we really want to crash hard? Shouldn't really happen anyway, though.
                    ErrorReport.NotifyUserOfProblem(
                        String.Format("There was a problem deleting this record (ID {0}).\n Record not found.", record.ID) );
                }
                return;
            }
            else if (!newContents.StartsWith("\\" + _solidSettings.RecordMarker))  // the FRAGMENT ABOVE case
            {
                // user edits resulted in an initial fragment; insert an "\\lx FRAGMENT line" -JMC 2013-09
                newContents = "\\" + _solidSettings.RecordMarker + " FRAGMENT!" + SolidSettings.NewLine + newContents;

                // JMC: and give a popup warning messagebox, ideally after showing "FRAGMENT!" but before applying it and updating the filter and right pane
            }


            // regex cleanup to remove tabs, and leading spaces -JMC 2013-09
            newContents = ReggieLeading.Replace(newContents, "");  // JMC: Could also paste these two lines into a toolbar button method that does "plain-text copy" (includes inferred like \+sn but no formatting)
            newContents = ReggieTab.Replace(newContents, " ");

            // JMC:! check for multiple \lx; i.e. make newContents into a string[] and put the following in a loop. (Issue #173 etc.)

            
            // Remove the inferred markers from the text
            // Encode the value correctly as per the solid marker settings (either utf-8 or iso-8859-1)
            var reader = SfmRecordReader.CreateFromText(newContents);
            reader.AllowLeadingWhiteSpace = true;
            if (reader.ReadRecord())  //JMC:! needs to be a while loop, in case the user inserted an \lx 
            {
                SfmRecord sfmRecord = reader.Record;
                RemoveInferredFields(sfmRecord);
                var sb = new StringBuilder();
                foreach (SfmField field in sfmRecord)
                {
                    field.Value = GetLatin1ValueFromUnicode(field.Marker, field.Value);
                    sb.Append("\\");
                    sb.Append(field.Marker);
                    if (field.Value != "")  // (issue #1206) don't insert trailing spaces that weren't in the file. -JMC 2013-09
                    {
                        sb.Append(" ");
                        sb.Append(field.Value);
                    }
                    sb.Append(field.Trailing);
                }
                record.SetRecordContents(sb.ToString(), _solidSettings);

                // JMC: need to also update the right pane display (e.g. to reflect bogus removed leading spaces, etc.)
            }
            else
            {
                int x;
                //throw new Exception("Solid was trying to update a record in a form which could not be read back in:"+newContents);
            }
        }

        private static void RemoveInferredFields(List<SfmField> sfmRecord)
        {
            sfmRecord.RemoveAll(rhs => rhs.Marker.StartsWith("+"));
        }

        public Font FontForMarker(string marker)
        {
            string writingSystemId = _solidSettings.FindOrCreateMarkerSetting(marker).WritingSystemRfc4646;

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
            SolidMarkerSetting setting =  _solidSettings.FindOrCreateMarkerSetting(field.Marker);
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
            SolidMarkerSetting setting =  _solidSettings.FindOrCreateMarkerSetting(marker);
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
