using System.Xml;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Processes
{
    public interface IProcess
    {
        SfmLexEntry Process(SfmLexEntry lexEntry, SolidReport report);
    }
}