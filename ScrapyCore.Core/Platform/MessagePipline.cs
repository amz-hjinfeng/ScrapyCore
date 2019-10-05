using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform
{
    public class MessagePipline : IMessagePipline
    {
        private readonly IMessageQueue messageQueueIn;

        private Queue<PlatformMessage> SiteToSiteMessages { get; }

        public Task<IMessageQueueHandler<PlatformMessage>> FetchMessage()
        {
            lock (SiteToSiteMessages)
            {
                if (this.SiteToSiteMessages.Count > 0)
                {
                    PlatformMessage platformMessage = SiteToSiteMessages.Dequeue();
                    return Task.FromResult((IMessageQueueHandler<PlatformMessage>)new PackagedMessageWithHandler() { MessageObject = platformMessage });
                }
            }
            return messageQueueIn.GetMessage<PlatformMessage>();

        }


        public void PushMessageBySiteToSiteCommand(PlatformMessage platformMessage)
        {
            lock (SiteToSiteMessages)
            {
                SiteToSiteMessages.Enqueue(platformMessage);
            }
        }

        public MessagePipline(IMessageQueue messageQueueIn)
        {
            this.messageQueueIn = messageQueueIn;
            SiteToSiteMessages = new Queue<PlatformMessage>();
        }

        public class PackagedMessageWithHandler : IMessageQueueHandler<PlatformMessage>
        {
            public PlatformMessage MessageObject { get; set; }

            public Task Complete()
            {
                return Task.CompletedTask;
            }
        }
    }
}
