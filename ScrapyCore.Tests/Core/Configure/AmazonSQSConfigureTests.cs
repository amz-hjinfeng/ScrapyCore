using System;
using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Storages;
using Xunit;

namespace ScrapyCore.Tests.Core.Configure
{
    public class AmazonSQSConfigureTests
    {
        IMessageQueueConfigure messageQueueConfigure;
        public AmazonSQSConfigureTests()
        {
            IStorage storage = new LocalFileSystemStorage(ConstVariable.ApplicationPath);
            messageQueueConfigure = new AmazonSQSConfigure(storage, "MockData/Core/Configure/messagequeueconfigure.json");
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
