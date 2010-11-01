using System;
using System.Globalization;
using NUnit.Framework;
using Palaso.TestUtilities;

namespace SolidGui.Tests.Export
{
    [TestFixture]
    public class ExportLiftTests
    {

        [Test]
        public void Variant_aInSingleEntry_ExportsVariant()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\a VariantOfTestLexeme
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("a", "variant", "en");
                e.AssertExportsSingleInstance("/lift/entry/variant/form[text='VariantOfTestLexeme']");
            }
        }

        [Test]
        public void BorrowedWord_bwInSingleEntry_ExportsEtymology()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\bw Thai
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("bw", "borrowedWord", "th");
                e.AssertExportsSingleInstance("/lift/entry/etymology[@type='borrowed' and @source='Thai']");
            }
        }

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
                e.AssertExportsSingleInstance("/lift/entry/relation[@type='confer']"); // I (CP) don't (yet) know how to test the target guid in xpath 2010-11
            }
        }

        [Test]
        public void Custom_SingleEntry_ExportsField()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\zx Custom Field
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("zx", "custom", "en");
                e.AssertExportsSingleInstance("/lift/entry/field[@type='zx']/form[@lang='en' and text='Custom Field']");
            }
        }

        [Test]
        public void Definition_EnglishAndNational_ExportsTwoForms()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\sn 
\de Definition
\dn National definition
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("de", "definition", "en", "sn", false);
                e.SetupMarker("dn", "definition", "th", "sn", false);
                e.Export();
                e.AssertHasSingleInstance("/lift/entry/sense/definition/form[@lang='en' and text='Definition']");
                e.AssertHasSingleInstance("/lift/entry/sense/definition/form[@lang='th' and text='National definition']");
            }

        }

        [Test]
        public void DateModified_DDMMMYYYYFormat_Exports()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\dt 27/Nov/2007
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("dt", "dateModified", "en");
                DateTime dateValue;
                DateTime.TryParseExact(                    
                    "27/Nov/2007", "dd/MMM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue
                );
                string expectedTime = dateValue.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ");
                e.AssertExportsSingleInstance(String.Format("/lift/entry[@dateCreated='{0}' and @dateModified='{0}']", expectedTime));
            }

        }

        [Test]
        public void DateModified_NoLeadingZero_Exports()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\dt 4/Nov/2007
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("dt", "dateModified", "en");
                DateTime dateValue;
                DateTime.TryParseExact(
                    "04/Nov/2007", "dd/MMM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue
                    );
                string expectedTime = dateValue.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ");
                e.AssertExportsSingleInstance(String.Format("/lift/entry[@dateCreated='{0}' and @dateModified='{0}']",
                                                            expectedTime));
            }
        }

        [Test]
        public void DateModified_TwoDigitYear_Exports()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\dt 04/Nov/07
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("dt", "dateModified", "en");
                DateTime dateValue;
                DateTime.TryParseExact(
                    "04/Nov/2007", "dd/MMM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue
                );
                string expectedTime = dateValue.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ");
                e.AssertExportsSingleInstance(
                    String.Format("/lift/entry[@dateCreated='{0}' and @dateModified='{0}']", expectedTime)
                );
            }
        }

        [Test]
        public void NoteEncyclopedic_SingleEntry_ExportsNote()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\sn 
\en Encyclopedic note
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("en", "noteEncyclopedic", "en", "sn", false);
                e.AssertExportsSingleInstance("/lift/entry/sense/note[@type='encyclopedic']/form[@lang='en' and text='Encyclopedic note']");
            }
        }

        [Test]
        public void Etymology_SingleEntryGlossAndComment_ExportsEtymology()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\et Etymology data
\es Etymology Source
\eg English gloss
\ec A comment
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("et", "etymology", "en");
                e.SetupMarker("es", "etymologySource", "en", "et", false);
                e.SetupMarker("eg", "gloss", "en", "et", false);
                e.SetupMarker("ec", "comment", "en", "et", false);
                e.Export();
                e.AssertHasSingleInstance("/lift/entry/etymology[@type='proto' and @source='Etymology Source']/form[@lang='en' and text='Etymology data']");
                e.AssertHasSingleInstance("/lift/entry/etymology[@type='proto' and @source='Etymology Source']/gloss[@lang='en' and text='English gloss']");
                Assert.Fail("What about the comment? CP 2010-11");
            }
        }

        [Test]
        public void Gloss_SingleEntryOneForm_ExportsGloss()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\sn
\ge Test Gloss
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("ge", "gloss", "en", "sn", false);
                e.AssertExportsSingleInstance("/lift/entry/sense/gloss[@lang='en' and text='Test Gloss']");
            }
        }

        [Test]
        public void Gloss_SingleEntryTwoForm_ExportsBothGlosses()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\sn
\ge English gloss
\gn National gloss
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("sn", "sense", "en", "lx", false);
                e.SetupMarker("ge", "gloss", "en", "sn", false);
                e.SetupMarker("gn", "gloss", "th", "sn", false);
                e.Export();
                e.AssertHasSingleInstance("/lift/entry/sense/gloss[@lang='en' and text='English gloss']");
                e.AssertHasSingleInstance("/lift/entry/sense/gloss[@lang='th' and text='National gloss']");
            }
        }

        [Test]
        public void Homograph_TwoLexemes_TwoEntriesInExport()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\lx TestLexeme
";
                // Note: \hm isn't currently used in the Lift Export.  Homograph numbers are assumed to be calculated. CP 2010-11
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.Export();
                AssertThatXmlIn.String(e.LiftAsString()).HasSpecifiedNumberOfMatchesForXpath(
                    "/lift/entry/lexical-unit/form[@lang='en' and text='TestLexeme']",
                    2
                );
            }
        }

        [Test]
        public void Citation_SingleEntry_ExportCitation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"
\lx TestLexeme
\lc Better Form
";
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("lc", "citation", "en");
                e.AssertExportsSingleInstance("/lift/entry/citation/form[@lang='en' and text='Better Form']");
            }
        }



    }
}
