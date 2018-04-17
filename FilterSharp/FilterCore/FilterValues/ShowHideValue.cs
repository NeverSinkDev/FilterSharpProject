using System;

namespace FilterCore.FilterValues
{
    public class ShowHideValue : IFilterValue
    {
        public string Value { get; set; }

        public ShowHideValue(string value)
        {
            if (value == "Show" || value == "Hide")
            {
                this.Value = value;
            }
            else
            {
                throw new Exception("invalid ShowHide value");
            }
        }

        public IFilterValue Clone()
        {
            return new ShowHideValue(this.Value);
        }

        public string CompileToText()
        {
            return "";
        }

        public bool Equals(IFilterValue other)
        {
            return this.Value == (other as ShowHideValue).Value;
        }

        public string GetStringIdent()
        {
            return this.Value;
        }

        public bool Validate()
        {
            return this.Value == "Show" || this.Value == "Hide";
        }
    }
}
