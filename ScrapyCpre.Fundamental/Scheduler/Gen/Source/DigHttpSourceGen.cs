using Newtonsoft.Json;
using ScrapyCore.Fundamental.Kernel.Extract.Http;
using ScrapyCore.Fundamental.Scheduler.Attributes;
using ScrapyCore.Fundamental.Scheduler.Impls.Web;
using ScrapyCore.Core.External;

namespace ScrapyCore.Fundamental.Scheduler.Gen
{
    [SourceGen("DigHttpSource", ParameterType = typeof(WebSeed))]
    public class DigHttpSourceGen : ISourceGen
    {
        public string GenType => "DigHttpSource";

        public ParamWithId GetParameter(object templateParameter, string identificationCode)
        {
            WebSeed seed = JsonConvert.DeserializeObject<WebSeed>(templateParameter.ToString());
            HttpSource httpSource = new HttpSource()
            {
                Accept = seed.Accept,
                ContentType = seed.ContentType,
                Encoding = seed.Encoding,
                Header = seed.Header,
                Method = seed.Method,
                Referer = seed.Referer,
                Url = seed.SeedUrl,
                UserAgent = seed.UserAgent
            };

            string hashid = (seed.SeedUrl + this.GenType + identificationCode).ToMD5Hex();
            string recommendLoacation = $"level-{seed.Depth}/" + hashid;

            return new ParamWithId()
            {
                Parameter = httpSource,
                Id = hashid,
                RecommendLocation = recommendLoacation,
                SourceType = "Http"
            };
        }
    }
}
