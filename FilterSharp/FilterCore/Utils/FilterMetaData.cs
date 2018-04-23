using System;
using System.Collections.Generic;

namespace FilterCore
{
    public class FilterMetaData
    {
        string Name { get; set; } // utility, proably wont affect actual content. just for better identification?

        // strictness/style/version?? -> meh, should also be useable for non-NS filters, right?
        // alt: just use 0 for those
        FilterStrictness Strictness { get; }
        FilterStyle Style { get; }

        internal FilterMetaData Clone()
        {
            throw new NotImplementedException();
        }
    }
}
