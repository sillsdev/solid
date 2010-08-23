using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Solid.Engine;
using SolidGui.Export;


namespace SolidEngineTests
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
        public void ExportSettingsFileCount_4()
        {
            Assert.AreEqual(4, _f.ExportSettings.Count);
        }

        [Test]
        public void ExportSetting0_LiftCorrect()
        {
            Assert.Greater(_f.ExportSettings.Count, 3);
            ExportHeader h = _f.ExportSettings[0];
            Assert.AreEqual("LIFT", h.Name);
            Assert.AreEqual("Xsl", h.Driver);
            Assert.AreEqual("LIFT (*.lift)|*.lift", h.FileNameFilter);
        }

        [Test]
        public void ExportSetting1_LiftAltCorrect()
        {
            Assert.Greater(_f.ExportSettings.Count, 3);
            ExportHeader h = _f.ExportSettings[1];
            Assert.AreEqual("LIFT from Alternate Hierarchy SFM", h.Name);
            Assert.AreEqual("Xsl", h.Driver);
            Assert.AreEqual("LIFT from Alternate Hierarchy SFM (*.lift)|*.lift", h.FileNameFilter);
        }

        [Test]
        public void ExportSetting2_StructureXMLCorrect()
        {
            Assert.Greater(_f.ExportSettings.Count, 3);
            ExportHeader h = _f.ExportSettings[2];
            Assert.AreEqual("Structured XML", h.Name);
            Assert.AreEqual("StructuredXml", h.Driver);
            Assert.AreEqual("Structured XML (*.xml)|*.xml", h.FileNameFilter);
        }

        [Test]
        public void ExportSetting3_FlatXMLCorrect()
        {
            Assert.Greater(_f.ExportSettings.Count, 3);
            ExportHeader h = _f.ExportSettings[3];
            Assert.AreEqual("Flat XML", h.Name);
            Assert.AreEqual("FlatXml", h.Driver);
            Assert.AreEqual("Flat XML (*.xml)|*.xml", h.FileNameFilter);
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