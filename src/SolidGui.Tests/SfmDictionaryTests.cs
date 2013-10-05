using System;
using System.IO;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Filter;
using SolidGui.Model;
using Palaso.TestUtilities;

namespace SolidGui.Tests
{
    [TestFixture]
    public class SfmDictionaryTests
    {
		private class EnvironmentForTest : IDisposable
		{
			private readonly TemporaryFolder _folder;
			private readonly SolidSettings _defaultSolidSettings;

			public EnvironmentForTest()
			{
				Palaso.Reporting.ErrorReport.IsOkToInteractWithUser = false;
				_folder = new TemporaryFolder("SfmDictionaryTests");
				_defaultSolidSettings = new SolidSettings();
			}

			public SolidSettings SolidSettingsForTest
			{
				get { return _defaultSolidSettings; }
			}

			public string DictionaryFilePath
			{
				get { return _folder.Combine(new[] { "SfmFile.db" }); }
			}

			public string AlternateDictionaryFilePath
			{
				get { return _folder.Combine(new[] {"AlternateSfmFile.db"}); }
			}

			public string DictionaryPath
			{
				get { return _folder.Path; }
			}

			public void CreateDictionary(string sfm)
			{
				File.WriteAllText(DictionaryFilePath, sfm);
			}

			public SfmDictionary OpenDictionary()
			{
				var dictionary = new SfmDictionary();
				dictionary.Open(DictionaryFilePath, SolidSettingsForTest, new RecordFilterSet());
				return dictionary;
			}

			public static string SfmWithTwoRecords()
			{
				return @"_sh SomeHeader
\lx first lexeme
\ge first gloss
\lx second lexeme
\ge second gloss
";
			}


			public void Dispose()
			{
				_folder.Dispose();
			}
		}

        [Test]
        public void OpenDictionary_With4Markers_AllMarkersPresent()
        {
			using (var e = new EnvironmentForTest())
			{
				const string sfm = @"
\lx
\a
\b
\b
\c
";
				e.CreateDictionary(sfm);

				var dictionary = e.OpenDictionary();

				Assert.That(dictionary.AllMarkers, Has.Some.EqualTo("lx"));
				Assert.That(dictionary.AllMarkers, Has.Some.EqualTo("a"));
				Assert.That(dictionary.AllMarkers, Has.Some.EqualTo("b"));
				Assert.That(dictionary.AllMarkers, Has.Some.EqualTo("c"));
			}
        }

        [Test]
        public void OpenDictionary_With2Records_ReadsIn2Records()
        {
			using (var e = new EnvironmentForTest())
			{
				e.CreateDictionary(EnvironmentForTest.SfmWithTwoRecords());
				var dictionary = e.OpenDictionary();
				Assert.That(dictionary.Count, Is.EqualTo(2));
			}
        }

        [Test]
        public void GetDirectoryPath_ReturnsPathToDirectoryContainingDictionary()
        {
			using (var e = new EnvironmentForTest())
			{
				e.CreateDictionary(EnvironmentForTest.SfmWithTwoRecords());
				var dictionary = e.OpenDictionary();
				Assert.AreEqual(e.DictionaryPath, dictionary.GetDirectoryPath());
			}
        }

        [Test]
        public void SaveAs_FileExists()
        {
			using (var e = new EnvironmentForTest())
			{
				e.CreateDictionary(EnvironmentForTest.SfmWithTwoRecords());
				var dictionary = e.OpenDictionary();
				Assert.That(File.Exists(e.AlternateDictionaryFilePath), Is.False);
				dictionary.SaveAs(e.AlternateDictionaryFilePath);
				Assert.That(File.Exists(e.AlternateDictionaryFilePath), Is.True);
			}
        }

        [Test]
        public void Save_ModifiedDictionary_ModifiedDataPresentInSavedFile()
        {
			using (var e = new EnvironmentForTest())
			{
				e.CreateDictionary(EnvironmentForTest.SfmWithTwoRecords());
				var dictionary = e.OpenDictionary();
				var entries = dictionary.AllRecords;
				entries[1].SetRecordContents(@"\lx Replaced Entry", e.SolidSettingsForTest);
				dictionary.Save();
				var dictionaryForRead = e.OpenDictionary();
				var result = dictionaryForRead.AllRecords.Find(entry => entry.LexEntry.FirstField.Value == "Replaced Entry");
				Assert.That(result, Is.Not.Null);
			}
        }

		[Test] // See http://projects.palaso.org/issues/261
		public void Save_SfmWithHeader_SavedFileKeepsHeader()
		{
			using (var e = new EnvironmentForTest())
			{
				const string sfm = @"\_sh Some Header
\_DateStampHasFourDigitYear

\lx a

\lx b
";
				e.CreateDictionary(sfm);
				var dictionary = e.OpenDictionary();
				dictionary.Save();
				var dictionaryText = File.ReadAllText(e.DictionaryFilePath);
				Assert.That(dictionaryText, Is.EqualTo(sfm));
			}
		}

		[Test] 
		public void Save_SfmWithEmptyMarker_SavedFileDoesntHaveTrailingSpaces()
		{
			using (var e = new EnvironmentForTest())
			{
				const string sfm = @"
\lx a
\ge
";
				e.CreateDictionary(sfm);
				var dictionary = e.OpenDictionary();
				dictionary.Save();
				var dictionaryText = File.ReadAllText(e.DictionaryFilePath);
				Assert.That(dictionaryText, Is.EqualTo(sfm));
			}
		}
    }
}
