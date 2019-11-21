using ScrapyCore.Fundamental.Kernel.Load;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Gen
{
    public class LoadEventData
    {
        public Dictionary<string, string> TransformToLoadMap { get; set; }

        public List<LoadEvent> LoadEvents { get; set; }

        public LoadEventData()
        {
            TransformToLoadMap = new Dictionary<string, string>();
            LoadEvents = new List<LoadEvent>();
        }


        public void AddLoadEvent(LoadEvent loadEvent)
        {
            LoadEvents.Add(loadEvent);
        }
    }
}
