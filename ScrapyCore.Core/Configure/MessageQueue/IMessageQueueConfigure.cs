using System;
using System.Collections.Generic;

namespace ScrapyCore.Core.Configure
{
    public interface IMessageQueueConfigure
    {
        string MessageQueueEngine { get; }

        string QueueName { get; set; }

        IDictionary<string,string> ConfigureDetail { get;  }
    }
}
