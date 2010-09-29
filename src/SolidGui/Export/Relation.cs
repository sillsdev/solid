namespace SolidGui.Export
{
    public class Relation
    {
        private string _targetID;
        private string _type;

        public Relation(string targetID, string type)
        {
            _targetID = targetID;
            _type = type;
        }

        public string Type
        {
            get { return _type; }
            set { _type = Type; }
        }

        public string TargetID
        {
            get { return _targetID; }
            set { _targetID = TargetID; }
        }
        

        
    }
}