using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public class ListValue : IFilterValue
    {
        public List<string> ValueList { get; set; }

        public ListValue(string value)
        {
            var result = new List<string>();
            var buffer = "";
            var inBrac = false;

            foreach (var c in value)
            {
                switch (c)
                {
                    case '"':
                        inBrac = !inBrac;
                        buffer += c;
                        if (!inBrac)
                        {
                            result.Add(buffer);
                            buffer = "";
                        }
                        break;

                    case ' ':
                        if (inBrac)
                        {
                            buffer += c;
                        }
                        else
                        {
                            if (buffer != "") result.Add(buffer);
                            buffer = "";
                        }
                        break;

                    default:
                        buffer += c;
                        break;
                }
            }

            if (buffer != "") result.Add(buffer);

            this.ValueList = result;
        }

        public IFilterValue Clone()
        {
            var res = new ListValue("")
            {
                ValueList = new List<string>(this.ValueList)
            };
            return res;
        }

        public string CompileToText()
        {
            return String.Join(" ", this.ValueList);
        }

        public bool Equals(IFilterValue other)
        {
            return this.ValueList.SequenceEqual((other as ListValue).ValueList);
        }

        public bool Validate()
        {
            return this.ValueList.Count > 0;
        }
    }

    public class BaseType : ListValue { public BaseType(string value) : base(value) { } }
    public class Class : ListValue { public Class(string value) : base(value) { } }
    public class SocketGroup : ListValue { public SocketGroup(string value) : base(value) { } }
}
