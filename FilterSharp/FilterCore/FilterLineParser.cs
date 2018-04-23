using FilterCore.FilterValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public partial class FilterLine
    {
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
                    if (res) index = j + 1;
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
                    value = value.Substring(0, i + 1);
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
    }
}
