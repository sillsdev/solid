// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Text;
using SolidGui.Model;

namespace SolidGui
{
    // Wraps a RecordManager and is itself a RecordManager. -JMC
    public abstract class RecordManagerDecorator : RecordManager  // Decided this class could be declared abstract. -JMC
    {
        protected RecordManager _recordManager;

        public override Record GetRecord(int index)
        {
            return (_recordManager != null) ? _recordManager.GetRecord(index) : base.GetRecord(index);
        }

        protected RecordManagerDecorator(RecordManager d)
        {
            _recordManager = d;
        }

        public override int Count
        {
            get 
            {
                return (_recordManager != null) ? _recordManager.Count : base.Count;
            }
        }

        public override Record Current
        {
            get
            {   
                return (_recordManager != null) ? _recordManager.Current : base.Current;
            }
        }

        public override int CurrentIndex
        {
            get { return (_recordManager != null) ? _recordManager.CurrentIndex : base.CurrentIndex; }
            set { MoveTo(value); }
        }

        public abstract bool Remove();

        public override bool HasPrevious()
        {
            return (_recordManager != null) ? _recordManager.HasPrevious() : base.HasPrevious();
        }

        public override bool HasNext()
        {
            return (_recordManager != null) ? _recordManager.HasNext() : base.HasNext();
        }

        public override bool MoveToNext()
        {
            return (_recordManager != null) ? _recordManager.MoveToNext() : base.MoveToNext();
        }

        public override bool MoveToPrevious()
        {
            return (_recordManager != null) ? _recordManager.MoveToPrevious() : base.MoveToPrevious();
        }

        public override bool MoveToFirst()
        {
            return (_recordManager != null) ? _recordManager.MoveToFirst() : base.MoveToFirst();
        }

        public override bool MoveToLast()
        {
            return (_recordManager != null) ? _recordManager.MoveToLast() : base.MoveToLast();
        }

        public override bool MoveTo(int index)
        {
            return (_recordManager != null) ? _recordManager.MoveTo(index) : base.MoveTo(index);
        }

        public override bool MoveToByID(int id)
        {
            return (_recordManager != null) ? _recordManager.MoveToByID(id) : base.MoveToByID(id);
        }

        public override IEnumerator<Record> GetEnumerator()
        {
            return _recordManager.GetEnumerator();
        }


    }
}
