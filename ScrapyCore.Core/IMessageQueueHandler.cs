using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core
{
    public interface IMessageQueueHandler<T>
    {
        T MessageObject { get; }

        Task Complete();
    }
}
