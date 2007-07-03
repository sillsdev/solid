using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class ProcessStructureTest
    {
        ProcessStructure _p;
        SolidSettings _settings;
        SolidReport _report;

        [TestFixtureSetUp]
        public void Init()
        {
            _settings = new SolidSettings();
            SolidMarkerSetting lxSetting = new SolidMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("root", SolidGui.MultiplicityAdjacency.Once));
            SolidMarkerSetting geSetting = new SolidMarkerSetting("ge");
            geSetting.StructureProperties.Add(new SolidStructureProperty("sn", SolidGui.MultiplicityAdjacency.MultipleApart));
            geSetting.InferedParent = "sn";
            SolidMarkerSetting snSetting = new SolidMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", SolidGui.MultiplicityAdjacency.MultipleApart));

            _settings.MarkerSettings.Add(lxSetting);
            _settings.MarkerSettings.Add(snSetting);
            _settings.MarkerSettings.Add(geSetting);

            _report = new SolidReport();
            
            _p = new ProcessStructure(_report, _settings);
        }

        [Test]
        public void ProcessStructure_InferNode_Correct()
        {
            string xmlIn = @"<entry><lx>a</lx><ge>g</ge></entry>";
            string xmlEx = @"<root><lx><data>a</data><sn><data /><ge><data>g</data></ge></sn></lx></root>";
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            XmlNode result = _p.Process(entry.DocumentElement);
            string xmlOut = result.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
            
        }


    }
}
