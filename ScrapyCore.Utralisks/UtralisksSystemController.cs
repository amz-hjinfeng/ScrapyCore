﻿using ScrapyCore.Core;
using ScrapyCore.Core.Platform.System;
using Microsoft.AspNetCore.Hosting;
using ScrapyCore.Utralisks.WebHosting;
using Microsoft.AspNetCore;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Processors.Model;
using System;
using ScrapyCore.Core.HostMachine;
using ScrapyCore.Core.Platform.Message;
using Newtonsoft.Json;
using System.Text;
using ScrapyCore.Fundamental.Kernel.Transform;
using ScrapyCore.Core.Metric;

namespace ScrapyCore.Utralisks
{
    public class UtralisksSystemController : SystemController
    {
        private readonly IHostedMachine hostedMachine;
        private IMessagePipline messagePipline;
        private IMessageQueue messageOut;
        private IMessageEntrance messageEntrance;
        public UtralisksSystemController(Bootstrap bootstrap, IHostedMachine hostedMachine)
            : base(bootstrap)
        {
            messageEntrance = new MessageEntrance(bootstrap.GetMessageQueueFromVariableSet("Entrance"));
            MetricCollections.Default.AddMetricCollector("idle", new IncreasedMetricCollector("UtraliskIdle"));
            MetricCollections.Default.AddMetricCollector("busy", new IncreasedMetricCollector("UtraliskBusy"));
            WorkingProcessor = new TransformIntegration(
                bootstrap.GetCachedFromVariableSet("CoreCache"),
                bootstrap.GetStorageFromVariableSet("CoreStorage")
                );
            IMessageTermination messageTermination = new MessageTermination(bootstrap, this);
            messagePipline = new MessagePipline(messageEntrance, messageTermination);
            messageOut = bootstrap.GetMessageQueueFromVariableSet("Termination");
            this.hostedMachine = hostedMachine;

        }

        public override IMessagePipline MessagePipline => this.messagePipline;

        protected override void HeartBeatProcessor()
        {
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
                Model = "Utralisks",
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
            try
            {
                logger.Info("Process Started");
                messagePipline.Drive().Wait();
                logger.Info("Process Completed");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        protected override void ProvisionWebHost()
        {
            logger.Info("Provision Web Host Started");
            Startup.SystemController = this;
            WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build().Run();
        }
    }
}
