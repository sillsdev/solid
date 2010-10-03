using System.IO;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Export;


namespace SolidGui.Tests.Export
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
            var f = ExportFactory.Singleton();
            Assert.IsNotNull(f);
        }

        [Test]
        public void ExportSettingsCount_1()
        {
            Assert.AreEqual(1, _f.ExportSettings.Count);
        }

        [Test]
        public void ExportSetting0_LiftCorrect()
        {
            Assert.Greater(_f.ExportSettings.Count, 0);
            var h = _f.ExportSettings[0];
            Assert.AreEqual("LIFT", h.Name);
            Assert.AreEqual("Lift", h.Driver);
            Assert.AreEqual("LIFT (*.lift)|*.lift", h.FileNameFilter);
        }

        [Test]
        public void CreateFromFileFilter_NotNull()
        {
            Assert.Greater(_f.ExportSettings.Count, 0);
            var h = _f.ExportSettings[0];
            var exporter = _f.CreateFromFileFilter(h.FileNameFilter);
            Assert.IsNotNull(exporter);
        }

        [Test]
        public void CreateFromFileFilterFail_IsNull()
        {
            Assert.Greater(_f.ExportSettings.Count, 0);
            var h = _f.ExportSettings[0];
            var exporter = _f.CreateFromFileFilter("nodriver");
            Assert.IsNull(exporter);
        }

    }
}