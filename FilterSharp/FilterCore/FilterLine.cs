using FilterCore.FilterValues;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public interface IFilterLine
    {
        string Ident { get; }
        IFilterValue Value { get; set; }
        string Comment { get; set; } // maybe list of strings or something. for the commands/tags etc.
        EntryDataType LineType { get; }
        bool Enabled { get; set; }

        bool Equals(IFilterLine line);
        IFilterLine Clone();

        void Init(); // parse input string

        string CompileToText();
        bool Validate();
    }

    [DebuggerDisplay("line: {this.CompileToText()}")]
    public partial class FilterLine : IFilterLine
    {
        public string Ident { get; set; }
        public IFilterValue Value { get; set; }
        public string Comment { get; set; } = "";
        public EntryDataType LineType { get; set; }        
        public bool Enabled { get; set; }
        public string Intro { get; set; } = "";
        public string Outro { get; set; } = "";

        private readonly string raw;

        public FilterLine(string rawText)
        {
            this.raw = rawText;
        }

        public IFilterLine Clone()
        {
            var res = new FilterLine(this.raw);
            res.Init();
            res.Value = this.Value;
            return res;
        }

        public string CompileToText()
        {
            var result = "";

            if (this.LineType == EntryDataType.Comment)
            {
                result = $"#{this.Intro}{this.Comment}";
            }

            else if (this.LineType == EntryDataType.Filler)
            {
                result = "";
            }

            else
            {
                result = this.Enabled ? "" : "#";
                result += this.Intro;
                result += this.Ident;

                if (this.Comment != "" || this.Outro != "" || !(this.Value is VoidValue || this.Value is ShowHideValue))
                {
                    result += " ";
                }

                result += this.Value.CompileToText();
                result += this.Outro;
                result += this.Comment;
            }

            return result + "\r\n";
        }

        public bool Equals(IFilterLine line)
        {
            if (this.Ident != line.Ident) return false;
            if (this.Enabled != line.Enabled) return false;
            return this.Value.Equals(line.Value);
        }

        public void Init()
        {
            this.ParseRawString(this.raw);
        }

        public bool Validate()
        {
            if (this.LineType == EntryDataType.Rule)
            {
                return this.Value.Validate();
            }

            return true;
        }
    }
}
