// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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