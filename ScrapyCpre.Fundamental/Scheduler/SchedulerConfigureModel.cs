using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class SchedulerConfigureModel : ISchedulerConfigure
    {
        public string Type { get; set; }

        IDictionary<string, string> IConfigure.ConfigureDetail => this.Configure.ToDictionary(x => x[0], x => x[1]);

        public string[][] Configure { get; set; }
    }
}
