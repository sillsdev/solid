using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class RecordManagerDecorator : RecordManager
    {
        protected RecordManager _d;

        protected RecordManagerDecorator(RecordManager d)
        {
            _d = d;
        }

        public override int Count
        {
            get { if(_d!=null)
                    return _d.Count;
                  return 0;
                }
        }

        public override Record Current
        {
            get
            {   if(_d != null)
                    return _d.Current;
                return null;
            }
        }

        public override bool MoveToFirst()
        {
            return _d.MoveToFirst();
        }

        public override bool MoveToLast()
        {
            return _d.MoveToLast();
        }

        public override bool MoveTo(int index)
        {
            return _d.MoveTo(index);
        }

        public override bool MoveToByID(int id)
        {
            return _d.MoveToByID(id);
        }

        public override IEnumerator<Record> GetEnumerator()
        {
            return _d.GetEnumerator();
        }


    }
}
