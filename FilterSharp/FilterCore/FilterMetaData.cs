namespace FilterCore
{
    public class FilterMetaData
    {
        string Name { get; set; } // utility, proably wont affect actual content. just for better identification?

        // strictness/style/version?? -> meh, should also be useable for non-NS filters, right?
        // alt: just use 0 for those
        FilterStrictness Strictness { get; }
        FilterStyle Style { get; }
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
