using System;
namespace ScrapyCore.Core.Configure.Caches
{
    public class CacheConfigureFactory :IConfigurationFactory<ICachingConfigure>
    {
        private static CacheConfigureFactory _factory;

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

        }


        public ICachingConfigure CreateConfigure(IStorage storage, string Path)
        {
            throw new NotImplementedException();
        }
    }
}
