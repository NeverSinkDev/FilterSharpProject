using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.Tests
{
    [TestFixture]
    public class FilterTests
    {
        [Test]
        public static void FilterExceptionTest()
        {
            var url = "http://www.filterblade.xyz/datafiles/filters/NeverSink/Normal/NeverSink's%20filter%20-%201-REGULAR.filter";
            var client = new WebClient();
            var s = client.DownloadString(url);
            var stringList = s.Split('\n');

            var parser = new FilterParser();
            var entryList = parser.ParseToEntryList(stringList.ToList());
            var filter = new Filter(entryList);

            Assert.IsTrue(filter.EntryList.Count > 400);

            filter.Validate();
            var text = filter.CompileToText();
            filter.Reset();

            System.IO.File.WriteAllText("C:\\Users\\Tobnac\\Desktop\\myFilter.txt", String.Join("\n", text));
        }
    }
}
