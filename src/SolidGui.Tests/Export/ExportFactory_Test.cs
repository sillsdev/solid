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

        [SetUp]
        public void Init()
        {
            _f = ExportFactory.Singleton();
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
            Assert.AreEqual(2, _f.ExportSettings.Count);
        }

        [Test]
        public void ExportSetting_FirstOneIsLift()
        {
            Assert.Greater(_f.ExportSettings.Count, 0);
            var h = _f.ExportSettings[0];
            Assert.AreEqual("LIFT", h.Name);
            Assert.AreEqual("Lift", h.Driver);
            Assert.AreEqual("LIFT (*.lift)|*.lift", h.FileNameFilter);
        }

		[Test]
		public void ExportSetting_SecondOneIsSummary()
		{
			Assert.Greater(_f.ExportSettings.Count, 1);
			var h = _f.ExportSettings[1];
			Assert.AreEqual("Field Summary", h.Name);
			Assert.AreEqual(ExportSummary.DriverName, h.Driver);
			Assert.AreEqual("SFM Field Summary (*.txt)|*.txt", h.FileNameFilter);
		}

	}
}