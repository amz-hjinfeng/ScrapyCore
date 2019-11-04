using ScrapyCore.Core;
using ScrapyCore.Core.Storages;
using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.StringTables;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.StringTables
{
    public class XPathOutHtmlTableConvertorTests
    {
        string xpath = "/html/body/div[@class='company']";
        string data = "";
        public XPathOutHtmlTableConvertorTests()
        {
            IStorage storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            data = storage.GetString("MockData/Fundamental/Transform/MockTableData.html");
        }

        [Fact]
        public void ConvertTest()
        {
            XPathOutHtmlTableConvertor convertor = new XPathOutHtmlTableConvertor(xpath);
            ContextData contentData = new ContextData() { ContentText = data };
            convertor.Convert(contentData);
            Assert.NotEmpty(contentData.Listing);
            Assert.NotNull(contentData.ContentText);


        }
    }
}
