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
        private static int _fieldId = 0;
        

        public SfmFieldModel(string marker) :
            this(marker, "")
        {
        }

        public SfmFieldModel(string marker, string value) :
            this(marker, value, 0, false)
        {
        }

        public SfmFieldModel(string markerNoSlash, string value, int depth, bool inferred)
        {
            Marker = markerNoSlash;
            Value = value;
            Depth = depth;
            Inferred = inferred;
            _id = _fieldId++;
            _children = new List<SfmFieldModel>();
        }

        public List<SfmFieldModel> Children 
        {
            get { return _children;  }
        }

        private SfmFieldModel _parent;
        public SfmFieldModel Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null)
                {
                    throw new ArgumentException(String.Format("Parent already exists in {0}", Marker));
                }
                _parent = value;
            }
        }

        public int FieldId { get; private set; }
        public bool Inferred { get; set; }
        public string Field { get; set; }

        public string Marker { get; private set; }
        public string Mapping { get; set; }



        public void AppendChild(SfmFieldModel node)
        {
            node.Parent = this;
            _children.Add(node);
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


        public SfmFieldModel this[int i]
        {
            get { return _children[i]; }
        }

        public override string ToString()
        {
            if(String.IsNullOrEmpty(Value))
            {
                return Marker;
            }
            return Marker + " " + Value;
        }
    }




}
