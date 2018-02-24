using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public abstract class OperatorValue : IFilterValue
    {        
        public int Value { get; set; }
        public string Operator { get; set; }

        public OperatorValue(int value, string op)
        {
            this.Value = value;
            this.Operator = op;
            this.Validate();
        }

        public virtual void Validate() { throw new NotImplementedException(); }

        public string CompileToText()
        {
            return $"{this.Operator} {this.Value}";
        }

        public bool Equals(IFilterValue otherVal)
        {
            var other = otherVal as OperatorValue;
            return this.Value == other.Value && this.Operator == other.Operator;
        }

        public IFilterValue Clone()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    public class ItemLevelValue : OperatorValue
    {
        public ItemLevelValue(int value, string op) : base(value, op) { }

        public override void Validate()
        {
            // todo
        }
    }

    public class DropLevelValue : OperatorValue
    {
        public DropLevelValue(int value, string op) : base(value, op) { }

        public override void Validate()
        {
            // todo
        }
    }
    
    /* 
     * TODO:
     * ItemLevel
     * DropLevel
     * Quality
     * Rarity
     * Sockets
     * LinkedSockets
     * Height
     * Width
     */
}
