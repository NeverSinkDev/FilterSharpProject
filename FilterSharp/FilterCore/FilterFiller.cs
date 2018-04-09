using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public class FilterFiller : IFilterEntry
    {
        public EntryDataType DataType { get; set; } = EntryDataType.Comment;
        public List<IFilterLine> LineList { get; set; } = new List<IFilterLine>();

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { if (value) throw new Exception("cannot enable comment"); enabled = value; }
        }

        public FilterFiller()
        {

        }

        public IFilterEntry Clone()
        {
            return new FilterComment(new List<IFilterLine>(this.LineList));
        }

        public string CompileToText()
        {
            var res = "";
            this.LineList.ForEach(l => res += l.CompileToText());
            return res;
        }

        public bool Equals(IFilterEntry line)
        {
            if (line is FilterFiller)
            {
                return this.LineList.SequenceEqual((line as FilterComment).LineList);
            }
            return false;
        }

        public void Init()
        {
            // nope
        }

        public void Reset()
        {
            // no "set" -> no reset
        }

        public bool Validate()
        {
            return !this.Enabled && this.LineList.All(x => !x.Enabled);
        }
    }
}
