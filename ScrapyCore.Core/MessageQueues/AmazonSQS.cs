using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrapyCore.Core.External;
using ScrapyCore.Core.External.Conventor;

namespace ScrapyCore.Core.MessageQueues
{
    public class AmazonSQS : MessageQueue
    {
        private IAmazonSQS amazonSQSClient;
        public AmazonSQS(IMessageQueueConfigure queueConfigure)
            : base(queueConfigure)
        {
            AmazonSQSConfig sqsConfigure = new AmazonSQSConfig()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(queueConfigure.ConfigureDetail.DefaultValue("region")),
                BufferSize = queueConfigure.ConfigureDetail.GetKeyAndConvertTo("buffer-size",StringToInt32Conventor.Instance)
            };
            amazonSQSClient = new AmazonSQSClient(sqsConfigure);
        }

        public override async Task<IMessageQueueHandler<T>> GetMessage<T>()
        {
            var messageList = await this.GetMessages<T>();
            var firstMessage = messageList.FirstOrDefault();
            return firstMessage;
        }

        public override async Task<T> GetMessageComplete<T>()
        {
            var firstMessage = await this.GetMessage<T>();
            if (firstMessage != null)
            {
                await firstMessage.Complete();
                return firstMessage.MessageObject;
            }
            return default(T);
        }

        public override async Task<IList<IMessageQueueHandler<T>>> GetMessages<T>()
        {
            var response = await amazonSQSClient.ReceiveMessageAsync(queueConfigure.QueueName);
            List<IMessageQueueHandler<T>> messageHandlers = new List<IMessageQueueHandler<T>>();
            if (response.Messages.Count > 0)
            {
                foreach (var item in response.Messages)
                {
                    AmazonSQSMessageQueueHandler<T> handler = new AmazonSQSMessageQueueHandler<T>(this.amazonSQSClient, item, queueConfigure.QueueName);
                    messageHandlers.Add(handler);
                }
            }
            return messageHandlers;
        }

        public override async Task SendQueueMessage<T>(T msgObj)
        {
            string jsonStr = JsonConvert.SerializeObject(msgObj);
            var response = await amazonSQSClient.SendMessageAsync(new SendMessageRequest()
            {
                MessageBody = jsonStr,
                QueueUrl = queueConfigure.QueueName
            });
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.HttpStatusCode.ToString());
            }
        }
    }
}
