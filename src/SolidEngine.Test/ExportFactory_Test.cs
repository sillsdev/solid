using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class ExportFactory_Test
    {
        ExportFactory _f;

        [TestFixtureSetUp]
        public void Init()
        {
            _f = ExportFactory.Singleton();
            _f.Path = Path.Combine(EngineEnvironment.PathOfBase, "exporters");
        }

        [Test]
        public void Singleton_NotNull()
        {
            ExportFactory f = ExportFactory.Singleton();
            Assert.IsNotNull(f);
        }

        [Test]
        public void ExportSettings_Has2Files()
        {
            Assert.AreEqual(3, _f.ExportSettings.Count);
        }

        [Test]
        public void ExportStructuredXmlSetting_Correct()
        {
            Assert.Greater(_f.ExportSettings.Count, 0);
            ExportHeader h = _f.ExportSettings[2];
            Assert.AreEqual("Structured XML", h.Name);
            Assert.AreEqual("StructuredXml", h.Driver);
            Assert.AreEqual("Structured XML (*.xml)|*.xml", h.FileNameFilter);
        }

        [Test]
        public void CreateFromFileFilter_NotNull()
        {
            Assert.Greater(_f.ExportSettings.Count, 0);
            ExportHeader h = _f.ExportSettings[0];
            IExporter exporter = _f.CreateFromFileFilter(h.FileNameFilter);
            Assert.IsNotNull(exporter);
        }

        [Test]
        public void CreateFromFileFilterFail_IsNull()
        {
            Assert.Greater(_f.ExportSettings.Count, 0);
            ExportHeader h = _f.ExportSettings[0];
            IExporter exporter = _f.CreateFromFileFilter("nodriver");
            Assert.IsNull(exporter);
        }

    }
}
