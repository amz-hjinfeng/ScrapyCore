using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.Strings;
using ScrapyCore.Fundamental.Kernel.Convertors.StringTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Strings
{
    public class RegexConvertorTests
    {
        string testPatten = @"https://blog.csdn.net/[\w|/|-]*";
        RegexConvertor conventor;
        DistinctConvertor inlineConventor;
        private string htmlPath;

        public RegexConvertorTests()
        {
            conventor = new RegexConvertor(testPatten);
            inlineConventor = new DistinctConvertor();
            htmlPath = ConstVariable.ApplicationPath + "/MockData/Fundamental/Conventors/MockedHtml.html";

        }


        [Fact]
        public void ConvertTest()
        {
            string data = File.ReadAllText(htmlPath);
            var contextData = conventor.Convert(new ContextData() { ContentText = data });
            Assert.NotNull(contextData);
            Assert.NotNull(contextData.Listing);
            Assert.NotEmpty(contextData.Listing);
        }

        [Fact]
        public void FlowDistinctTest()
        {
            string data = File.ReadAllText(htmlPath);
            var contextData = conventor.Convert(new ContextData() { ContentText = data });
            Assert.Equal(487, contextData.Listing.Count);
            contextData = inlineConventor.Convert(contextData);
            Assert.NotNull(contextData);
            Assert.NotNull(contextData.Listing);
            Assert.NotEmpty(contextData.Listing);
            Assert.Equal(311, contextData.Listing.Count);
        }
    }
}



//<a class="btn-readmore" data-report-view='{"mod":"popu_376","dest":"https://blog.csdn.net/weixin_34343689/article/details/93993168","strategy":"readmore"}' data-report-click='{"mod":"popu_376","dest":"https://blog.csdn.net/weixin_34343689/article/details/93993168","strategy":"readmore"}'>

