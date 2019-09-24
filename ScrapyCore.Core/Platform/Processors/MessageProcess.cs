using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.Processors
{
    public abstract class MessageProcess : IMessageProcessor
    {
        public abstract Task ProcessAsync(PlatformMessage platformMessage);
    }
}
