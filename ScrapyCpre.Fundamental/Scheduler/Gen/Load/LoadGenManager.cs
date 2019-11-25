using ScrapyCore.Fundamental.Kernel.Load;
using ScrapyCore.Fundamental.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Gen
{
    public class LoadGenManager
    {
        private static LoadGenManager instance;

        public static LoadGenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoadGenManager();
                }
                return instance;
            }
        }


        private LoadGenManager()
        { }

        public LoadEventData GenerateLoadEvent(TransformEventData eventData, ScheduleLoad scheduleLoad)
        {
            LoadEventData loadEventData = new LoadEventData();
            var providers = scheduleLoad.LoadProviders;
            List<string> selectedTransform = scheduleLoad
                    .LoadMaps
                    .Select(x => x.FromTransform)
                    .Distinct()
                    .ToList();

            foreach (var transformKey in selectedTransform)
            {
                var transforms = eventData.TransformEvents[transformKey];
                var loads = scheduleLoad.LoadMaps
                    .Where(x => x.FromTransform == transformKey);
                foreach (var transform in transforms)
                {
                    var jobId = Guid.NewGuid().ToString();
                    LoadEvent loadEvent = new LoadEvent()
                    {
                        JobId = jobId,
                        LoadProviders = providers,
                        MessageId = transform.MessageId,
                        SourceId = transform.SourceId,
                        TransformId = transform.JobId,
                        Data = loads.Select(x => new LoadDataNavigator()
                        {
                            DataPacket = transform.SaveTo,
                            Provider = new LoadProviderSelection()
                            {
                                Name = x.LoadProvider,
                                Parameter = x.Parameter /// Parameter Enablement
                            }
                        }).ToArray()
                    };
                    loadEventData.AddLoadEvent(loadEvent);
                    loadEventData.TransformToLoadMap[transform.JobId] = jobId;
                }
            }
            return loadEventData;
        }
    }
}
