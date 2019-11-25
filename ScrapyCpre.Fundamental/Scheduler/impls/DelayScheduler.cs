using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScrapyCore.Core.External;
using ScrapyCore.Core.External.Conventor;
using ScrapyCore.Core.External.Utils;
using ScrapyCore.Core.Platform;
using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Kernel.Extract.Http;
using ScrapyCore.Fundamental.Scheduler.Gen;
using ScrapyCore.Fundamental.Scheduler.Impls.Web;
using ScrapyCore.Fundamental.Scheduler.Models;

namespace ScrapyCore.Fundamental.Scheduler.impls
{
    public class DelayScheduler : IScheduler
    {
        int sendRate;
        public DelayScheduler(ISchedulerConfigure schedulerConfigure)
        {
            sendRate = schedulerConfigure.ConfigureDetail.GetKeyAndConvertTo("SendRate", StringToInt32Conventor.Instance);
        }

        public async Task ScheduleBack(ScrapySource source, PlatformModel platformModel, List<string> urls, ScheduleMessage scheduleMessage)
        {
            HttpSource httpSource = JsonConvert.DeserializeObject<HttpSource>(source.Source.Parameters.ToString());
            if (httpSource.Layer > 0)
            {
                var transforms = scheduleMessage.Transforms
                    .Where(x => x.MapToSource.Contains(source.Name))
                    .Select(x => { x.MapToSource = new string[] { source.Name }; return x; })
                    .ToArray();
                var scheduleSource = scheduleMessage.Sources.Where(x => x.Name == source.Name).First();
                var loadMaps = scheduleMessage.LandingTargets.LoadMaps
                    .Where(x => transforms.Any(y => y.Name == x.FromTransform))
                    .ToArray();
                WebSeed webSeed = JsonConvert.DeserializeObject<WebSeed>(scheduleSource.Parameters.ToString());
                foreach (var url in urls)
                {
                    webSeed.SeedUrl = url;
                    webSeed.Depth = httpSource.Layer - 1;
                    scheduleSource.Parameters = webSeed;
                    ScheduleMessage subSchedule = new ScheduleMessage()
                    {
                        MessageId = scheduleMessage.MessageId,
                        Sources = new ScheduleSource[] { scheduleSource },
                        Transforms = transforms,
                        LandingTargets = new ScheduleLoad()
                        {
                            LoadProviders = scheduleMessage.LandingTargets.LoadProviders,
                            LoadMaps = loadMaps
                        },
                        MessageName = scheduleMessage.MessageName,
                        Scheduler = scheduleMessage.Scheduler
                    };
                    await Task.Delay(sendRate);
                    await ScheduleNew(subSchedule, platformModel);
                }
            }
        }

        public Task ScheduleNew(ScheduleMessage scheduleMessage)
        {
            throw new NotImplementedException();
        }

        public async Task ScheduleNew(ScheduleMessage scheduleMessage, PlatformModel platformModel)
        {
            Dictionary<string, ScrapySource> sourceDict =
                SourceGenManager.Instance.GenerateSource(scheduleMessage.Sources, scheduleMessage.MessageId);

            TransformEventData transformEventData =
                TransformGenManager.Instance.GenerateTransform(sourceDict, scheduleMessage.Transforms);

            LoadEventData loadEventData = LoadGenManager.Instance
                .GenerateLoadEvent(transformEventData, scheduleMessage.LandingTargets);

            MessageIndexer messageIndexer = new MessageIndexer()
            {
                MessageId = scheduleMessage.MessageId,
                MessageName = scheduleMessage.MessageName,
                StartTime = DateTime.Now,
                SourceJobIds = sourceDict.Values
                        .Select(x => x.JobId).ToDictionary(x => x, x => 0),
                TransformJobIds = transformEventData.TransformEvents
                        .SelectMany(x => x.Value)
                        .Select(x => x.JobId)
                        .ToDictionary(x => x, x => 0),
                LoadJobIds = loadEventData.LoadEvents
                        .Select(x => x.JobId)
                        .ToDictionary(x => x, x => 0)
            };
            TaskingManager manager = new TaskingManager();

            manager.AddTask(platformModel.CoreCache.StoreAsync(PrefixConst.MESSAGE_JOBs + messageIndexer.MessageId, messageIndexer));

            foreach (var srcKey in transformEventData.SourceMapToTransform.Keys)
            {
                manager.AddTask(platformModel.CoreCache.StoreAsync(
                    PrefixConst.SOURCE_TRANSFOR_MAP + srcKey,
                    transformEventData.SourceMapToTransform[srcKey]));
            }
            foreach (var transkv in loadEventData.TransformToLoadMap)
            {
                manager.AddTask(platformModel.CoreCache.StoreStringAsync(
                    PrefixConst.TRANSFORM_LOAD_MAP + transkv.Key, transkv.Value));
            }

            foreach (var source in sourceDict.Values)
            {
                manager.AddTask(platformModel.CoreCache.StoreAsync(
                    PrefixConst.SOURCE_META + source.JobId,
                    source));
            }
            foreach (var transform in transformEventData.TransformEvents.SelectMany(x => x.Value))
            {
                manager.AddTask(platformModel.CoreCache.StoreAsync(
                    PrefixConst.TRANSFORM_META + transform.JobId, transform));
            }

            foreach (var load in loadEventData.LoadEvents)
            {
                manager.AddTask(platformModel.CoreCache.StoreAsync(
                    PrefixConst.LOAD_META + load.JobId, load));
            }



            await manager.WhenAll();
            await PublishSourceJobs(
                    scheduleMessage.MessageName,
                    scheduleMessage.MessageId,
                    platformModel.PlatformExit,
                    messageIndexer.SourceJobIds.Keys.ToArray());
        }

        private async Task PublishSourceJobs(string name, string messageId, IPlatformExit exit, params string[] sourceJobs)
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
