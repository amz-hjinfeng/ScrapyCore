using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Storages;
using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Kernel.Extract.Http;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Extract
{
    public class SourceModelTests
    {
        string httpSourceDemoString = "";
        public SourceModelTests()
        {
            IStorage storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            httpSourceDemoString = storage.GetString("MockData/Fundamental/Extract/httpsourcedemo.json");
        }


        [Fact]
        public void ScrapySourceTest()
        {
            Assert.NotEmpty(httpSourceDemoString);
            ScrapySource scrapySource = JsonConvert.DeserializeObject<ScrapySource>(httpSourceDemoString);
            Assert.Equal("afdbc418-c2fe-42c8-9ad3-e4b88a26a968", scrapySource.JobId);
            Assert.Equal("186f5599-1eb0-498f-8104-fb8111611d51", scrapySource.MessageId);
            Assert.NotNull(scrapySource.Source);
            Assert.NotNull(scrapySource.Source.Type);
            Assert.Equal("Http", scrapySource.Source.Type);
            Assert.NotNull(scrapySource.Source.Parameters);
        }

        [Fact]
        public void HttpSourceTest()
        {
            ScrapySource scrapySource = JsonConvert.DeserializeObject<ScrapySource>(httpSourceDemoString);
            HttpSource httpSource = JsonConvert.DeserializeObject<HttpSource>(scrapySource.Source.Parameters.ToString());
            Assert.NotNull(httpSource);
            Assert.Equal("http://www.sina.com.cn", httpSource.Referer);
            Assert.Equal("https://news.sina.com.cn/c/2019-10-27/doc-iicezzrr5215576.shtml?cre=tianyi&mod=pchp&loc=10&r=0&rfunc=91&tj=none&tr=12", httpSource.Url);
            Assert.Equal("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8", httpSource.Accept);
            Assert.Equal("Chrome_1", httpSource.UserAgent);
            Assert.Equal("text/html; charset=utf-8", httpSource.ContentType);
            Assert.Equal("utf-8", httpSource.Encoding);
            Assert.Equal("GET", httpSource.Method);
            Assert.Equal(2, httpSource.Header.Count);
        }
    }
}
