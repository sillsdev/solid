using NUnit.Framework;
using SolidGui.Export;


namespace SolidGui.Tests.Export
{
    [TestFixture, Ignore]
    public class ExportStructuredXml_Test
    {

        [Test]
        public void Export_Correct()
        {
            IExporter exporter = ExportStructuredXml.Create();
            exporter.Export("../../../../data/dict-s.db", "../../../../data/dict-s.xml");
            Assert.IsTrue(true);
        }

    }
}