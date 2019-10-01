using ScrapyCore.Core;
using ScrapyCore.Core.Platform.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ScrapyCore.Utralisks
{
    public class UtralisksSystemController : SystemController
    {
        public UtralisksSystemController(Bootstrap bootstrap)
            : base(bootstrap)
        {

        }

        protected override void Processor()
        {
            Thread.Sleep(1000);
        }
    }
}
