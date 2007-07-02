using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SolidEngine
{
    class SFMReader
    {
        String _file;

        public SFMReader(String file)
        {
            _file = file;
        }

        public void Read()
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(_file))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
