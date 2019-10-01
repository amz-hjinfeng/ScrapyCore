using ScrapyCore.Core;
using ScrapyCore.Core.Platform.System;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using ScrapyCore.Utralisks.WebHosting;
using Microsoft.AspNetCore;

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
            Thread.Sleep(10000);
        }

        protected override void ProvisionWebHost()
        {
            WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Run();
        }
    }
}
