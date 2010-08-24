/*
* 
* An XmlReader implementation for loading SFM delimited files
* 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using SolidGui.Processes;

namespace SolidGui.Engine
{
    /// <summary>
    /// Summary description for SolidXmlReader.
    /// </summary>
    public class SolidXmlReader : XmlReaderDecorator
    {
        enum XmlState
        {
            FromSFMXmlReader,
            FromNodeReader,
            Eof,
            Closed
        }

        //public class ProcessStore : List<IProcess>
        //{
        //}

        private SfmXmlReader _sfmXmlReader;

        //private ProcessStore _processes;

        private XmlState _xmlState = XmlState.FromSFMXmlReader;

        private SolidSettings _settings;

        private SolidReport _report = new SolidReport();

        /// <summary>
        /// Construct SolidXmlReader.  You must specify an HRef
        /// location or a TextReader before calling Read().
        /// </summary>
        public SolidXmlReader()
            : base(null)
        {
            _sfmXmlReader = new SfmXmlReader();
            _d = _sfmXmlReader;
        }

        /// <summary>
        /// Construct an SolidXmlReader.
        /// </summary>
        /// <param name="input">The uri of the input stream</param>
        public SolidXmlReader(string uri, SolidSettings settings)
            : base(null)
        {
            _sfmXmlReader = new SfmXmlReader(uri);
            _d = _sfmXmlReader;
            _settings = settings;
        }

        public SolidXmlReader(string fileName)
            : base(null)
        {
            _sfmXmlReader = new SfmXmlReader(new StreamReader(fileName));
            _d = _sfmXmlReader;
            _settings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(fileName));
        }

        /// <summary>
        /// Construct an SolidXmlReader.
        /// </summary>
        /// <param name="input">The input text reader</param>
        public SolidXmlReader(TextReader input, SolidSettings settings)
            : base(null)
        {
            _sfmXmlReader = new SfmXmlReader(input);
            _d = _sfmXmlReader;
            _settings = settings;
        }

        public SolidReport Report
        {
            get { return _report; }
            set { _report = value; }
        }

        //public ProcessStore Processes
        //{
        //    get { return _processes; }
        //}

        public SolidSettings SolidSettings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public XmlNode ReadRecord()
        {
            XmlNode retval = null;
            if (_sfmXmlReader.ReadToFollowing(RecordName))
            {
                XmlReader entryReader = _sfmXmlReader.ReadSubtree();
                // Load the current record from xr into an XmlDocument
                XmlDocument xmlSource = new XmlDocument();
                xmlSource.Load(entryReader);
                IProcess process = new ProcessStructure(_settings);
                XmlNode xmlDestination = process.Process(xmlSource.DocumentElement, _report);
                _d = new XmlNodeReader(xmlDestination);
                retval = xmlDestination;
            }
            return retval;
        }

        /// <summary>
        /// Specifies the name of the root element, the default is "root".
        /// </summary>
        public string RootName
        {
            get
            {
                return _sfmXmlReader.RootName;
            }
            set
            {
                _sfmXmlReader.RootName = value;
            }
        }

        /// <summary>
        /// Specifies the name of the XML element generated for each record
        /// in the SFM data.  The default is "entry".
        /// </summary>
        public string RecordName
        {
            get
            {
                return _sfmXmlReader.RecordName;
            }
            set
            {
                _sfmXmlReader.RecordName = value;
            }
        }

        public override bool Read()
        {
            bool retval = false;

            switch (_xmlState)
            {
                case XmlState.FromSFMXmlReader:
                    retval = _sfmXmlReader.Read();
                    if (_sfmXmlReader.Name == "entry" && _sfmXmlReader.NodeType == XmlNodeType.Element)
                    {
                        XmlReader entryReader = _sfmXmlReader.ReadSubtree();
                        // Load the current record from xr into an XmlDocument
                        XmlDocument xmlSource = new XmlDocument();
                        xmlSource.Load(entryReader);
                        IProcess process = new ProcessStructure(_settings);
                        XmlNode xmlDestination = process.Process(xmlSource.DocumentElement, _report);
                        _d = new XmlNodeReader(xmlDestination);
                        _xmlState = XmlState.FromNodeReader;
                        retval = _d.Read();
                    }
                    break;
                case XmlState.FromNodeReader:
                    retval = _d.Read();
                    if (!retval)
                    {
                        _xmlState = XmlState.FromSFMXmlReader;
                        _d = _sfmXmlReader;
                        goto case XmlState.FromSFMXmlReader;
                    }
                    break;
            }

            return retval;
        }

        public override int Depth
        {
            get
            {
                int retval = 0;
                switch (_xmlState)
                {
                    case XmlState.FromSFMXmlReader:
                        retval = _sfmXmlReader.Depth;
                        break;
                    case XmlState.FromNodeReader:
                        retval = _d.Depth + 1;
                        break;
                }
                return retval;
            }
        }
        
        /*
        public override ReadState ReadState
        {
            get
            {
                return (_d != null) ? _d.ReadState : _sfmXmlReader.ReadState;
            }
        }
        */
    }
}