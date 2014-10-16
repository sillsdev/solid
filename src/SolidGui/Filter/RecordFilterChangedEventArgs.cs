// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using SolidGui.Filter;

namespace SolidGui
{
    public class RecordFilterChangedEventArgs : System.EventArgs 
    {
        public RecordFilter RecordFilter;  //TODO: Hmm... rename? -JMC

        public RecordFilterChangedEventArgs(RecordFilter recordFilter)
        {
            RecordFilter = recordFilter;
        }
    }
}