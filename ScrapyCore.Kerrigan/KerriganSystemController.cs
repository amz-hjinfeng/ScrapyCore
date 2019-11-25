using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.HostMachine;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.Processors.Model;
using ScrapyCore.Core.Platform.System;
using ScrapyCore.Fundamental.Kernel.Load;
using ScrapyCore.Kerrigan.WebHosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Kerrigan
{
    public class KerriganSystemController : SystemController
    {
        private readonly IHostedMachine hostedMachine;
        private IMessagePipline messagePipline;
        private IMessageQueue messageOut;
        private LoadProviderManager loadProviderManager;
        public KerriganSystemController(Bootstrap bootstrap, IHostedMachine hostedMachine)
            : base(bootstrap)
        {
            var entrance = new MessageEntrance(bootstrap.GetMessageQueueFromVariableSet("Entrance"));
            this.loadProviderManager = new LoadProviderManager();
            this.WorkingProcessor = new LoadIntegration(
                bootstrap.GetCachedFromVariableSet("HeartbeatCache"),
                bootstrap.GetStorageFromVariableSet("CoreStorage"),
                this.loadProviderManager
            );
            IMessageTermination messageTermination = new MessageTermination(bootstrap, this);
            messagePipline = new MessagePipline(entrance, messageTermination);
            messageOut = bootstrap.GetMessageQueueFromVariableSet("Termination");
            this.hostedMachine = hostedMachine;


        }

        public override IMessagePipline MessagePipline => this.messagePipline;

        protected override void HeartBeatProcessor()
        {
            //Do nothing first for integrating with Utralisks
            PlatformMessage platformMessage = new PlatformMessage()
            {
                Command = new Core.Platform.Commands.Command()
                {
                    CommandCode = Core.Platform.Commands.CommandCode.HeartBeat,
                    CommandType = Core.Platform.Commands.CommandTransfer.Random,
                },
                NextJump = null
            };
            HeartBeatModel heartBeatModel = new HeartBeatModel()
            {
                ChannelId = bootstrap.GetVariableSet("Termination"),
                SentTime = DateTime.Now,
                Id = hostedMachine.Id,
                Model = "Kerrigan",
                External = hostedMachine
            };
            platformMessage.Routes.Add(new MessageRoute(
                 new Pricipal()
                 {
                     Id = hostedMachine.Id,
                     IpAddress = hostedMachine.PrivateIpAddress
                 }
               ));
            platformMessage.MessageData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(heartBeatModel));
            messageOut.SendQueueMessage(platformMessage).Wait();
        }

        protected override void Processor()
        {
            logger.Debug("Process Started");
            messagePipline.Drive().Wait();
            logger.Debug("Process Completed");
        }

        protected override void ProvisionWebHost()
        {
            logger.Info("Provision Web Host Started");
            Startup.SystemController = this;
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            webHost.Run();
        }
    }
}
