using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidGui
{
    public class Record
    {
        class Field
        {
            int _id;
            string _marker;
            string _value;
            int _depth;
            int _errorState;
            bool _inferred;
        }

        private string _value;

        public static event EventHandler RecordTextChanged;


        public Record(string value)
        {
            _value = value;
        }

        public string GetField(int id)
        {
            return "";
        }

        public void SetField(int id, string value)
        {
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                string oldValue = _value;
                _value = value;

                if (oldValue != _value && RecordTextChanged != null)
                {
                        RecordTextChanged.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
