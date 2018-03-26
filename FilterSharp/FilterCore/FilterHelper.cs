using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public static class FilterHelper
    {

    }

    public class FilterIdent
    {
        public string Ident { get; set; }
        public bool IsLegitIdent { get; set; } = true;

        public bool HasNoValue { get; set; } = false;
        public bool HasListValue { get; set; } = false;
        public bool HasBoolValue { get; set; } = false;
        public bool HasOperatorValue { get; set; } = false;
        public bool HasColorValue { get; set; } = false;
        public bool HasSoundValue { get; set; } = false;
        public bool HasNumberValue { get; set; } = false;

        public int OrderPosition { get; set; }

        public FilterIdent(string ident)
        {
            this.Ident = ident;

            // todo: rarity
            // todo: add performance order positions

            switch (ident)
            {
                case "Show":
                case "Hide":
                case "DisableDropSound":
                    this.HasNoValue = true;
                    break;

                case "BaseType":
                case "Class":
                case "SocketGroup":
                    this.HasListValue = true;
                    break;

                case "SetTextColor":
                case "SetBorderColor":
                case "SetBackgroundColor":
                    this.HasColorValue = true;
                    break;

                case "ItemLevel":
                case "DropLevel":
                case "Sockets":
                case "LinkedSockets":
                case "Quality":
                case "Height":
                case "Width":
                    this.HasOperatorValue = true;
                    break;

                case "Identified":
                case "Corrupted":
                case "ShaperItem":
                case "ElderItem":
                case "ShapedMap":
                case "ElderMap":
                    this.HasBoolValue = true;
                    break;

                case "PlayAlertSound":
                    this.HasSoundValue = true;
                    break;

                case "SetFontSize":
                    this.HasNumberValue = true;
                    break;

                default:
                    this.IsLegitIdent = false;
                    break;
            }
        }

        public static List<string> IdentList = new List<string>()
        {
            "Show",
        "Hide",
        "ItemLevel",
        "DropLevel",
        "Quality",
        "Rarity",
        "Class",
        "BaseType",
        "Sockets",
        "LinkedSockets",
        "SocketGroup",
        "Height",
        "Width",
        "Identified",
        "Corrupted",
        "SetTextColor",
        "SetBorderColor",
        "SetBackgroundColor",
        "PlayAlertSound",
        "SetFontSize"
        };
    }

    public enum Operator
    {
        Less,
        LE,
        EQ,
        GE,
        Greater
    }

    public enum SoundID
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fiveteen,
        Sixteen,
        ShAlchemy,
        // todo
    }

    public enum EntryDataType
    {
        Comment = 0,
        Filler = 1,
        Rule = 2 // alt: Show + Hide (+ Disable?)
    }

    public enum FilterStrictness
    {
        Regular,
        SemiStrict,
        Strict,
        VeryStrict,
        UberStrict,
        UberPlusStrict
    }

    public enum FilterStyle
    {
        Normal,
        Blue,
        Purple,
        Slick,
        StreamSound
    }
}
