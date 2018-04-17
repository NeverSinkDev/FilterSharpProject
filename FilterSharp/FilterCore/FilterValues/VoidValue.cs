using System;

namespace FilterCore.FilterValues
{
    public class VoidValue : IFilterValue
    {
        public string Raw { get; set; }

        public VoidValue(string value)
        {
            this.Raw = value;
        }

        public IFilterValue Clone()
        {
            return new VoidValue(this.Raw);
        }

        public string CompileToText()
        {
            return "";
        }

        public bool Equals(IFilterValue other)
        {
            return other is VoidValue;
        }

        public bool Validate()
        {
            var res = this.Raw == "";
            if (!res) throw new Exception();
            return res;
        }

        public string GetStringIdent() => "";
    }
}
