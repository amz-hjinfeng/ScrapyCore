using System;
namespace ScrapyCore.Core.Configure.MessageQueue
{
    public class MessageQueueConfigureFactory :IConfigurationFactory<IMessageQueueConfigure>
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

        private MessageQueueConfigureFactory()
        {

        }

        public IMessageQueueConfigure CreateConfigure(IStorage storage, string Path)
        {
            throw new NotImplementedException();
        }
    }
}
