using ScrapyCore.Fundamental.Kernel.Load;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Models
{
    public class ScheduleLoad
    {
        public LoadProviderNavigator[] LoadProviders { get; set; }

        public LoadMap[] LoadMaps { get; set; }

        public class LoadMap
        {
            public string FromTransform { get; set; }

            public string LaadProvider { get; set; }
        }
    }
}
