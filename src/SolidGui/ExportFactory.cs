using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    class Export
    {
        public class Types
        {
            public static int StructuredXml()
            {
                return 1;
            }

            public static int Lift()
            {
                return 2;
            }

            public static int Flex()
            {
                return 3;
            }

            public static int FlatXml()
            {
                return 4;
            }

            public static string FilterString()
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("StructuredXml (*.xml)|*.xml");
                builder.Append("|Lift (*.Lift)|*.lift");
                builder.Append("|Flex (*.Flex)|*.flex");
                builder.Append("|FlatXml (*.xml)|*.xml");

                return builder.ToString();
            }
        }

        public static void FlatXml(string sourceFile, string destinationFile)
        {}

        public static void Lift(string sourceFile, string destinationFile)
        {}

        public static void Flex(string sourceFile, string destinationFile)
        {}

        public static void StructuredXml(string sourceFile, string destinationFile)
        {}
    }
}
