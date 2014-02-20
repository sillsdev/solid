// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT



namespace SolidGui.Engine
{
    public class SolidStructureProperty
    {
        private string _parent;

        public SolidStructureProperty()
        {
            Parent = "";
            Multiplicity = MultiplicityAdjacency.Once; // was .MultipleApart; but it's better to promote best practices -JMC Feb 2014
        }

        public SolidStructureProperty(string parent,MultiplicityAdjacency ma)
        {
            Parent = parent;
            Multiplicity = ma;
        }

        public SolidStructureProperty(string parent)
        {
            Parent = parent;
            Multiplicity = MultiplicityAdjacency.MultipleApart;
        }

        public override string ToString()
        {
            return Parent;
        }

        public string Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        public MultiplicityAdjacency Multiplicity { get; set; }
    }
}