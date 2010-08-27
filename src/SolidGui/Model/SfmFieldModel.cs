using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidGui.Model
{
    public class SfmFieldModel
    {
        private int _id;
        private int _errorState;
        private readonly List<SfmFieldModel> _children;

        public SfmFieldModel(string marker)
        {
            Marker = marker;
            Inferred = false;
            _children = new List<SfmFieldModel>();
        }


        public SfmFieldModel(string markerNoSlash, string value, int depth, bool inferred, int id)
        {
            Marker = markerNoSlash;
            Value = value;
            Depth = depth;
            Inferred = inferred;
            _id = id;
            _children = new List<SfmFieldModel>();
        }

        public IEnumerable<SfmFieldModel> Children 
        {
            get { return _children;  }
        }
        
        public SfmFieldModel Parent { get; set; }
        public int FieldId { get; private set; }
        public bool Inferred { get; set; }
        public string Field { get; set; }

        public string Marker { get; private set; }
        public string Mapping { get; set; }

        public SfmFieldModel NextSibling // TODO Remove this CP 2010-08
        {
            get { throw new NotImplementedException(); }
        }

        public SfmFieldModel FirstChild // TODO Remove this CP 2010-08
        {
            get { throw new NotImplementedException(); }
        }

        public object Attributes // TODO Remove this CP 2010-08
        {
            get { throw new NotImplementedException(); }
        }

        

        public SfmFieldModel AppendChild(SfmFieldModel node)
        {
            throw new NotImplementedException();
        }



        public string ToStructuredString() // TODO Move to UI Adapter CP 2010-08
        {
            int spacesInIndentation = 4;
                
            string indentation = new string(' ', Depth*spacesInIndentation);
                
            if(!Inferred)
                return indentation + "\\" + Marker + " " + Value;
            else
                return indentation + "\\+" + Marker + " " + Value;

        }
            
        public int ErrorState
        {
            get { return _errorState; }
            set { _errorState = value; }
        }

        public int Id
        {
            get { return _id; }
        }

        public string Value { get; set; }

        public int Depth { get; set; }


        private SfmFieldModel this[int i]
        {
            get { return _children[i]; }
        }
    }




}
