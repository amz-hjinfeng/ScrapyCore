using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;

namespace ScrapyCore.Core
{
    public interface IServiceFactory<TService, TConfigure> : IServiceKeys
        where TConfigure : IConfigure
    {
        TService GetService(TConfigure configure);
    }
}
