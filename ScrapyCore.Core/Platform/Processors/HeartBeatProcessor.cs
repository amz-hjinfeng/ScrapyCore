using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.Processors.Model;

namespace ScrapyCore.Core.Platform.Processors
{
    public class HeartBeatProcessor : IMessageProcessor
    {
        private readonly ICache channelStatus;

        public HeartBeatProcessor(ICache channelStatus)
        {
            this.channelStatus = channelStatus;
        }

        public async Task ProcessAsync(PlatformMessage platformMessage)
        {
            string hbdInfo = Encoding.UTF8.GetString(platformMessage.MessageData);
            HeartBeatModel heartBeatModel = JsonConvert.DeserializeObject<HeartBeatModel>(hbdInfo);
            await channelStatus.StoreAsync("instance-" + heartBeatModel.Id, heartBeatModel, new TimeSpan(0, 0, 10));
            await channelStatus.StoreAsync("channel-" + heartBeatModel.ChannelId, new ChannelModel()
            {
                Congestion = (DateTime.Now - heartBeatModel.SentTime).TotalMilliseconds,
                Id = heartBeatModel.ChannelId
            }, new TimeSpan(0, 0, 10));

        }
    }
}
