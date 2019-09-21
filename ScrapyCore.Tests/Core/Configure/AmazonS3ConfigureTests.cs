using System;
using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.Storage;
using ScrapyCore.Core.Storages;
using Xunit;

namespace ScrapyCore.Tests.Core.Configure
{
    public class AmazonS3ConfigureTests
    {
        IStorageConfigure s3Configure;

        public AmazonS3ConfigureTests()
        {
            IStorage storage =StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            s3Configure= StorageConfigureFactory.Instance.CreateStorageConfigure(storage, "MockData/Core/Configure/s3configure.json");
        }

        [Fact]
        public void PrefixTest()
        {
            Assert.Equal("/prefix", s3Configure.Prefix);
        }

        [Fact]
        public void ConfigureDetailCountTest()
        {
            Assert.Equal(2, s3Configure.ConfigureDetail.Count);
        }

        [Fact]
        public void StorageTypeTest()
        {
            Assert.Equal("AmazonS3", s3Configure.StorageType);
        }

    }
}
