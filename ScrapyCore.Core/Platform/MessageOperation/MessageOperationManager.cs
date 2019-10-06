using ScrapyCore.Core.Platform.Commands;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.Processors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform.MessageOperation
{
    public class MessageOperationManager : IMessageTermination
    {
        private Dictionary<CommandTransfer, IMessageRawOperation> messageOperations;

        public IMessageRawOperation GetRawOperation(CommandTransfer transfer)
        {
            if (messageOperations.ContainsKey(transfer))
            {
                return messageOperations[transfer];
            }
            return null;
        }

        public Task Terminate(PlatformMessage platformMessage)
        {
            return this.GetRawOperation(platformMessage.Command.CommandType).Push(platformMessage);
        }

        private MessageOperationManager(Builder builder)
        {
            messageOperations = new Dictionary<CommandTransfer, IMessageRawOperation>();
            messageOperations[CommandTransfer.SiteToSite] = new SiteToSiteOperation();
            messageOperations[CommandTransfer.Forward] = new ForwardOperation(builder.MessageQueueOut);
            messageOperations[CommandTransfer.Random] = new RandomOperation(builder.MessageProcessorManager);
        }


        public class Builder
        {
            public IMessageQueue MessageQueueOut { get; private set; }

            public MessageProcessorManager MessageProcessorManager { get; private set; }

            public MessageOperationManager Build()
            {
                return new MessageOperationManager(this);
            }

            public void WithMessageQueueOut(IMessageQueue messageQueue)
            {
                this.MessageQueueOut = messageQueue;
            }


            public void WithProcessManager(MessageProcessorManager messageProcessorManager)
            {
                this.MessageProcessorManager = messageProcessorManager;
            }

        }
    }
}
