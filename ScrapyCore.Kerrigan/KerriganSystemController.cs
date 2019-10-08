using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.HostMachine;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.Processors.Model;
using ScrapyCore.Core.Platform.System;
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
                Model = "Kerrigan"
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
            WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Run();
        }
    }
}
