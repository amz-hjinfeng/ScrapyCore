using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.Strings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Strings
{
    public class QuerySelectorConvertorTests
    {
        private string mockHtml;
        public QuerySelectorConvertorTests()
        {
            mockHtml = File.ReadAllText(Path.Combine(ConstVariable.ApplicationPath, "MockData/Fundamental/Conventors/MockQuerySelectorTestSource.html"));
        }

        [Fact]
        public void ConvertTest()
        {
            QuerySelectorConvertor querySelectorSelectSource = new QuerySelectorConvertor(".date-source a:link");
            QuerySelectorConvertor querySelectorDateTime = new QuerySelectorConvertor(".date-source span");
            ContextData contextData = new ContextData() { ContentText = mockHtml };
            var result = querySelectorSelectSource.Convert(contextData);
            Assert.NotNull(result);
            Assert.Equal("SourceTest", result.ContentText);
            var resultDateTime = querySelectorDateTime.Convert(contextData);
            Assert.NotNull(resultDateTime);
            Assert.Equal("2019年10月27日 07:24", resultDateTime.ContentText);
        }
    }
}

//C:\Users\zhbit\source\repos\ScrapyCore\ScrapyCore.Tests\MockData\Fundamental\Conventors\MockedHtml.html
