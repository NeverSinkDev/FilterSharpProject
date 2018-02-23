using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore
{
    public class FilterEntry
    {
        // test commit
        // more test commit
        // whoops
    }

    public interface IFilter
    {
        List<IFilterEntry> EntryList { get; set; }
        FilterMetaData MetaData { get; set; } // strictness, style, version, name, ...

        List<string> CompileToText();

        bool Equals(IFilter line);
        IFilter Clone();
        void Reset();
    }

    public class FilterMetaData
    {
        string Name { get; set; } // utility, proably wont affect actual content. just for better identification?

        // strictness/style/version?? -> meh, should also be useable for non-NS filters, right?
        // alt: just use 0 for those
        FilterStrictness Strictness { get; }
        FilterStyle Style { get; }
    }

    public interface IFilterEntry
    {
        EntryDataType DataType { get; }
        int EntryID { get; }
        bool Enabled { get; set; }
        List<IFilterLine> LineList { get; }

        List<string> CompileToText();

        // setter/getter
        //T GetValue<T>(FilterIdent ident); // normal getter for type/ident --> unnecessary because overload
        T GetValue<T>(FilterIdent ident, int nr = 0); // get n-th value of given type
        List<T> GetAllValues<T>(FilterIdent ident); // --> get ALL values for e.g. ItemLevel
        int GetValueTypeCount<T>(FilterIdent ident); // --> how many e.g. ItemLevel lines this entry has
        // same for set
        // same for remove
        // "HasValue" --> ValueTypeCount != 0

        IList<T> GetValueOfType<T>();
        T GetValueOfTypeFirst<T>();
        T GetValueOfTypeAt<T>(int nr);

        bool Equals(IFilterEntry line);
        IFilterEntry Clone();
        void Reset();
    }

    public interface IFilterLine
    {
        FilterIdent Ident { get; }
        IFilterValue Value { get; set; }
        string Comment { get; set; } // maybe list of strings or something. for the commands/tags etc.
        
        bool Equals(IFilterLine line);
        IFilterLine Clone();
        void Reset();

        List<string> CompileToText();
    }

    public interface IFilterValue
    {
        // type -> stringList, operator+Value, bool, ... --> identifier
        // value field for each type??
        // Generic type T? <-- meh :/
        // ALWAYS List<string>, even for numbers
        List<string> ValueList { get; set; }
        FilterIdent Ident { get; set; }

        void Validate(); // called whenever you change the value
        // opBased -> opera + int
        // --> check int range for ident
        // color -> 3 or 4 ints
        // --> check int range -> 0-255
        // lists -> not empty list of strings
        // --> check baseTypes?
        // bool -> string
        // --> check: can be translated to bool?
        // rarity -> opera string
        // --> check: is string a rarity?
        // sound -> int/string <optional int>
        // --> check: is int/string valid sound id, is optional int in range: 0-300


        // get/set value:
        /*
         * oper int
         * oper string
         * List<string>
         * bool
         * List<int> (3 oder 4)
         * 
         * sound:
         * int int
         * string int
         * string
         * int
         */

        //Operator GetOperator();     // for opBased          -> ValueList[0];
        //int GetNumber();            // for opBased          -> parseInt ValueList[1];
        //bool GetBool();             // for bool             -> parseBool ValueList[0];
        //List<string> GetList();     // for lists            -> full ValueList;
        //string GetRarity();         // for opBased rarity   -> ValueList[1];
        //string GetSound();          // sound                -> always return string, even for soundID 1-16
        //int? GetVolume();           // sound                -> volume. no volume -> return...null? 300??

        // v2
        // different class for each type of value:
        // list
        // sound
        // operator
        // bool
        // --> todo: rarity: is operator AND list

        bool Equals(IFilterValue line);
        IFilterValue Clone();
        void Reset();
    }

    public abstract class OperatorValue
    {
        public int Value { get; set; }
        public string Operator { get; set; }
        public virtual void Validate() { }
    }
    public class ItemLevelValue : OperatorValue { }

    public class FilterParser
    {
        IFilter Parse(List<string> stringList)
        {
            // what to return? filter? entryList?
            return null;
        }
    }

    public enum FilterIdent // todo: sort for performance
    {
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
