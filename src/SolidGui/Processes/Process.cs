using System.Xml;
using SolidGui.Engine;

namespace SolidGui.Processes
{
    public interface IProcess
    {
        XmlNode Process(XmlNode source, SolidReport report);
    }
}