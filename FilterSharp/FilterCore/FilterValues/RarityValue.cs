using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public class RarityValue : IFilterValue
    {
        private List<string> rarityList;
        public List<string> RarityList
        {
            get { return rarityList; }
            set { this.SetValue(value); }
        }

        private string opera;
        public string Opera
        {
            get { return opera; }
            set { this.SetValue(value, this.operaRarity); }
        }

        private string operaRarity;
        public string OperaRarity
        {
            get { return operaRarity; }
            set { this.SetValue(this.opera, value); }
        }

        private string raw;
        private string type; // either "opera" or "list"

        public static List<string> AllRarityList = new List<string> { "Normal", "Magic", "Rare", "Unique" };

        public RarityValue(string raw)
        {
            this.raw = raw;
            var temp = raw.Split(' ');
            var splits = temp.Where(x => x != "").ToList();

            if (raw[0] == '<' || raw[0] == '>' || raw[0] == '=')
            {
                // opera based
                if (splits.Count != 2) throw new Exception();

                this.Opera = splits[0];
                this.OperaRarity = splits[1];
                this.type = "opera";
            }

            else if (RarityValue.AllRarityList.Contains(raw))
            {
                // direct, single value
                if (splits.Count != 1) throw new Exception();
                this.RarityList = new List<string>() { raw };
                this.type = "list";
            }

            else if (splits.All(x => RarityValue.AllRarityList.Contains(x)))
            {
                // list of rarities
                this.RarityList = splits;
                this.type = "list";
            }

            else throw new Exception("invalid rarity value raw string");
        }

        public IFilterValue Clone()
        {
            return new RarityValue(this.raw);
        }

        public string CompileToText()
        {
            if (this.type == "opera")
            {
                return this.opera + " " + this.operaRarity;
            }

            else if (this.type == "list")
            {
                return String.Join(" ", this.RarityList);
            }

            throw new Exception();
        }

        public bool Equals(IFilterValue other)
        {
            throw new NotImplementedException();
        }

        private void SetValue(List<string> rarityList)
        {
            this.rarityList = rarityList;
            this.type = "list";
        }

        private void SetValue(string opera, string rarity)
        {
            this.opera = opera;
            this.operaRarity = rarity;
            this.type = "opera";
        }

        public bool Validate()
        {
            if (this.type == "opera")
            {
                switch (this.Opera)
                {
                    case "<=":
                    case "<":
                    case ">":
                    case ">=":
                    case "=":
                        break;

                    default:
                        return false;
                }

                if (!RarityValue.AllRarityList.Contains(this.OperaRarity))
                {
                    return false;
                }

                return true;
            }

            else if (this.type == "list")
            {
                if (this.RarityList.Any(x => !RarityValue.AllRarityList.Contains(x)))
                {
                    return false;
                }

                return true;
            }

            else
            {
                throw new Exception();
            }
        }

        public string GetStringIdent() => "Rarity";
    }
}
