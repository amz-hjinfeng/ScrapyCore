using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.StringTables;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.StringTables
{
    public class TrimTableConvertorTests
    {
        ContextData contextData;
        public TrimTableConvertorTests()
        {
            contextData = new ContextData();
            contextData.Listing.Add("abc_ ");
            contextData.Listing.Add(" sda ");
        }

        [Fact]
        public void ConvertTest()
        {
            TrimTableConvertor trimTableConvertor = new TrimTableConvertor();
            trimTableConvertor.Convert(contextData);

            Assert.Equal("abc_", contextData.Listing[0].ToString());
            Assert.Equal("sda", contextData.Listing[1].ToString());

        }
    }
}
