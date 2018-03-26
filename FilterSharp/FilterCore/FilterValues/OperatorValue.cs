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

        public OperatorValue(string value)
        {

        }

        public virtual bool Validate() { throw new NotImplementedException(); }

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
        public ItemLevelValue(string value) : base(value) { }

        public override bool Validate()
        {
            return true;
            // todo
        }
    }

    public class DropLevelValue : OperatorValue
    {
        public DropLevelValue(string value) : base(value) { }

        public override bool Validate()
        {
            return true;
            // todo
        }
    }

    public class Quality : OperatorValue
    {
        public Quality(string value) : base(value) { }

        public override bool Validate()
        {
            // todo
            return true;
        }
    }

    public class Sockets : OperatorValue
    {
        public Sockets(string value) : base(value) { }

        public override bool Validate()
        {
            // todo
            return true;
        }
    }

    public class LinkedSockets : OperatorValue
    {
        public LinkedSockets(string value) : base(value) { }

        public override bool Validate()
        {
            // todo
            return true;
        }
    }

    public class Height : OperatorValue
    {
        public Height(string value) : base(value) { }

        public override bool Validate()
        {
            // todo
            return true;
        }
    }

    public class Width : OperatorValue
    {
        public Width(string value) : base(value) { }

        public override bool Validate()
        {
            // todo
            return true;
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
