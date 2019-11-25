using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    [Attributes.Convertor(nameof(QuerySelectorConvertor), typeof(string))]
    public class QuerySelectorConvertor : Convertor
    {
        private readonly string query;

        public QuerySelectorConvertor(string query)
        {
            this.query = query;
        }

        public override ContextData Convert(ContextData contentData)
        {
            var document = contentData.AngleSharpDocument;
            var element = document.QuerySelector(query);
            ContextData newContextData = new ContextData();
            newContextData.ContentText = element.Text();
            return newContextData;
        }
    }
}
