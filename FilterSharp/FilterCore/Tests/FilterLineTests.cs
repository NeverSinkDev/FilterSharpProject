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
        public static void FLineAcceptanceTest()
        {
            var line = new FilterLine("Show");
            //Assert.AreEqual(line.DataType, EntryDataType.Rule);
        }
    }
}
