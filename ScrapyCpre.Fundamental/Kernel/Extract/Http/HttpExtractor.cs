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
    [Extractor("Http", ParameterType = typeof(HttpSource))]
    public class HttpExtractor : ExtractorBase
    {
        public HttpExtractor(
            [Inject("default-agents")]
            IUserAgentPool userAgentPool,
            [Inject("CoreStorage")]
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
                webRequest.UserAgent = httpSource.UserAgent == "Random" ?
                    UserAgentPool.GetRandomUserAgent().AgentString
                    : UserAgentPool.GetUserAgent(httpSource.UserAgent).AgentString;
                if (httpSource.Header != null && httpSource.Header.Count > 0)
                {
                    foreach (var kv in httpSource.Header)
                    {
                        webRequest.Headers.Add(kv.Key, kv.Value);
                    }
                }
                webRequest.ContentType = httpSource.ContentType;
                var response = await webRequest.GetResponseAsync();
                Stream stream = response.GetResponseStream();
                MemoryStream content = new MemoryStream();
                stream.CopyTo(content);
                stream.Flush();
                stream.Close();
                content.Seek(0, SeekOrigin.Begin);
                await Storage.WriteBytes(response.Headers.ToByteArray(), path + ".head");
                await Storage.WriteStream(content, path);
                Logger.Info("Http Extractor finished :" + httpSource.Url);
                content.Dispose();
            }
            catch (WebException webex)
            {

            }
            catch (Exception ex)
            {
                Logger.Error("HttpExtractor Exception", ex);
            }
        }
    }
}
