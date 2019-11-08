using ScrapyCore.Fundamental.Kernel.Transform;
using ScrapyCore.Fundamental.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Models
{
    public class ScheduleTransform
    {
        public string Name { get; set; }

        public string ExportAs { get; set; }

        public string[] MapToSource { get; set; }

        public FieldDefinition[] FieldDefinitions { get; set; }


    }
}
