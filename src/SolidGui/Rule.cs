using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class Rule
    {
        private bool _required;
        private string _marker;
        private string _name;

        public Rule()
        {}

        public Rule(string name, string marker, bool required)
        {
            Name = name;
            Marker = marker;
            Required = required;
        }

        public Rule(string marker)
        {
            Name = "";
            Marker = marker;
            Required = false;
        }

        public bool Required
        {
            get
            {
                return _required;
            }
            set
            {
                _required = value;
            }
        }

        public string Marker
        {
            get
            {
                return _marker;
            }
            set
            {
                _marker = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}
