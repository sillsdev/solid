using System;
using System.Collections.Generic;
using System.Linq;
using Palaso.Code;
using SolidGui.Engine;
using SolidGui.Mapping;

namespace SolidGui.Model
{
    public class SfmLexEntry
    {
        private readonly List<SfmFieldModel> _fields;
        public int RecordId { get; private set; }
        private static int _recordID = 1;

        public SfmLexEntry()
        {
            _fields = new List<SfmFieldModel>();
            RecordId = _recordID++;
        }

        private SfmLexEntry(IEnumerable<SfmField> fields) :
            this()
        {
            foreach (var f in fields)
            {
                _fields.Add(new SfmFieldModel(f.Marker, f.Value, f.Trailing, 0, false));
            }
        }

        public List<SfmFieldModel> Fields
        {
            get { return _fields; }
        }

        public SfmFieldModel FirstField
        {
            get
            {
                if (_fields != null && _fields.Count > 0)
                {
                    return _fields[0];
                }
                return null;
            }
        }

        public string GetLexemeForm(SolidSettings solidSettings)
        {
            // Assume that the lx is first, it always will be.
            Guard.Against(_fields.Count == 0, "No fields in this SfmLexEntry");
            return _fields[0].DecodedValue(solidSettings).Trim();
        }

        /// <summary>
        /// Citation form or if it doesn't exist, a Lexeme Form
        /// </summary>
        /// <param name="solidSettings"></param>
        /// <returns></returns>
        public string GetHeadWord(SolidSettings solidSettings)
        {
             Guard.Against(_fields.Count == 0, "No fields in this SfmLexEntry");
            var citationFormSetting = 
                solidSettings.MarkerSettings.Find(s => "citation" == s.GetMappingConceptId(SolidMarkerSetting.MappingType.Lift));
            if (citationFormSetting == null)
                return GetLexemeForm(solidSettings);

            var citationField = _fields.Find(f => f.Marker == citationFormSetting.Marker);

            if(citationField == null)
                return GetLexemeForm(solidSettings);

            return citationField.DecodedValue(solidSettings).Trim();
        }

        // JMC:? Temporarily avoiding all build warnings; don't forget to uncomment the following later
        // [Obsolete("This method does not decode the value, use GetName(SolidSettings) instead")]
        public string Name
        {
            get
            {
                // Assume that the lx is first, it always will be.
                Guard.Against(_fields.Count == 0, "No fields in this SfmLexEntry");
                return _fields[0].Value;
            }
        }

        public static SfmLexEntry CreateFromReaderFields(IEnumerable<SfmField> fields)
        {
            return new SfmLexEntry(fields);
        }

        public static SfmLexEntry CreateFromText(string text)
        {

            if (String.IsNullOrEmpty(text))
            {
                // throw new ArgumentException("text cannot be null or empty");
                text = "";
            }

            var reader = SfmRecordReader.CreateFromText(text);
            reader.ReadRecord(); 

            var entry = new SfmLexEntry(reader.Fields);

            return entry;
        }

        public SfmFieldModel this[int i]
        {
            get { return _fields[i]; }
        }

        public void MoveField(SfmFieldModel field, int after)
        {
            int from = _fields.IndexOf(field);
            _fields.RemoveAt(from);
            if (from > after)
            {
                _fields.Insert(after + 1, field);
            }
            else
            {
                _fields.Insert(after, field);
            }
        }

        public void RemoveField(int index)
        {
            if (index == 0)
                throw new ApplicationException("Cannot remove the first field, which is the record marker");

            if (index < 0 || index >= _fields.Count)
                throw new ArgumentOutOfRangeException(string.Format("RemoveField({0}) was asked to remove an index which is out of range", index));

            _fields.RemoveAt(index);
        }

        public void InsertFieldAt(SfmFieldModel field, int indexForThisField)
        {
            _fields.Insert(indexForThisField, field);
        }

        public SfmFieldModel GetFirstFieldWithMarker(string marker)  // JMC: can we also auto-scroll the UI to this marker? (#284)
        {
            return _fields.FirstOrDefault(f => f.Marker == marker);
        }

        public string GetField(int id)
        {
            SfmFieldModel field = _fields.Find(aField => aField.Id == id);
            return field.ToString();
        }

        public bool IsMarkerNotEmpty(string marker)
        {
            return _fields.Find( f => f.Marker == marker && f.Value != string.Empty ) != null;
        }

        public bool HasMarker(string marker)
        {
            return _fields.Find(
                       f => f.Marker == marker
                       ) != null;
        }

        public void AppendField(SfmFieldModel field)
        {
            _fields.Add(field);
        }

        public static SfmLexEntry CreateDefault(SfmFieldModel field)
        {
            var entry = new SfmLexEntry();
            entry.AppendField(field);
            return entry;
        }


    }

}
