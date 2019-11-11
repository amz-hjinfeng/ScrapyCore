using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core;
using ScrapyCore.Core.External.Utils;
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

            TransformEventData transformEventData =
                TransformGenManager.Instance.GenerateTransform(sourceDict, scheduleMessage.Transforms);

            LoadEventData loadEventData = LoadGenManager.Instance
                .GenerateLoadEvent(transformEventData, scheduleMessage.LandingTarget);


            MessageIndexer messageIndexer = new MessageIndexer()
            {
                MessageId = scheduleMessage.MessageId,
                SourceJobIds = sourceDict.Values
                        .Select(x => x.JobId).ToList(),
                TransformJobIds = transformEventData.TransformEvents
                        .SelectMany(x => x.Value)
                        .Select(x => x.JobId).ToList(),
                LoadJobIds = loadEventData.LoadEvents
                        .Select(x => x.JobId)
                        .ToList()
            };
            TaskingManager manager = new TaskingManager();

            manager.AddTask(coreCache.StoreAsync(PrefixConst.MESSAGE_JOBs + messageIndexer.MessageId, messageIndexer));

            foreach (var srcKey in transformEventData.SourceMapToTransform.Keys)
            {
                manager.AddTask(coreCache.StoreAsync(
                    PrefixConst.SOURCE_TRANSFOR_MAP + srcKey,
                    transformEventData.SourceMapToTransform[srcKey]));
            }
            foreach (var transkv in loadEventData.TransformToLoadMap)
            {
                manager.AddTask(coreCache.StoreStringAsync(
                    PrefixConst.TRANSFORM_LOAD_MAP + transkv.Key, transkv.Value));
            }

            foreach (var source in sourceDict.Values)
            {
                manager.AddTask(coreCache.StoreAsync(
                    PrefixConst.SOURCE_META + source.JobId,
                    source));
            }
            foreach (var transform in transformEventData.TransformEvents.SelectMany(x => x.Value))
            {
                manager.AddTask(coreCache.StoreAsync(
                    PrefixConst.TRANSFORM_META, transform));
            }

            foreach (var load in loadEventData.LoadEvents)
            {
                manager.AddTask(coreCache.StoreAsync(
                    PrefixConst.LOAD_META + load.JobId, load));
            }

            await manager.Wait();
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
