using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.Strings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Strings
{
    public class XPathInnerHtmlConvertorTests
    {
        public string htmlPath;
        public XPathInnerHtmlConvertorTests()
        {
            htmlPath = ConstVariable.ApplicationPath + "/MockData/Fundamental/Conventors/MockedHtml.html";
        }

        [Fact]
        public void ConvertTest()
        {
            string data = File.ReadAllText(htmlPath);
            ContentData contentData = new ContentData()
            {
                ContentText = data
            };
            XPathInnerHtmlConvertor pathConvertor =
                new XPathInnerHtmlConvertor("/html/body/div[@class='mask-dark']");
            var contentResult = pathConvertor.Convert(contentData);
            Assert.NotEmpty(contentResult.ContentText);
            Assert.Equal("test", contentResult.ContentText);


        }
    }
}
