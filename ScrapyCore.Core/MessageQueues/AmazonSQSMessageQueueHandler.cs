using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.MessageQueues
{
    public class AmazonSQSMessageQueueHandler<T> : IMessageQueueHandler<T>
    {
        private readonly IAmazonSQS amazonSQSClient;
        private readonly Message message;
        private readonly string queue;

        public AmazonSQSMessageQueueHandler(IAmazonSQS amazonSQSClient, Message message, string queue)
        {
            this.amazonSQSClient = amazonSQSClient;
            this.message = message;
            this.queue = queue;
            MessageObject = JsonConvert.DeserializeObject<T>(message.Body);
        }

        public T MessageObject { get; }

        public async Task Complete()
        {
            await amazonSQSClient.DeleteMessageAsync(new DeleteMessageRequest()
            {
                QueueUrl = queue,
                ReceiptHandle = message.ReceiptHandle
            });
        }
    }
}
