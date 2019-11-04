using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.StringTables
{
    [Attributes.Convertor(nameof(InnerTextTableConvertor), null)]
    public class InnerTextTableConvertor : Convertor
    {
        public override ContextData Convert(ContextData contentData)
        {
            var newContextData = new ContextData();
            foreach (string outStr in contentData.Listing)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(outStr);
                newContextData.Listing.Add(htmlDoc.DocumentNode.InnerText);
            }
            return newContextData;
        }
    }
}
