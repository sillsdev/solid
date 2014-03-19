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
\lx    EntryTwo
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("cf", "confer", "en");

                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "EntryTwo");
                //"Compare" is the FLEx precedent for this relation name.
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/relation[@type='Compare' and @ref='{0}']", target), 1);

                e.AssertNoErrorWasReported();

            }
        }

 

        [Test] [Ignore("Important! Enable this test once Palaso fixes #1083")]
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
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/relation[@type='_component-lexeme' and @ref='{0}']", guid), 1);
                // JMC:! issue #1083: need to export complex forms more clearly (not as things FLEx sees as implicit variants)
                // This means adding two <trait> elements: is-primary and complex-form-type
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath("/lift/entry/relation/trait", 2);
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
                //note, FLEx uses "Synonyms", plural.
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Synonyms' and @ref='{0}']", target), 1);

                e.AssertNoErrorWasReported();
            }
        }

        [Test]
        public void LexicalFunctionWithMultiplesSeparatedByCommas_MakesCorrectRelations()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\lf ant
\lv awake, alert

\lx awake
\lx alert
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("lf", "lexicalRelationType", "en", "sn", true);
                e.SetupMarker("lv", "lexicalRelationLexeme", "en", "lf", false);
                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target1 = GetGuidOfLexeme(dom, "awake");
                //note, FLEx uses "Synonyms", plural.
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Antonym' and @ref='{0}']", target1),1);
                var target2 = GetGuidOfLexeme(dom, "alert");
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Antonym' and @ref='{0}']", target2), 1);

                e.AssertNoErrorWasReported();
            }
        }

        [Test]
        public void LexicalFunctionWithMatchingEntryInCitationForm_MakesCorrectRelation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\lf SYN
\lv tired

\lx tire
\lc tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("lf", "lexicalRelationType", "en", "sn", true);
                e.SetupMarker("lv", "lexicalRelationLexeme", "en", "lf", false);
                e.SetupMarker("lc", "citation", "en", "lx", false);
                e.Export();

                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());

                var target = GetGuidOfLexeme(dom, "tire"); //nb: the lx, not the lc
                //note, FLEx uses "Synonyms", plural.
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Synonyms' and @ref='{0}']", target), 1);

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
        public void RelationFromEntryLevel_OK()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx blue

\lx red
\lf PointToThisEntry
\lv blue";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("lf", "lexicalRelationType", "en", "lx", true);
                e.SetupMarker("lv", "lexicalRelationLexeme", "en", "lf", false);
                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "blue");
                e.AssertNoErrorWasReported();
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/relation[@type='PointToThisEntry' and @ref='{0}']", target), 1);
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
                //note, FLEx uses "Synonyms", plural.
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Synonyms' and @ref='{0}']", target), 1);

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
\lf Ant=tired

\lx tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("lf", "lexicalRelationType", "en", "sn", true);
                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "tired");
                //note, FLEx uses "Antonym", singular
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Antonym' and @ref='{0}']", target), 1);

                e.AssertNoErrorWasReported();
            }
        }


        [Test]
        public void SpecificMarkerAntonym_HasMatchingEntry_MakesCorrectRelation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\ant tired

\lx tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("ant", "antonym", "en", "sn", true);
                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "tired");
                //note, FLEx uses "Antonym", singular
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Antonym' and @ref='{0}']", target), 1);

                e.AssertNoErrorWasReported();
            }
        }

        [Test]
        public void SynonymSpecificMarker_HasMatchingEntry_MakesCorrectRelation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx sleepy
\sn
\th tired

\lx tired
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("th", "synonym", "en", "sn", true);
                e.Export();
                var dom = new XmlDocument();
                dom.LoadXml(e.LiftAsString());
                var target = GetGuidOfLexeme(dom, "tired");
                //note, FLEx uses "Synonyms", plural
                AssertThatXmlIn.Dom(dom).HasSpecifiedNumberOfMatchesForXpath(string.Format("/lift/entry/sense/relation[@type='Synonyms' and @ref='{0}']", target), 1);

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
                e.AssertWarningWasReported();
             }
        }




	}
}
