using System;
using System.Threading.Tasks;
using ScrapyCore.Core.Configure;
using StackExchange.Redis;

namespace ScrapyCore.Core.Caches
{
    public class RedisCache : CommonCache
    {
        public RedisCache(ICachingConfigure cachingConfigure)
            : base(cachingConfigure)
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
