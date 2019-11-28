using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.HostMachine;
using ScrapyCore.Core.Metric;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.Processors.Model;
using ScrapyCore.Core.Platform.System;
using ScrapyCore.Fundamental.Kernel.Load;
using ScrapyCore.Kerrigan.WebHosting;
using System;
using System.Text;

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
            MetricCollections.Default.AddMetricCollector("idle", new IncreasedMetricCollector("KerriganIdle"));
            MetricCollections.Default.AddMetricCollector("busy", new IncreasedMetricCollector("KerriganBusy"));
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
            try
            {
                logger.Debug("Process Started");
                messagePipline.Drive().Wait();
                logger.Debug("Process Completed");
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
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            webHost.Run();
        }
    }
}
