using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public interface IFilter
    {
        List<IFilterEntry> EntryList { get; set; }
        FilterMetaData MetaData { get; set; } // strictness, style, version, name, ...

        List<string> CompileToText();

        bool Equals(IFilter line);
        IFilter Clone();
        void Reset();
    }

    public class Filter
    {
        public Filter(string filePath)
        {
            var parser = new FilterParser();
            //parser.ParseToEntryList()
        }

        public Filter(List<IFilterEntry> entryList)
        {

        }

    }
}
