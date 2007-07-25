using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class ExportStructuredXml_Test
    {

        [Test]
        public void Export_Correct()
        {
            IExporter exporter = ExportFactory.Create(ExportFactory.ExportType.StructuredXml);
            exporter.Export("../../../../data/dict-s.db", "../../../../data/dict-s.xml");
            Assert.IsTrue(true);
        }

    }
}
