using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Palaso.WritingSystems;
using SolidEngine;

namespace SolidGui
{
    public class SfmEditorPM
    {
        private SolidSettings _solidSettings;

        public class RecordEditedEventArgs:EventArgs
        {
            public string _record;

            public RecordEditedEventArgs(string record)
            {
                _record = record;
            }
        }

        public SolidSettings Settings
        {
            set
            {
                _solidSettings = value; 
            }
        }

        public void UpdateCurrentRecord(Record record, string update)
        {
            // Remove the inferred markers from the text
            // Encode the value correctly as per the solid marker settings (either utf-8 or iso-8859-1)
            SfmRecordReader reader = new SfmRecordReader(new StringReader(update));
            reader.AllowLeadingWhiteSpace = true;
            if (reader.Read())
            {
                SfmRecord sfmRecord = reader.Record;
                sfmRecord.RemoveAll(
                    delegate(SfmField rhs)
                    {
                        return rhs.key.StartsWith("+");
                    }
                );
                StringBuilder sb = new StringBuilder();
                foreach (SfmField field in sfmRecord)
                {
                    field.value = ValueToMdf(field.key, field.value);
                    sb.Append("\\");
                    sb.Append(field.key);
                    sb.Append(" ");
                    sb.Append(field.value);
                    sb.Append("\n");
                }
                record.SetRecord(sb.ToString(), _solidSettings);
            }
            else
            {
                throw new Exception("Current record is not readable sfm");
            }
        }

        public Font FontForMarker(string marker)
        {
            Palaso.WritingSystems.LdmlInFolderWritingSystemRepository repository =
                new LdmlInFolderWritingSystemRepository();
            string writingSystemId = _solidSettings.FindMarkerSetting(marker).WritingSystem;

            if (!string.IsNullOrEmpty(writingSystemId))
            {
                Palaso.WritingSystems.WritingSystemDefinition definition = repository.LoadDefinition(writingSystemId);
                if (null != definition)
                {
                    return new Font(definition.DefaultFontName, 12);
                }
            }
            if (FontIsInstalled("Doulos SIL"))
            {
                return new Font("Doulos SIL", 12);
            }

            return new Font(FontFamily.GenericSansSerif, 12);
        }

        public string ValueToUnicode(string marker, string value)
        {
            string retval;
            SolidMarkerSetting setting =  _solidSettings.FindMarkerSetting(marker);
            if (setting != null && setting.Unicode)
            {
                retval = string.Empty;
                if (value.Length > 0)
                {
                    Encoding byteEncoding = Encoding.GetEncoding("iso-8859-1");
                    //Encoding byteEncoding = Encoding.Unicode;
                    byte[] valueAsBytes = byteEncoding.GetBytes(value);
                    Encoding stringEncoding = Encoding.UTF8;
                    retval = stringEncoding.GetString(valueAsBytes);
                    if (retval.Length == 0)
                    {
                        retval = "Non Unicode Data Found";
                    }
                }
            }
            else
            {
                retval = value;
            }
            return retval;
        }

        public string ValueToMdf(string marker, string value)
        {
            string retval;
            SolidMarkerSetting setting =  _solidSettings.FindMarkerSetting(marker);
            if (setting != null && setting.Unicode)
            {
                Encoding stringEncoding = Encoding.UTF8;
                byte[] valueAsBytes = stringEncoding.GetBytes(value);
                Encoding byteEncoding = Encoding.GetEncoding("iso-8859-1");
                retval = byteEncoding.GetString(valueAsBytes);
            }
            else
            {
                retval = value;
            }
            return retval;
        }

        private bool FontIsInstalled(string name)
        {
            foreach (FontFamily family in System.Drawing.FontFamily.Families)
            {
                if (family.Name == name)
                    return true;
            }
            return false;
        }

    }
}
