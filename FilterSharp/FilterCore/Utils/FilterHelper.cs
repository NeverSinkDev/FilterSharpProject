using FilterCore.FilterValues;
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
        public static bool IsLegitIdent(string ident)
        {
            return FilterIdent.IdentList.Contains(ident);
        }

        public static bool IsValuelessIdent(string ident)
        {
            return FilterIdent.ValuelessIdents.Contains(ident);
        }

        public static readonly List<string> IdentList = new List<string>()
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
            "SetFontSize",
            "ShaperItem",
            "ElderItem",
            "ShapedMap",
            "ElderMap",
            "DisableDropSound"
        };

        public static readonly List<string> ValuelessIdents = new List<string>() { "Show", "Hide", "DisableDropSound" };
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
