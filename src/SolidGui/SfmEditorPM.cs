using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SolidGui.Engine;
using SolidGui.Model;


namespace SolidGui
{
    public class SfmEditorPM
    {
        private SolidSettings _solidSettings;
        private readonly RecordNavigatorPM _navigatorModel;

        public class RecordEditedEventArgs:EventArgs
        {
            public string Record;

            public RecordEditedEventArgs(string record)
            {
                Record = record;
            }
        }

        public SfmEditorPM(RecordNavigatorPM navigatorModel)
        {
            _navigatorModel = navigatorModel;
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

        public void UpdateCurrentRecord(Record record, string newContents)
        {
            // Remove the inferred markers from the text
            // Encode the value correctly as per the solid marker settings (either utf-8 or iso-8859-1)
            var reader = SfmRecordReader.CreateFromText(newContents);
            reader.AllowLeadingWhiteSpace = true;
            if (reader.ReadRecord())
            {
                SfmRecord sfmRecord = reader.Record;
                RemoveInferredFields(sfmRecord);
                var sb = new StringBuilder();
                foreach (SfmField field in sfmRecord)
                {
                    field.Value = GetLatin1ValueFromUnicode(field.Marker, field.Value);
                    sb.Append("\\");
                    sb.Append(field.Marker);
                    sb.Append(" ");
                    sb.Append(field.Value);
                    sb.Append(field.Trailing);
                }
                record.SetRecordContents(sb.ToString(), _solidSettings);
            }
            else
            {
                throw new Exception("Solid was trying to update a record in a form which could not be read back in:"+newContents);
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
