using RabbitMQ.Client;
using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.External;
using ScrapyCore.Core.External.Conventor;
using Newtonsoft.Json;

namespace ScrapyCore.Core.MessageQueues
{
    public class RabbitMQ : IMessageQueue
    {
        public IMessageQueueConfigure MessageQueueConfigure { get; }

        public string Exchange { get; }
        public string RouteKey { get; }

        private IModel channel;

        public RabbitMQ(IMessageQueueConfigure messageQueueConfigure)
        {
            MessageQueueConfigure = messageQueueConfigure;
            Exchange = messageQueueConfigure.ConfigureDetail.DefaultValue("exchange");
            RouteKey = messageQueueConfigure.ConfigureDetail.DefaultValue("route-key");



            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = messageQueueConfigure.ConfigureDetail.DefaultValue("host-name"),
                UserName = messageQueueConfigure.ConfigureDetail.DefaultValue("user-name"),
                Password = messageQueueConfigure.ConfigureDetail.DefaultValue("password"),
                VirtualHost = messageQueueConfigure.ConfigureDetail.DefaultValue("virtual-host"),

            };

            channel = factory.CreateConnection().CreateModel();

            channel.ExchangeDeclare(
                Exchange,
                messageQueueConfigure.ConfigureDetail.DefaultValue("type"),
                messageQueueConfigure.ConfigureDetail.GetKeyAndConvertTo("ex-durable", StringToBoolConvertor.Instance),
                messageQueueConfigure.ConfigureDetail.GetKeyAndConvertTo("ex-auto-delete", StringToBoolConvertor.Instance));

            channel.QueueDeclare(messageQueueConfigure.QueueName,
                messageQueueConfigure.ConfigureDetail.GetKeyAndConvertTo("q-durable", StringToBoolConvertor.Instance),
                messageQueueConfigure.ConfigureDetail.GetKeyAndConvertTo("q-exclusive", StringToBoolConvertor.Instance),
                messageQueueConfigure.ConfigureDetail.GetKeyAndConvertTo("q-auto-delete", StringToBoolConvertor.Instance)
                );


        }

        public Task<IMessageQueueHandler<T>> GetMessage<T>()
        {
            var getResult = channel.BasicGet(MessageQueueConfigure.QueueName, false);
            if (getResult != null)
            {
                var messageModel = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(getResult.Body));
                return Task.FromResult((IMessageQueueHandler<T>)new RabbitMQMessageHandler<T>(channel, getResult.DeliveryTag, messageModel));
            }
            else
            {
                return Task.FromResult((IMessageQueueHandler<T>)null);
            }
        }

        public async Task<T> GetMessageComplete<T>()
        {
            var handler = await GetMessage<T>();
            await handler.Complete();
            return handler.MessageObject;
        }

        public Task<IList<IMessageQueueHandler<T>>> GetMessages<T>()
        {
            throw new NotImplementedException();
        }

        public Task SendQueueMessage<T>(T msgObj)
        {
            var iBasicProp = channel.CreateBasicProperties();
            channel.BasicPublish(
                Exchange, RouteKey, iBasicProp, Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(msgObj)
                ));
            return Task.CompletedTask;
        }
    }
}
