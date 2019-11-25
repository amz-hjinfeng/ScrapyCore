using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Extract.Http
{
    public class HttpSource
    {
        public string GenName { get; set; }
        public int Layer { get; set; }
        public string Encoding { get; set; }
        public string ContentType { get; set; }
        public string Method { get; set; }
        public string Referer { get; set; }
        public string Accept { get; set; }
        public string UserAgent { get; set; }
        public Dictionary<string, string> Header { get; set; }
        public string Url { get; set; }
    }
}
