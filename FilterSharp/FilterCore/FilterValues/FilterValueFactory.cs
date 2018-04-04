using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCore.FilterValues
{
    public class FilterValueFactory
    {
        public IFilterValue GenerateFilterValue(string ident, string value)
        {
            switch (ident)
            {
                case "Show": return new VoidValue(value);
                case "Hide": return new VoidValue(value);
                case "DisableDropSound": return new VoidValue(value);
                case "BaseType": return new BaseType(value);
                case "Class": return new Class(value);
                case "SocketGroup": return new SocketGroup(value);
                case "SetTextColor": return new TextColor(value);
                case "SetBorderColor": return new BorderColor(value);
                case "SetBackgroundColor": return new BackgroundColor(value);
                case "ItemLevel": return new ItemLevelValue(value);
                case "DropLevel": return new DropLevelValue(value);
                case "Sockets": return new Sockets(value);
                case "LinkedSockets": return new LinkedSockets(value);
                case "Quality": return new Quality(value);
                case "Height": return new Height(value);
                case "Width": return new Width(value);
                case "Identified": return new Identified(value);
                case "Corrupted": return new Corrupted(value);
                case "ShaperItem": return new ShaperItem(value);
                case "ElderItem": return new ElderItem(value);
                case "ShapedMap": return new ShapedMap(value);
                case "ElderMap": return new ElderMap(value);
                case "PlayAlertSound": return new SoundValue(value);
                case "SetFontSize": return new FontSizeValue(value);
                case "Rarity": return new RarityValue(value);

                default:
                    throw new Exception("invalid ident");
            }
        }
    }
}
