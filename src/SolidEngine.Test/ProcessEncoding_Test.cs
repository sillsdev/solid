using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
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
            SolidMarkerSetting lxSetting = new SolidMarkerSetting("lx");
            lxSetting.Unicode = false;

            SolidMarkerSetting geSetting = new SolidMarkerSetting("ge");
            geSetting.Unicode = false;

            SolidMarkerSetting guSetting = new SolidMarkerSetting("gu");
            guSetting.Unicode = true;

            _settings.MarkerSettings.Add(lxSetting);
            _settings.MarkerSettings.Add(geSetting);
            _settings.MarkerSettings.Add(guSetting);

            _p = new ProcessEncoding(_settings);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AsciiDataAsNonUnicode_Correct()
        {
            string xmlIn = "<entry><lx>1</lx><ge>english</ge></entry>";
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(0, report.Count);
        }

        [Test]
        public void AsciiDataAsUnicode_Correct()
        {
            string xmlIn = "<entry><lx>1</lx><gu>english</gu></entry>";
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(0, report.Count);
        }

        [Test]
        public void UpperAsciiDataAsNonUnicode_ReportError()
        {
            string xmlIn = "<entry><lx>1</lx><ge>\xA9\xA9</ge></entry>";
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(1, report.Count);
            Assert.AreEqual("Marker \\ge may use a hacked font", report.Entries[0].Description);
        }

        [Test]
        public void UpperAsciiDataAsUnicode_Correct()
        {
            string xmlIn = "<entry><lx>1</lx><gu>\xC2\xA9\xC2\xA9</gu></entry>";
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(0, report.Count);
        }

        [Test]
        public void BadUnicode_ReportError()
        {
            string xmlIn = "<entry><lx>1</lx><gu>abc \xA9\xA9\xA9</gu></entry>";
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            _p.Process(entry.DocumentElement, report);

            Assert.AreEqual(1, report.Count);
            Assert.AreEqual("Marker \\gu contains bad unicode data", report.Entries[0].Description);
        }

    }
}
