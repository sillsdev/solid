using System;
using System.Collections.Generic;
using System.Text;

namespace SolidConsole
{

    public class SolidRule
    {
        private bool _required;
        private string _marker;
        private string _name;

        public SolidRule()
        {
            Name = "";
            Marker = "";
            Required = false;
        }

        public SolidRule(string name, string marker, bool required)
        {
            Name = name;
            Marker = marker;
            Required = required;
        }

        public SolidRule(string marker)
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
