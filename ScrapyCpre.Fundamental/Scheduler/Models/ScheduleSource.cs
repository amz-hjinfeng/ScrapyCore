using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Models
{
    public class ScheduleSource
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public object Parameters { get; set; }
    }
}
