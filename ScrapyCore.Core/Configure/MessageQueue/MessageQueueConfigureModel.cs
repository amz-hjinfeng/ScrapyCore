using System;
namespace ScrapyCore.Core.Configure.MessageQueue
{
    public class MessageQueueConfigureModel
    {
        public string MessageQueueEngine { get; set; }

        public string QueueName { get; set; }

        public string [][] Configure { get; set; }

    }
}
