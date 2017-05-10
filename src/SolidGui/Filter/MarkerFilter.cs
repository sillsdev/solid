// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System.Collections.Generic;
using SolidGui.Model;

namespace SolidGui.Filter
{
    public sealed class MarkerFilter : RecordFilter
    {
        public readonly string Marker;
        public const string MarkerLabel = "Marker ";

        public MarkerFilter(RecordManager recordManager, string marker) :
            base(recordManager, MarkerLabel + marker)
        {
            Marker = marker;
            UpdateFilter(); 
        }

        public override void UpdateFilter()
        {
            _indexesOfRecords.Clear();
            SfmFieldModel f;
            for (int i = 0; i < _recordManager.Count; i++)
            {
                _recordManager.MoveTo(i);
                f = _recordManager.Current.GetFirstFieldWithMarker(Marker);
                if (f != null) // fixed #1201 by checking for not null instead of not empty; i.e. include empty fields too. -JMC 2013-09
                // if (_recordManager.Current.IsMarkerNotEmpty(_marker))  
                {
                    _indexesOfRecords.Add(i);
                }
            }
        }

        public override IEnumerable<string> HighlightMarkers
        {
            get { return new[] { Marker }; }
        }
        public override string Description(int index)
        {
            return string.Format("Records containing {0}", Marker);
        }

        /// <summary>
        /// Which line number should be scrolled to when first opening the current record. 
        /// </summary>
        public override int CurrentInitialLine()
        {
            string m = Marker;
            int i = 0;
            foreach (SfmFieldModel f in Current.Fields)
            {
                if (f.Marker == Marker)
                    return i;
                i +=  f.Newlines();
            }
            return 0;
        }

    }
}