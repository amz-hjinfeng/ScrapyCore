using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Impls.Web
{
    public class WebSeed
    {
        public string Encoding { get; set; }
        public string ContentType { get; set; }
        public string Method { get; set; }
        public string Referer { get; set; }
        public string Accept { get; set; }
        public string UserAgent { get; set; }
        public Dictionary<string, string> Header { get; set; }
        public string SeedUrl { get; set; }
        public int Depth { get; set; }

    }
}
