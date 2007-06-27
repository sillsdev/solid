using System.IO;
using System.Xml;
using NUnit.Framework;

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
            _markerRulesModel.AddRule("name","nt", true);
            _markerRulesModel.AddRule("name","ge", true);
            _markerRulesModel.AddRule("name","ps", false);
            _markerRulesModel.RulesXmlPath = _xmlPath;
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
            XmlNodeList nodelist = doc.GetElementsByTagName("Rule");

            Assert.AreEqual(3, nodelist.Count );
        }
        [Test]
        public void GetRuleReturnsCorrectRule()
        {
            Rule rule = _markerRulesModel.GetRule("nt");
            Assert.AreEqual(rule.Marker,"nt");
        }

        [Test]
        public void MarkerAlreadyHasRuleFindsRealMarker()
        {
            Assert.IsTrue(_markerRulesModel.MarkerAlreadyHasRule("nt"));
        }

        [Test]
        public void MarkerAllreadyHasRuleDoesntFindFakeMarker()
        {
            Assert.IsFalse(_markerRulesModel.MarkerAlreadyHasRule("soidjfsod"));
        }

        [Test]
        public void ReadRulesFromXmlWillGetTheListOfRules()
        {
            MarkerRulesPM model = new MarkerRulesPM();

            model.RulesXmlPath = _xmlPath;

            _markerRulesModel.WriteRulesToXml();
            model.ReadRulesFromXml();

            Assert.IsTrue(model.MarkerAlreadyHasRule("nt"));
        }
    }
}
