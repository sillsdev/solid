using SolidGui;

namespace SolidConsole
{
    public class SolidStructureProperty
    {
        private string _parent;
        private MultiplicityAdjacency _multipleAdjacent;

        public SolidStructureProperty()
        {
            Parent = "";
            _multipleAdjacent = MultiplicityAdjacency.Once;
        }

        public SolidStructureProperty(string parent,MultiplicityAdjacency ma)
        {
            Parent = parent;
            _multipleAdjacent = ma;
        }

        public SolidStructureProperty(string parent)
        {
            Parent = parent;
            _multipleAdjacent = MultiplicityAdjacency.Once;
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

        public MultiplicityAdjacency MultipleAdjacent
        {
            get
            {
                return _multipleAdjacent;
            }
            set
            {
                _multipleAdjacent = value;
            }
        }
    }
}
