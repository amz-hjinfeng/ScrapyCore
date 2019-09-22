using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapyCore.Core.Configure.MessageQueue
{
    public class RabbitMQConfigure : IMessageQueueConfigure
    {
        public RabbitMQConfigure(MessageQueueConfigureModel messageQueueConfigureModel)
        {
            this.QueueName = messageQueueConfigureModel.QueueName;
            this.ConfigureDetail = messageQueueConfigureModel.Configure.ToDictionary(x => x[0], x => x[1]);
        }

        public string MessageQueueEngine => "RabbitMQ";

        public string QueueName { get; }

        public IDictionary<string, string> ConfigureDetail { get; }
    }
}
