using System;
using System.Collections.Generic;
using System.Text;

using SolidTests;

namespace SolidTests
{
    class Program
    {
        static void Main(string[] args)
        {
            SolidFileTest t = new SolidFileTest();
            t.SolidFile_Write1Read1_Correct();
            
            //SfmReader_Header_Test t = new SfmReader_Header_Test();
            //t.HeaderOnly_Header_Correct();

            //SfmReader_Read_Test t = new SfmReader_Read_Test();
            //t.EmptySFMRecordRead_False();
            
            //SfmXmlReader_XmlDoc_Test t = new SfmXmlReader_XmlDoc_Test();
            //t.HeaderDoc_Correct();
            //t.EmptyDoc1();
            //t.EmptySfmDoc_Correct();

            //SfmXmlReaderTests t = new SfmXmlReaderTests();
            //t.SFMHeaderDocument_Correct();
        }
    }
}