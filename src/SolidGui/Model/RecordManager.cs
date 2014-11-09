// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System.Collections.Generic;

namespace SolidGui.Model
{
    // Decided this class could be declared abstract. -JMC (Jon Coombs) 
    // TODO: Maybe should be reduced further, into an interface? That would seem better than returning nulls. -JMC
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

        /// <summary>
        /// Indicates which line number should be scrolled to when first opening the current record. 
        /// Marker filters should simply return the first highlighted line's number.
        /// Error filters should return the first highlighted line that has this filter's error.
        /// </summary>
        /// <returns></returns>
        public virtual int CurrentInitialLine()
        {
            return 0;
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