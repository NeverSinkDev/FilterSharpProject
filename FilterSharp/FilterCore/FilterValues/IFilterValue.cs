using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public interface IFilterValue
    {
        bool Validate(); // called whenever you change the value
        string CompileToText();
        bool Equals(IFilterValue other);
        IFilterValue Clone();
    }
}
