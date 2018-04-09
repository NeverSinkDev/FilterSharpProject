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

        string CompileToText();
        bool Equals(IFilter line);
        IFilter Clone();
        void Reset();
    }

    public class Filter : IFilter
    {
        public List<IFilterEntry> EntryList { get; set; }
        public FilterMetaData MetaData { get; set; }

        public Filter(List<IFilterEntry> entryList)
        {
            this.EntryList = entryList;
        }

        public bool Validate()
        {
            return this.EntryList.All(x => x.Validate());
        }

        public IFilter Clone()
        {
            throw new NotImplementedException();
        }

        public string CompileToText()
        {
            var strB = new StringBuilder(5000);
            this.EntryList.ForEach(e => strB.Append(e.CompileToText()));
            return strB.ToString();
        }

        public bool Equals(IFilter filter)
        {
            return this.EntryList.SequenceEqual(filter.EntryList);
        }

        public void Reset()
        {
            this.EntryList.ForEach(x => x.Reset());
        }
    }
}
