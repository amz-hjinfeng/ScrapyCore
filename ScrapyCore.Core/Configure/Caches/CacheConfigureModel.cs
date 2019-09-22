using System;
namespace ScrapyCore.Core.Configure.Caches
{
    public class CacheConfigureModel
    {
        public string CacheEngine { get; set; }

        public int ExpireMiniSeconds { get; set; }

        public string[][] ConfigureDetail { get; set; }
    }
}
