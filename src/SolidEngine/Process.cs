using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public interface IProcess
    {
        XmlNode Process(XmlNode source, SolidReport report);
    }
}
