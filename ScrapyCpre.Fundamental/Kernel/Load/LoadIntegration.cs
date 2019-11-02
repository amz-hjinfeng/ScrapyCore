using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class LoadIntegration : IWorkingMessageProcessor
    {

        public LoadIntegration(ICache coreCache)
        {
            CoreCache = coreCache;
        }

        public ICache CoreCache { get; }

        public Task Process(byte[] processMessage)
        {
            KernelMessage kernelMessage = JsonConvert.DeserializeObject<KernelMessage>(Encoding.UTF8.GetString(processMessage));


            return Task.CompletedTask;
        }
    }
}
