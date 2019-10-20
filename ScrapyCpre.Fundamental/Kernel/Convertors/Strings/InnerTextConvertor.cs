using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{

    [Attributes.Convertor(nameof(InnerTextConvertor), null)]
    public class InnerTextConvertor : Convertor
    {
        public override ContextData Convert(ContextData contentData)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(contentData.ContentText);
            contentData.ContentText = htmlDoc.DocumentNode.InnerText;
            return contentData;
        }
    }
}
