using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ScrapyCore.Core.Configure.MessageQueue
{
    public class MessageQueueConfigureFactory : IConfigurationFactory<IMessageQueueConfigure>
    {

        private static MessageQueueConfigureFactory _factory;
        public static MessageQueueConfigureFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new MessageQueueConfigureFactory();
                return _factory;
            }
        }

        private Dictionary<string, Type> MessageQueueTypes { get; }

        private MessageQueueConfigureFactory()
        {
            MessageQueueTypes = typeof(MessageQueueConfigureFactory)
                .Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => x.GetInterfaces().Contains(typeof(IMessageQueueConfigure)))
                .ToDictionary(x => x.Name.Substring(0, x.Name.Length - "Configure".Length), x => x);
        }

        public IMessageQueueConfigure CreateConfigure(IStorage storage, string path)
        {
            var configureModel = JsonConvert.DeserializeObject<MessageQueueConfigureModel>(storage.GetString(path));
            if (MessageQueueTypes.ContainsKey(configureModel.MessageQueueEngine))
            {
                return Activator.CreateInstance(MessageQueueTypes[configureModel.MessageQueueEngine], configureModel) as IMessageQueueConfigure;
            }
            return null;
        }
    }
}
