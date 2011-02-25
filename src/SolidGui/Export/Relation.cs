namespace SolidGui.Export
{
    public class Relation
    {
        private string _targetForm;
        private string _type;

        public Relation(string targetForm, string type)
        {
            _targetForm = targetForm;
            _type = type;
        }

        /// <summary>
        /// Synonym, Antonym, BaseForm, Confer, etc.
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = Type; }
        }

        /// <summary>
        /// When we're reading relations in SFM, we just have a word to target, not a real ID.
        /// During export, this ID is looked up and exported as a GUID to the actual entry.
        /// </summary>
        public string TargetForm
        {
            get { return _targetForm; }
            set { _targetForm = TargetForm; }
        }
        

        
    }
}