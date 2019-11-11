using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.External;
using ScrapyCore.Core.External.Conventor;

namespace ScrapyCore.Core.Caches
{
    public class LocalMemoryCache : CommonCache
    {
        private int expireInMiniSeconds;

        private ConcurrentDictionary<string, CacheModel> memory;

        public LocalMemoryCache(ICachingConfigure cachingConfigure) : base(cachingConfigure)
        {
            var maxObjects = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("max-objects", StringToInt32Conventor.Instance);
            var maxConcurrency = cachingConfigure.ConfigureDetail.GetKeyAndConvertTo("max-concurrency", StringToInt32Conventor.Instance);
            expireInMiniSeconds = cachingConfigure.ExpireInMiniSeconds;
            memory = new ConcurrentDictionary<string, CacheModel>(maxConcurrency, maxObjects);

            Task.Run(() =>
            {
                var keys = memory.Keys.ToList();
                foreach (var item in keys)
                {
                    if (memory.ContainsKey(item) && memory.TryGetValue(item, out var model) && IsExpire(model))
                    {
                        memory.TryRemove(item, out var cacheModel);
                    }
                }
                Thread.Sleep(expireInMiniSeconds);
            });

        }

        public override bool IsKeyExist(string key)
        {
            return memory.ContainsKey(key);
        }

        public override Task<bool> IsKeyExistAsync(string key)
        {
            return Task.FromResult(IsKeyExist(key));
        }

        public override bool Remove(string key)
        {
            if (memory.ContainsKey(key))
            {
                return memory.TryRemove(key, out var cacheModel);
            }
            return false;
        }

        public override Task<bool> RemoveAsync(string key)
        {
            return Task.FromResult(Remove(key));
        }

        public override T Restore<T>(string key)
        {
            if (memory.ContainsKey(key) && memory.TryGetValue(key, out var model) && !IsExpire(model))
            {
                return model.Data as T;
            }
            else
            {
                return null;
            }
        }

        public override Task<T> RestoreAsync<T>(string key)
        {
            return Task.FromResult(Restore<T>(key));
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

        private bool IsExpire(CacheModel model)
        {
            return model.LastUpdate.AddMilliseconds(this.expireInMiniSeconds) < DateTime.Now;
        }

        private class CacheModel
        {
            public DateTime LastUpdate { get; set; }

            public Object Data { get; set; }

        }

    }
}
