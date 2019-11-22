using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.External.Utils;
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


        public async Task Process(byte[] processMessage, IPlatformExit platformExit)
        {
            KernelMessage kernelMessage = JsonConvert.DeserializeObject<KernelMessage>(Encoding.UTF8.GetString(processMessage));
            ScrapySource scrapySource = await coreCache.RestoreAsync<ScrapySource>(PrefixConst.SOURCE_META + kernelMessage.JobId);
            var sourceType = scrapySource.Source.Type;
            IExtractor extractor = extractorManager.GetExtrator(sourceType);
            await extractor.ExtractTarget(scrapySource.Source.Parameters.ToString(), scrapySource.SaveTo);
            string transformJobIdsKey = PrefixConst.SOURCE_TRANSFOR_MAP + kernelMessage.JobId;
            TaskingManager taskingManager = new TaskingManager();
            List<string> jobIds = await coreCache.RestoreAsync<List<string>>(transformJobIdsKey);
            foreach (var x in jobIds)
            {
                KernelMessage transforMsg = new KernelMessage()
                {
                    JobId = x,
                    MessageId = kernelMessage.MessageId,
                    MessageName = kernelMessage.MessageName
                };
                taskingManager.AddTask(platformExit.OutRandom(x));
            }
            await taskingManager.WhenAll();
        }
    }
}
