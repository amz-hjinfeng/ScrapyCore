using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public class SourceIntergation : IWorkingMessageProcessor
    {
        private readonly ICache coreCache;
        private readonly IExtractorManager extractorManager;

        public SourceIntergation(ICache coreCache, IExtractorManager extractorManager)
        {
            this.coreCache = coreCache;
            this.extractorManager = extractorManager;
        }
        public async Task Process(byte[] message)
        {
            KernelMessage kernelMessage = JsonConvert.DeserializeObject<KernelMessage>(Encoding.UTF8.GetString(message));
            ScrapySource scrapySource = await coreCache.RestoreAsync<ScrapySource>("Source" + kernelMessage.JobId);
            var sourceType = scrapySource.Source.Type;
            IExtractor extractor = extractorManager.GetExtrator(sourceType);
            await extractor.ExtractTarget(scrapySource.Source.Parameters.ToString(), scrapySource.SaveTo);
        }
    }
}
