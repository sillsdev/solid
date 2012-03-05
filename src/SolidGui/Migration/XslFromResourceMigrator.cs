﻿using System.IO;
using System.Reflection;
using Palaso.Migration;

namespace SolidGui.Migration
{
    internal class XslFromResourceMigrator : XslMigrationStrategy
    {
        public XslFromResourceMigrator(int fromVersion, int toVersion, string xslSource)
            : base(fromVersion, toVersion, xslSource)
        {
        }

        protected override TextReader OpenXslStream(string xslSource)
        {
            string resourceName = "SolidGui.Migration." + xslSource;
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            return new StreamReader(stream);
        }
    }
}