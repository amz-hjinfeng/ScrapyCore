using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.System;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform.Processors
{
    public class SacrificeProcessor : IMessageProcessor
    {
        private readonly ISystemController systemController;

        public SacrificeProcessor(ISystemController systemController)
        {
            this.systemController = systemController;
        }

        public Task ProcessAsync(PlatformMessage platformMessage)
        {
            systemController.Stop();
            systemController.Terminate();
            return Task.CompletedTask;
        }
    }
}
