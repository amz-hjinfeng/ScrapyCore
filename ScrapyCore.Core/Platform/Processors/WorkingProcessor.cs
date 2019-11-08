using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.Processors
{
    public class WorkingProcessor : IMessageProcessor
    {
        private readonly IPlatformExit exit;

        public WorkingProcessor(IWorkingMessageProcessor workProcessor, IPlatformExit exit)
        {
            WorkProcessor = workProcessor;
            this.exit = exit;
        }

        public IWorkingMessageProcessor WorkProcessor { get; }

        public Task ProcessAsync(PlatformMessage platformMessage)
        {
            return WorkProcessor.Process(platformMessage.MessageData, exit);
        }
    }
}
