using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core
{
    public interface IMessageQueue
    {
        Task<T> GetMessageComplete<T>();

        Task<IMessageQueueHandler<T>> GetMessage<T>();

        Task<IList<IMessageQueueHandler<T>>> GetMessages<T>();

        Task SendQueueMessage<T>(T msgObj);

    }
}
