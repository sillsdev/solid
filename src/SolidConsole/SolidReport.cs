using System;
using System.Collections.Generic;
using System.Text;

namespace SolidConsole
{
    public class SolidReport
    {
        public class Error
        {
            int _sourceLine;
            string _errorClass;
            string _marker;
            string _description;

            public Error()
            {

            }
        }

        public class Record
        {

            int _id;



            public Record()
            {
            }

        }

        
        public SolidReport()
        {

        }
    }
}
