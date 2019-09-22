using ScrapyCore.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ScrapyCore.IntegrationTests
{
    public class BootstrapOnTest
    {
        private static readonly Bootstrap bootstrap;
        private readonly MessageModel messageModel;

        static BootstrapOnTest()
        {
            bootstrap = new Bootstrap();
        }
        public BootstrapOnTest()
        {
            messageModel = new MessageModel()
            {
                Messages = "Hello,This is a message for testing",
            };
        }

        [Fact]
        public void ProvisioningTest()
        {
            Assert.NotEmpty(bootstrap.Provisioning.MessageQueues);
            Assert.NotEmpty(bootstrap.Provisioning.Caches);
            Assert.NotEmpty(bootstrap.Provisioning.Storages);
            Assert.NotNull(bootstrap.Provisioning.ThreadManager);
            Assert.NotEmpty(bootstrap.Provisioning.UseragentPools);
        }
        [Fact]
        public async Task MessageQueueTest()
        {
            var kerriganToHydralisk = bootstrap.Provisioning.MessageQueues["kerrigan-hydralisk"];
            var hydraliskToUtralisks = bootstrap.Provisioning.MessageQueues["hydralisk-utralisks"];
            var utralisksToKerrigan = bootstrap.Provisioning.MessageQueues["utralisks-kerrigan"];
            for (int i = 0; i < 100; i++)
            {
                messageModel.Number = i;
                await kerriganToHydralisk.SendQueueMessage(messageModel);
                Thread.Sleep(10);
                var handler = await kerriganToHydralisk.GetMessage<MessageModel>();
                Assert.NotNull(handler);
                Assert.NotNull(handler.MessageObject);
                Assert.Equal(messageModel.Messages, handler.MessageObject.Messages);
                await handler.Complete();

                await hydraliskToUtralisks.SendQueueMessage(messageModel);
                Thread.Sleep(10);
                handler = await hydraliskToUtralisks.GetMessage<MessageModel>();
                Assert.NotNull(handler);
                Assert.NotNull(handler.MessageObject);
                Assert.Equal(messageModel.Messages, handler.MessageObject.Messages);
                await handler.Complete();

                await utralisksToKerrigan.SendQueueMessage(messageModel);
                Thread.Sleep(10);
                handler = await utralisksToKerrigan.GetMessage<MessageModel>();
                Assert.NotNull(handler);
                Assert.NotNull(handler.MessageObject);
                Assert.Equal(messageModel.Messages, handler.MessageObject.Messages);
                await handler.Complete();
            }
        }

        [Fact]
        public async Task CacheTests()
        {
            var cache = bootstrap.Provisioning.Caches["default-cache"];
            Assert.NotNull(cache);
            await cache.StoreAsync("testKey", messageModel);
            Assert.True(cache.IsKeyExist("testKey"));
            Assert.Equal(messageModel.Messages, cache.Restore<MessageModel>("testKey").Messages);

        }


        public class MessageModel
        {

            public string Messages { get; set; }

            public int Number { get; set; }
        }
    }
}
