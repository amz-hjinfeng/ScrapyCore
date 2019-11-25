using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler
{
    public interface ISchedulerConfigure : IConfigure
    {
        string Type { get; set; }
    }
}
