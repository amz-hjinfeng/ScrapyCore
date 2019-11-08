using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    [Attributes.Convertor(nameof(XPathOuterHtmlConvertor), typeof(string))]
    public class XPathOuterHtmlConvertor : Convertor
    {
        private readonly string xPathValue;

        public XPathOuterHtmlConvertor(string xPathValue)
        {
            this.xPathValue = xPathValue;
        }

        public override ContextData Convert(ContextData contentData)
        {
            var doc = contentData.AgilityDocument;
            contentData.ContentText = doc.DocumentNode.SelectSingleNode(xPathValue).OuterHtml;
            return contentData;
        }
    }
}
