using System;
using System.Collections.Generic;

namespace ScrapyCore.Core.Configure
{
    public interface ICachingConfigure : IConfigure
    {

        string CacheType { get; }

        int ExpireInMiniSeconds { get; }

        Dictionary<string,string> ConfigureDetail { get; }
    }
}
