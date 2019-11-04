using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.StringTables
{
    [Attributes.Convertor(nameof(XPathOutHtmlTableConvertor), typeof(string))]
    public class XPathOutHtmlTableConvertor : Convertor
    {
        private readonly string xpath;

        public XPathOutHtmlTableConvertor(string xpath)
        {
            this.xpath = xpath;
        }

        public override ContextData Convert(ContextData contentData)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(contentData.ContentText);
            var nodes = doc.DocumentNode.SelectNodes(xpath);
            for (int i = 0; i < nodes.Count; i++)
            {
                contentData.Listing.Add(nodes[i].OuterHtml);
            }
            return contentData;
        }
    }
}
