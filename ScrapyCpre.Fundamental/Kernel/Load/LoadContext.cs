using ScrapyCore.Fundamental.Scheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class LoadContext
    {
        public object Parameter { get; set; }

        public PlatformModel PlatformModel { get; set; }

        public LoadEvent LoadEvent { get; set; }
    }
}
