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

        private void Init()
        {
            _settings = new SolidSettings();
            SolidMarkerSetting lxSetting = new SolidMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("root", SolidGui.MultiplicityAdjacency.Once));
            SolidMarkerSetting geSetting = new SolidMarkerSetting("ge");
            geSetting.StructureProperties.Add(new SolidStructureProperty("sn", SolidGui.MultiplicityAdjacency.MultipleApart));
            SolidMarkerSetting snSetting = new SolidMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", SolidGui.MultiplicityAdjacency.MultipleApart));

            _settings.MarkerSettings.Add(lxSetting);
            _settings.MarkerSettings.Add(snSetting);
            _settings.MarkerSettings.Add(geSetting);

            _p = new ProcessStructure(_settings);
        }

        [Test]
        public void ProcessStructure_InferNode_Correct()
        {
            string xmlIn = @"<entry><lx>a</lx><ge>g</ge></entry>";
            string xmlEx = "<root><lx><data>a</data><sn inferred=\"true\"><data /><ge><data>g</data></ge></sn></lx></root>";

            Init();
            SolidMarkerSetting setting =  _settings.FindMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "sn";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            _p.Process(entry.DocumentElement);
            string xmlOut = _p.Document.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
            
        }

        [Test]
        public void ProcessStructure_NoInferReqd_Correct()
        {
            string xmlIn = @"<entry><lx>a</lx><sn></sn><ge>g</ge></entry>";
            string xmlEx = @"<root><lx><data>a</data><sn><data /><ge><data>g</data></ge></sn></lx></root>";

            Init();
            SolidMarkerSetting setting = _settings.FindMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            _p.Process(entry.DocumentElement);
            string xmlOut = _p.Document.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

        [Test]
        public void ProcessStructure_LiftMapping_Correct()
        {
            string xmlIn = @"<entry><lx>b</lx></entry>";
            string xmlEx = "<root><lx lift=\"a\"><data>b</data></lx></root>";

            Init();
            SolidMarkerSetting setting = _settings.FindMarkerSetting("lx");
            Assert.IsNotNull(setting);
            setting.Mapping[(int)SolidMarkerSetting.MappingType.Lift] = "a";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            _p.Process(entry.DocumentElement);
            string xmlOut = _p.Document.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

        [Test]
        public void ProcessStructure_FlexMapping_Correct()
        {
            string xmlIn = @"<entry><lx>b</lx></entry>";
            string xmlEx = "<root><lx flex=\"a\"><data>b</data></lx></root>";

            Init();
            SolidMarkerSetting setting = _settings.FindMarkerSetting("lx");
            Assert.IsNotNull(setting);
            setting.Mapping[(int)SolidMarkerSetting.MappingType.Flex] = "a";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            _p.Process(entry.DocumentElement);
            string xmlOut = _p.Document.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

    }
}
