using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidGui.Model
{
    public class SfmLexEntry
    {
        public int RecordId { get; private set; }

        public SfmLexEntry()
        {
            
        }

        public SfmFieldModel FirstChild
        {
            get { throw new NotImplementedException(); }
        }

        public SfmLexEntry DocumentElement
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public SfmFieldModel ImportNode(SfmFieldModel source, bool b)
        {
            throw new NotImplementedException();
        }

        public XmlAttribute CreateAttribute(string s)
        {
            throw new NotImplementedException();
        }

        public SfmFieldModel CreateElement(string s)
        {
            throw new NotImplementedException();
        }

        public static SfmLexEntry CreateFromText(string text)
        {
            throw new NotImplementedException();
        }
    }

}
