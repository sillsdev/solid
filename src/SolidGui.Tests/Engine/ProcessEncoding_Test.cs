using System.Xml;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Processes;


namespace SolidGui.Tests.Engine
{
    [TestFixture]
    public class ProcessEncodingTest
    {
        ProcessEncoding _p;
        SolidSettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = new SolidSettings();
            var lxSetting = _settings.FindOrCreateMarkerSetting("lx");
            lxSetting.Unicode = false;

            var geSetting = _settings.FindOrCreateMarkerSetting("ge");
            geSetting.Unicode = false;

            var guSetting = _settings.FindOrCreateMarkerSetting("gu");
            guSetting.Unicode = true;

            _p = new ProcessEncoding(_settings);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AsciiDataAsNonUnicode_Correct()
        {
            const string xmlIn = "<entry><lx>1</lx><ge>english</ge></entry>";
            var entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            var report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(0, report.Count);
        }

        [Test]
        public void AsciiDataAsUnicode_Correct()
        {
            const string xmlIn = "<entry><lx>1</lx><gu>english</gu></entry>";
            var entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            var report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(0, report.Count);
        }

        [Test]
        public void UpperAsciiDataAsNonUnicode_ReportError()
        {
            const string xmlIn = "<entry><lx>1</lx><ge>\xA9\xA9</ge></entry>";
            var entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            var report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(1, report.Count);
            Assert.AreEqual("Marker \\ge may use a hacked font", report.Entries[0].Description);
        }

        [Test]
        public void UpperAsciiDataAsUnicode_Correct()
        {
            const string xmlIn = "<entry><lx>1</lx><gu>\xC2\xA9\xC2\xA9</gu></entry>";
            var entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            var report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(0, report.Count);
        }

        [Test]
        public void BadUnicode_ReportError()
        {
            const string xmlIn = "<entry><lx>1</lx><gu>abc \xA9\xA9\xA9</gu></entry>";
            var entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            var report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(1, report.Count);
            Assert.AreEqual("Marker \\gu contains bad unicode data", report.Entries[0].Description);
        }

    }
}