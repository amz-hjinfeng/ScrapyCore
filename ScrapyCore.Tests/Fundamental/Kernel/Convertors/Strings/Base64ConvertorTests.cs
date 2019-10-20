using ScrapyCore.Fundamental.Kernel.Convertors.Strings;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Strings
{
    public class Base64ConvertorTests
    {
        Base64Convertor convertor;
        public Base64ConvertorTests()
        {
            convertor = new Base64Convertor();
        }

        [Fact]
        public void ConvertTest()
        {
            string testData = "abcdefghigkokasd;ladaenx";
            var data = convertor.Convert(new ScrapyCore.Fundamental.Kernel.ContentData() { ContentText = testData });

            Assert.NotNull(data.ContentText);
            Assert.Equal("YWJjZGVmZ2hpZ2tva2FzZDtsYWRhZW54", data.ContentText);
        }
    }
}
