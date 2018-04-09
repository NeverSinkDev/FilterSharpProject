using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public class FilterComment : IFilterEntry
    {
        public EntryDataType DataType { get; set; } = EntryDataType.Comment;
        public List<IFilterLine> LineList { get; set; } = new List<IFilterLine>();

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { if (value) throw new Exception("cannot enable comment"); enabled = value; }
        }

        public FilterComment()
        {

        }

        public FilterComment(List<IFilterLine> lineList)
        {
            this.LineList = lineList;
        }

        public IFilterEntry Clone()
        {
            return new FilterComment(new List<IFilterLine>(this.LineList));
        }

        public string CompileToText()
        {
            var strB = new StringBuilder(100);
            this.LineList.ForEach(l => strB.Append(l.CompileToText()));
            return strB.ToString();
        }

        public bool Equals(IFilterEntry line)
        {
            if (line is FilterComment)
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
