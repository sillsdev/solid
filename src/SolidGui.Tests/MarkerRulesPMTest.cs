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
            _markerRulesModel.RulesXmlPath = _xmlPath;
            _markerRulesModel.AddRule("name1","nt", true);
            _markerRulesModel.AddRule("name2","ge", true);
            _markerRulesModel.AddRule("name3","ps", false);
            
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
            Rule rule = _markerRulesModel.GetRule("name1");
            Assert.AreEqual(rule.Name,"name1");
        }

        [Test]
        public void RuleNameDoesNotExistAssertsNamesExistance()
        {
            Assert.IsFalse(_markerRulesModel.RuleNameDoesNotExist("name1"));
        }

        [Test]
        public void RuleNameDoesNotExistAssertsNamesNonExistance()
        {
            Assert.IsTrue(_markerRulesModel.RuleNameDoesNotExist("nt"));
        }

        [Test]
        public void ReadRulesFromXmlWillGetTheListOfRules()
        {
            MarkerRulesPM model = new MarkerRulesPM();

            model.RulesXmlPath = _xmlPath;

            _markerRulesModel.WriteRulesToXml();
            model.ReadRulesFromXml();

            Assert.IsFalse(model.RuleNameDoesNotExist("name1"));
        }
    }
}
