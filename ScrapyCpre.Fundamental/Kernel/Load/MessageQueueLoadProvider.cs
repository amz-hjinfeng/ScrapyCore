using ScrapyCore.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class MessageQueueLoadProvider : LoadProvider
    {
        private readonly IMessageQueue messageQueue;

        public MessageQueueLoadProvider(IMessageQueue messageQueue)
        {
            this.messageQueue = messageQueue;
        }

        public override Task Load(Stream content, object paramter)
        {
            throw new NotImplementedException();
        }
    }
}
