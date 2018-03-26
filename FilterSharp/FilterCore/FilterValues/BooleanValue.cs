using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public class BooleanValue : IFilterValue
    {
        public string StringValue { get; set; }
        public bool BoolValue { get; set; }

        public BooleanValue(string boolString)
        {         
            if (boolString == "True")
            {
                this.BoolValue = true;
                this.StringValue = boolString;
            }
            else if (boolString == "False")
            {
                this.BoolValue = false;
                this.StringValue = boolString;
            }
        }

        public BooleanValue(bool boolValue)
        {
            this.BoolValue = boolValue;
            this.StringValue = boolValue ? "True" : "False";
        }

        public IFilterValue Clone()
        {
            throw new NotImplementedException();
        }

        public string CompileToText()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IFilterValue other)
        {
            throw new NotImplementedException();
        }

        public bool Validate()
        {
            return this.StringValue != null;
        }
    }

    public class Identified : BooleanValue { public Identified(string boolString) : base(boolString) { } }
    public class Corrupted : BooleanValue { public Corrupted(string boolString) : base(boolString) { } }
    public class ShaperItem : BooleanValue { public ShaperItem(string boolString) : base(boolString) { } }
    public class ElderItem : BooleanValue { public ElderItem(string boolString) : base(boolString) { } }
    public class ShapedMap : BooleanValue { public ShapedMap(string boolString) : base(boolString) { } }
    public class ElderMap : BooleanValue { public ElderMap(string boolString) : base(boolString) { } }
}
