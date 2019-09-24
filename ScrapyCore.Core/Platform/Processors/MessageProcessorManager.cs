using ScrapyCore.Core.Platform.Commands;
using ScrapyCore.Core.Platform.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform.Processors
{
    public class MessageProcessorManager
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
            // Need All System things to couple inside.
        }

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            public MessageProcessorManager Build()
            {
                return new MessageProcessorManager(this);
            }
        }
    }
}
