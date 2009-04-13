using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class RecordManagerDecorator : RecordManager
    {
        protected RecordManager _d;

        public override Record GetRecord(int index)
        {
            return (_d != null) ? _d.GetRecord(index) : base.GetRecord(index);
        }

        protected RecordManagerDecorator(RecordManager d)
        {
            _d = d;
        }

        public override int Count
        {
            get 
            {
                return (_d != null) ? _d.Count : base.Count;
            }
        }

        public override Record Current
        {
            get
            {   
                return (_d != null) ? _d.Current : base.Current;
            }
        }

        public override int CurrentIndex
        {
            get { return (_d != null) ? _d.CurrentIndex : base.CurrentIndex; }
            set { MoveTo(value); }
        }

        public override bool HasPrevious()
        {
            return (_d != null) ? _d.HasPrevious() : base.HasPrevious();
        }

        public override bool HasNext()
        {
            return (_d != null) ? _d.HasNext() : base.HasNext();
        }

        public override bool MoveToNext()
        {
            return (_d != null) ? _d.MoveToNext() : base.MoveToNext();
        }

        public override bool MoveToPrevious()
        {
            return (_d != null) ? _d.MoveToPrevious() : base.MoveToPrevious();
        }

        public override bool MoveToFirst()
        {
            return (_d != null) ? _d.MoveToFirst() : base.MoveToFirst();
        }

        public override bool MoveToLast()
        {
            return (_d != null) ? _d.MoveToLast() : base.MoveToLast();
        }

        public override bool MoveTo(int index)
        {
            return (_d != null) ? _d.MoveTo(index) : base.MoveTo(index);
        }

        public override bool MoveToByID(int id)
        {
            return (_d != null) ? _d.MoveToByID(id) : base.MoveToByID(id);
        }

        public override IEnumerator<Record> GetEnumerator()
        {
            return _d.GetEnumerator();
        }


    }
}
