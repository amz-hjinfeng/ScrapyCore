using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public class ScrapySource
    {
        public string GenType { get; set; }
        public string Name { get; set; }
        public string MessageId { get; set; }
        public string JobId { get; set; }
        public string SaveTo { get; set; }
        public SourceObject Source { get; set; }
    }
}
