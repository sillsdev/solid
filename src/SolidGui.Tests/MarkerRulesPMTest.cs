using System.Collections.Generic;
using System.IO;
using System.Xml;
using SolidGui.Engine;
using NUnit.Framework;
using SIL.TestUtilities;

namespace SolidGui.Tests
{
    [TestFixture]
    public class MarkerRulesPMTest
    {
        private MarkerRulesPM _markerRulesModel;
        private string _folderPath;
        private string _xmlPath;

        [SetUp]
        public void Setup()
        {
            _folderPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_folderPath);
            _xmlPath = Path.Combine(_folderPath, "test.xml");

            _markerRulesModel = new MarkerRulesPM();
            _markerRulesModel.RulesXmlPath = _xmlPath;

            List<string> allMarkers = new List<string>();

            allMarkers.Add("lx");
            allMarkers.Add("nt");
            allMarkers.Add("ps");

            _markerRulesModel.AllMarkers = allMarkers;
            _markerRulesModel.SelectedMarker = "lx";

            _markerRulesModel.AddProperty("nt",MultiplicityAdjacency.Once);
            _markerRulesModel.AddProperty("ge",MultiplicityAdjacency.Once);
            _markerRulesModel.AddProperty("ps",MultiplicityAdjacency.Once);
            
        }

        [TearDown]
        public void TearDown()
        {
            TestUtilities.DeleteFolderThatMayBeInUse(_folderPath);
        }

       [Test]
        public void WriteRulestoXmlWillWriteXmlFile()
        {
            _markerRulesModel.WriteRulesToXml();
            string[] file = File.ReadAllLines(_xmlPath);
            System.Xml.XmlDocument doc = new XmlDocument();
            doc.Load(_xmlPath);
            XmlNodeList nodelist = doc.GetElementsByTagName("SolidStructureProperty");

            Assert.AreEqual(3, nodelist.Count );
        }
        [Test]
        public void GetRuleReturnsCorrectRule()
        {
            SolidStructureProperty structureProperty = _markerRulesModel.GetProperty("nt");
            Assert.AreEqual(structureProperty.Parent,"nt");
        }

        [Test]
        public void RuleNameDoesNotExistAssertsNamesExistance()
        {
            Assert.IsFalse(_markerRulesModel.PropertyDoesNotExist("nt"));
        }

        [Test]
        public void RuleNameDoesNotExistAssertsNamesNonExistance()
        {
            Assert.IsTrue(_markerRulesModel.PropertyDoesNotExist("nts"));
        }

        [Test]
        public void ReadRulesFromXmlWillGetTheListOfRules()
        {
            MarkerRulesPM model = new MarkerRulesPM();

            model.RulesXmlPath = _xmlPath;

            _markerRulesModel.WriteRulesToXml();
            model.ReadRulesFromXml();

            model.SelectedMarker = "lx";

            Assert.IsFalse(model.PropertyDoesNotExist("nt"));
        }
    }
}
