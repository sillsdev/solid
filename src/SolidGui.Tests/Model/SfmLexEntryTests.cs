using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Tests.Model
{
	[TestFixture]
	public class SfmLexEntryTests
	{
		[Test]
		public void GetLexemeForm_Default_Throws()
		{
			var entry = new SfmLexEntry();
			var solidSettings = new SolidSettings();
			Assert.Throws<InvalidOperationException>(() => entry.GetLexemeForm(solidSettings));
		}

		[Test]
        public void GetLexemeForm_FromSFM_MatchesLxFieldValue()
		{
			const string sfmIn = @"\lx test1";

			SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);
			var solidSettings = new SolidSettings();

			Assert.AreEqual("test1", entry.GetLexemeForm(solidSettings));
		}

        [Test]
        public void GetHeadWord_HasCitationForm_GivesCitationForm()
        {
            const string sfmIn = @"
\lx foo
\lc foobar";

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);
            var solidSettings = new SolidSettings();
            SetupMarker(solidSettings, "lc", "citation", "en");
            Assert.AreEqual("foobar", entry.GetHeadWord(solidSettings));
        }


        public void SetupMarker(SolidSettings settings, string marker, string liftConcept, string writingSystem)
        {
            var setting = settings.FindOrCreateMarkerSetting(marker);
            setting.WritingSystemRfc4646 = writingSystem;
            setting.Mappings[1] = liftConcept;
        }

        [Test]
        public void GetHeadWord_NoCitationForm_GivesLexemeForm()
        {
            const string sfmIn = @"\lx foo";

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);
            var solidSettings = new SolidSettings();
            SetupMarker(solidSettings, "lc", "citation", "en");

            Assert.AreEqual("foo", entry.GetHeadWord(solidSettings));
        }

	    [Test]
        public void GetHeadWord_NoCitationFormMapping_GivesLexemeForm()
        {
            const string sfmIn = @"\lx foo
\lc DontGiveMe";

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

	        Assert.AreEqual("foo", entry.GetHeadWord(new SolidSettings()));
        }

		[Test]
		public void FirstField_WithValidLx_ReturnsFirstField()
		{
			const string sfmIn = @"\lx test1";
			SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);
			Assert.AreEqual("lx", entry.FirstField.Marker);
		}

		[Test]
		public void CreateFromText_ValidEntry()
		{
			const string sfmIn = @"
\lx a1
\cc b2";
			var entry = SfmLexEntry.CreateFromText(sfmIn);

			Assert.AreEqual("lx", entry[0].Marker);
			Assert.AreEqual("a1", entry[0].Value);

			Assert.AreEqual("cc", entry[1].Marker);
			Assert.AreEqual("b2", entry[1].Value);
		}

		[Test]
		public void CreateFromText_BiggishEntry_Valid()
		{
			const string sfmIn = @"
\lx test
\aa
\bb
\aa
\bb";
			//expecting:

			/*
             * \lx test
             * \aa
             * \bb
             * \aa
             * \bb
             */

			var entry = SfmLexEntry.CreateFromText(sfmIn);

			Assert.AreEqual("lx", entry[0].Marker);
			Assert.AreEqual("test", entry[0].Value);

			Assert.AreEqual("aa", entry[1].Marker);
			Assert.AreEqual("bb", entry[2].Marker);
			Assert.AreEqual("aa", entry[3].Marker);
			Assert.AreEqual("bb", entry[4].Marker);
		}

		[Test]
		public void CreateFromText_TwoFieldEntryWithOneEmptyValue_ReadsBothValues()
		{
			const string sfmIn = @"
\lx cc
\sn";
			var entry = SfmLexEntry.CreateFromText(sfmIn);

			Assert.AreEqual("lx", entry[0].Marker);
			Assert.AreEqual("cc", entry[0].Value);

			Assert.AreEqual("sn", entry[1].Marker);
			Assert.AreEqual("", entry[1].Value);
		}

		[Test]
		public void AppendField_NormalLexEntry_AppendSuccessfully()
		{
			const string sfmIn = @"
\lx test1
\sn sense1";
			var entry = SfmLexEntry.CreateFromText(sfmIn);
			var field = new SfmFieldModel(@"ge", @"gloss");

			entry.AppendField(field);

			Assert.AreEqual("ge", entry[2].Marker);
			Assert.AreEqual("gloss", entry[2].Value);
		}

		[Test]
		public void CreateDefault_CreateSuccessfully()
		{
			var field = new SfmFieldModel(@"lx");
			var entry = SfmLexEntry.CreateDefault(field);

			Assert.AreEqual("lx", entry[0].Marker);
		}
	}
}