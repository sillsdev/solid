using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class ExportLift_Test
    {

        [Test]
        public void Export_Correct()
        {
            IExporter exporter = ExportFactory.Create(ExportFactory.ExportType.Lift);
            exporter.Export("../../../../data/dict-s.db", "../../../../data/dict-s.lift");
            Assert.IsTrue(true);
        }

    }
}
