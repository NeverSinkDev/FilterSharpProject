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
}
