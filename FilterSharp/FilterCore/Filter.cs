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

    public class Filter : IFilter
    {
        public Filter(string filePath)
        {
            var parser = new FilterParser();
            //parser.ParseToEntryList()
        }

        public Filter(List<IFilterEntry> entryList)
        {
            this.EntryList = entryList;
        }

        public List<IFilterEntry> EntryList { get; set; }
        public FilterMetaData MetaData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IFilter Clone()
        {
            throw new NotImplementedException();
        }

        public List<string> CompileToText()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IFilter line)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
