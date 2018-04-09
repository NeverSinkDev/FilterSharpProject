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

        private string raw;

        public OperatorValue(int value, string op)
        {
            this.Value = value;
            this.Operator = op;
            this.Validate();
        }

        public OperatorValue(string value)
        {
            this.raw = value;

            if (value[0] == '<' || value[0] == '>' || value[0] == '=')
            {
                // with operator
                var temp = value.Split(' ');
                var splits = temp.Where(x => x != "").ToList();
                if (splits.Count != 2) throw new Exception();

                this.Operator = splits[0];
                this.Value = Int32.Parse(splits[1]);
            }

            else
            {
                // direct value
                this.Value = Int32.Parse(value);
                this.Operator = "=";
            }
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
            throw new Exception("todo");
        }

        public virtual string GetStringIdent() => throw new Exception(); 
    }

    public class ItemLevelValue : OperatorValue
    {
        public ItemLevelValue(string value) : base(value) { }

        public override bool Validate()
        {
            return this.Value <= 100 && this.Value >= 1;
        }

        public override string GetStringIdent() => "ItemLevel";
    }

    public class DropLevelValue : OperatorValue
    {
        public DropLevelValue(string value) : base(value) { }

        public override bool Validate()
        {
            return this.Value <= 100 && this.Value >= 1;
        }

        public override string GetStringIdent() => "DropLevel";
    }

    public class Quality : OperatorValue
    {
        public Quality(string value) : base(value) { }

        public override bool Validate()
        {
            return this.Value <= 33 && this.Value >= 0;
        }

        public override string GetStringIdent() => "Quality";
    }

    public class Sockets : OperatorValue
    {
        public Sockets(string value) : base(value) { }

        public override bool Validate()
        {
            return this.Value <= 6 && this.Value >= 1;
        }

        public override string GetStringIdent() => "Sockets";
    }

    public class LinkedSockets : OperatorValue
    {
        public LinkedSockets(string value) : base(value) { }

        public override bool Validate()
        {
            return this.Value <= 6 && this.Value >= 1;
        }

        public override string GetStringIdent() => "LinkedSockets";
    }

    public class Height : OperatorValue
    {
        public Height(string value) : base(value) { }

        public override bool Validate()
        {
            return this.Value <= 4 && this.Value >= 1;
        }

        public override string GetStringIdent() => "Height";
    }

    public class Width : OperatorValue
    {
        public Width(string value) : base(value) { }

        public override bool Validate()
        {
            return this.Value <= 2 && this.Value >= 1;
        }

        public override string GetStringIdent() => "Width";
    }
}
