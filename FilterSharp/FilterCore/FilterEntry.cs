using FilterCore.FilterValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public class FilterEntry
    {
        // test commit
        // more test commit
        // whoops
    }

    public interface IFilterEntry
    {
        EntryDataType DataType { get; }
        int EntryID { get; }
        bool Enabled { get; set; }
        List<IFilterLine> LineList { get; }

        List<string> CompileToText();

        // setter/getter
        //T GetValue<T>(FilterIdent ident); // normal getter for type/ident --> unnecessary because overload
        T GetValue<T>(FilterIdent ident, int nr = 0); // get n-th value of given type
        List<T> GetAllValues<T>(FilterIdent ident); // --> get ALL values for e.g. ItemLevel
        int GetValueTypeCount<T>(FilterIdent ident); // --> how many e.g. ItemLevel lines this entry has
        // same for set
        // same for remove
        // "HasValue" --> ValueTypeCount != 0

        IList<T> GetValueOfType<T>();
        T GetValueOfTypeFirst<T>();
        T GetValueOfTypeAt<T>(int nr);

        bool Equals(IFilterEntry line);
        IFilterEntry Clone();
        void Reset();
    }

}
