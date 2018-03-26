using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public class FontSizeValue : IFilterValue
    {
        public int Value { get; set; }

        public FontSizeValue(string value)
        {
            this.Value = Int32.Parse(value);
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
            return this.Value >= 18 && this.Value <= 45;
        }
    }

    public class VoidValue : IFilterValue
    {
        public VoidValue(string value)
        {
            if (value != "")
            {
                throw new Exception("invalid voidValue: " + value);
            }
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
            throw new NotImplementedException();
        }
    }
}
