using System;
using System.Collections.Generic;
using System.Text;
using ScrapyCore.Core.External;
using Xunit;

namespace ScrapyCore.Tests.Core.External
{
    public class BytesOperatorsTests
    {
        public BytesOperatorsTests()
        {

        }

        [Fact]
        public void ToHexTest()
        {
            byte[] testData = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            var hex = testData.ToHex();
            Assert.Equal("0102030405", hex);

        }
    }
}
