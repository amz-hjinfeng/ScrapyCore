using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.Strings;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Strings
{
    public class Md5ConvertorTests
    {
        Md5Convertor convertor;
        public Md5ConvertorTests()
        {
            convertor = new Md5Convertor();
        }

        [Fact]
        public void ConvertTest()
        {
            var contentResult = convertor.Convert(new ContentData()
            {
                ContentText = "Hello world!"
            });
            Assert.NotEmpty(contentResult.ContentText);
            Assert.Equal("86fb269d190d2c85f6e0468ceca42a20", contentResult.ContentText);

        }
    }
}
