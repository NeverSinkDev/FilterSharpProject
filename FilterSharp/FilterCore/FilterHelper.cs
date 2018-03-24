using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public static class FilterHelper
    {
        public static FilterIdent TranslateStringToIdent(string ident)
        {
            switch (ident)
            {
                case "Show":
                    return FilterIdent.Show;

                case "Hide":
                    return FilterIdent.Hide;

                case "ItemLevel":
                    return FilterIdent.ItemLevel;

                case "DropLevel":
                    return FilterIdent.DropLevel;

                case "Quality":
                    return FilterIdent.Quality;

                case "Rarity":
                    return FilterIdent.Rarity;

                case "Class":
                    return FilterIdent.Class;

                case "BaseType":
                    return FilterIdent.BaseType;

                case "Sockets":
                    return FilterIdent.Sockets;

                case "LinkedSockets":
                    return FilterIdent.LinkedSockets;

                case "SocketGroup":
                    return FilterIdent.SocketGroup;

                case "Height":
                    return FilterIdent.Height;

                case "Width":
                    return FilterIdent.Width;

                case "Identified":
                    return FilterIdent.Identified;

                case "Corrupted":
                    return FilterIdent.Corrupted;

                case "SetTextColor":
                    return FilterIdent.SetTextColor;

                case "SetBorderColor":
                    return FilterIdent.SetBorderColor;

                case "SetBackgroundColor":
                    return FilterIdent.SetBackgroundColor;

                case "PlayAlertSound":
                    return FilterIdent.PlayAlertSound;

                case "SetFontSize":
                    return FilterIdent.SetFontSize;

                default:
                    throw new Exception("unknown filter ident: " + ident);
            }
        }

        /// TODO::: ELDER + SHAPER + SHAPED/ELDERED-MAP!!!

        public static IEnumerable<string> IdentList = new List<string>()
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

    public enum FilterIdent // todo: sort for performance
    {
        Show,
        Hide,
        ItemLevel,
        DropLevel,
        Quality,
        Rarity,
        Class,
        BaseType,
        Sockets,
        LinkedSockets,
        SocketGroup,
        Height,
        Width,
        Identified,
        Corrupted,
        SetTextColor,
        SetBorderColor,
        SetBackgroundColor,
        PlayAlertSound,
        SetFontSize
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
