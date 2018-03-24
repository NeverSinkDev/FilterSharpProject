using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.Tests
{
    [TestFixture]
    public class FilterLineTests
    {
        [Test]
        public static void FLineBasicTest()
        {
            var line = new FilterLine("Show");
            line.Init();
            Assert.AreEqual(line.LineType, EntryDataType.Rule);
        }
    }
}
