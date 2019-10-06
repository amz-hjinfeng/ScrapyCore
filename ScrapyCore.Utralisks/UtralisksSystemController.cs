using ScrapyCore.Core;
using ScrapyCore.Core.Platform.System;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using ScrapyCore.Utralisks.WebHosting;
using Microsoft.AspNetCore;
using ScrapyCore.Core.Platform;
using log4net;

namespace ScrapyCore.Utralisks
{
    public class UtralisksSystemController : SystemController
    {
        private IMessagePipline messagePipline;
        public UtralisksSystemController(Bootstrap bootstrap)
            : base(bootstrap)
        {
            IMessageEntrance messageEntrance = new MessageEntrance(bootstrap.GetMessageQueueFromVariableSet("Entrance"));
            IMessageTermination messageTermination = new MessageTermination(bootstrap, this);
            messagePipline = new MessagePipline(messageEntrance, messageTermination);
        }

        protected override void Processor()
        {
            logger.Info("Process Started");
            messagePipline.Drive().Wait();
            logger.Info("Process Completed");
        }

        protected override void ProvisionWebHost()
        {
            logger.Info("Provision Web Host Started");
            WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Run();
        }
    }
}
