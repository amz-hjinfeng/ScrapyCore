using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.MessageOperation
{
    public class SiteToSiteOperation : MessageRawOperation
    {
        public override async Task Push(PlatformMessage platformMessage)
        {
            try
            {
                if (platformMessage.NextJump != null)
                {
                    logger.Info("Jump tp " + platformMessage.NextJump.Id);
                    HttpWebRequest httpWebRequest = WebRequest.CreateHttp(new Uri($"http://{platformMessage.NextJump.IpAddress}/api/SiteToSiteJump", UriKind.Absolute));
                    httpWebRequest.Headers.Add("x-principal", platformMessage.NextJump.IpAddress);
                    httpWebRequest.Headers.Add("x-principal-id", platformMessage.NextJump.Id);
                    httpWebRequest.Method = "POST";
                    var data = JsonConvert.SerializeObject(platformMessage);
                    var buffer = Encoding.UTF8.GetBytes(data);
                    var stream = await httpWebRequest.GetRequestStreamAsync();
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                    await stream.FlushAsync();
                    var response = await httpWebRequest.GetResponseAsync();
                    logger.Info("Complete jump " + platformMessage.NextJump.IpAddress);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Jump to " + platformMessage.NextJump.Id, ex);
            }
        }
    }
}
