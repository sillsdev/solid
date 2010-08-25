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
        private string _marker;
        private string _value;
        private int _errorState;


        public SfmFieldModel(string marker)
        {
            Marker = marker;
            Inferred = false;
        }


        public SfmFieldModel(string markerNoSlash, string value, int depth, bool inferred, int id)
        {
            _marker = markerNoSlash;
            _value = value;
            Depth = depth;
            Inferred = inferred;
            _id = id;
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

        

        public SfmFieldModel AppendChild(SfmFieldModel node)
        {
            throw new NotImplementedException();
        }



        public string ToStructuredString()
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

     
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int Depth { get; private set; }


        
    
    }




}
