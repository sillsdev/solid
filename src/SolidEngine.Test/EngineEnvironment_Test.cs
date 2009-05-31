using System;
using System.Collections.Generic;
using System.Text;
using SolidEngine;
using NUnit.Framework;

namespace SolidEngineTests {
	[TestFixture]
	public class EngineEnvironment_Test
	{
		[Ignore]
		public void PathOfBase_Correct()
		{
			// This test is environment specific.
			Assert.IsTrue(EngineEnvironment.PathOfBase.EndsWith("trunk"));
		}
	}
}