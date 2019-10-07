using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.MessageOperation;
using ScrapyCore.Core.Platform.Processors;
using ScrapyCore.Core.Platform.System;

namespace ScrapyCore.Core.Platform
{
    public class MessageTermination : IMessageTermination
    {
        private const string TERMINATION = "Termination";
        private const string HEARTBEAT_CACHE = "HeartbeatCache";
        IMessageOperationManager manager;
        public MessageTermination(Bootstrap bootstrap, ISystemController systemController)
        {
            MessageOperationManager.Builder operationBuilder = new MessageOperationManager.Builder();
            operationBuilder.WithMessageQueueOut(bootstrap.GetMessageQueueFromVariableSet(TERMINATION));

            MessageProcessorManager messageProcessorManager = MessageProcessorManager.Builder.NewBuilder()
                 .WithSystemController(systemController)
                 .WithHeartbeatCache(bootstrap.GetCachedFromVariableSet(HEARTBEAT_CACHE))
                 .Build();
            operationBuilder.WithProcessManager(messageProcessorManager);

            manager = operationBuilder.Build();
        }

        public async Task Terminate(PlatformMessage platformMessage)
        {
            var operation = manager.GetRawOperation(platformMessage.Command.CommandType);
            await operation.Push(platformMessage);
        }
    }
}
