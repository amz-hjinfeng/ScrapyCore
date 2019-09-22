using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.MessageQueue;
using ScrapyCore.Core.Storages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Core.Configure
{
    public class RabbitMQConfigureTests
    {
        IMessageQueueConfigure messageQueueConfigure;
        public RabbitMQConfigureTests()
        {
            IStorage storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            messageQueueConfigure =
                MessageQueueConfigureFactory.Factory.CreateConfigure(storage, "MockData/Core/Configure/rabbitmq.json");
        }

        [Fact]
        public void MessageQueueEngineTest()
        {
            Assert.Equal("RabbitMQ", messageQueueConfigure.MessageQueueEngine);
        }

        [Fact]
        public void QueueNameTest()
        {
            Assert.Equal("Kerrigan", messageQueueConfigure.QueueName);
        }

        [Fact]
        public void ConfigureDetailTest()
        {
            Assert.NotEmpty(messageQueueConfigure.ConfigureDetail);
            Assert.Equal(12, messageQueueConfigure.ConfigureDetail.Count);
        }
    }
}
