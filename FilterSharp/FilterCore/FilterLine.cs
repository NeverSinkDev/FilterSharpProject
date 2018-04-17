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
    public class FilterLine : IFilterLine
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

                if (this.Comment != "" || this.Outro != "" || !(this.Value is VoidValue))
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

        private void ParseRawString(string raw)
        {
            var line = raw;
            var index = 0;

            if (line == "")
            {
                this.LineType = EntryDataType.Filler;
                return;
            }

            // collect pre-# intro spaces
            this.Intro += CollectSpaces();
            
            // check for comment
            if (CheckForChar('#'))
            {
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

            // collect post-# intro spaces
            this.Intro += CollectSpaces();

            // check ident (= first word)
            var ident = GetNextWord();
            if (!FilterIdent.IsLegitIdent(ident))
            {
                if (this.Enabled) throw new Exception("invalid ident");

                // line is comment
                this.LineType = EntryDataType.Comment;
                this.ParseComment(ident + GetRemaining());
                return;
            }

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

            // catch idents without value
            if (FilterIdent.IsValuelessIdent(ident))
            {   
                // catch invalid comment style and identify comments like "# Hide other stuff"
                if (remaining != "" && remaining != " " && ident != "DisableDropSound")
                {
                    if (this.Enabled) throw new Exception("invalid comment style after valueLess ident");

                    this.LineType = EntryDataType.Comment;
                    this.ParseComment(ident + remaining);
                }

                if (ident == "Show" || ident == "Hide")
                {
                    this.Value = new ShowHideValue(ident);
                    return;
                }

                this.Value = new VoidValue("");
                return;
            }

            // "remaining" is now the value for the ident
            if (!this.ParseValue(remaining, ident))
            {
                // invalid value -> mark as comment
                this.Comment = ident + remaining;
                this.MarkAsComment();
            }

            // return;

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

            bool CheckForChar(char key)
            {
                for (int j = index; j < line.Length; j++)
                {
                    if (line[j] == ' ' || line[j] == '\t')
                    {
                        continue;
                    }

                    var res = line[j] == key;
                    if (res) index = j+1;
                    return res;
                }

                throw new Exception("end of line");
                //return false;
            }

            string GetRemaining()
            {
                // basically line.Substring(i+1) with failSave...
                var res = "";

                for (int j = index; j < line.Length; j++)
                {
                    res += line[j];
                }

                return res;
            }

            string CollectSpaces()
            {
                var res = "";

                for (int i = index; index < line.Length; index++)
                {
                    if (line[index] == ' ' || line[index] == '\t')
                    {
                        res += line[index];
                    }
                    else
                    {
                        break;
                    }
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

        private bool ParseValue(string value, string ident)
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

            // skip space between ident and value
            value = value.Substring(1);

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
