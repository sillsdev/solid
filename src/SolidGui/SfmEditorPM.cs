using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class SfmEditorPM
    {
        private string _currentRecord;
        public class RecordEditedEventArgs:EventArgs
        {
            public string _record;

            public RecordEditedEventArgs(string record)
            {
                _record = record;
            }
        }
    }
}
