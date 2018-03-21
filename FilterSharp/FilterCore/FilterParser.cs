using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public class FilterParser
    {
        public IFilter Parse(List<string> stringList)
        {
            // what to return? filter? entryList?
            return null;
        }

        public List<IFilterEntry> ParseToEntryList(List<string> stringList)
        {
            // concept similar to Lexer & Parser:

            // lexer: reads stringList into a filterLine each
            // also identifying their type (RuleStart/Disable/Comment/Filler/RuleContent)
            // maybe also already parse: intro, ident, value, outro, comment, ...

            // parser: reads and interprets those filterLines
            // build them into FilterEntries

            return null;
        }
    }
}
