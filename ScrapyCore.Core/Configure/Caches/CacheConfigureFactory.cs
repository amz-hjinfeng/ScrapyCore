using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ScrapyCore.Core.Configure.Caches
{
    public class CacheConfigureFactory :IConfigurationFactory<ICachingConfigure>
    {
        private static CacheConfigureFactory _factory;
        private readonly Dictionary<string, Type> cacheTypes;

        public static CacheConfigureFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new CacheConfigureFactory();
                return _factory;
            }
        }


        private CacheConfigureFactory()
        {
            cacheTypes = typeof(CacheConfigureFactory).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => x.GetInterfaces().Contains(typeof(ICachingConfigure)))
                .ToDictionary(x => x.Name.Substring(0, x.Name.Length - "Configure".Length), x => x);
        }


        public ICachingConfigure CreateConfigure(IStorage storage, string path)
        {
            var cacheObject= JsonConvert.DeserializeObject<CacheConfigureModel>(storage.GetString(path));
            if (cacheTypes.ContainsKey(cacheObject.CacheEngine))
            {
                return Activator.CreateInstance(cacheTypes[cacheObject.CacheEngine], cacheObject) as ICachingConfigure;
            }
            return null;
        }
    }
}
