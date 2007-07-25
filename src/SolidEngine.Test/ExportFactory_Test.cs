using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class ExportFactory_Test
    {

        [Test]
        public void CreateExportStructuredXml_NotNull()
        {
            IExporter exporter = ExportFactory.Create(ExportFactory.ExportType.StructuredXml);
            Assert.IsNotNull(exporter);
        }

        [Test]
        public void CreateExportLift_NotNull()
        {
            IExporter exporter = ExportFactory.Create(ExportFactory.ExportType.Lift);
            Assert.IsNotNull(exporter);
        }

    }
}
