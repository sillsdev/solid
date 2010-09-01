using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SolidGui.Engine;

namespace SolidGui.Model
{
    public class SfmLexEntry
    {
        private readonly List<SfmFieldModel> _fields;
        public int RecordId { get; private set; }

        public SfmLexEntry()
        {
            _fields = new List<SfmFieldModel>();
        }

        private SfmLexEntry(IEnumerable<SfmField> fields) :
            this()
        {
            foreach (var f in fields)
            {
                _fields.Add(new SfmFieldModel(f.key, f.value));
            }
        }

        public List<SfmFieldModel> Fields
        {
            get { return _fields; }
        }

        public SfmFieldModel FirstField
        {
            get { return _fields[0]; }
        }

        public string Name
        {
            // Return the lx data value
            get
            {
                if(_fields.Count>0)
                {
                    return _fields[0].Value;
                }
                return "";
            }
        }

        public static SfmLexEntry CreateFromReader(SfmRecordReader reader)
        {
            return new SfmLexEntry(reader.Fields);
        }

        public static SfmLexEntry CreateFromText(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentException("text cannot be null or empty");
            }

            var reader = SfmRecordReader.CreateFromText(text);
            reader.Read();

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

        public SfmFieldModel GetFirstFieldWithMarker(string marker)
        {
            return _fields.FirstOrDefault(f => f.Marker == marker);
        }

        public string GetField(int id)
        {
            SfmFieldModel field = _fields.Find(delegate(SfmFieldModel aField) { return aField.Id == id; });
            return field.ToString();
        }

        public bool IsMarkerNotEmpty(string marker)
        {
            return _fields.Find(
                       f => f.Marker == marker && f.Value != string.Empty
                       ) != null;
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
