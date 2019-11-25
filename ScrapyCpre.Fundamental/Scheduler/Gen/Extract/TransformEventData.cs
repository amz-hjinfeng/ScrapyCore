using ScrapyCore.Fundamental.Kernel.Transform;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Gen
{
    public class TransformEventData
    {
        public Dictionary<string, List<string>> SourceMapToTransform { get; set; }
        public Dictionary<string, List<TransformEvent>> TransformEvents { get; set; }
    }
}
