using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.Caches
{
    public class MemoryCached : CommonCache
    {

        public MemoryCached(ICachingConfigure cachingConfigure) : base(cachingConfigure)
        {
        }

        public override bool IsKeyExist(string key)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> IsKeyExistAsync(string key)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public override T Restore<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override Task<T> RestoreAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override Task<string> RestoreStringAsync(string key)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<string>> SearchKeys(string keyPatten)
        {
            throw new NotImplementedException();
        }

        public override void Store<T>(string key, T model, TimeSpan? timeSpan = null)
        {
            throw new NotImplementedException();
        }

        public override Task StoreAsync<T>(string key, T model, TimeSpan? timeSpan = null)
        {
            throw new NotImplementedException();
        }

        public override Task StoreStringAsync(string key, string strValue, TimeSpan? timeSpan = null)
        {
            throw new NotImplementedException();
        }
    }
}
