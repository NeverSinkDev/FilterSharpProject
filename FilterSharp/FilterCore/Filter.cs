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
        IEnumerable<FilterRule> RuleList { get; set; }

        string CompileToText();
        bool Equals(IFilter line);
        IFilter Clone();
        void Reset();
    }

    public class Filter : IFilter
    {
        public List<IFilterEntry> EntryList { get; set; }
        public FilterMetaData MetaData { get; set; }

        private IEnumerable<FilterRule> ruleList;
        public IEnumerable<FilterRule> RuleList
        {
            get
            {
                if (ruleList == null) ruleList = this.EntryList.Where(x => x is FilterRule) as IEnumerable<FilterRule>;
                return ruleList;
            }
            set { ruleList = value; }
        }

        public Filter(List<IFilterEntry> entryList)
        {
            this.EntryList = entryList;
        }

        public bool Validate()
        {
            var valid = this.EntryList.All(x => x.Validate());
            if (!valid) throw new Exception("filter is not valid");
            return valid;
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
