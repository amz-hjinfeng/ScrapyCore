using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrapyCore.Core.Configure.Caches
{
    public class RedisCacheConfigure : ICachingConfigure
    {
        private readonly CacheConfigureModel cacheConfigureModel;

        public RedisCacheConfigure(CacheConfigureModel cacheConfigureModel)
        {
            this.cacheConfigureModel = cacheConfigureModel;
            this.ConfigureDetail = cacheConfigureModel.ConfigureDetail.ToDictionary(x => x[0], x => x[1]);
        }

        public string CacheType => "RedisCache";

        public int ExpireInMiniSeconds => cacheConfigureModel.ExpireMiniSeconds;

        public IDictionary<string, string> ConfigureDetail { get; }
    }
}
