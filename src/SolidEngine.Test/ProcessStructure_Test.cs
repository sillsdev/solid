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

        private void Init()
        {
            _settings = new SolidSettings();
            SolidMarkerSetting lxSetting = new SolidMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", SolidGui.MultiplicityAdjacency.Once));
            SolidMarkerSetting geSetting = new SolidMarkerSetting("ge");
            geSetting.StructureProperties.Add(new SolidStructureProperty("sn", SolidGui.MultiplicityAdjacency.MultipleApart));
            SolidMarkerSetting snSetting = new SolidMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", SolidGui.MultiplicityAdjacency.MultipleApart));
            SolidMarkerSetting bbSetting = new SolidMarkerSetting("bb");
            bbSetting.StructureProperties.Add(new SolidStructureProperty("lx",SolidGui.MultiplicityAdjacency.MultipleApart));

            _settings.MarkerSettings.Add(lxSetting);
            _settings.MarkerSettings.Add(snSetting);
            _settings.MarkerSettings.Add(geSetting);
            _settings.MarkerSettings.Add(bbSetting);

            _p = new ProcessStructure(_settings);
        }

        [Test]
        public void InfersNodeForEverySeperateChildWhenChildCanAppearUnderParentOnce()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">test1</lx><cc>fire</cc><sn></sn><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

            Init();
            SolidMarkerSetting ccSetting = new SolidMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", SolidGui.MultiplicityAdjacency.Once));
            _settings.MarkerSettings.Add(ccSetting);
            Assert.IsNotNull(_settings.FindMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeForEveryChildTogetherWhenChildCanAppearUnderParentOnce()
        {
            string xmlIn = "<entry record=\"5\"><lx field=\"1\">test2</lx><cc>fire</cc><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

            Init();
            SolidMarkerSetting ccSetting = new SolidMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", SolidGui.MultiplicityAdjacency.Once));
            _settings.MarkerSettings.Add(ccSetting);
            Assert.IsNotNull(_settings.FindMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeForEverySeperateChildWhenChildCanAppearUnderParentMultipleTogether()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">test1</lx><cc>fire</cc><sn></sn><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

            Init();
            SolidMarkerSetting ccSetting = new SolidMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", SolidGui.MultiplicityAdjacency.MultipleTogether));
            _settings.MarkerSettings.Add(ccSetting);
            Assert.IsNotNull(_settings.FindMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeOnceForEveryChildTogetherWhenChildCanAppearUnderParentMultipleTogether()
        {
            string xmlIn = "<entry record=\"5\"><lx field=\"1\">test2</lx><cc>fire</cc><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

            Init();
            SolidMarkerSetting ccSetting = new SolidMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", SolidGui.MultiplicityAdjacency.MultipleTogether));
            _settings.MarkerSettings.Add(ccSetting);
            Assert.IsNotNull(_settings.FindMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeOnceForAllChildrenSeperatedWhenChildCanAppearUnderParentMultipleApart()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">test1</lx><cc>fire</cc><sn></sn><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><sn><data /></sn><cc><data>foo</data></cc><sn><data /></sn><cc><data>bar</data></cc></bb></lx></entry>";

            Init();
            SolidMarkerSetting ccSetting = new SolidMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", SolidGui.MultiplicityAdjacency.MultipleApart));
            _settings.MarkerSettings.Add(ccSetting);
            SolidMarkerSetting snSetting = _settings.FindMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("bb",SolidGui.MultiplicityAdjacency.MultipleApart));
            Assert.IsNotNull(_settings.FindMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeOnceForAllChildrenTogetherAndSeperatedWhenChildCanAppearUnderParentMultipleApart()
        {
            string xmlIn = "<entry record=\"5\"><lx field=\"1\">test2</lx><cc>fire</cc><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><cc><data>foo</data></cc><sn><data /></sn><cc><data>bar</data></cc></bb></lx></entry>";

            Init();
            SolidMarkerSetting ccSetting = new SolidMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", SolidGui.MultiplicityAdjacency.MultipleApart));
            _settings.MarkerSettings.Add(ccSetting);
            SolidMarkerSetting snSetting = _settings.FindMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("bb", SolidGui.MultiplicityAdjacency.MultipleApart));
            Assert.IsNotNull(_settings.FindMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void ProcessStructure_InferNode_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><ge>g</ge></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn inferred=\"true\"><data /><ge><data>g</data></ge></sn></lx></entry>";

            Init();
            SolidMarkerSetting setting =  _settings.FindMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "sn";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
            
        }

        [Test]
        public void ProcessStructure_NoInferReqd_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><sn></sn><ge>g</ge></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn><data /><ge><data>g</data></ge></sn></lx></entry>";

            Init();
            SolidMarkerSetting setting = _settings.FindMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

        [Test]
        public void ProcessStructure_NoInferInsertAnyway_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><sn field=\"2\"></sn><ge field=\"3\">g</ge><zz field=\"4\">z</zz></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn field=\"2\"><data /><ge field=\"3\"><data>g</data><zz field=\"4\"><data>z</data></zz></ge></sn></lx></entry>";

            Init();
            SolidMarkerSetting setting = _settings.FindMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

        [Test]
        public void ProcessStructure_LiftMapping_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">b</lx></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\" lift=\"a\"><data>b</data></lx></entry>";

            Init();
            SolidMarkerSetting setting = _settings.FindMarkerSetting("lx");
            Assert.IsNotNull(setting);
            setting.Mapping[(int)SolidMarkerSetting.MappingType.Lift] = "a";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

        [Test]
        public void ProcessStructure_FlexMapping_Correct()
        {
            string xmlIn = "<entry record=\"5\"><lx field=\"1\">b</lx></entry>";
            string xmlEx = "<entry record=\"5\"><lx field=\"1\" flex=\"a\"><data>b</data></lx></entry>";

            Init();
            SolidMarkerSetting setting = _settings.FindMarkerSetting("lx");
            Assert.IsNotNull(setting);
            setting.Mapping[(int)SolidMarkerSetting.MappingType.Flex] = "a";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

    }
}
