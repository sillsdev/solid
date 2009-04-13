namespace SolidGui
{
    public class Field
    {
        private int _id;
        private string _marker;
        private string _value;
        private int _depth;
        private int _errorState;
        private bool _inferred;

        public Field(string markerNoSlash, string value, int depth, bool inferred, int id)
        {
            _marker = markerNoSlash;
            _value = value;
            _depth = depth;
            _inferred = inferred;
            _id = id;
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

        public string Marker
        {
            get { return _marker; }
        }


        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int Depth
        {
            get { return _depth; }
        }


        public bool Inferred
        {
            get { return _inferred; }
        }
    }
}