using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrapyCore.Core.Configure.Caches
{
    public class RedisConfigure :ICachingConfigure
    {
        private readonly CacheConfigureModel cacheConfigureModel;

        public RedisConfigure(CacheConfigureModel cacheConfigureModel)
        {
            this.cacheConfigureModel = cacheConfigureModel;
            this.ConfigureDetail = cacheConfigureModel.ConfigureDetail.ToDictionary(x => x[0], x => x[1]);
        }

        public string CacheType => "Redis";

        public int ExpireInMiniSeconds => cacheConfigureModel.ExpireMiniSeconds;

        public Dictionary<string, string> ConfigureDetail { get; }
    }
}
