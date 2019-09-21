using System;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.MessageQueues
{
    public class MessageQueueFactory :IServiceFactory<IMessageQueue,IMessageQueueConfigure>
    {
        private static MessageQueueFactory _factory;

        public static MessageQueueFactory Factory
        {

            get
            {
                if(_factory == null)
                {
                    _factory = new MessageQueueFactory();
                }
                return _factory;
            }
        }


        public MessageQueueFactory()
        {
        }

        public IMessageQueue GetService(IMessageQueueConfigure configure)
        {
            throw new NotImplementedException();
        }
    }
}
