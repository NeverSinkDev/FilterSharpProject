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
        EntryDataType LineType { get; }

        bool Equals(IFilterLine line);
        IFilterLine Clone();
        void Reset();

        void Init(); // parse input string

        List<string> CompileToText();
    }

    public class FilterLine : IFilterLine
    {
        private string raw;

        public FilterLine(string rawText)
        {
            this.raw = rawText;
        }

        public FilterIdent Ident => throw new NotImplementedException();

        public IFilterValue Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Comment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public EntryDataType LineType => throw new NotImplementedException();

        public IFilterLine Clone()
        {
            throw new NotImplementedException();
        }

        public List<string> CompileToText()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IFilterLine line)
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            this.ParseRawString(this.raw);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        private void ParseRawString(string raw)
        {
            // parse this.raw and detect:
            // type (Rule, Comment, Filler),
            // ident,
            // value,
            // enable
            // intro, outro, comment
        }
    }
}
