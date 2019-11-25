using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class PlatformModel
    {
        public IPlatformExit PlatformExit { get; set; }

        public ICache CoreCache { get; set; }
    }
}
