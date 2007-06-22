using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class Record
    {
        private string _value;

        public static event EventHandler RecordTextChanged;


        public Record(string value)
        {
            _value = value;
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
