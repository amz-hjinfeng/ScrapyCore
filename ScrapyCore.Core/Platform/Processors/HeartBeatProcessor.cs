using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.Processors
{
    public class HeartBeatProcessor : IMessageProcessor
    {
        private readonly ICache channelStatus;

        public HeartBeatProcessor(ICache channelStatus)
        {
            this.channelStatus = channelStatus;
        }

        public Task ProcessAsync(PlatformMessage platformMessage)
        {
            ///Deserialze the body as 
            return null;
        }
    }
}
