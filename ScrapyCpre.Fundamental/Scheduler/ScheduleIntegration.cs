using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class ScheduleIntegration : IWorkingMessageProcessor
    {
        const string META_PREFIX = "meta-";
        private readonly IStorage coreStorage;
        private readonly ICache coreCache;
        private readonly IPlatformExit platformExit;

        public ScheduleIntegration(IStorage coreStorage, ICache coreCache, IPlatformExit platformExit)
        {
            this.coreStorage = coreStorage;
            this.coreCache = coreCache;
            this.platformExit = platformExit;
        }

        public async Task ScheduleNew(ScheduleMessage scheduleMessage)
        {
            await StoreMeta(scheduleMessage);
            IScheduler scheduler = ScheduleManager.Manager
                 .GetScheduler(scheduleMessage.Scheduler);

            await scheduler.ScheduleNew(scheduleMessage);

        }

        public Task StoreMeta(ScheduleMessage scheduleMessage)
        {
            return coreCache.StoreAsync(PrefixConst.MESSAGE_META + scheduleMessage.MessageId, scheduleMessage);
        }

        public Task<ScheduleMessage> RestoreMeta(KernelMessage kernelMessage)
        {
            return coreCache.RestoreAsync<ScheduleMessage>(PrefixConst.MESSAGE_META + kernelMessage.MessageId);
        }

        public async Task Process(byte[] processMessage, IPlatformExit platformExit)
        {
            KernelMessage kernelMessage =
          JsonConvert.DeserializeObject<KernelMessage>(
              Encoding.UTF8.GetString(processMessage));
            ScheduleMessage scheduleMessage = await RestoreMeta(kernelMessage);
            var scheduler = ScheduleManager.Manager
                .GetScheduler(scheduleMessage.Scheduler);
        }
    }
}
