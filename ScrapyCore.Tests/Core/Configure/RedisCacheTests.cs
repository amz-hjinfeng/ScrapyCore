using ScrapyCore.Core;
using ScrapyCore.Core.Caches;
using ScrapyCore.Core.Configure.Caches;
using ScrapyCore.Core.Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScrapyCore.Tests.Core.Configure
{

    public class RedisCacheTests
    {
        private readonly ICache redisCache;
        private readonly TestModel testModel;

        public RedisCacheTests()
        {
            IStorage storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            var redisConfigure = CacheConfigureFactory.Factory.CreateConfigure(storage, "MockData/Core/Configure/RedisConfigure.json");
            redisCache = CacheFactory.Factory.GetService(redisConfigure);
            testModel = new TestModel()
            {
                Field1 = "field1",
                Field2 = "field2"
            };
        }
        [Fact]
        public void StoreTest()
        {
            redisCache.Store("testKey", testModel);
            Assert.True(redisCache.IsKeyExist("testKey"));
        }
        [Fact]
        public void IsExistKeyTest()
        {
            redisCache.Store("testKey2", testModel);
            Assert.True(redisCache.IsKeyExist("testKey2"));
            Assert.False(redisCache.IsKeyExist("testKey2X"));
        }
        [Fact]
        public async Task StoreAsyncTest()
        {
            await redisCache.StoreAsync("testKeyAsync", testModel);
            Assert.True(await redisCache.IsKeyExistAsync("testKeyAsync"));
        }
        [Fact]
        public async Task IsExistKeyAsyncTest()
        {
            redisCache.Store("testKey23", testModel);
            Assert.True(await redisCache.IsKeyExistAsync("testKey23"));
            Assert.False(await redisCache.IsKeyExistAsync("testKey32X3"));
        }
        [Fact]
        public async Task RestoreAsyncTest()
        {
            string key = "testKeyAsyncRestore";
            redisCache.Store(key, testModel);
            var restoreModel = await redisCache.RestoreAsync<TestModel>(key);
            Assert.Equal(testModel.Field1, restoreModel.Field1);
            Assert.Equal(testModel.Field2, restoreModel.Field2);
        }
        [Fact]
        public void RestoreTest()
        {
            string key = "testKeyAsync1Restore";
            redisCache.Store(key, testModel);
            var restoreModel = redisCache.Restore<TestModel>(key);
            Assert.Equal(testModel.Field1, restoreModel.Field1);
            Assert.Equal(testModel.Field2, restoreModel.Field2);
        }
        [Fact]
        public void RemoveTest()
        {
            string key = "keytoDelete";
            Assert.False(redisCache.IsKeyExist(key));
            redisCache.Store(key, testModel);
            Assert.True(redisCache.IsKeyExist(key));
            Assert.True(redisCache.Remove(key));
            Assert.False(redisCache.IsKeyExist(key));
        }
        [Fact]
        public async Task RemoveAsyncTest()
        {
            string key = "keytoDelete";
            Assert.False(await redisCache.IsKeyExistAsync(key));
            redisCache.Store(key, testModel);
            Assert.True(await redisCache.IsKeyExistAsync(key));
            Assert.True(await redisCache.RemoveAsync(key));
            Assert.False(await redisCache.IsKeyExistAsync(key));
        }


        public class TestModel
        {
            public string Field1 { get; set; }

            public string Field2 { get; set; }
        }
    }
}
