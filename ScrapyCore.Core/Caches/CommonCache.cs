using System;
using System.Threading.Tasks;
using log4net;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.Caches
{
    public abstract class CommonCache : ICache
    {
        protected static ILog Logger => LogManager.GetLogger("Scrapy-Repo", typeof(CommonCache));

        private readonly ICachingConfigure cachingConfigure;

        protected CommonCache(ICachingConfigure cachingConfigure)
        {
            this.cachingConfigure = cachingConfigure;
        }

        public abstract bool IsKeyExist(string key);
        public abstract Task<bool> IsKeyExistAsync(string key);
        public abstract bool Remove(string key);
        public abstract Task<bool> RemoveAsync(string key);
        public abstract T Restore<T>(string key) where T : class, new();
        public abstract Task<T> RestoreAsync<T>(string key) where T : class, new();
        public abstract void Store<T>(string key, T model) where T : class, new();
        public abstract Task StoreAsync<T>(string key, T model) where T : class, new();


    }
}
