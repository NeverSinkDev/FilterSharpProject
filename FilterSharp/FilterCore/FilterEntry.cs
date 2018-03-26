using FilterCore.FilterValues;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    [DebuggerDisplay("{DataType} with {LineList.Count} lines: {this.CompileToText()[0]}")]
    public class FilterEntry : IFilterEntry
    {
        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { this.SetEnabled(value); }
        }
        public EntryDataType DataType { get; }
        public List<IFilterLine> LineList { get; set; } = new List<IFilterLine>();

        private List<IFilterLine> InitialLineList;

        public FilterEntry(EntryDataType type)
        {
            this.DataType = type;
        }        

        public List<T> GetAllValues<T>() where T : IFilterValue
        {
            return this.LineList.Where(x => x.Value is T).Select(x => x.Value) as List<T>;
        }

        public T GetValue<T>(int nr = 0) where T : IFilterValue
        {
            return (T) this.GetLine<T>(nr).Value;
        }

        public int GetValueTypeCount<T>()
        {
            return this.LineList.Where(x => x.Value is T).Count();
        }

        public void SetValue<T>(IFilterValue value, int nr = 0) where T : IFilterValue
        {
            var line = this.GetLine<T>(nr);

            if (line != null)
            {
                line.Value = value;
                line.Validate();
                return;
            }

            line = new FilterLine("")
            {
                Value = value,
                Ident = null
            };
        }

        public void RemoveLine<T>(int nr = 0) where T : IFilterValue
        {
            this.LineList.Remove(this.GetLine<T>(nr));
        }

        public void Init()
        {
            this.InitialLineList = new List<IFilterLine>(this.LineList);
        }

        public IFilterEntry Clone()
        {
            var res = new FilterEntry(this.DataType);
            this.LineList.ForEach(x => res.LineList.Add(x.Clone()));
            this.InitialLineList.ForEach(x => res.InitialLineList.Add(x.Clone()));
            res.Enabled = this.Enabled;
            return res;
        }

        public List<string> CompileToText()
        {
            return this.LineList.Select(x => x.CompileToText()).ToList();
        }

        public bool Equals(IFilterEntry entry)
        {
            if (this.Enabled != entry.Enabled) return false;
            return this.LineList.SequenceEqual(entry.LineList);
        }

        public void Reset()
        {
            this.LineList = new List<IFilterLine>(this.InitialLineList);
        }

        public bool Validate()
        {
            return this.LineList.All(x => x.Validate());
        }

        public void SetEnabled(bool enable)
        {
            this.LineList.Where(x => x.LineType == EntryDataType.Rule).ToList().ForEach(x => x.Enabled = enable);
            this.enabled = enable;
        }

        private IFilterLine GetLine<T>(int nr = 0) where T : IFilterValue
        {
            foreach (var line in this.LineList)
            {
                if (line is T)
                {
                    if (nr <= 0)
                    {
                        return line;
                    }
                    else
                    {
                        nr--;
                    }
                }
            }

            return null;
        }
    }

    public interface IFilterEntry
    {
        EntryDataType DataType { get; }
        bool Enabled { get; set; }
        List<IFilterLine> LineList { get; }

        List<string> CompileToText();
        void Init();

        // setter/getter
        T GetValue<T>(int nr = 0) where T : IFilterValue; // get n-th value of given type
        List<T> GetAllValues<T>() where T : IFilterValue; // --> get ALL values for e.g. ItemLevel
        int GetValueTypeCount<T>(); // --> how many e.g. ItemLevel lines this entry has
        void SetValue<T>(IFilterValue value, int nr = 0) where T : IFilterValue;
        void RemoveLine<T>(int nr = 0) where T : IFilterValue;

        bool Equals(IFilterEntry line);
        IFilterEntry Clone();
        void Reset();
        bool Validate();
    }

}
