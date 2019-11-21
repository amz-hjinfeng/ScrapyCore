using Newtonsoft.Json;
using ScrapyCore.Core.Platform.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform.Processors
{
    public class MessageQueuePlatformExit : IPlatformExit
    {
        public MessageQueuePlatformExit(IMessageQueue messageQueue)
        {
            MessageQueue = messageQueue;
        }

        public IMessageQueue MessageQueue { get; }

        public async Task OutRandom<T>(T obj)
        {
            PlatformMessage platformMessage = new PlatformMessage()
            {
                Command = new Commands.Command()
                {
                    CommandCode = Commands.CommandCode.Working,
                    CommandType = Commands.CommandTransfer.Random
                },
                MessageData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj))
            };
            await MessageQueue.SendQueueMessage(platformMessage);

        }

        public Task OutTransfer<T>(T obj, string target)
        {
            throw new NotImplementedException();
        }
    }
}
