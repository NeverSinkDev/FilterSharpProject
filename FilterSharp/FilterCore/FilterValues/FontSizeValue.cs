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
            return new FontSizeValue(this.Value.ToString());
        }

        public string CompileToText()
        {
            return this.Value.ToString();
        }

        public bool Equals(IFilterValue other)
        {
            if (!(other is FontSizeValue)) return false;
            return (other as FontSizeValue).Value == this.Value;
        }

        public bool Validate()
        {
            return this.Value >= 18 && this.Value <= 45;
        }

        public string GetStringIdent() => "SetFontSize";
    }

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

    public class ShowHideValue : IFilterValue
    {
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

        public string GetStringIdent()
        {
            throw new NotImplementedException();
        }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
