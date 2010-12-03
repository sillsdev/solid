using System;
using System.Collections.Generic;
using SolidGui.Model;

namespace SolidGui.Filter
{
	public sealed class MarkerFilter : RecordFilter
	{
		private readonly string _marker;

		public MarkerFilter(RecordManager recordManager, string marker) :
			base(recordManager, String.Format("Marker {0}", marker))
		{
			_marker = marker;
			UpdateFilter();
		}

		public override void UpdateFilter()
		{
			_indexesOfRecords.Clear();
			for (int i = 0; i < _recordManager.Count; i++)
			{
				_recordManager.MoveTo(i);
				if (_recordManager.Current.IsMarkerNotEmpty(_marker))
				{
					_indexesOfRecords.Add(i);
				}
			}
		}

		public override IEnumerable<string> HighlightMarkers
		{
			get { return new[] { _marker }; }
		}
		public override string Description(int index)
		{
			return string.Format("Records containing {0}", _marker);
		}
    
	}
}