using System;
using System.Collections.Generic;

namespace ScrapyCore.Core.Configure
{
    public interface ICachingConfigure : IConfigure
    {

        int ExpireInMiniSeconds { get; }

        string CacheType { get; }
    }
}
