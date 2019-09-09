using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.MessageQueues
{
    public abstract class MessageQueue : IMessageQueue
    {
        protected IMessageQueueConfigure queueConfigure;

        public MessageQueue(IMessageQueueConfigure queueConfigure)
        {
            this.queueConfigure = queueConfigure;
        }

        public abstract Task<IMessageQueueHandler<T>> GetMessage<T>();
        public abstract Task<T> GetMessageComplete<T>();
        public abstract Task<IList<IMessageQueueHandler<T>>> GetMessages<T>();
        public abstract Task SendQueueMessage<T>(T msgObj);
    }
}
