using Newtonsoft.Json;
using ScrapyCore.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Impls.Web
{
    public class WebSeed
    {
        [Field(AcceptTypeName = "string", DefaultValue = "utf-8", FieldName = nameof(Encoding))]
        public string Encoding { get; set; }

        [Field(AcceptTypeName = "string", DefaultValue = "text/html; charset=utf-8", FieldName = nameof(ContentType))]
        public string ContentType { get; set; }

        [Field(AcceptTypeName = "string", DefaultValue = "GET", FieldName = nameof(Method))]
        public string Method { get; set; }

        [Field(AcceptTypeName = "string", DefaultValue = "", FieldName = nameof(Referer))]
        public string Referer { get; set; }

        [Field(AcceptTypeName = "string", DefaultValue = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8", FieldName = nameof(Accept))]
        public string Accept { get; set; }

        [Field(AcceptTypeName = "string", DefaultValue = "Random", FieldName = nameof(UserAgent))]
        public string UserAgent { get; set; }

        [Field(AcceptTypeName = "map", DefaultValue = "", FieldName = nameof(Header))]
        public Dictionary<string, string> Header { get; set; }

        [Field(AcceptTypeName = "string", DefaultValue = "https://www.sina.com.cn", FieldName = nameof(SeedUrl))]
        public string SeedUrl { get; set; }

        [Field(AcceptTypeName = "int", DefaultValue = "2", FieldName = nameof(Depth))]
        public int Depth { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
