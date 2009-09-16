using NUnit.Framework;
using Solid.Engine;

namespace Solid.EngineTests {
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