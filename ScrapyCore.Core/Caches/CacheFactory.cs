using System;
using System.Collections.Generic;
using System.Linq;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.Caches
{
    public class CacheFactory : IServiceFactory<ICache, ICachingConfigure>
    {
        private CacheFactory()
        {
            cacheTypes = typeof(CacheFactory).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => x.GetInterface(nameof(ICache)) != null)
                .ToDictionary(x => x.Name, x => x);
        }
        private static CacheFactory _factory;
        private readonly Dictionary<string, Type> cacheTypes;

        public static CacheFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new CacheFactory();
                return _factory;
            }
        }

        public ICache GetService(ICachingConfigure configure)
        {
            if (cacheTypes.ContainsKey(configure.CacheType))
            {
                return Activator.CreateInstance(cacheTypes[configure.CacheType], configure) as ICache;
            }
            return null;
        }
    }
}
