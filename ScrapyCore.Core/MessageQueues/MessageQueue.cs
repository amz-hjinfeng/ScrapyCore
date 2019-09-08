using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.MessageQueues
{
    public abstract class MessageQueue : IMessageQueue
    {
        protected string queueName;

        public MessageQueue(string queueName)
        {
            this.queueName = queueName;
        }

        public abstract Task<IMessageQueueHandler<T>> GetMessage<T>();
        public abstract Task<T> GetMessageComplete<T>();
        public abstract Task<IList<IMessageQueueHandler<T>>> GetMessages<T>();
        public abstract Task SendQueueMessage<T>(T msgObj);
    }
}
