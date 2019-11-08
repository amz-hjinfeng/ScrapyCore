using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel
{
    public class ContextData
    {
        public string ContentText { get; set; }

        public List<KeyValuePair<string, string>> Session { get; set; }

        public List<object> Listing { get; set; }

        private Lazy<IDocument> angleSharpDocument;

        private Lazy<HtmlDocument> agilityDocument;

        public IDocument AngleSharpDocument => angleSharpDocument.Value;

        public HtmlDocument AgilityDocument => agilityDocument.Value;

        public ContextData()
        {
            Session = new List<KeyValuePair<string, string>>();
            Listing = new List<object>();
            ContentText = string.Empty;

            angleSharpDocument = new Lazy<IDocument>(() =>
            {
                var config = Configuration.Default;
                var context = BrowsingContext.New(config);
                var document = context.OpenAsync(req => req.Content(this.ContentText)).Result;
                return document;
            });

            agilityDocument = new Lazy<HtmlDocument>(() =>
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(ContentText);
                return doc;
            });

        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
