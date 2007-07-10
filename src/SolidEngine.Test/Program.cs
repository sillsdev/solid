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
            //SolidReportTest t = new SolidReportTest();
            //t.SolidReport_SaveOpen_Correct();

            SolidXmlReaderTests t = new SolidXmlReaderTests();
            t.SFMEmptyDocument_Correct();

            //ProcessStructureTest t = new ProcessStructureTest();
            //t.ProcessStructure_NoInferInsertAnyway_Correct();

            //SolidifierTest t = new SolidifierTest();
            //t.Solidifier_InferNode_Correct();
        }
    }
}
