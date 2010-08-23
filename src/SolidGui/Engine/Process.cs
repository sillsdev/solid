using System.Xml;

namespace SolidGui.Engine
{
    public interface IProcess
    {
        XmlNode Process(XmlNode source, SolidReport report);
    }
}