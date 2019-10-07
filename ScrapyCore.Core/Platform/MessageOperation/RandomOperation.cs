using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.Processors;

namespace ScrapyCore.Core.Platform.MessageOperation
{
    public class RandomOperation : MessageRawOperation
    {
        public RandomOperation(IMessageProcessorManager messageProcessorManager)
        {
            MessageProcessorManager = messageProcessorManager;
        }

        public IMessageProcessorManager MessageProcessorManager { get; }

        public override Task Push(PlatformMessage platformMessage)
        {
            return MessageProcessorManager.ProcessMessage(platformMessage);
        }
    }
}
