using ScrapyCore.Core;
using ScrapyCore.Core.Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ScrapyCore.Fundamental.Kernel.Transform;
using Newtonsoft.Json;
using System.IO;

namespace ScrapyCore.Tests.Fundamental.Kernel.Transform
{
    public class TransformIntegrationTests
    {
        string mockHtmlData;
        string kernelMessage;
        string datafromCache;
        string result;
        ICache coreCache;
        IStorage coreStorage;
        public TransformIntegrationTests()
        {
            IStorage storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);

            mockHtmlData = storage.GetString("MockData/Fundamental/Transform/MockTableData.html");
            kernelMessage = storage.GetString("MockData/Fundamental/Transform/kernelMessage.json");
            datafromCache = storage.GetString("MockData/Fundamental/Transform/httpTransform.json");
            result = storage.GetString("MockData/Fundamental/Transform/transformResult.txt");
            coreCache = Mock.Of<ICache>();
            coreStorage = Mock.Of<IStorage>();

            Mock.Get(coreCache)
                .Setup(x => x.RestoreAsync<TransformEvent>(It.IsAny<string>()))
                .Returns(Task.FromResult(JsonConvert.DeserializeObject<TransformEvent>(datafromCache)));

            Mock.Get(coreStorage)
                .Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(mockHtmlData));

            Mock.Get(coreStorage)
                .Setup(x => x.WriteStream(It.IsAny<Stream>(), It.IsAny<String>()))
                .Returns(new Func<Stream, string, Task>(AssertTransformResult));

        }

        [Fact]
        public async Task ProcessTest()
        {
            byte[] byteKernelMessage = Encoding.UTF8.GetBytes(kernelMessage);
            TransformIntegration transformIntegration = new TransformIntegration(coreCache, coreStorage);
            await transformIntegration.Process(byteKernelMessage, null);
        }

        private async Task AssertTransformResult(Stream stream, string path)
        {
            StreamReader streamReader = new StreamReader(stream);
            string data = await streamReader.ReadToEndAsync();
            Assert.Equal(result, data);
            Assert.NotNull(path);
            Assert.Equal("Load/sina/level7/0120.json", path);

        }
    }
}