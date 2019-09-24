using ScrapyCore.Core.Platform.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform
{
    public class MessagingLayer
    {
        private readonly Pricipal pricipal;
        private readonly IMessageQueue messageQueueIn;
        private readonly IMessageQueue messageQueueQut;
        private PlatformMessage platformMessage;

        public MessagingLayer(Pricipal pricipal,
            IMessageQueue messageQueueIn,
            IMessageQueue messageQueueQut
            )
        {
            this.pricipal = pricipal;
            this.messageQueueIn = messageQueueIn;
            this.messageQueueQut = messageQueueQut;
        }

        public async Task MessagingProcessor(IMessageProcessor processor)
        {
            var messageHandler = await messageQueueIn.GetMessage<PlatformMessage>();
            MessageRoute messageRoute = new MessageRoute(pricipal);
            this.platformMessage = messageHandler.MessageObject;
            await processor.ProcessAsync(platformMessage);
            messageRoute.Complete();
            await messageHandler.Complete();
            this.platformMessage.Routes.Add(messageRoute);
            await messageQueueQut.SendQueueMessage(this.platformMessage);
        }


    }
}
