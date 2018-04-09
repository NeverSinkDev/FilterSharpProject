using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public class ColorValue : IFilterValue
    {
        public List<int> ColorValueList { get; set; }

        public ColorValue(int r, int g, int b, int? a)
        {
            var res = new List<int>(4) { r, g, b };
            if (a.HasValue) res.Add(a.Value);
            this.ColorValueList = res;
        }

        public ColorValue(List<int> colors)
        {
            this.ColorValueList = colors;
        }

        public ColorValue(string value)
        {
            var splits = value.Split(' ');
            this.ColorValueList = splits.Select(x => Int32.Parse(x)).ToList();
        }

        public IFilterValue Clone()
        {
            return new ColorValue(new List<int>(this.ColorValueList));
        }

        public string CompileToText()
        {
            return String.Join(" ", this.ColorValueList);
        }

        public bool Equals(IFilterValue other)
        {
            if (other is ColorValue)
            {
                return (other as ColorValue).ColorValueList.SequenceEqual(this.ColorValueList);
            }
            return false;
        }

        public bool Validate()
        {
            if (this.ColorValueList.Count < 3 || this.ColorValueList.Count > 4) return false;
            var r = this.ColorValueList[0] >= 0 && this.ColorValueList[0] <= 255;
            var g = this.ColorValueList[1] >= 0 && this.ColorValueList[1] <= 255;
            var b = this.ColorValueList[2] >= 0 && this.ColorValueList[2] <= 255;
            var a = this.ColorValueList.Count == 3 || (this.ColorValueList[3] >= 0 && this.ColorValueList[3] <= 255);
            return r && g && b && a;
        }

        public virtual string GetStringIdent()
        {
            throw new Exception();
        }
    }

    // CONCRETE COLOR OBJECTS
    // -> simply inheriting from ColorValue
    public class TextColor : ColorValue { public TextColor(string value) : base(value) { } public override string GetStringIdent() => "SetTextColor"; }
    public class BorderColor : ColorValue { public BorderColor(string value) : base(value) { } public override string GetStringIdent() => "SetBorderColor"; }
    public class BackgroundColor : ColorValue { public BackgroundColor(string value) : base(value) { } public override string GetStringIdent() => "SetBackgroundColor"; }
}
