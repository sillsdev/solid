using System;
using System.Globalization;
using System.Xml;
using NUnit.Framework;
using Palaso.TestUtilities;

namespace SolidGui.Tests.Export
{
    [TestFixture]
    public class ExportLift_RelationTests
    {
        [Test]
        public void Confer_cfInFirstEntry_ExportsRelationInFirstEntry()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx EntryOne
\cf EntryTwo
\lx EntryTwo
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("cf", "confer", "en");

                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "EntryTwo");
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/relation[@type='confer' and @ref='{0}']", target), 1);

                e.AssertNoErrorWasReported();

            }
        }

 

        [Test]
        public void SubEntry_MakesTwoLiftEntriesWithSubPointedAtBase()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx tired
\se dog tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("se", "subentry", "en");
                e.AssertExportsSingleInstance("/lift/entry/lexical-unit/form[@lang='en' and text='tired']");
                 e.AssertExportsSingleInstance("/lift/entry/lexical-unit/form[@lang='en' and text='dog tired']");
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var guid = GetGuidOfLexeme(dom, "tired");
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/relation[@type='BaseForm' and @ref='{0}']",guid), 1);
            }
        }

        private string GetGuidOfLexeme(XmlDocument dom, string lexicalForm)
        {
            var n = dom.SelectSingleNode(string.Format("/lift/entry[lexical-unit/form[text='{0}']]", lexicalForm));
            return n.Attributes["guid"].Value;
        }


        [Test]
        public void LexicalFunctionWithMatchingEntry_MakesCorrectRelation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\lf SYN
\lv tired

\lx tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("lf", "lexicalRelationType", "en", "sn", true);
                e.SetupMarker("lv", "lexicalRelationLexeme", "en", "lf", false);
                e.Export(); 
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "tired");
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='SYN' and @ref='{0}']", target), 1);

                e.AssertNoErrorWasReported();
            }
        }


        [Test]
        public void LexicalTargetWithoutPreceedingFunction_ErrorOutput()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\ps v
\lv tired

\lx tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("ps", "grammi", "en", "sn", true);
                e.SetupMarker("lv", "lexicalRelationLexeme", "en", "lf", false);
                e.Export();
                e.AssertErrorWasReported();
            }
        }

        [Test]
        public void LexicalFunctionWithoutTarget_ErrorOutput()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\ps v
\lf antonym

\lx awake
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("lf", "lexicalRelationType", "en", "sn", true);
                e.Export();
                e.AssertErrorWasReported();
            }
        }
        [Test]
        public void SynonymWithMatchingEntry_MakesCorrectRelation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\syn tired

\lx tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("syn", "synonym", "en", "sn", true);
                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "tired");
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='synonym' and @ref='{0}']", target), 1);

                e.AssertNoErrorWasReported();
            }
        }

        [Test]
        public void NewStyleLexicalFunction_HasMatchingEntry_MakesCorrectRelation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\lf Syn=tired

\lx tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("lf", "lexicalRelationType", "en", "sn", true);
                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "tired");
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Syn' and @ref='{0}']", target), 1);

                e.AssertNoErrorWasReported();
            }
        }

        [Test]
        public void LexicalFunctionWithNoMatchingEntry_OutputsError()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\lf SYN
\lv notThere
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("lf", "lexicalRelationType", "en");
                e.SetupMarker("lv", "lexicalRelationLexeme", "en");
                e.Export();
                e.AssertErrorWasReported();
             }
        }




	}
}
