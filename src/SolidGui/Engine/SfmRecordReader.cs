// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using SolidGui.Engine;

namespace SolidGui.Engine
{

    public class SfmRecordReader : IDisposable
    {

        enum StateParse
        {
            Header,
            GotLx,
            BuildValue,
            BuildKey,
        }
        
        StateParse _stateParse = StateParse.Header;
        TextReader _r;
        private SfmRecord _record;
        private string _headerLinux = ""; 

        string _startKey = "lx";  // JMC: use global setting!
        private readonly List<char> _enders = new List<char> {' ', '\t', '\r', '\n', '\\', '\0'}; // All chars that end an SFM marker (SFM key)

        private static Regex ReggieLeading = new Regex(
            @"^[ \t]+\\", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static Regex ReggieTab = new Regex(
            @"\t", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static Regex ReggieLx = new Regex(
            @"^[ \t]*\\" + SolidSettings.NewLine + @"\b", RegexOptions.Compiled | RegexOptions.CultureInvariant);


        private long _size = 0;
        private int _recordStartLine;
        private int _recordEndLine;
        private int _recordID = -1;

        // Reading state
        private int _line = 1;
        private int _col = 1;
        private string _separator = " ";

        private Encoding _encoding = SolidSettings.LegacyEncoding;  //was: Encoding.GetEncoding("iso-8859-1");

        #region Properties
        private bool _allowLeadingWhiteSpace;

        public bool AllowLeadingWhiteSpace
        {
            get { return _allowLeadingWhiteSpace; }
            set { _allowLeadingWhiteSpace = value; }
        }

        public int SizeEstimate  // file size estimate
        {
            // JMC: a rough attempt at scaling most longs down to ints (not guaranteed to work well; might wrap around)
            get { return _size > int.MaxValue ? int.MaxValue : (int)(_size / 160); } 
        }

        public int RecordID
        {
            get { return _recordID; }
        }

        public int RecordStartLine
        {
            get { return _recordStartLine; }
        }

        public int RecordEndLine  
        {
            get { return _recordEndLine; }
        }

        #endregion

        #region Constructors
        private SfmRecordReader(TextReader stream)
        {  // the location of the file
            _r = stream;
            _size = 1024; // a wild guess -JMC
            _record = new SfmRecord();
        }

        private SfmRecordReader(string filePath)
        {  // the location of the file
            _r = new StreamReader(filePath, _encoding, false);
            var fi = new FileInfo(filePath);
            _size = fi.Length;
            _record = new SfmRecord();
        }
        #endregion

        public void Close()
        {
            _r.Close();
        }

        public static int ReadOneChar(TextReader r)
        {
            // For now, guarantee that we see all newlines as a single \n
            // (That is, replace all \r\n with \n, and then all \r with \n)
            // Read a tab as a space.
            int c = r.Read();
            if (c == -1) return c; //EOF
            if (c == '\r')
            {
                if (r.Peek() == '\n')
                {
                    // skip the \r
                    c = r.Read();
                }
                else
                {
                    // convert \r to \n
                    c = '\n';
                }
            }
            // Simplify tab to space.
            return (c == '\t') ? ' ' : c;
        }

        /// <summary>
        /// Goes through the stream copying it into the header until the record marker is encountered.
        /// </summary>
        /// <returns>left-overs that were read in but aren't header chars; typically @"\lx", or an empty string if entire stream was the header.</returns>
        private string ReadHeader()
        {
            string ret = "";
            string stopAt = "\\" + _startKey;   // typically @"\lx"
            int len = stopAt.Length;            // typically 3
            var sbMatch = new StringBuilder();
            var sbHeader = new StringBuilder();
            int tmp; // it's so annoying that we need this
            char c;
            int L = 0;
            while (true)
            {
                tmp = ReadOneChar(_r);  //or two, in the case of \r\n
                if (tmp == -1)
                {
                    break; // EOF
                }

                c = (char) tmp;

                // Append what we find; though we'll have to back out the last (len) chars
                if (c == '\n')
                {
                    sbMatch.Clear(); // no match; start over
                    _col = 1;
                    _line++;
                    sbHeader.Append(SolidSettings.NewLine);
                }
                else
                {
                    sbHeader.Append(c);
                    _col++;
                }

                L = sbMatch.Length;  // the current length of a possible match
                if (c == stopAt[L] && _col-1 == L+1)
                {
                    sbMatch.Append(c); // still matches
                }
                else
                {
                    sbMatch.Length = 0; // no match; start over; equivalent to sbMatch.Clear();
                }

                if (sbMatch.Length == len) 
                {
                    // we've found \lx, but does it end?
                    tmp = _r.Peek();
                    if ( tmp == -1 || _enders.Contains((char)tmp) ) 
                    {
                        // yes, end of marker (key)
                        sbHeader.Remove(sbHeader.Length - len, len);
                        ret = stopAt;
                        _stateParse = StateParse.GotLx;

                        // discard separator for now (usually the space in @"\lx ")
                        _separator = "" + (char)ReadOneChar(_r); 

                        break;
                    }
                    // no match; start over (e.g. @"\lxhaha" isn't a match
                    sbMatch.Length=0;
                }
                         
            }
            _headerLinux = sbHeader.ToString();
            return ret;
        }

        // Read in ONE record from the text stream (into the Record property), OR returns false if no more records in stream.
        // Calling code should also check whether the Header property is non-empty after calling this.
        public bool ReadRecord()
        {
            bool retval = false;
            _record.Clear();  // but don't clear the header! (nor the separator)

            if (_stateParse == StateParse.Header)
            {
                ReadHeader();
            }

            SfmField currentField = new SfmField();
            if (_stateParse == StateParse.GotLx)
            {
                retval = true;
                currentField.Marker = _startKey;
                _recordID++;
                _stateParse = StateParse.BuildValue;
            }
            else if (_r.Peek() == -1) //EOF
            {
                return false;
            }
            else
            {
                Trace.Assert(_r.Read() == 0, "There is a bug in ReadRecord; please let the developers know.");
                return false;
            }

            StringBuilder sb = new StringBuilder(2048);

            int temp = -1;
            char curr = '\0';
            string stemp = "";
            _recordStartLine = _line;
            _recordEndLine = -1;
            _col = 1;
            bool eof = false;
            bool initialSlash;

            while (true)  // look at one char or one \r\n sequence; we'll break upon hitting another lx, or on EOF
            {
                //temp = _r.Read();
                temp = ReadOneChar(_r);

                eof = temp == -1;
                curr = (char)temp;

                if ( (!eof) && (!_enders.Contains(curr)) )  // the typical case
                {
                    sb.Append(curr);
                    _col++;
                    continue;
                }

                // It's a tab, space, newline, slash, or EOF; proceed...
                
                if ( (curr == '\n') )  //  || (curr == '\r') )
                {
                    _col = 1;
                    _line++;
                    initialSlash = false;
                }
                else
                {
                    initialSlash = ((_col == 1) && (curr == '\\'));
                    _col++;
                }

                if (_stateParse == StateParse.BuildKey)
                {
                    // end of field marker
                    _separator = "" + curr;
                    stemp = sb.ToString();
                    if (stemp == _startKey)
                    {
                        _stateParse = StateParse.GotLx;
                        break;
                    }
                    currentField.Marker = stemp;
                    currentField.SourceLine = (curr == '\n') ? _line - 1 : _line;
                    sb.Length=0;
                    _stateParse = StateParse.BuildValue;
                    if (eof)
                    {
                        _record.Add(currentField);
                        break;
                    }
                    if (curr == '\n')
                    {
                        if ((char) _r.Peek() == '\\') // no data
                        {
                            // end of field value (duplicate code below)
                            currentField.SetSplitValue(sb.ToString(), _separator);
                            _record.Add(currentField);
                            sb.Length=0;
                            currentField = new SfmField(); // clear
                            _stateParse = StateParse.BuildKey;

                            _r.Read(); //toss the upcoming slash
                            _col = 2;
                        }
                    }
                }

                else if (_stateParse == StateParse.BuildValue)
                {
                    if (curr == '\n')
                    {
                        sb.Append(SolidSettings.NewLine); 
                    }
                    else if (!eof && !initialSlash)
                    {
                        sb.Append(curr);
                    }
                    if (initialSlash || eof) 
                    {
                        // end of field value (duplicate code above)
                        currentField.SetSplitValue(sb.ToString(), _separator);
                        _record.Add(currentField);
                        sb.Length=0;
                        currentField = new SfmField(); // clear
                        _stateParse = StateParse.BuildKey;
                    }

                }

                if (eof) break;

            }

            // Determine _recordEndLine (usually = _line - 1)
            _recordEndLine = (_stateParse == StateParse.GotLx) ? _line - 1 : _line;
            _recordEndLine = (curr == '\n') ? _recordEndLine - 1 : _recordEndLine;
            // So, there's a max of -2 total, which occurs when a record is followed by "\lx\n"
            // The minimum is -0, when a record ends in EOF.

            return retval;
        }

        public int FieldCount
        {
            get
            {
                return _record.Count;
            }
        }

        public string this[int i]
        {
            get
            {
                return Value(i);
            }
        }

        public string HeaderLinux
        {
            get
            {
                return _headerLinux;
            }
        }

        public static string HeaderToWrite(string headerLinux, string newLine)
        {
            string tmp = headerLinux.Replace("\r\n", "\n");
            tmp = tmp.Replace("\r", "\n");
            return tmp.Replace("\n", newLine); //JMC: a single regex would be better
        }


        public SfmRecord Record
        {
            get
            {
                return _record;
            }
        }

        public IEnumerable<SfmField> Fields
        {
            get { return _record; }
        }

        public SfmField Field(int i)
        {
            return _record[i];
        }

        public string Key(int i)
        {
            return _record[i].Marker;
        }

        public string Value(int i)
        {
            return _record[i].Value;
        }

        public string Value(string key)
        {
            SfmField result = _record.Find( (SfmField item) => { return item.Marker == key; });
            if (result == null)
            {
                throw new ArgumentOutOfRangeException("key");
            }
            return result.Value;
        }

        public string Trailing(string key)
        {
            SfmField result = _record.Find( (SfmField item) => { return item.Marker == key; });
            if (result == null)
            {
                throw new ArgumentOutOfRangeException("key");
            }
            return result.Trailing;
        }

        // Given a string read it into one or more records. -JMC
        public static SfmRecordReader CreateFromText(string text)
        {

            // Start with regex cleanup to remove tabs, and leading spaces -JMC 2013-09
            // JMC: Could also paste these two lines into a toolbar button method that does "plain-text copy" (includes inferred like \+sn but no formatting).
            //   Toolbar button and/or add Ctrl-C to SfmEditorView, _contentsBox_KeyDown .
            string s = ReggieLeading.Replace(text, "\\");
            s = ReggieTab.Replace(s, " ");

            var stream = new StringReader(s);
            return new SfmRecordReader(stream);
        }

        public static SfmRecordReader CreateFromFilePath(string filePath)
        {
            return new SfmRecordReader(filePath);
        }

        public void Dispose()
        {
            _r.Dispose();
        }
    }
}