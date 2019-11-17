using System;
using System.Collections.Generic;

namespace ScrapyCore.Core.Configure
{
    public interface IConfigure
    {
        IDictionary<string, string> ConfigureDetail { get; }
    }
}
