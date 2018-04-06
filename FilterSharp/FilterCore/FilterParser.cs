using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public class FilterParser
    {
        public List<IFilterEntry> ParseToEntryList(List<string> stringList)
        {
            // concept similar to Lexer & Parser:

            // lexer: reads stringList into a filterLine each
            // also identifying their type (RuleStart/Disable/Comment/Filler/RuleContent)
            // maybe also already parse: intro, ident, value, outro, comment, ...

            // parser: reads and interprets those filterLines
            // build them into FilterEntries

            var lineList = this.BuildLineList(stringList);
            var entryList = this.BuildEntryList(lineList);
            entryList.ForEach(x => x.Init());
            return entryList;
        }

        //////////////////////////////////////////////////

        private List<IFilterLine> BuildLineList(List<string> stringList)
        {
            var resultList = new List<IFilterLine>(stringList.Count);

            stringList.ForEach(rawLine => resultList.Add(new FilterLine(rawLine)));
            resultList.ForEach(line => line.Init());

            return resultList;
        }

        private List<IFilterEntry> BuildEntryList(List<IFilterLine> lineList)
        {
            var resultList = new List<IFilterEntry>(lineList.Count / 5);
            IFilterEntry currentEntry = null;

            foreach (var line in lineList)
            {
                // init entry or create new one when finding new Show/Hide ident
                if (currentEntry == null || (line.LineType == EntryDataType.Rule && (line.Ident == "Show" || line.Ident == "Hide")))
                {
                    currentEntry = FilterEntryFactory.GenerateEntryOfType(line.LineType);
                    currentEntry.Enabled = line.Enabled;
                    resultList.Add(currentEntry);
                }

                currentEntry.LineList.Add(line);
            }

            return this.CleanUpEntryList(resultList);
        }

        private List<IFilterEntry> CleanUpEntryList(List<IFilterEntry> entryList)
        {
            // group non-rules between rules to their own entry(s)
            // re-build and optimize structure

            // when finding "Rule" entry:
            // -> remove the last few non-Rule lines from list
            // ---> create and add new entry with those non-Rule lines

            var resultList = new List<IFilterEntry>(entryList.Count);
            //EntryDataType? currentType = null;

            for (int i = 0; i < entryList.Count; i++)
            {
                var entry = entryList[i];

                // skip non-Rule entries
                // (should probably only be the first one)
                if (entry.DataType != EntryDataType.Rule)
                {
                    resultList.Add(entry);
                    continue;
                }

                var endComments = new List<IFilterLine>();

                // iterate lines from behind and collect all non-rule lines at the end of the entry
                for (int j = entry.LineList.Count - 1; j >= 0; j--)
                {
                    var line = entry.LineList[j];

                    if (line.LineType == EntryDataType.Rule)
                    {
                        break;
                    }
                    
                    endComments.Insert(0, line);
                }

                // skip if there were no endComments anyway
                if (endComments.Count == 0)
                {
                    continue;
                }

                // delete endComments from original entry
                entry.LineList.RemoveRange(entry.LineList.Count - endComments.Count, endComments.Count);
                resultList.Add(entry);

                // add new entry with the comments
                var commentEntry = new FilterComment(endComments);
                resultList.Add(commentEntry);
            }

            //Trace.Write($"refactored filter structure from {entryList.Count} to {resultList.Count} entries");

            return resultList;
        }

    }
}
