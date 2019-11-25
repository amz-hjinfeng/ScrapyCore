using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Kernel.Transform;
using ScrapyCore.Fundamental.Scheduler.Models;
using ScrapyCore.Core.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Gen
{
    public class TransformGenManager
    {
        private static TransformGenManager instance;

        public static TransformGenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransformGenManager();
                }
                return instance;
            }
        }

        public TransformEventData GenerateTransform(IDictionary<string, ScrapySource> sources, ScheduleTransform[] scheduleTransform)
        {
            TransformEventData generated = new TransformEventData()
            {
                SourceMapToTransform = new Dictionary<string, List<string>>(),
                TransformEvents = new Dictionary<string, List<TransformEvent>>() /// ALL these should be store to cache.
            };
            foreach (var item in scheduleTransform)
            {
                List<TransformEvent> transformEvents = new List<TransformEvent>();
                foreach (var sourceName in item.MapToSource)
                {
                    var scrapySource = sources[sourceName];
                    string newGuid = Guid.NewGuid().ToString();
                    TransformEvent transformEvent = new TransformEvent()
                    {
                        FieldDefinitions = item.FieldDefinitions,
                        GetFrom = scrapySource.SaveTo,
                        ExportAs = item.ExportAs,
                        JobId = newGuid,
                        SaveTo = "/Load/" + newGuid.ToMD5Hex(),
                        MessageId = scrapySource.MessageId,
                        SourceId = scrapySource.JobId
                    };
                    AddSourceMapToTransform(generated.SourceMapToTransform, scrapySource, transformEvent);
                    transformEvents.Add(transformEvent);
                }
                generated.TransformEvents[item.Name] = transformEvents;
            }
            return generated;
        }

        private void AddSourceMapToTransform(Dictionary<string, List<string>> sourceMapToTransform, ScrapySource scrapySource, TransformEvent transformEvent)
        {
            if (!sourceMapToTransform.ContainsKey(scrapySource.JobId))
            {
                sourceMapToTransform[scrapySource.JobId] = new List<string>();
            }
            var list = sourceMapToTransform[scrapySource.JobId];
            list.Add(transformEvent.JobId);
        }
    }
}
