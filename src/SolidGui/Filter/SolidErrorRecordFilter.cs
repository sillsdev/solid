﻿using System.Collections.Generic;
using SolidGui.Engine;
using SolidGui.Filter;

namespace SolidGui.Model
{
	public class SolidErrorRecordFilter : RecordFilter
	{
		private readonly string _marker;
		SolidReport.EntryType _errorType;

		readonly List<string> _errorMessages = new List<string>();

		public SolidErrorRecordFilter(SfmDictionary d, string marker,SolidReport.EntryType errorType, string name) :
			base(d, name)
		{
			_marker = marker;
			_errorType = errorType;
			_name = name;
		}


		public void AddEntry(int sfmLexEntryIndex)
		{
			if (!_indexesOfRecords.Contains(sfmLexEntryIndex))
			{
				_indexesOfRecords.Add(sfmLexEntryIndex);
			}
		}

		public override IEnumerable<string> HighlightMarkers
		{
			get { return new[] { _marker }; }
		}

		public override string Description(int index)
		{
			return _errorMessages[index];
		}


	}
}