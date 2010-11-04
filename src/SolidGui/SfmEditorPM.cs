using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

using Palaso.WritingSystems;
using SolidGui.Engine;
using SolidGui.Model;


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
            SfmRecordReader reader = SfmRecordReader.CreateFromText(newContents);
            reader.AllowLeadingWhiteSpace = true;
            if (reader.Read())
            {
                SfmRecord sfmRecord = reader.Record;
                RemoveInferredFields(sfmRecord);
                StringBuilder sb = new StringBuilder();
                foreach (SfmField field in sfmRecord)
                {
                    field.Value = GetLatin1ValueFromUnicode(field.Marker, field.Value);
                    sb.Append("\\");
                    sb.Append(field.Marker);
                    sb.Append(" ");
                    sb.Append(field.Value);
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
            sfmRecord.RemoveAll(rhs => rhs.Marker.StartsWith("+"));
        }

        public Font FontForMarker(string marker)
        {
            var repository = new LdmlInFolderWritingSystemStore();
            string writingSystemId = _solidSettings.FindOrCreateMarkerSetting(marker).WritingSystemRfc4646;

            // Get the default font information from the writing system.
            if (!String.IsNullOrEmpty(writingSystemId))
            {
                Palaso.WritingSystems.WritingSystemDefinition definition = repository.LoadDefinition(writingSystemId);
                if (null != definition)
                {
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
            foreach (FontFamily family in FontFamily.Families)
            {
                if (family.Name == name)
                    return true;
            }
            return false;
        }

    }
}
