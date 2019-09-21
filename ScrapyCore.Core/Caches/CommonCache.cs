using System;
using System.Threading.Tasks;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.Caches
{
    public abstract class CommonCache :ICache
    {
        private readonly ICachingConfigure cachingConfigure;

        protected CommonCache(ICachingConfigure cachingConfigure)
        {
            this.cachingConfigure = cachingConfigure;
        }

        public abstract T Restore<T>(string key) where T : class, new();
        public abstract Task<T> RestoreAsync<T>(string key) where T : class, new();
        public abstract void Store<T>(string key, T model) where T : class, new();
        public abstract Task StoreAsync<T>(string key, T model) where T : class, new();
    }
}
