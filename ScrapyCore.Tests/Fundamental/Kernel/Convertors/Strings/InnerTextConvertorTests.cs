using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.Strings;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Strings
{
    public class InnerTextConvertorTests
    {
        public InnerTextConvertorTests()
        {

        }

        [Fact]
        public void ConvertTest()
        {
            string srcData = "<div class=\"mask-dark\">test</div>";
            InnerTextConvertor innerTextConvertor = new InnerTextConvertor();
            Assert.Equal("test",
                innerTextConvertor.Convert(new ContentData()
                {
                    ContentText = srcData,
                }).ContentText
            );

        }
    }
}
