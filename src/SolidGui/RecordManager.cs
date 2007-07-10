using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SolidEngine;

namespace SolidGui
{
    public class RecordManager /*: IEnumerator<Record>, IEnumerable<Record> */
    {
        // Enumerator Methods
        public virtual void Reset()
        {
        }

        public virtual bool MoveNext()
        {
            return false;
        }

        public virtual IEnumerator < Record > GetEnumerator()
        {
            return null; // this; //!!! SHould return a dummy implementation.
        }

        public virtual Record Current
        {
            get
            {
                return null; // new Record(); //!!! Perhaps a default record would be better CJP
            }
        }

        public virtual int Count
        {
            get { return 0; }
        }

        public virtual int CurrentIndex
        {
            get { return 0; }
            set { MoveTo(value); }
        }

        public virtual bool HasPrevious()
        {
            return false;
        }

        public virtual bool HasNext()
        {
            return false;
        }

        public virtual bool MoveToNext()
        {
            return false;
        }

        public virtual bool MoveToPrevious()
        {
            return false;
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

        //public virtual void OnRecordProcess(XmlNode structure, SolidReport report)
        //{
        //}

    }
}
