using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

using Palaso.WritingSystems;

using SolidEngine;

namespace SolidGui
{
    public class SfmEditorPM
    {
        private SolidSettings _solidSettings;
    	private RecordNavigatorPM _navigatorModel;

    	public class RecordEditedEventArgs:EventArgs
        {
            public string _record;

            public RecordEditedEventArgs(string record)
            {
                _record = record;
            }
        }

		public SfmEditorPM(RecordNavigatorPM navigatorModel)
		{
			_navigatorModel = navigatorModel;
		}

		public SolidSettings SolidSettings
        {
            set
            {
                _solidSettings = value; 
            }
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
            SfmRecordReader reader = new SfmRecordReader(new StringReader(newContents));
            reader.AllowLeadingWhiteSpace = true;
            if (reader.Read())
            {
                SfmRecord sfmRecord = reader.Record;
                RemoveInferredFields(sfmRecord);
                StringBuilder sb = new StringBuilder();
                foreach (SfmField field in sfmRecord)
                {
                    field.value = GetLatin1ValueFromUnicode(field.key, field.value);
                    sb.Append("\\");
                    sb.Append(field.key);
                    sb.Append(" ");
                    sb.Append(field.value);
                    sb.Append("\n");
                }
                record.SetRecordContents(sb.ToString(), _solidSettings);
            }
            else
            {
                throw new Exception("Current record is not readable sfm");
            }
        }

        private static void RemoveInferredFields(List<SfmField> sfmRecord)
        {
            sfmRecord.RemoveAll(rhs => rhs.key.StartsWith("+"));
        }

        public Font FontForMarker(string marker)
        {
            LdmlInFolderWritingSystemStore repository =
                new LdmlInFolderWritingSystemStore();
            string writingSystemId = _solidSettings.FindOrCreateMarkerSetting(marker).WritingSystemRfc4646;

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

        public string GetUnicodeValueFromLatin1(string marker, string value)
        {
            string retval;
            SolidMarkerSetting setting =  _solidSettings.FindOrCreateMarkerSetting(marker);
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
						// TODO: Need to lock this field of the current record at this point.
						// The editor must *never* write back to the model (for this field)
                    }
                }
            }
            else
            {
                retval = value;
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
            foreach (FontFamily family in FontFamily.Families)
            {
                if (family.Name == name)
                    return true;
            }
            return false;
        }

    }
}
