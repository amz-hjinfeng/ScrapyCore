using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ScrapyCore.Core;
using ScrapyCore.Core.HostMachine;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.System;
using ScrapyCore.Kerrigan.WebHosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.Kerrigan
{
    public class KerriganSystemController : SystemController
    {
        private readonly IHostedMachine hostedMachine;
        private IMessagePipline messagePipline;
        private IMessageQueue messageOut;
        public KerriganSystemController(Bootstrap bootstrap, IHostedMachine hostedMachine)
            : base(bootstrap)
        {
            IMessageEntrance messageEntrance = new MessageEntrance(bootstrap.GetMessageQueueFromVariableSet("Entrance"));
            IMessageTermination messageTermination = new MessageTermination(bootstrap, this);
            messagePipline = new MessagePipline(messageEntrance, messageTermination);
            messageOut = bootstrap.GetMessageQueueFromVariableSet("Termination");
            this.hostedMachine = hostedMachine;
        }

        protected override void HeartBeatProcessor()
        {
            //Do nothing first for integrating with Utralisks
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
