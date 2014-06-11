using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using SolidGui.Mapping;

namespace SolidGui.Tests.Mapping
{
	[TestFixture]
	public class MappingPMTests
	{
		[Test]
		public void ConceptToString_NullNode_NoThrow()
		{
			var model = new MappingPM.Concept(null);
			var result = model.Label();
			Assert.That(result, Is.EqualTo(""));
		}

		[Test]
		public void ConceptGetId_NullNode_NoThrow()
		{
			var model = new MappingPM.Concept(null);
			var result = model.GetId();
			Assert.That(result, Is.EqualTo(""));
		}

	}
}
