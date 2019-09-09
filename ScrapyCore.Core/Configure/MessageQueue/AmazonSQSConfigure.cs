using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScrapyCore.Core.Consts;

namespace ScrapyCore.Core.Configure
{
    public class AmazonSQSConfigure:IMessageQueueConfigure
    {
        public AmazonSQSConfigure(IStorage storage) :
            this(storage, PathConstants.MessageQueueConfigurePath)
        { }

        public AmazonSQSConfigure(IStorage configurefile,string path)
        {
            string configureData = configurefile.GetString(path);
            Model model = JsonConvert.DeserializeObject<Model>(configureData);
            this.QueueName = model.QueueName;
            ConfigureDetail = new Dictionary<string, string>();
            foreach(var item in model.Configure)
            {
                ConfigureDetail.Add(item[0], item[1]);
            }
        }

        public string MessageQueueEngine => "AmazonSQS";

        public string QueueName { get; }

        public IDictionary<string, string> ConfigureDetail { get; private set; }

        private class Model
        {
            public string QueueEngine { get; set; }

            public string QueueName { get; set; }

            public string[][] Configure { get; set; }
        }

    }
}
