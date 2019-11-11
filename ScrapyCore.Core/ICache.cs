using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapyCore.Core
{
    public interface ICache
    {
        void Store<T>(string key, T model, TimeSpan? timeSpan = null) where T : class, new();

        Task StoreAsync<T>(string key, T model, TimeSpan? timeSpan = null) where T : class, new();

        Task StoreStringAsync(string key, string strValue, TimeSpan? timeSpan = null);

        T Restore<T>(string key) where T : class, new();

        Task<T> RestoreAsync<T>(string key) where T : class, new();

        Task<string> RestoreStringAsync(string key);

        Task<bool> IsKeyExistAsync(string key);

        bool IsKeyExist(string key);

        bool Remove(string key);

        Task<bool> RemoveAsync(string key);

        Task<IEnumerable<string>> SearchKeys(string keyPatten);
    }
}
