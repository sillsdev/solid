using System;
using System.Collections.Generic;
using System.Text;

namespace SolidEngine
{
    public class ExportFactory
    {
        public enum ExportType
        {
            StructuredXml,
            FlatXml,
            Lift,
            Flex,
            Max
        }

        public static IExporter Create(ExportType type)
        {
            IExporter retval = null;
            switch (type)
            {
                case ExportType.StructuredXml:
                    retval = new ExportStructuredXml();
                    break;
                case ExportType.Lift:
                    retval = new ExportLift();
                    break;
            }
            return retval;
        }
    }
}
