using System;
using System.Collections.Generic;
using System.Text;

namespace SolidConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SFMReader a = new SFMReader("hi there");
                a.Read();
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
