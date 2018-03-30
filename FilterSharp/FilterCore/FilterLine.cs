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
    public class FilterLine : IFilterLine
    {
        public FilterIdent Ident { get; set; }
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
            if (this.LineType == EntryDataType.Comment)
            {
                return "# " + this.Comment;
            }

            if (this.LineType == EntryDataType.Filler)
            {
                return "";
            }

            var comment = this.Enabled ? "" : "# ";
            //var intro = this.Ident.Ident == "Show" || this.Ident.Ident == "Hide" ? "" : "\t";
            var intro = this.Intro;
            return $"{comment}{intro}{this.Ident.Ident} {this.Value.CompileToText()}{this.Outro}{this.Comment}";
        }

        public bool Equals(IFilterLine line)
        {
            if (this.Ident.Ident != line.Ident.Ident) return false;
            if (this.Enabled != line.Enabled) return false;
            return this.Value.Equals(line.Value);
        }

        public void Init()
        {
            this.ParseRawString(this.raw);
            this.IdentifiyIntroOutro();
        }

        private void IdentifiyIntroOutro()
        {
            if (this.LineType != EntryDataType.Rule) return;

            var index = this.raw.IndexOf(this.Ident.Ident);
            this.Intro = this.raw.Substring(0, index);

            //var v = this.Value.CompileToText();
            //index = this.raw.IndexOf(v) + v.Length;
            //this.Outro = this.raw.Substring(index);
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
                this.Enabled = false;

                line = line.Substring(1).Trim();

                if (line.Length == 0)
                {
                    // empty comment line
                    this.LineType = EntryDataType.Comment;
                    return;
                }
            }
            else
            {
                this.Enabled = true;
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
                if (this.Enabled) throw new Exception("invalid ident");

                // line is comment
                this.ParseComment(line);
                return;
            }

            // line is definitely an actual rule line by now
            this.LineType = EntryDataType.Rule;
            this.Ident = ident;

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
                this.Value = new VoidValue("");
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
            this.Comment += comment;
        }

        private void MarkAsComment()
        {
            this.LineType = EntryDataType.Comment;
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
