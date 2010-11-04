using System;
using System.IO;
using NUnit.Framework;
using SolidGui.Engine;


namespace SolidGui.Tests.Engine
{
    [TestFixture]
    public class SfmReader_Read_Test
    {

        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void EmptySFM_HeaderCount_0()
        {
            const string sfm = @"";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.AreEqual(false, result);
            Assert.AreEqual(0, r.Header.Count);
        }

        [Test]
        public void HeaderOnly_Header_Correct()
        {
            const string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                               "\\_DateStampHasFourDigitYear\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.AreEqual(false, result);
            Assert.AreEqual(2, r.Header.Count);
            Assert.AreEqual("_sh", r.Header[0].Marker);
            Assert.AreEqual("v3.0  269  MDF 4.0 (alternate hierarchy)", r.Header[0].Value);
            Assert.AreEqual("_DateStampHasFourDigitYear", r.Header[1].Marker);
            Assert.AreEqual("", r.Header[1].Value);
        }

        [Test]
        public void EmptySFMRecordRead_False()
        {
            const string sfm = @"";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void HeaderOnlySFMRecordRead_False()
        {
            const string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                               "\\_DateStampHasFourDigitYear\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ReadNoHeader_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "\\ge b\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
        }

        [Test]
        public void ReadNoHeaderTabDelimited_Correct()
        {
            const string sfm = "\\lx\ta\n" +
                               "\\ge\tb\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
        }

        [Test]
        public void ReadEmptyValue_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "\\ge\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("", r.Value("ge"));
        }

        [Test]
        public void ReadEmptyKey_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "\\\n" +
                               "\\ge b";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(3, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
            Assert.AreEqual("", r.Key(1));
        }

        [Test]
        public void ReadIndentedMarker_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "  \\ge b\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            r.AllowLeadingWhiteSpace = true;
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
        }

        [Test]
        public void ReadBackslashInValue_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "\\ge \\b\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("\\b", r.Value("ge"));
        }

        [Test]
        public void ReadWrappedText_Correct ()
        {
            const string sfm = "\\lx a\n" +
                               "\\ge b\nc";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read ();
            Assert.IsTrue (result);
            Assert.AreEqual (0, r.Header.Count);
            Assert.AreEqual (2, r.FieldCount);
            Assert.AreEqual ("a", r.Value ("lx"));
            Assert.AreEqual ("b\nc", r.Value ("ge"));
        }

        private SfmRecordReader ReadOneRecordData ()
        {
            const string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                               "\\_DateStampHasFourDigitYear\n" +
                               "\\lx a\n" +
                               "\\ge b\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            return r;
        }

        private SfmRecordReader ReadTwoRecordData()
        {
            const string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                               "\\_DateStampHasFourDigitYear\n" +
                               "\\lx a\n" +
                               "\\ge b\n" +
                               "\\lx c\n" +
                               "\\gn d\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            return r;
        }

        [Test]
        public void OneSFMRecordReadToEOF_Correct()
        {
            var r = ReadOneRecordData();
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("lx", r.Key(0));
        }

        [Test]
        public void OneSFMRecordReadToNextMarker_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("ge", r.Key(1));
        }

        [Test]
        public void OneSFMRecordRead_Key0_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual("lx", r.Key(0));
        }

        [Test]
        public void OneSFMRecordRead_Key1_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual("ge", r.Key(1));
        }

        [Test]
        public void RecordStartLine_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual(3, r.RecordStartLine);
        }

        [Test]
        public void RecordEndLine_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual(4, r.RecordEndLine);
        }

        [Test]
        public void Record_EOF_Correct()
        {
            var r = ReadTwoRecordData(); // Reads the first record for us.
            bool result = r.Read();
            Assert.IsTrue(result);
            result = r.Read();
            Assert.IsFalse(result);
        }

        [Test]
        public void RecordID_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual(0, r.RecordID);
            bool result = r.Read();
            Assert.IsTrue(result); // Should be for two records.
            Assert.AreEqual(1, r.RecordID);
        }

        [Test]
        public void CreateFromFilePath_ExistingFile_ReadsOk()
        {
            using (var e = new EnvironmentForTest())
            {
                const string input = @"
\lx test1
\cc fire
\sn
\cc foo
\sn
\cc bar";

                using (var writer = new StreamWriter(e.TempFilePath))
                {
                    writer.Write(input);
                    writer.Close();
                }
                using (var reader = new StreamReader(e.TempFilePath))
                {
                    var output = reader.ReadToEnd();
                    reader.Close();
                    Assert.AreEqual(input, output);
                }
            }
        }

        public class EnvironmentForTest : IDisposable
        {
            public EnvironmentForTest()
            {
                TempFilePath = Path.GetTempFileName();
            }

            public string TempFilePath { get; private set; }

            public void Dispose()
            {
                File.Delete(TempFilePath);
            }
        }
    }
}