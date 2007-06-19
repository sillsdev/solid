using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
//            Console.Write("Hello World\n");
        }
    }
}
