using System;
using System.Collections.Generic;
using System.Text;
using SolidEngine;
using NUnit.Framework;

namespace SolidTests
{
    [TestFixture]
    public class EngineEnvironment_Test
    {
        [Test]
        public void PathOfBase_Correct()
        {
            Assert.AreEqual("C:\\src\\sil\\solid\\trunk", EngineEnvironment.PathOfBase);
        }
    }
}
