using System;
using System.Globalization;
using System.Xml;
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

            //NB:, as of FLEx 7 beta 2, it ignores <variant> anyways.

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
        public void Variant_2FreeVariants()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx form
                            \va 1
                           \va 2";
                e.SetupMarker("va", "variant", "en");
                e.AssertExportsSingleInstance("/lift/entry/variant/form[text='1']");
                e.AssertExportsSingleInstance("/lift/entry/variant/form[text='2']");
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
		public void Custom_TwoEntry_ExportsTwoSiblings()
		{
			using (var e = new ExportTestScenario())
			{
				e.Input = @"
\lx TestLexeme
\sn
\zx Custom Field1
\zx Custom Field2
";
				e.SetupMarker("lx", "lexicalUnit", "en");
				e.SetupMarker("sn", "sense", "en", "lx", false);
				e.SetupMarker("zx", "custom", "en", "sn", false);
				e.AssertExportsSingleInstance("/lift/entry/sense/field[@type='zx']/form[@lang='en' and text='Custom Field1']");
				e.AssertExportsSingleInstance("/lift/entry/sense/field[@type='zx']/form[@lang='en' and text='Custom Field2']");
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
            	e.AssertExportsSingleInstance(String.Format("/lift/entry[@dateCreated='{0}' and @dateModified='{0}']", ExpectedTime("27/Nov/2007")));
            }

        }

    	private string ExpectedTime(string dateAsString)
    	{
    		DateTime dateValue;
    		DateTime.TryParseExact(                    
    			dateAsString, "dd/MMM/yyyy",
    			CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue
    			);
    		return dateValue.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ");
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
                e.AssertExportsSingleInstance(String.Format("/lift/entry[@dateCreated='{0}' and @dateModified='{0}']",
                                                            ExpectedTime("04/Nov/2007")));
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
                e.AssertExportsSingleInstance(
					String.Format("/lift/entry[@dateCreated='{0}' and @dateModified='{0}']", ExpectedTime("04/Nov/2007"))
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

        public void Pronunciation_ExportedAsPronunciation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx boat
                            \pr bot";
                e.SetupMarker("pr", "pronunciation", "en");
                e.AssertExportsSingleInstance("/lift/entry/pronunciation/form[text='bot']");
            }
        }
        [Test]
        public void GrammarNote_ExportedAsNoteWithGrammarType()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx boat
                            \sn
                            \ng about grammar";
                e.SetupMarker("ng", "noteGrammar", "en", "sn", true);
                e.AssertExportsSingleInstance("/lift/entry/sense/note[@type='grammar']/form[text='about grammar']");
            }
        }
        [Test]
        public void Note_ExportedAsNoteWithNoType()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx boat
                            \sn
                            \nt blah blah";
                e.SetupMarker("nt", "note", "en", "sn", true);
                e.AssertExportsSingleInstance("/lift/entry/sense/note[not(@type)]/form[text='blah blah']");
            }
        }
        [Test]
        public void Reversal_ExportedAsReversal()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx boat
                            \sn
                            \re ship";
                e.SetupMarker("re", "reversal", "en", "sn", true);
                e.AssertExportsSingleInstance("/lift/entry/sense/reversal/form[text='ship']");
            }
        }

        [Test]
        public void Etymology_2Etymologies()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx form
                            \et proto1
                            \eg g1
                            \et proto2
                            \eg g2";
                e.SetupMarker("et", "etymology", "en");
                e.SetupMarker("eg", "etymologyGloss", "en");
                e.AssertExportsSingleInstance("/lift/entry/etymology/form[text='proto1']");
                e.AssertExportsSingleInstance("/lift/entry/etymology/form[text='proto2']");
            }
        }

        [Test]
        public void Etymology_ProtoAndSourceAndGloss_ExportedToEtymologyElementWithTypeOfProto()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx form
                            \et proto
                            \eg gloss
                            \es English";
                e.SetupMarker("et", "etymology", "en");
                e.SetupMarker("eg", "etymologyGloss", "en");
                e.SetupMarker("es", "etymologySource", "en");
                e.AssertExportsSingleInstance("/lift/entry/etymology[@type='proto' and @source='English']");
                e.AssertExportsSingleInstance("/lift/entry/etymology/form[@lang='en' and text='proto']");
            }
        }

        [Test]
        public void Etymology_HadComment_Exports()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx form
                            \et proto
                            \ec comment";
                e.SetupMarker("et", "etymology", "en");
                e.SetupMarker("ec", "etymologyComment", "en");
                e.AssertExportsSingleInstance("/lift/entry/etymology/field[@type='comment']/form[@lang='en' and text='comment']");
            }
        }

		[Test] // http://projects.palaso.org/issues/520
		public void DateTime_NestedStructure_Exports()
		{
			using (var e = new ExportTestScenario())
			{
				e.Input = @"
\lx form
\sn
\rf 
\xe example
\dt 1/Nov/2010
";
				e.SetupMarker("sn", "sense", "en", "lx", false);
				e.SetupMarker("rf", "example", "en", "sn", false);
				e.SetupMarker("xe", "exampleSentence", "en", "rf", false);
				e.SetupMarker("dt", "dateModified", "en", "lx", false);
				e.AssertExportsSingleInstance(String.Format(
					"/lift/entry[@dateCreated='{0}' and @dateModified='{0}']",
				    ExpectedTime("01/Nov/2010")
				));
			}
		}

		[Test] // http://projects.palaso.org/issues/521
		public void SemanticDomain_WithOneDomain_ExportsTraitUnderSense()
		{
			using (var e = new ExportTestScenario())
			{
				e.Input = @"
\lx form
\sn
\sd 1 Universe
";
				e.SetupMarker("sn", "sense", "en", "lx", false);
				e.SetupMarker("sd", "semanticDomain", "en", "sn", false);
				e.AssertExportsSingleInstance("/lift/entry/sense/trait[@name='semantic-domain-ddp4' and @value='1 Universe']");
			}
		}

		[Test] // http://projects.palaso.org/issues/521
		public void SemanticDomain_WithTwoDomains_ExportsTwoTraitUnderSense()
		{
			using (var e = new ExportTestScenario())
			{
				e.Input = @"
\lx form
\sn
\sd 1 Universe
\sd 2 Sky
";
				e.SetupMarker("sn", "sense", "en", "lx", false);
				e.SetupMarker("sd", "semanticDomain", "en", "sn", false);
				e.AssertExportsSingleInstance("/lift/entry/sense/trait[@name='semantic-domain-ddp4' and @value='1 Universe']");
				e.AssertExportsSingleInstance("/lift/entry/sense/trait[@name='semantic-domain-ddp4' and @value='2 Sky']");
			}
		}

	}
}
