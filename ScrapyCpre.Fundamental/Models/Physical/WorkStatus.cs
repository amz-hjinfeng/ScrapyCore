using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCpre.Fundamental.Models.Physical
{
    public class WorkStatus
    {
        public Guid Id { get; set; }

        public DateTime QueuedTime { get; set; }

        public DateTime ProcessTime { get; set; }

        public DateTime AnalysisTIme { get; set; }

        public MessageStatus WorkingStatus { get; set; }
    }
}
