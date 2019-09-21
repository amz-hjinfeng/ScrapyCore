using System;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.Caches
{
    public class CacheFactory :IServiceFactory<ICache,ICachingConfigure>
    {
        private CacheFactory()
        {

        }


        private static CacheFactory _factory;
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
            throw new NotImplementedException();
        }
    }
}
