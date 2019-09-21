using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using ScrapyCore.Core.Caches;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.Caches;
using ScrapyCore.Core.Configure.MessageQueue;
using ScrapyCore.Core.Configure.Storage;
using ScrapyCore.Core.Configure.UserAgents;
using ScrapyCore.Core.MessageQueues;
using ScrapyCore.Core.Storages;
using ScrapyCore.Core.UserAgents;

namespace ScrapyCore.Core
{
    public class Bootstrap
    {

        private IStorage initialStorage;
        public Bootstrap()
        {
            string location = Assembly.GetCallingAssembly().Location;
            var applicationPath = Path.GetDirectoryName(location);
            initialStorage = Storages.StorageFactory.Factory.GetLocalStorage(applicationPath);
            var model = JsonConvert.DeserializeObject<Model>(initialStorage.GetString("Bootstrap.json"));
        }

        public ProvisioningModel Provisioning { get; }



        public class ProvisioningModel
        {

            private ProvisioningModel(Model model,IStorage storage)
            {

                Dictionary<string, IStorage> storages = new Dictionary<string, IStorage>();
                Dictionary<string, ICache> caches = new Dictionary<string, ICache>();
                Dictionary<string, IUserAgentPool> useragentPools = new Dictionary<string, IUserAgentPool>();
                Dictionary<string, IMessageQueue> messageQueues = new Dictionary<string, IMessageQueue>();

                Provision(model.Provisioning.Storages,
                    storages,
                    StorageConfigureFactory.Instance,
                    StorageFactory.Factory);

                Provision(model.Provisioning.Caches,
                    caches,
                    CacheConfigureFactory.Factory,
                    CacheFactory.Factory);

                Provision(model.Provisioning.UserAgents,
                    useragentPools,
                    UserAgentConfigureFactory.Factory,
                    UserAgentPoolFactory.Factory);

                Provision(model.Provisioning.MessageQueues,
                    messageQueues,
                    MessageQueueConfigureFactory.Factory,
                    MessageQueueFactory.Factory);

                Storages = storages;
                Caches = caches;
                UseragentPools = useragentPools;
                MessageQueues = messageQueues;
                Storage = storage;
            }

            private void Provision<Target,TConfigure>(NameConfigure[] nameConfigures,
                Dictionary<string,Target> container,
                IConfigurationFactory<TConfigure> configurationFactory,
                IServiceFactory<Target,TConfigure> serviceFactory) where TConfigure:IConfigure
            {
                foreach(var item in nameConfigures)
                {
                    var configure = configurationFactory.CreateConfigure(this.Storage, item.ConfigureFile);
                    var instance = serviceFactory.GetService(configure);
                    container[item.Name] = instance;
                }
            }

            public IReadOnlyDictionary<string,IStorage> Storages { get; }

            public IReadOnlyDictionary<string,ICache> Caches { get; }
            public IReadOnlyDictionary<string, IUserAgentPool> UseragentPools { get; }
            public IReadOnlyDictionary<string, IMessageQueue> MessageQueues { get; }
            private IStorage Storage { get; }
        }

        private class Model
        {
            public BootstrapModel Bootstrap { get; set; }

            public ConfigureDetail Provisioning { get; set; }

            public ConfigureDetail LazyLoad { get; set; }

        }

        private class BootstrapModel
        {
            public string Storage { get; set; }

            public string ThreadMode { get; set; }
        }

        private class NameConfigure
        {
            public string Name { get; set; }

            public string ConfigureFile { get; set; }
        }

        private class ConfigureDetail
        {
            public NameConfigure[] Storages { get; set; }
            public NameConfigure[] MessageQueues { get; set; }
            public NameConfigure[] UserAgents { get; set; }
            public NameConfigure[] Caches { get; set; }

        }

    }
}
