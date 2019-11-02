using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ScrapyCore.Core;
using ScrapyCore.Fundamental.Kernel.Extract.Http;
using System.IO;
using ScrapyCore.Core.Storages;
using ScrapyCore.Fundamental.Kernel.Extract;
using Newtonsoft.Json;

namespace ScrapyCore.Tests.Fundamental.Kernel.Extract
{
    public class HttpExtractorTests
    {
        IUserAgentPool userAgentPool;
        IStorage storage;
        string parameter;
        string path;
        public HttpExtractorTests()
        {
            userAgentPool = Mock.Of<IUserAgentPool>();
            Mock.Get(userAgentPool)
                .Setup(x => x.GetUserAgent("Chrome_1")).Returns(new ScrapyCore.Core.UserAgents.UserAgent()
                {
                    AgentString = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/22.0.1207.1 Safari/537.1"
                });
            storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            var httpSourceDemoString = storage.GetString("MockData/Fundamental/Extract/httpsourcedemo.json");
            ScrapySource scrapySource = JsonConvert.DeserializeObject<ScrapySource>(httpSourceDemoString);
            this.parameter = scrapySource.Source.Parameters.ToString();
            this.path = "httpExtractorFile.txt";

        }

        [Fact]
        public async Task ExtractTargetTest()
        {
            HttpExtractor httpExtractor = new HttpExtractor(userAgentPool, storage);
            await httpExtractor.ExtractTarget(parameter, path);
            FileInfo fileInfo = new FileInfo(Path.Combine(ConstVariable.ApplicationPath, "httpExtractorFile.txt"));
            Assert.True(fileInfo.Exists);
            Assert.NotEqual(0, fileInfo.Length);
            File.Delete(fileInfo.FullName);
            Assert.Equal(userAgentPool, httpExtractor.UserAgentPool);
            Assert.Equal(storage, httpExtractor.Storage);


        }
    }
}
