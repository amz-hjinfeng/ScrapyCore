using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Models
{
    public class ScheduleMessage
    {
        public string MessageId { get; set; }
        public string MessageName { get; set; }
        public string Scheduler { get; set; }
        public ScheduleSource[] Sources { get; set; }
        public ScheduleTransform[] Transforms { get; set; }
        public ScheduleLoad LandingTarget { get; set; }
    }
}
