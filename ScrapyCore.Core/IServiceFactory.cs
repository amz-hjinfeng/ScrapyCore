using ScrapyCore.Core.Configure;
using System;
namespace ScrapyCore.Core
{
    public interface IServiceFactory<TService, TConfigure>
        where TConfigure : IConfigure
    {
        TService GetService(TConfigure configure);
    }
}
