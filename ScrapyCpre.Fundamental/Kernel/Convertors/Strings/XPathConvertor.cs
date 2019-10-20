using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    public class XPathConvertor : Convertor
    {
        private readonly string xPathValue;

        public XPathConvertor(string xPathValue)
        {
            this.xPathValue = xPathValue;
        }

        public override ContentData Convert(ContentData contentData)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(contentData.ContentText);
            contentData.ContentText = doc.DocumentNode.SelectSingleNode(xPathValue).OuterHtml;
            return contentData;
        }
    }
}
