using ScrapyCore.Core.Platform.Commands;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform.Processors
{
    public class MessageProcessorManager : IMessageProcessorManager
    {
        private Dictionary<CommandCode, IMessageProcessor> Processors { get; set; }

        public Task ProcessMessage(PlatformMessage platformMessage)
        {
            var commandCode = platformMessage.Command.CommandCode;
            if (Processors.ContainsKey(commandCode))
            {
                return Processors[commandCode].ProcessAsync(platformMessage);
            }
            return Task.CompletedTask;
        }

        private MessageProcessorManager(Builder builder)
        {
            Processors = new Dictionary<CommandCode, IMessageProcessor>
            {
                [CommandCode.Sacrifice] = new SacrificeProcessor(builder.SystemController),
                [CommandCode.HeartBeat] = new HeartBeatProcessor(builder.HeartbeatCache),
                [CommandCode.Working] = new WorkingProcessor(builder.SystemController.WorkingProcessor),
                [CommandCode.Configure] = new ConfigureProcessor()
            };
            // Need All System things to couple inside.
        }

        public class Builder
        {
            public ISystemController SystemController { get; private set; }

            public ICache HeartbeatCache { get; private set; }

            public static Builder NewBuilder()
            {
                return new Builder();
            }

            private Builder() { }

            public MessageProcessorManager Build()
            {
                return new MessageProcessorManager(this);
            }

            public Builder WithSystemController(ISystemController controller)
            {
                SystemController = controller;
                return this;
            }

            public Builder WithHeartbeatCache(ICache cache)
            {
                HeartbeatCache = cache;
                return this;
            }
        }
    }
}
