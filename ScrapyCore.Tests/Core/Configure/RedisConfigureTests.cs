using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.Caches;
using ScrapyCore.Core.Storages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Core.Configure
{
    public class RedisConfigureTests
    {
        private readonly ICachingConfigure redisConfigure;

        public RedisConfigureTests()
        {
            IStorage storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            redisConfigure = CacheConfigureFactory.Factory.CreateConfigure(storage, "MockData/Core/Configure/RedisConfigure.json");
        }

        [Fact]
        public void CacheTypeTest()
        {
            Assert.Equal("RedisCache", redisConfigure.CacheType);
        }

        [Fact]
        public void ConfigureDetailTest()
        {
            Assert.Equal(14, redisConfigure.ConfigureDetail.Count);
        }


    }
}
