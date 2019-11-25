using Newtonsoft.Json;
using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Load;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Load
{
    public class LoadProviderManagerTests
    {
        public LoadProviderManagerTests()
        {
            Console.WriteLine(ConstVariable.ApplicationPath);
        }

        [Fact]
        public void GetLoadProviderTest()
        {
            LoadProviderManager loadProviderManager = new LoadProviderManager();
            TestModel testModel = JsonConvert.DeserializeObject<TestModel>(TestData);
            ILoadProvider loadProvider = loadProviderManager.GetLoadProvider(testModel.Type, testModel.Name, testModel.Context);
            Assert.IsType<StorageLoadProvider>(loadProvider);

            ILoadProvider loadProviderNull = loadProviderManager.GetLoadProvider("asdasd", testModel.Name, testModel.Context);
            Assert.Null(loadProviderNull);

            ILoadProvider loadProviderExists = loadProviderManager.GetLoadProvider(testModel.Type, testModel.Name, testModel.Context);
            Assert.Equal(loadProvider, loadProviderExists);

        }

        public class TestModel
        {
            public string Type { get; set; }

            public string Name { get; set; }

            public object Context { get; set; }


        }

        const string TestData =
@"{
    'Type': 'Storage',
    'Name': 'Aritical',
    'Context': {
        'StorageType': 'AmazonS3',
        'Prefix': '/prefix',
        'Configure': [
            [ 'region', 'ap-southeast-1' ],
            [ 'bucket', 'hjinfeng-bucket' ]
        ]
    }
}";

    }

}
