using ScrapyCore.Fundamental.Kernel.Convertors.Enums;
using ScrapyCore.Fundamental.Meta;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public class HttpSourceMeta
    {
        private CookieContainer _cookieContainer = new CookieContainer();
        //private int _cookieClean = 0;

        public string Encoding { get; set; }

        public string ContentType { get; set; }

        public string Host { get; set; }

        public string Method { get; set; }

        public string Referer { get; set; }

        public string Accept { get; set; }

        public string UserAgent { get; set; }

        public int Sleep { get; set; }

        public int CookieClean { get; set; }

        public List<KeyValuePair<string, string>> Headers { get; set; }

        public ConvertorNavigator UrlConventorNavigator { get; set; }

        public HttpSourceMeta()
        {
            Encoding = "utf-8";
            ContentType = "text/html; charset=utf-8";
            Method = "GET";
            Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
            Host = string.Empty;
            Referer = string.Empty;

            Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Accept-Language",  "zh-CN,zh;q=0.8,en;q=0.6"),
                new KeyValuePair<string, string>("Cache-Control","max-age=0")
            };
        }

    }
}
