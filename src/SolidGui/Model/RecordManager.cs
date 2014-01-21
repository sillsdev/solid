// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System.Collections.Generic;

namespace SolidGui.Model
{
    // Decided this class could be declared abstract. -JMC (Jon Coombs) 
    // JMC:? Maybe could even be reduced to an interface? That would seem better than returning nulls. 
    public abstract class RecordManager
    {

        public virtual Record GetRecord(int index)
        {
            return null;
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