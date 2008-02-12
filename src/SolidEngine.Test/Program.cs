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
            //ExportLift_Test t = new ExportLift_Test();
            //t.ExportSamples_Correct();

            ProcessEncodingTest t = new ProcessEncodingTest();
            t.Setup();
            t.UpperAsciiDataAsUnicode_Correct();

            //Encoding_Test t = new Encoding_Test();
            //t.Store0x00To0xFFInString_Correct();

            //SolidReportTest t = new SolidReportTest();
            //t.SolidReport_SaveOpen_Correct();

            //SolidXmlReader_XmlDoc_Test t = new SolidXmlReader_XmlDoc_Test();
            //t.OneEntrySfmDoc_Correct();

            //EngineEnvironment_Test t = new EngineEnvironment_Test();
            //t.PathOfBase_Correct();

            //SfmReader_Read_Test t = new SfmReader_Read_Test();
            //t.Init();
            //t.ReadIndentedMarker_Correct();

            //ExportFactory_Test t = new ExportFactory_Test();
            //t.Init();
            //t.ExportSettings_Has2Files();

            //ExportXsl_Test t = new ExportXsl_Test();
            //t.Export_Correct();

            //SolidXmlReaderTests t = new SolidXmlReaderTests();
            //t.SFMEmptyDocument_Correct();

            //ProcessStructureTest t = new ProcessStructureTest();
            //t.ProcessStructure_NoInferInsertAnyway_Correct();

            //SolidifierTest t = new SolidifierTest();
            //t.Solidifier_InferNode_Correct();
        }
    }
}
