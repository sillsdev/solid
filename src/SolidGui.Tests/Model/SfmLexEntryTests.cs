using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using SolidGui.Model;

namespace SolidGui.Tests.Model
{
    [TestFixture]
    public class SfmLexEntryTests
    {
        [Test]
        public void Name_NotNull()
        {
            var entry = new SfmLexEntry();
            Assert.IsNotNull(entry.Name);
        }

        [Test]
        public void Name_MatchesLxFieldValue()
        {
            const string sfmIn = @"\lx test1";

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            Assert.AreEqual("test1", entry.Name);
        }
    }
}
