using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            DictionaryTest t = new DictionaryTest();
            t.SetUp();
            t.CopyToWritesDictionaryToFile();
        }
    }
}
