using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.MessageQueues
{
    public class AmazonSQS : MessageQueue
    {
        private AmazonSQSClient amazonSQSClient;
        public AmazonSQS(string queueName)
            : base(queueName)
        {
            amazonSQSClient = new AmazonSQSClient();
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
            var response = await amazonSQSClient.ReceiveMessageAsync(queueName);
            List<IMessageQueueHandler<T>> messageHandlers = new List<IMessageQueueHandler<T>>();
            if (response.Messages.Count > 0)
            {
                foreach (var item in response.Messages)
                {
                    AmazonSQSMessageQueueHandler<T> handler = new AmazonSQSMessageQueueHandler<T>(this.amazonSQSClient, item, queueName);
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
                QueueUrl = queueName
            });
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.HttpStatusCode.ToString());
            }
        }
    }
}
