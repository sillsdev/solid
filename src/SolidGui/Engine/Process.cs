using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Solid.Engine
{
    public interface IProcess
    {
        XmlNode Process(XmlNode source, SolidReport report);
    }
}
