using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using SolidGui.Engine;

namespace SolidGui.Engine
{
    public class SfmRecordReader : IDisposable
    {
        enum StateLex
        {
            StartFile,
            StartOfRecord, // Holding startkey from previous scan.
            BuildKey,
            BuildValue,
            EOF
        }

        enum StateParse
        {
            Header,
            Records
        }

        SfmRecord _record = new SfmRecord();
        SfmRecord _header = new SfmRecord();

        string _startKey = "lx";

        TextReader _r;
        StateLex _stateLex = StateLex.StartFile;
        StateParse _stateParse = StateParse.Header;

        private int _recordStartLine;
        private int _recordEndLine;
        private int _recordID = 0;

        // Internal buffer state
        private char[] _buffer;
        private int _pos;
        private int _used;

        // Reading state
        private int _line = 1;
        private int _col = 1;
        private int _backslashCount = 0;  //JMC: delete?

        private int _bufferSize = 4096;

        Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        #region Properties
        private bool _allowLeadingWhiteSpace;

        public bool AllowLeadingWhiteSpace
        {
            get { return _allowLeadingWhiteSpace; }
            set { _allowLeadingWhiteSpace = value; }
        }

        public int BufferSize
        {
            get 
            { 
                return _bufferSize; 
            }
            set 
            {
                _bufferSize = value;
                _buffer = new char[_bufferSize];
                _pos = 0;
                _used = 0;
            }
        }

        public int RecordID
        {
            get { return _recordID; }
        }

        public int RecordStartLine
        {
            get { return _recordStartLine; }
        }

        public int RecordEndLine  //JMC: unused except in tests? delete?
        {
            get { return _recordEndLine; }
        }

        public TextReader Reader
        {
            get
            {
                return _r;
            }
        }
        #endregion

        #region Constructors
        private SfmRecordReader(TextReader stream)
        {  // the location of the file
            _r = stream;
            _buffer = new char[_bufferSize];
        }

        private SfmRecordReader(string filePath)
        {  // the location of the file
            _r = new StreamReader(filePath, _encoding, false);
            _buffer = new char[_bufferSize];
        }
        #endregion

        public void Close()
        {
            _r.Close();
        }

        // Read a record.
        public bool Read()
        {
            bool retval = false;
            // Check parse state
            switch (_stateParse)
            {
                case StateParse.Header:
                    _stateParse = StateParse.Records;
                    retval = ReadRecord();
                    // Store the header regardless of what is returned. May only be a header in the file.
                    _header = new SfmRecord(_record); 
                    if (retval)
                    {
                        retval = ReadRecord();
                    }
                    break;
                case StateParse.Records:
                    retval = ReadRecord();
                    _recordID++;
                    break;
            }
            return retval;
        }

        private bool ReadRecord()  // JMC: Need to rewrite this method, relying less on the \r\n sequence, and maybe using peek
        {
            _record.Clear();

            bool retval = false;
            //!!!            int startMatch = 0;
            //            int startLimit = _startKey.Length;
            SfmField currentField = new SfmField();
            if (_stateLex == StateLex.StartOfRecord)
            {
                currentField.Marker = _startKey;
                _stateLex = StateLex.BuildValue;  //JMC: seems false
            }
            StringBuilder sb = new StringBuilder(1024);
            char c1 = '\0';
            char c0 = '\0';
            _recordStartLine = _line;
            bool stillWhite = true;
            while (_stateLex != StateLex.StartOfRecord && _stateLex != StateLex.EOF)
            {
                c1 = c0;
                c0 = ReadChar();

                // First, deal with boundary conditions (EOL, backslash, leading whitespace)

                if (isEOL(c0) && !isEOL(c1))
                { // the beginning of the end (of the line); update the line and column statistics
                    _line++;
                    _col = 1;
                    _backslashCount = 0;
                    stillWhite = true;
                }
                else if (c0 == '\\')
                {
                    // See if we've bumped into the beginning of the next field.
                    // (This allows \ in the value - but constrains the sfm to toolbox lexicon format.)
                    if (_col == 1 || (_allowLeadingWhiteSpace && stillWhite))
                    {
                        // Yep, new field ahead, so save the current one's value.
                        if (_stateLex == StateLex.BuildValue)
                        {
                            currentField.SetSplitValue(sb.ToString());

                            // char[] trim = { ' ', '\t', '\x0a', '\x0d' };  //JMC: bad perf.; move to const or readonly (under SfmField?)
                            //JMC: add constants for x0a (0x0a \n) and x0d (\r)
                            // currentField.Value = currentField.Value.TrimEnd(trim); //JMC: disable this? Or better, add a Trailing property to SfmField
                            //JMC: fix the duplicate code below too

                            onField(currentField);
                            // Start fresh
                            currentField = new SfmField();
                        }
                        _stateLex = StateLex.BuildKey;
                        sb.Length = 0;
                        currentField.SourceLine = _line;
                    }
                    _backslashCount++;
                }
                else if (stillWhite && c0 != ' ' && c0 != 0x09)  // end of leading whitespace ; JMC: need to add: public const char Tab = 0x09;
                {
                    stillWhite = false;  // c0 isn't space or tab (but might be newline)
                }

                // Scan for the start of record and update state if found

                switch (_stateLex)
                {
                    case StateLex.BuildKey:
                        if (c0 == ' ' || c0 == 0x09 || isEOL(c0))  //end of key
                        {
                            currentField.Marker = sb.ToString();
                            _stateLex = StateLex.BuildValue;
                            sb.Length = 0;
                            if (currentField.Marker == _startKey) //done reading header, or done reading previous record
                            {
                                _stateLex = StateLex.StartOfRecord;
                                _recordEndLine = _line - 1; 
                                retval = true;
                            }
                        }
                        else if (c0 != '\\')
                        {
                            sb.Append(c0);  //store character (any backslashes found inside a key are dropped)
                        }
                        break;
                    case StateLex.StartFile:
                    case StateLex.BuildValue:
                        // See http://projects.palaso.org/issues/show/244
                        sb.Append (c0);
                        /*
                        if (!isEOL(c0))
                        {
                            sb.Append(c0);
                        }
                        else
                        {
                            if (!isEOL(c1))
                            {
                                sb.Append (' ');
                            }
                        }
                     */
                        break;
                    case StateLex.EOF:
                        if (currentField.Marker != String.Empty)
                        {
                            currentField.SetSplitValue(sb.ToString());

//                            currentField.Value = sb.ToString();  //JMC: refactor, since this is duplicate code (see above)
//                            char [] trim = { ' ', '\t', '\x0a', '\x0d' };
//                            currentField.Value = currentField.Value.TrimEnd (trim);
                            onField(currentField);
                            currentField = new SfmField();
                            _recordEndLine = _line - 1; 
                            retval = true;
                        }
                        break;
                }
                if (!isEOL(c0))  //JMC: remove the if? (Would have to change the other _col behavior above)
                {
                    _col++;
                }
                if (isEOL(c0) && isEOL(c1))  
                {
                    // two consecutive EOL; 'reset' c0 so that subsequent EOL are counted correctly.
                    // JMC: Hmm, consecutive EOL. Start looking here for bug 619 http://projects.palaso.org/issues/619
                    c0 = '\0';
                }

            }

            return retval;
        }

        bool isEOL(char c)
        {
            if (c == 0x0d || c == 0x0a)
            {
                return true;
            }
            return false;
        }

        void onField(SfmField field)
        {
            _record.Add(field);
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

        public SfmRecord Header
        {
            get
            {
                return _header;
            }
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
            //get { throw new NotImplementedException(); }
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
            SfmField result = _record.Find(delegate(SfmField item) { return item.Marker == key; });
            if (result == null)
            {
                throw new ArgumentOutOfRangeException("key");
            }
            return result.Value;
        }

        char ReadChar() // side effect: can set _stateLex to EOF
        {
            if (_pos == _used)
            {
                _pos = 0;
                _used = _r.Read(_buffer, 0, _buffer.Length);
            }
            if (_pos == _used) //??? Is there a better test for effectively EOF here?
            {
                _buffer[_pos] = '\0';
                _stateLex = StateLex.EOF;
            }
            return _buffer[_pos++];
        }

        public static SfmRecordReader CreateFromText(string text)
        {
            var stream = new StringReader(text);
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