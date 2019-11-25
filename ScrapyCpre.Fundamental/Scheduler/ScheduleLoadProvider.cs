using Newtonsoft.Json;
using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Kernel.Load;
using ScrapyCore.Fundamental.Kernel.Transform;
using ScrapyCore.Fundamental.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class ScheduleLoadProvider : LoadProvider
    {
        private readonly IScheduler scheduler;
        public ScheduleLoadProvider(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        public override async Task Load(Stream content, LoadContext ldContext)
        {
            var sourceId = ldContext.LoadEvent.SourceId;
            ScrapySource scrapySource = await
                ldContext.PlatformModel.CoreCache.RestoreAsync<ScrapySource>(PrefixConst.SOURCE_META + sourceId);
            if (scrapySource.GenType == "DigHttpSource")
            {
                StreamReader reader = new StreamReader(content);
                List<TransformFieldWithValue> values =
                    JsonConvert.DeserializeObject<List<TransformFieldWithValue>>(await reader.ReadToEndAsync());
                List<string> urls = values[0].Value;
                var message = await ldContext.PlatformModel.CoreCache.RestoreAsync<ScheduleMessage>(PrefixConst.MESSAGE_META + scrapySource.MessageId);
                await scheduler.ScheduleBack(scrapySource, ldContext.PlatformModel, urls, message);
            }
            //scheduler.
        }
    }
}
