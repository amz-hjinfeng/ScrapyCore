using ScrapyCore.Fundamental.Kernel.Convertors.Strings;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Strings
{
    public class ReplaceConvertorTests
    {
        public ReplaceConvertorTests()
        {

        }

        [Fact]
        public void ConvertTest()
        {
            string source = "Hello! {REPLACE_PLACEHOLDER}";
            string replaceTarget = "That 's that target";
            ReplaceConvertor replaceConvertor = new ReplaceConvertor(new ReplaceConvertor.ConvertorInput()
            {
                PlaceHolder = "{REPLACE_PLACEHOLDER}",
                ReplaceTarget = replaceTarget
            });

            var result = replaceConvertor.Convert(new ScrapyCore.Fundamental.Kernel.ContentData()
            {
                ContentText = source
            });
            Assert.NotEmpty(result.ContentText);
            Assert.Equal("Hello! That 's that target", result.ContentText);
        }
    }
}
