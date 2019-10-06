using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScrapyCore.Tests.Core.Platform
{
    public class MessageEntranceTest
    {
        MessageEntrance messageEntrance;
        private PlatformMessage PlatformMessageInQueue;
        private PlatformMessage PlatformMessageInMemory;
        public MessageEntranceTest()
        {
            this.PlatformMessageInQueue = new PlatformMessage()
            {
                Command = new ScrapyCore.Core.Platform.Commands.Command()
                {
                    CommandCode = ScrapyCore.Core.Platform.Commands.CommandCode.Configure,
                    CommandType = ScrapyCore.Core.Platform.Commands.CommandTransfer.Random
                }
            };
            this.PlatformMessageInMemory = new PlatformMessage()
            {
                Command = new ScrapyCore.Core.Platform.Commands.Command()
                {
                    CommandCode = ScrapyCore.Core.Platform.Commands.CommandCode.HeartBeat,
                    CommandType = ScrapyCore.Core.Platform.Commands.CommandTransfer.Forward
                }
            };

            IMessageQueueHandler<PlatformMessage> messageQueueHandler = Moq.Mock.Of<IMessageQueueHandler<PlatformMessage>>();
            Moq.Mock.Get(messageQueueHandler).Setup(x => x.MessageObject).Returns(PlatformMessageInQueue);
            IMessageQueue messageQueue = Moq.Mock.Of<IMessageQueue>();
            Moq.Mock.Get(messageQueue).Setup(x => x.GetMessage<PlatformMessage>())
                .Returns(Task.FromResult(messageQueueHandler));

            messageEntrance = new MessageEntrance(messageQueue);

        }

        [Fact]
        public async Task FetchMessageTest()
        {
            messageEntrance.PushMessageBySiteToSiteCommand(PlatformMessageInMemory);

            var handler = await messageEntrance.FetchMessage();
            Assert.NotNull(handler);
            Assert.Equal(PlatformMessageInMemory, handler.MessageObject);

            handler = await messageEntrance.FetchMessage();
            Assert.NotNull(handler);
            Assert.Equal(PlatformMessageInQueue, handler.MessageObject);
        }
    }
}
