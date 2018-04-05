using FilterCore.FilterValues;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public interface IFilterEntry
    {
        List<IFilterLine> LineList { get; set; }
        EntryDataType DataType { get; set; }
        bool Enabled { get; set; }

        List<string> CompileToText();
        void Init();

        bool Equals(IFilterEntry line);
        IFilterEntry Clone();
        void Reset();
        bool Validate();
    }

    public static class FilterEntryFactory
    {
        public static IFilterEntry GenerateEntryOfType(EntryDataType type)
        {
            switch(type)
            {
                case EntryDataType.Comment:
                    return new FilterComment();

                case EntryDataType.Filler:
                    return new FilterFiller();

                case EntryDataType.Rule:
                    return new FilterRule();
            }

            throw new Exception("invalid data type");
        }
    }

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

        public List<string> CompileToText()
        {
            return this.LineList.Select(x => x.CompileToText()).ToList();
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

        public List<string> CompileToText()
        {
            return this.LineList.Select(x => x.CompileToText()).ToList();
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
