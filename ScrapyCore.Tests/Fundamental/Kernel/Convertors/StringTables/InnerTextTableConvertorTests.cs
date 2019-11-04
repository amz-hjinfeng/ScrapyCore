using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.StringTables;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.StringTables
{
    public class InnerTextTableConvertorTests
    {
        ContextData contextData;
        public InnerTextTableConvertorTests()
        {
            contextData = new ContextData();
            contextData.Listing.Add("<company>1</company>");
            contextData.Listing.Add("<company>2</company>");
            contextData.Listing.Add("<company>3</company>");
            contextData.Listing.Add("<company>4</company>");
            contextData.Listing.Add("<company>5</company>");
            contextData.Listing.Add("<company>6</company>");
        }

        [Fact]
        public void ConvertTest()
        {
            InnerTextTableConvertor innerTextTable = new InnerTextTableConvertor();
            var result = innerTextTable.Convert(contextData);
            Assert.Equal(6, result.Listing.Count);
            Assert.Equal("1", result.Listing[0].ToString());

        }
    }
}
