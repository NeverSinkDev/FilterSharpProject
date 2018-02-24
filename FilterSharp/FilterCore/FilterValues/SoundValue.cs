using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public class SoundValue : IFilterValue
    {
        public string SoundID { get; set; }
        public int? Volume { get; set; }

        public SoundValue(String soundID, int? volume)
        {
            this.SoundID = soundID;
            this.Volume = volume;
            this.Validate();
        }

        public void Validate()
        {
            // todo
        }

        public string CompileToText()
        {
            var res = this.SoundID;

            if (this.Volume.HasValue)
            {
                res += " " + this.Volume;
            }

            return res;
        }

        public bool Equals(IFilterValue otherObj)
        {
            var other = otherObj as SoundValue;
            return this.SoundID == other.SoundID && this.Volume == other.Volume;
        }

        public IFilterValue Clone()
        {
            return new SoundValue(this.SoundID, this.Volume);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
