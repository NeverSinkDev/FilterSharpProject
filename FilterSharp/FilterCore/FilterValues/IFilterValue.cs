using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public interface IFilterValue
    {
        // type -> stringList, operator+Value, bool, ... --> identifier
        // value field for each type??
        // Generic type T? <-- meh :/
        // ALWAYS List<string>, even for numbers


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
        string CompileToText();

        bool Equals(IFilterValue other);
        IFilterValue Clone();
        void Reset();
    }
}
