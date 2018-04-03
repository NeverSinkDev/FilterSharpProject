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
                return "#" + this.Comment;
            }

            else if (this.LineType == EntryDataType.Filler)
            {
                return "";
            }

            var result = "";
            result += this.Enabled ? "" : "# ";
            result += this.Intro;
            result += this.Ident.Ident;

            if (this.Comment != "" || this.Outro != "" || !(this.Value is VoidValue))
            {
                result += " ";
            }

            result += this.Value.CompileToText();
            result += this.Outro;
            result += this.Comment;
            return result;
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
        }

        private void ParseRawString(string raw)
        {
            var line = raw;
            var index = 0;

            if (line == "")
            {
                this.LineType = EntryDataType.Filler;
                return;
            }        
            
            if (PeekNextChar() == '#')
            {
                GetNextChar();
                this.Enabled = false;                

                // empty comment line
                if (line.Length == index)
                {                   
                    this.LineType = EntryDataType.Comment;
                    this.Comment = "";
                    return;
                }
            }
            else
            {
                this.Enabled = true;
            }

            var ident = new FilterIdent(PeekNextWord());
            if (!ident.IsLegitIdent)
            {
                if (this.Enabled) throw new Exception("invalid ident");

                // line is comment
                this.LineType = EntryDataType.Comment;
                this.ParseComment(GetRemaining());
                return;
            }

            // "delete/skip" ident
            GetNextWord();

            // line is definitely an actual rule line by now
            this.LineType = EntryDataType.Rule;
            this.Ident = ident;

            // parse comment at the end of line
            var remaining = GetRemaining();
            if (remaining.Contains('#'))
            {
                var tempIndex = remaining.IndexOf('#');
                var comment = remaining.Substring(tempIndex);
                this.ParseComment(comment);

                remaining = remaining.Substring(0, tempIndex);
            }

            if (ident.HasNoValue)
            {
                this.Value = new VoidValue("");
                this.ParseComment(remaining);
                return;
            }

            // "remaining" is now the value for the ident
            if (!this.ParseValue(remaining, ident))
            {
                this.MarkAsComment();
            }

            ////// UTILITY FUNCTIONS //////////

            string GetNextWord()
            {
                var buffer = "";

                for (int j = index; index < line.Length; index++)
                {
                    if (line[index] == ' ' || line[index] == '\t')
                    {
                        // spaces before word -> skip those
                        if (buffer == "") continue;

                        // space after word -> end of word -> return
                        break;
                    }

                    buffer += line[index];
                }

                return buffer;
            }

            string PeekNextWord()
            {
                var buffer = "";

                for (int j = index; j < line.Length; j++)
                {
                    if (line[j] == ' ' || line[j] == '\t')
                    {
                        // spaces before word -> skip those
                        if (buffer == "") continue;

                        // space after word -> end of word -> return
                        break;
                    }

                    buffer += line[j];
                }

                return buffer;
            }

            char GetNextChar()
            {
                for (int j = index; index < line.Length; index++)
                {
                    if (line[index] == ' ' || line[index] == '\t')
                    {
                        continue;
                    }

                    return line[index];
                }

                throw new Exception("end of line");
            }

            char PeekNextChar()
            {
                for (int j = index; j < line.Length; j++)
                {
                    if (line[j] == ' ' || line[j] == '\t')
                    {
                        continue;
                    }

                    return line[j];
                }

                throw new Exception("end of line");
            }

            string GetRemaining()
            {
                // basically line.Substring(i+1) with failSave...
                var res = "";

                for (int j = index+1; j < line.Length; j++)
                {
                    res += line[j];
                }

                return res;
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
            // sanatise value string
            var outro = "";
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (value[i] == ' ' || value[i] == '\t')
                {
                    outro += value[i];
                }

                else
                {
                    this.Outro = outro;
                    value = value.Substring(0, i+1);
                    break;
                }
            }

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
