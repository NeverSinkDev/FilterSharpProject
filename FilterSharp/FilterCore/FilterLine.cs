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
        public FilterIdent Ident { get; set; }
        public IFilterValue Value { get; set; }
        public string Comment { get; set; }
        public EntryDataType LineType { get; set; }

        private readonly string raw;

        public FilterLine(string rawText)
        {
            this.raw = rawText;
        }

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

            raw = raw.Replace("\t", "");

            if (raw == "")
            {
                this.LineType = EntryDataType.Filler;
                return;
            }

            // raw can be:            
            // [#] line [# comment]
            // # comment

            var line = this.raw.Trim();
            if (line.First() == '#')
            {
                // line is disabled or comment

                line = line.Substring(1).Trim();

                if (line.Length == 0)
                {
                    // line is fillter // empty comment
                    return;
                }
            }

            var ident = "";
            foreach (var c in line)
            {
                if (c == ' ') break;
                ident += c;
            }

            if (!FilterHelper.IdentList.Contains(ident))
            {
                //Trace.Write("----> invalid ident: " + ident + "\n");
                // line is comment
                return;
            }

            // line is definitely an actual rule line by now
            this.LineType = EntryDataType.Rule;
            this.Ident = FilterHelper.TranslateStringToIdent(ident);

            line = line.Substring(ident.Length).Trim();

            if (line.Contains('#'))
            {
                var index = line.IndexOf('#');
                var comment = line.Substring(index);
                // parse comment

                line = line.Substring(0, index).Trim();
            }

            if (line.Length == 0)
            {
                // done
                return;
            }

            if (this.Ident == FilterIdent.Show || this.Ident == FilterIdent.Hide)
            {
                // parse comment (line)
                return;
            }

            // "line" is now the value for the ident

            this.ParseValue(line);
        }

        private void ParseValue(string value)
        {
            // todo

            switch (this.Ident)
            {
                case FilterIdent.BaseType:
                case FilterIdent.Class:
                    break;
            }
        }
    }
}
