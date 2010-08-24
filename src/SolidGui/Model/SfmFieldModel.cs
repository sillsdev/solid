using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidGui.Model
{
    public class SfmFieldModel
    {
        public SfmFieldModel(string marker)
        {
            Marker = marker;
            Inferred = false;
        }

        public IEnumerable ChildNodes;
        public int FieldId { get; private set; }
        public bool Inferred { get; set; }
        public string Field { get; set; }

        public string Marker { get; private set; }
        public string Mapping { get; set; }

        public SfmFieldModel NextSibling
        {
            get { throw new NotImplementedException(); }
        }

        public SfmFieldModel FirstChild
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public object Attributes
        {
            get { throw new NotImplementedException(); }
        }

        public string Value
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        public SfmFieldModel AppendChild(SfmFieldModel node)
        {
            throw new NotImplementedException();
        }
    }




}
