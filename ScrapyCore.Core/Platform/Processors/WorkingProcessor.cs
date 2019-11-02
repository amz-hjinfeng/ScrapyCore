using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.Processors
{
    public class WorkingProcessor : IMessageProcessor
    {
        public WorkingProcessor(IWorkingMessageProcessor workProcessor)
        {
            WorkProcessor = workProcessor;
        }

        public IWorkingMessageProcessor WorkProcessor { get; }

        public Task ProcessAsync(PlatformMessage platformMessage)
        {
            return WorkProcessor.Process(platformMessage.MessageData);
        }
    }
}
