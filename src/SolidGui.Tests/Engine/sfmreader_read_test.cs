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
            bool result = r.ReadRecord();
            Assert.AreEqual(false, result);
            Assert.AreEqual("", r.Header);
//            Assert.AreEqual(0, r.Header.Count);
        }

        [Test]
        public void HeaderOnly_Header_Correct()
        {
            const string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\r\n" +
                               "\\_DateStampHasFourDigitYear\r\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.AreEqual(false, result);
            Assert.AreEqual(sfm, r.Header);

/*
            Assert.AreEqual(2, r.Header.Count);
            Assert.AreEqual("_sh", r.Header[0].Marker);
            Assert.AreEqual("v3.0  269  MDF 4.0 (alternate hierarchy)", r.Header[0].Value);
            Assert.AreEqual("_DateStampHasFourDigitYear", r.Header[1].Marker);
            Assert.AreEqual("", r.Header[1].Value);
 */
        }

        [Test]
        public void EmptySFMRecordRead_False()
        {
            const string sfm = @"";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void HeaderOnlySFMRecordRead_False()
        {
            const string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                               "\\_DateStampHasFourDigitYear\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ReadTinyRecord_Correct()
        {
            const string sfm = "\\lx";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("", r.Header);
            Assert.AreEqual(1, r.FieldCount);
            Assert.AreEqual("", r.Value("lx"));
            result = r.ReadRecord();
            Assert.IsFalse(result);
        }

        [Test]
        public void ReadNoHeader_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "\\ge b\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("", r.Header);

/*
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
*/
        }

        [Test]
        public void ReadHeaderConfusing_Correct()
        {
            const string header = "\\lxh\r\n" +
                                  " \\lx not \\lx\r\n" +
                                  "\\lxHaha\r\n";
            const string sfm = header + "\\lx finally!";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual(header, r.Header);
        }

        [Test]
        public void ReadNoHeaderTabDelimited_Correct()
        {
            const string sfm = "\\lx\ta\n" +
                               "\\ge\tb\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("", r.Header);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
        }

        [Test]
        public void ReadEmptyValue_Correct()
        {
            const string sfm = "\\lx a\r\n" +
                               "\\ge\n" +
                               "\\de\r\n \r\n" +
                               "\\dt";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("", r.Header);
            Assert.AreEqual(4, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("", r.Value("ge"));
            Assert.AreEqual("", r.Value("de"));
        }

        [Test]
        public void ReadEmptyKey_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "\\\n" +
                               "\\ge b\n" +
                               "\\\n" +
                               "\\\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("", r.Header);
            Assert.AreEqual(5, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
            Assert.AreEqual("", r.Key(1));
            Assert.AreEqual("", r.Key(3));
            Assert.AreEqual("", r.Key(4));
        }

        [Test]
        public void ReadEmptyLxEmptyKey_Correct()
        {
            const string sfm = "head\r\n" +
                               "\\lx\n" +
                               "\\\n" +
                               "\\ge b\n" +
                               "\\lx"
                               ;
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("head\r\n", r.Header);
            Assert.AreEqual(3, r.FieldCount);
            Assert.AreEqual("", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
            Assert.AreEqual("lx", r.Key(0));
            Assert.AreEqual("", r.Key(1));
            Assert.AreEqual("ge", r.Key(2));
            result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("head\r\n", r.Header);
            Assert.AreEqual(1, r.FieldCount);
            Assert.AreEqual("", r.Value("lx"));
        }

        [Test]
        [Ignore("No longer the job of the main parser; will handle with regex in the UI. -JMC 2013-09")]
        public void ReadIndentedMarker_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "  \\ge b\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            r.AllowLeadingWhiteSpace = true;
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("", r.Header);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
        }

        [Test]
        public void ReadBackslashInValue_Correct()
        {
            const string sfm = "\\lx a\n" +
                               "\\ge \\b \\zblah\\z\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("", r.Header);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("\\b \\zblah\\z", r.Value("ge"));
        }

        [Test]
        public void ReadWrappedText_Correct ()
        {
            const string sfm = "\\lx a\n" +
                               "\\ge b\nc";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord ();
            Assert.IsTrue (result);
            Assert.AreEqual("", r.Header);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual ("a", r.Value ("lx"));
            Assert.AreEqual ("b\r\nc", r.Value ("ge"));
        }

        [Test]
        public void ReadNewlinesPreserved_Correct()
        {
            const string sfm1 = "\\lx a\n" +
                               "b\n\n\r" +
                               "\\ge c\n\n" +
                               "\\rf\nd\n\n" +
                               "\\dt\n\n" +
                               "\\lx f";  //note that \dt is both final and empty, plus extra trailing
            const string sfm = "\\lx a\n" +
                               "\\dt\n\n" +
                               "\\lx f";  //note that \dt is both final and empty, plus extra trailing
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            Assert.AreEqual("", r.Header);
/*
            Assert.AreEqual(4, r.FieldCount);
            Assert.AreEqual("a\r\nb", r.Value("lx"));
            Assert.AreEqual("\r\n\r\n\r\n", r.Trailing("lx"));
            Assert.AreEqual("\r\n\r\n", r.Trailing("ge"));
            Assert.AreEqual("\r\n\r\n", r.Trailing("rf"));
*/
            Assert.AreEqual("\r\n\r\n", r.Trailing("dt"));
            r.ReadRecord();
            Assert.AreEqual(1, r.FieldCount);
            Assert.AreEqual("\r\n", r.Trailing("lx"));
        }


        private SfmRecordReader ReadOneRecordData()
        {
            const string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                               "\\_DateStampHasFourDigitYear\n" +
                               "\\lx a\n" +
                               "\\ge b\n";
            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
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
            bool result = r.ReadRecord();
            Assert.AreEqual(true, result);
            return r;
        }

        [Test]
        public void EqualizeNewlines()
        {
            string sfm = "header Windows 1\r\nLinux 2\n\nOldMac 2\r\rCombo \r\r\n\n\r";
            string h = "header Windows 1\r\nLinux 2\r\n\r\nOldMac 2\r\n\r\nCombo \r\n\r\n\r\n\r\n";
            sfm += "\\lx   Windows 1\r\nLinux 2\n\nOldMac 2\r\rCombo \r\r\n\n\r";
            string val = "  Windows 1\r\nLinux 2\r\n\r\nOldMac 2\r\n\r\nCombo";
            string trail = " \r\n\r\n\r\n\r\n";

            var r = SfmRecordReader.CreateFromText(sfm);
            bool result = r.ReadRecord();
            Assert.AreEqual(true, result);
            Assert.AreEqual(h, r.Header);
            Assert.AreEqual(val, r.Value("lx"));
            //Assert.AreEqual(trail, r.Fields. );  //JMC: unfinished
        }

        [Test]
        public void OneSFMRecordReadToEOF_Correct()
        {
            var r = ReadOneRecordData();
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("lx", r.Key(0));
        }


        [Test]
        public void SplitTrailingSpaceSimple()
        {
            string val = "value\r\n";
            var f = new SfmField();
            f.SetSplitValue(val);
            Assert.AreEqual(f.Value, "value");
            Assert.AreEqual(f.Trailing, "\r\n");
        }

        [Test]
        public void SplitTrailingSpaceLots()
        {
            string val = "long long \r\n wrapped field. . .  \r \r\n\r\n\t\r\n";
            var f = new SfmField();
            f.SetSplitValue(val);
            Assert.AreEqual(f.Value, "long long \r\n wrapped field. . .");
            Assert.AreEqual(f.Trailing, "  \r \r\n\r\n\t\r\n");
        }

        [Test]
        public void OneSFMRecordReadToNextMarker_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("ge", r.Key(1));
            r.ReadRecord();
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("gn", r.Key(1));
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
            r.ReadRecord();
            Assert.AreEqual(5, r.RecordStartLine);
        }

        [Test]
        public void RecordEndLine_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual(4, r.RecordEndLine);
            r.ReadRecord();
            Assert.AreEqual(7, r.RecordEndLine);
        }

        [Test]
        public void Record_EOF_Correct()
        {
            var r = ReadTwoRecordData(); // Reads the first record for us.
            bool result = r.ReadRecord();
            Assert.IsTrue(result);
            result = r.ReadRecord();
            Assert.IsFalse(result);
        }

        [Test]
        public void RecordID_Correct()
        {
            var r = ReadTwoRecordData();
            Assert.AreEqual(0, r.RecordID);
            bool result = r.ReadRecord();
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
\cc bar
\lx test2


\lx test3";

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