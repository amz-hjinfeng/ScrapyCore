using System;
using System.Collections.Generic;

namespace ScrapyCore.Core.Configure
{
    public class AmazonSQSConfigure:IMessageQueueConfigure
    {
        public AmazonSQSConfigure(IStorage configurefile)
        {
        }

        public string MessageQueueEngine => "AmazonSQS";

        public string QueueName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDictionary<string, string> ConfigureDetail => throw new NotImplementedException();
    }
}
