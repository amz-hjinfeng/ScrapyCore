using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Repository;
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
        private static ILog logger = LogManager.GetLogger(typeof(Bootstrap));

        private IStorage initialStorage;

        public Bootstrap()
            : this("Bootstrap.json") { }

        public Bootstrap(string boostrapFile)
        {
            ILoggerRepository repository = LogManager.CreateRepository("Scrapy-Repo");
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            string location = Assembly.GetCallingAssembly().Location;
            var applicationPath = Path.GetDirectoryName(location);

            logger.Debug("Application Location:" + applicationPath);

            initialStorage = StorageFactory.Factory.GetLocalStorage(applicationPath);
            var model = JsonConvert.DeserializeObject<Model>(initialStorage.GetString(boostrapFile));
            Provisioning = new ProvisioningModel(model, initialStorage);
        }

        public ProvisioningModel Provisioning { get; }

        public class ProvisioningModel
        {
            public IThreadManager ThreadManager { get; }
            public ProvisioningModel(Model model, IStorage storage)
            {
                this.ThreadManager = Threading.ThreadManager.BuildThreadManager(model.Bootstrap.ThreadMode, 100);
                Storage = storage;
                Dictionary<string, IStorage> storages = new Dictionary<string, IStorage>();
                Dictionary<string, ICache> caches = new Dictionary<string, ICache>();
                Dictionary<string, IUserAgentPool> useragentPools = new Dictionary<string, IUserAgentPool>();
                Dictionary<string, IMessageQueue> messageQueues = new Dictionary<string, IMessageQueue>();

                logger.Info("Provisioning Storage.");
                Provision(model.Provisioning.Storages,
                    storages,
                    StorageConfigureFactory.Instance,
                    StorageFactory.Factory);

                logger.Info("Provisioning Cache.");
                Provision(model.Provisioning.Caches,
                    caches,
                    CacheConfigureFactory.Factory,
                    CacheFactory.Factory);

                logger.Info("Provisioning UserAgents.");
                Provision(model.Provisioning.UserAgents,
                    useragentPools,
                    UserAgentConfigureFactory.Factory,
                    UserAgentPoolFactory.Factory);

                logger.Info("Provisioning MessageQueue.");
                Provision(model.Provisioning.MessageQueues,
                    messageQueues,
                    MessageQueueConfigureFactory.Factory,
                    MessageQueueFactory.Factory);

                Storages = storages;
                Caches = caches;
                UseragentPools = useragentPools;
                MessageQueues = messageQueues;

                logger.Info($"Provisioned:Storage-{storages.Count},Cache-{caches.Count},UserAgentPools-{useragentPools.Count},MessageQueues-{messageQueues.Count}");
            }

            public IReadOnlyDictionary<string, IStorage> Storages { get; }
            public IReadOnlyDictionary<string, ICache> Caches { get; }
            public IReadOnlyDictionary<string, IUserAgentPool> UseragentPools { get; }
            public IReadOnlyDictionary<string, IMessageQueue> MessageQueues { get; }
            private IStorage Storage { get; }
            private void Provision<Target, TConfigure>(
                        NameConfigure[] nameConfigures,
                        Dictionary<string, Target> container,
                        IConfigurationFactory<TConfigure> configurationFactory,
                        IServiceFactory<Target, TConfigure> serviceFactory) where TConfigure : IConfigure
            {
                logger.Info("Provisioning..");
                foreach (var item in nameConfigures)
                {
                    try
                    {
                        logger.Debug($"Provisioning:{item.Name}");
                        var configure = configurationFactory.CreateConfigure(this.Storage, item.ConfigureFile);
                        var instance = serviceFactory.GetService(configure);
                        container[item.Name] = instance;
                        logger.Debug($"Provisioned:{item.Name}");
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"{item.Name}Provision fai, Caused by{ex.Message}");
                    }
                }
                logger.Info("Provisioned");
            }
        }

        public class Model
        {
            public BootstrapModel Bootstrap { get; set; }

            public ConfigureDetail Provisioning { get; set; }

            public ConfigureDetail LazyLoad { get; set; }

        }

        public class BootstrapModel
        {
            public string Storage { get; set; }

            public string ThreadMode { get; set; }
        }

        public class NameConfigure
        {
            public string Name { get; set; }

            public string ConfigureFile { get; set; }
        }

        public class ConfigureDetail
        {
            public NameConfigure[] Storages { get; set; }
            public NameConfigure[] MessageQueues { get; set; }
            public NameConfigure[] UserAgents { get; set; }
            public NameConfigure[] Caches { get; set; }

        }

    }
}
