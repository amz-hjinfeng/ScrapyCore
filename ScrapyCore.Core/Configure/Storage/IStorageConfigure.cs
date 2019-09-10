using System;
using System.Collections.Generic;

namespace ScrapyCore.Core.Configure
{
    public interface IStorageConfigure
    {
        string Prefix { get; }

        string StorageType { get; }

        IDictionary<string,string> ConfigureDetail { get; }
    }
}
