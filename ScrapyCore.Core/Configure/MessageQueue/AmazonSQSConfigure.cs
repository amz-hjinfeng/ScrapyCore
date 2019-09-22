using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ScrapyCore.Core.Configure.MessageQueue;
using ScrapyCore.Core.Consts;

namespace ScrapyCore.Core.Configure
{
    public class AmazonSQSConfigure:IMessageQueueConfigure
    {
        public AmazonSQSConfigure(MessageQueueConfigureModel configureModel)
        {
            this.ConfigureDetail = configureModel.Configure.ToDictionary(x => x[0], x => x[1]);
            this.QueueName = configureModel.QueueName;
        }

        public string MessageQueueEngine => "AmazonSQS";

        public string QueueName { get; }

        public IDictionary<string, string> ConfigureDetail { get; private set; }

    }
}
