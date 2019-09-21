using System;
namespace ScrapyCore.Core.Configure
{
    public interface IConfigurationFactory<TConfigure> where TConfigure:IConfigure
    {
        TConfigure CreateConfigure(IStorage storage, string Path);
    }
}
