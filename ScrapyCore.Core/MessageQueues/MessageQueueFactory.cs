using System;
using System.Collections.Generic;
using System.Linq;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.MessageQueues
{
    public class MessageQueueFactory : IServiceFactory<IMessageQueue, IMessageQueueConfigure>
    {
        private static MessageQueueFactory _factory;
        private Dictionary<string, Type> messageQueueTypes;

        public static MessageQueueFactory Factory
        {

            get
            {
                if (_factory == null)
                {
                    _factory = new MessageQueueFactory();
                }
                return _factory;
            }
        }
        public MessageQueueFactory()
        {
            messageQueueTypes = typeof(MessageQueueFactory).Assembly.GetTypes()
                 .Where(x => !x.IsAbstract)
                 .Where(x => !x.IsInterface)
                 .Where(x => x.GetInterface(nameof(IMessageQueue)) != null)
                 .ToDictionary(x => x.Name, x => x);
        }

        public IMessageQueue GetService(IMessageQueueConfigure configure)
        {
            if (messageQueueTypes.ContainsKey(configure.MessageQueueEngine))
            {
                return Activator.CreateInstance(messageQueueTypes[configure.MessageQueueEngine], configure) as IMessageQueue;
            }
            return null;
        }
    }
}
