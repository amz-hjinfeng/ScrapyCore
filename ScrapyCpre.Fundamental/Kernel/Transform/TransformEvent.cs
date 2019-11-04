using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Transform
{
    public class TransformEvent
    {
        public string MessageId { get; set; }

        public string JobId { get; set; }

        public string ExportAs { get; set; }

        public string GetFrom { get; set; }

        public string SaveTo { get; set; }

        public FieldDefinition[] FieldDefinitions { get; set; }

    }
}
