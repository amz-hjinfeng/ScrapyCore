using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Kernel.Transform;
using ScrapyCore.Fundamental.Scheduler.Gen;
using ScrapyCore.Fundamental.Scheduler.Models;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class DefaultSchueduler : IScheduler
    {
        private readonly IPlatformExit exit;
        private readonly ICache coreCache;

        public DefaultSchueduler(IPlatformExit exit, ICache corecache)
        {
            this.exit = exit;
            this.coreCache = corecache;
        }

        public async Task ScheduleBack(string messageId, string jobId)
        {
            throw new NotImplementedException();
        }

        public async Task ScheduleNew(ScheduleMessage scheduleMessage)
        {

            Dictionary<string, ScrapySource> sourceDict =
                SourceGenManager.Instance.GenerateSource(scheduleMessage.Sources, scheduleMessage.MessageId);

            MessageIndexer messageIndexer = new MessageIndexer()
            {
                MessageId = scheduleMessage.MessageId,
                SourceJobIds = sourceDict.Values.Select(x => x.JobId).ToList()
            };

            Dictionary<string, TransformEvent> transforms =
                new Dictionary<string, TransformEvent>();
            foreach (var item in scheduleMessage.Transforms)
            {
                foreach (var srcKey in item.MapToSource)
                {
                    var source = sourceDict[srcKey];
                    TransformEvent transformEvent = new TransformEvent()
                    {
                        FieldDefinitions = item.FieldDefinitions,
                        ExportAs = item.ExportAs,
                        GetFrom = source.SaveTo,
                        SaveTo = "Hello Data",
                        JobId = Guid.NewGuid().ToString(),
                        MessageId = scheduleMessage.MessageId
                    };

                }
            }

            foreach (var item in scheduleMessage.LandingTarget.LoadMaps)
            {

            }


            await coreCache.StoreAsync(PrefixConst.MESSAGE_JOBs + messageIndexer.MessageId, messageIndexer);

            await PublishSourceJobs(scheduleMessage.MessageName, scheduleMessage.MessageId, messageIndexer.SourceJobIds.ToArray());

        }




        private async Task PublishSourceJobs(string name, string messageId, params string[] sourceJobs)
        {
            foreach (var item in sourceJobs)
            {
                KernelMessage kernelMessage = new KernelMessage()
                {
                    JobId = item,
                    MessageId = messageId,
                    MessageName = name
                };
                await exit.OutRandom(kernelMessage);
            }
        }


    }
}
