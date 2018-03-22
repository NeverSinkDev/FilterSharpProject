using FilterCore.FilterValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public class FilterEntry : IFilterEntry
    {
        // test commit
        // more test commit
        // whoops
        public FilterEntry(EntryDataType type)
        {
            this.DataType = type;
        }

        public EntryDataType DataType { get; }
        public int EntryID => throw new NotImplementedException();
        public bool Enabled { get; set; }
        public List<IFilterLine> LineList { get; set; }

        public IFilterEntry Clone()
        {
            throw new NotImplementedException();
        }

        public List<string> CompileToText()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IFilterEntry line)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllValues<T>()
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>(int nr = 0)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetValueOfType<T>()
        {
            throw new NotImplementedException();
        }

        public T GetValueOfTypeAt<T>(int nr)
        {
            throw new NotImplementedException();
        }

        public T GetValueOfTypeFirst<T>()
        {
            throw new NotImplementedException();
        }

        public int GetValueTypeCount<T>()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
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
        T GetValue<T>(int nr = 0); // get n-th value of given type
        List<T> GetAllValues<T>(); // --> get ALL values for e.g. ItemLevel
        int GetValueTypeCount<T>(); // --> how many e.g. ItemLevel lines this entry has
        // same for set
        // same for remove
        // "HasValue" --> ValueTypeCount != 0

        // todo: where to put/save initial values?
        // e.g. entry has initialValue: Class Currency
        // now we "entry.RemoveLine(Class);"
        // if the initValues are in the line or in the valueObj of the line, it would be lost at this point

        IList<T> GetValueOfType<T>();
        T GetValueOfTypeFirst<T>();
        T GetValueOfTypeAt<T>(int nr);

        bool Equals(IFilterEntry line);
        IFilterEntry Clone();
        void Reset();
    }

}
