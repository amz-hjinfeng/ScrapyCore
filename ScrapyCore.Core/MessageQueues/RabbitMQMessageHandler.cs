using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.MessageQueues
{
    public class RabbitMQMessageHandler<T> : IMessageQueueHandler<T>
    {
        private readonly IModel model;
        private readonly ulong delieveTag;

        public T MessageObject { get; }


        public RabbitMQMessageHandler(IModel model, ulong delieveTag, T messageObject)
        {
            this.model = model;
            this.delieveTag = delieveTag;
            this.MessageObject = messageObject;
        }

        public Task Complete()
        {
            model.BasicAck(delieveTag, false);
            return Task.CompletedTask;
        }
    }
}
