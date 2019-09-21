using System;
using System.Collections.Generic;

namespace ScrapyCore.Core.Configure
{
    public interface IMessageQueueConfigure : IConfigure
    {
        string MessageQueueEngine { get; }

        string QueueName { get; }

        IDictionary<string,string> ConfigureDetail { get;  }
    }
}
