using System;
using System.Threading.Tasks;

namespace ScrapyCore.Core
{
    public interface ICache
    {
        void Store<T>(string key, T model) where T : class, new() ;

        Task StoreAsync<T>(string key, T model) where T : class, new();

        T Restore<T>(string key) where T : class, new();

        Task<T> RestoreAsync<T>(string key) where T : class, new();
    }
}
