using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SolidEngine
{
    public class SolidFile
    {
        public class SolidFileData
        {


            public SolidFileData()
            {
            }
        }

        private SolidFileData _solidData = new SolidFileData();

        private string _file;

        #region Properties
        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public SolidSettings MarkerSettings
        {
            get { return _solidData.MarkerSettings; }
        }

        #endregion

        #region Constructors
        public SolidFile()
        {
        }

        public SolidFile(string file)
        {
        }
        #endregion

        #region Public Methods
        public void Open(string file)
        {
            _file = file;
            Read();
        }

        public void Read()
        {
            XmlSerializer xs = new XmlSerializer(typeof(SolidFileData));
            try
            {
                using (StreamReader reader = new StreamReader(_file))
                {
                    _solidData = (SolidFileData)xs.Deserialize(reader);
                }
            }
            catch
            {
                _solidData = new SolidFileData();
            }

        }

        public void Write()
        {
            XmlSerializer xs = new XmlSerializer(typeof(SolidFileData));
            using (StreamWriter writer = new StreamWriter(_file))
            {
                xs.Serialize(writer, _solidData);
            }
        }
        #endregion

    }
}
