﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.HostMachine;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.Processors.Model;
using ScrapyCore.Core.Platform.System;
using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Hydralisk.WebHosting;
using System;
using System.Text;

namespace ScrapyCore.Hydralisk
{
    public class HydraliskSystemController : SystemController
    {
        private readonly IHostedMachine hostedMachine;
        private IMessagePipline messagePipline;
        private IMessageQueue messageOut;
        public HydraliskSystemController(Bootstrap bootstrap, IHostedMachine hostedMachine)
            : base(bootstrap)
        {
            IMessageEntrance messageEntrance = new MessageEntrance(bootstrap.GetMessageQueueFromVariableSet("Entrance"));
            WorkingProcessor = new SourceIntergation(bootstrap.Provisioning.Caches["default-cache"],
                new ExtractorManager(bootstrap.InjectionProvider));
            IMessageTermination messageTermination = new MessageTermination(bootstrap, this);
            messagePipline = new MessagePipline(messageEntrance, messageTermination);
            messageOut = bootstrap.GetMessageQueueFromVariableSet("Termination");
            this.hostedMachine = hostedMachine;
            //TODO : Cache should comes from the variable.

        }

        public override IMessagePipline MessagePipline => this.messagePipline;

        protected override void HeartBeatProcessor()
        {
            // TODO: Refactor to a method or may be class
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
                Model = "Hydralisk"
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
            WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Run();
        }
    }
}
