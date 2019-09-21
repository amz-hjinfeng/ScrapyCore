using System;
namespace ScrapyCore.Core
{
    public interface IServiceFactory<TService,TConfigure>
    {
        TService GetService(TConfigure configure);
    }
}
