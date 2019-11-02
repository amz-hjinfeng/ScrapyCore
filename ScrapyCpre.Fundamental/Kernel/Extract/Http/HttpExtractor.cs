using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Injection;
using ScrapyCore.Fundamental.Kernel.Extract.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Extract.Http
{
    [Extractor("Http")]
    public class HttpExtractor : ExtractorBase
    {
        public HttpExtractor(
            [Inject("default-agents")]
            IUserAgentPool userAgentPool,
            [Inject("source-storage")]
            IStorage storage)
        {
            UserAgentPool = userAgentPool;
            Storage = storage;
        }

        public IUserAgentPool UserAgentPool { get; }
        public IStorage Storage { get; }

        public override async Task ExtractTarget(string paramter, string path)
        {
            try
            {
                HttpSource httpSource = JsonConvert.DeserializeObject<HttpSource>(paramter);
                HttpWebRequest webRequest = WebRequest.CreateHttp(httpSource.Url);
                webRequest.Accept = httpSource.Accept;
                webRequest.Method = httpSource.Method;
                webRequest.Referer = httpSource.Referer;
                webRequest.UserAgent = UserAgentPool.GetUserAgent(httpSource.UserAgent).AgentString;
                foreach (var kv in httpSource.Header)
                {
                    webRequest.Headers.Add(kv.Key, kv.Value);
                }
                webRequest.ContentType = httpSource.ContentType;
                var response = await webRequest.GetResponseAsync();
                Stream stream = response.GetResponseStream();
                await Storage.WriteStream(stream, path);
            }
            catch (Exception ex)
            {
                Logger.Error("HttpExtractor Exception", ex);
            }
        }
    }
}
