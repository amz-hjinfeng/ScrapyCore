using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.MessageOperation
{
    public class ForwardOperation : MessageRawOperation
    {
        private readonly IMessageQueue messageQueue;

        public ForwardOperation(IMessageQueue messageQueue)
        {
            this.messageQueue = messageQueue;
        }
        public override Task Push(PlatformMessage platformMessage)
        {
            return messageQueue.SendQueueMessage(platformMessage);
        }
    }
}
