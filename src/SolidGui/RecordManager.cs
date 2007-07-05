using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SolidEngine;

namespace SolidGui
{
    public class RecordManager
    {
        class SolidObserver : Solidifier.Observer
        {
            private RecordManager _o;

            public SolidObserver(RecordManager o)
            {
                _o = o;
            }

            public override void OnRecordProcess(XmlNode structure, SolidReport report)
            {
                _o.OnRecordProcess(structure, report);
            }
        }

        public virtual int Count
        {
            get { return 0; }
        }

        public virtual Record Current
        {
            get { return null; }
        }

        public virtual bool MoveToFirst()
        {
            return MoveTo(0);
        }

        public virtual bool MoveToLast()
        {
            bool retval = false;
            if (Count > 0)
            {
                retval = MoveTo(Count - 1);
            }
            return retval;
        }

        public virtual bool MoveTo(int index)
        {
            return false;
        }

        public virtual bool MoveToByID(int id)
        {
            return false;
        }

        public virtual void OnRecordProcess(XmlNode structure, SolidReport report)
        {
        }

    }
}
