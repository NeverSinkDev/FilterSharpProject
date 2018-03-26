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
        // initialValue object?
        string Comment { get; set; } // maybe list of strings or something. for the commands/tags etc.
        EntryDataType LineType { get; }

        bool Equals(IFilterLine line);
        IFilterLine Clone();
        void Reset();

        void Init(); // parse input string

        string CompileToText();
    }

    [DebuggerDisplay("line: {raw}")]
    public class FilterLine : IFilterLine
    {
        public string Ident { get; set; }
        public IFilterValue Value { get; set; }
        public string Comment { get; set; }
        public EntryDataType LineType { get; set; }

        private bool enabled;
        public bool Enabled { get { return enabled; } }

        private readonly string raw;

        public FilterLine(string rawText)
        {
            this.raw = rawText;
        }

        public IFilterLine Clone()
        {
            throw new NotImplementedException();
        }

        public string CompileToText()
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

            var line = raw.Replace("\t", " ").Trim();

            if (line == "")
            {
                this.LineType = EntryDataType.Filler;
                return;
            }

            // raw can be:            
            // [#] line [# comment]
            // # comment
            
            if (line.First() == '#')
            {
                // line is disabled or comment
                this.enabled = false;

                line = line.Substring(1).Trim();

                if (line.Length == 0)
                {
                    // empty comment line
                    this.LineType = EntryDataType.Filler;
                    return;
                }
            }

            // collect first word
            var firstWord = "";
            foreach (var c in line)
            {
                if (c == ' ') break;
                firstWord += c;
            }
            var ident = new FilterIdent(firstWord);

            if (!ident.IsLegitIdent)
            {
                // line is comment
                this.ParseComment(line);
                return;
            }

            // line is definitely an actual rule line by now
            this.LineType = EntryDataType.Rule;
            this.Ident = ident.Ident;

            line = line.Substring(firstWord.Length).Trim();

            // find and parse comment at the end
            if (line.Contains('#'))
            {
                var index = line.IndexOf('#');
                var comment = line.Substring(index);
                this.ParseComment(comment);

                line = line.Substring(0, index).Trim();
            }

            if (ident.HasNoValue)
            {
                this.ParseComment(line);
                return;
            }

            // "line" is now the value for the ident
            if (!this.ParseValue(line, ident))
            {
                this.MarkAsComment();
            }
        }

        private void ParseComment(string comment)
        {
        }

        private void MarkAsComment()
        {

        }

        private bool ParseValue(string value, FilterIdent ident)
        {
            var fac = new FilterValueFactory();
            var val = fac.GenerateFilterValue(ident, value);

            if (val.Validate())
            {
                this.Value = val;
                return true;
            }

            return false;
        }
    }
}
