using NUnit.Framework;
using SolidGui.Engine;

namespace SolidGui.Tests.Engine
{
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