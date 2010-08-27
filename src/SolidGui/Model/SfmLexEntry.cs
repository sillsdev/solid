using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SolidGui.Engine;

namespace SolidGui.Model
{
    public class SfmLexEntry
    {
        public List<SfmFieldModel> _fields;
        public int RecordId { get; private set; }

        public SfmLexEntry()
        {
            _fields = new List<SfmFieldModel>();
        }

        // TODO pretty sure this will be needed. Suspect that Record is doing it for us at the moment and not delegating to us CP 2010-08
        //public IEnumerable<SfmFieldModel> Fields
        //{
        //    get { return _fields; }
        //}

        public SfmFieldModel FirstChild
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            // Return the lx data value // TODO CP 2010-08
            get { throw new NotImplementedException(); }
        }

        public static SfmLexEntry CreateFromText(string text)
        {
            throw new NotImplementedException();
        }

        private void SetRecordContents(string s, SolidSettings settings)
        {
            throw new NotImplementedException();
        }

        public string ToStructuredString()
        {
            throw new NotImplementedException();
        }

        public bool IsMarkerNotEmpty(string _marker)
        {
            throw new NotImplementedException();
        }

        public SfmFieldModel this[int i]
        {
            get { return _fields[i]; }
        }
    }

}
