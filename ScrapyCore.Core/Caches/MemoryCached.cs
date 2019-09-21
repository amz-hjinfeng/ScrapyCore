using System;
using System.Threading.Tasks;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.Caches
{
    public class MemoryCached : CommonCache
    {

        public MemoryCached(ICachingConfigure cachingConfigure) : base(cachingConfigure)
        {
        }

        public override T Restore<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override Task<T> RestoreAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override void Store<T>(string key, T model)
        {
            throw new NotImplementedException();
        }

        public override Task StoreAsync<T>(string key, T model)
        {
            throw new NotImplementedException();
        }
    }
}
