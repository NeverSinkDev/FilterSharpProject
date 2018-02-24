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

        public IFilterValue Clone()
        {
            return new ColorValue(new List<int>(this.ColorValueList));
        }

        public string CompileToText()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IFilterValue other)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
