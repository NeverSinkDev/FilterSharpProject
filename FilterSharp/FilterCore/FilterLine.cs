using FilterCore.FilterValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public interface IFilterLine
    {
        FilterIdent Ident { get; }
        IFilterValue Value { get; set; }
        // initialValue object?
        string Comment { get; set; } // maybe list of strings or something. for the commands/tags etc.

        bool Equals(IFilterLine line);
        IFilterLine Clone();
        void Reset();

        List<string> CompileToText();
    }

    public class FilterLine
    {
    }
}
