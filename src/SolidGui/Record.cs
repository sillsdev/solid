using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class Record
    {
        private string _value;

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
                _value = value;
            }
        }
    }
}
