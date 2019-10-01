using System;
using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.MessageQueue;
using ScrapyCore.Core.Storages;
using Xunit;

namespace ScrapyCore.Tests.Core.Configure
{
    public class AmazonSQSConfigureTests 
    {
        IMessageQueueConfigure messageQueueConfigure;
        public AmazonSQSConfigureTests()
        {
            IStorage storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            messageQueueConfigure =
                MessageQueueConfigureFactory.Factory.CreateConfigure(storage, "MockData/Core/Configure/amazonsqs.json");

        }

        [Fact]
        public void MessageQueueEngineTest()
        {
            Assert.Equal("AmazonSQS", messageQueueConfigure.MessageQueueEngine);
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
            Assert.Equal(2, messageQueueConfigure.ConfigureDetail.Count);

        }

    }
}
