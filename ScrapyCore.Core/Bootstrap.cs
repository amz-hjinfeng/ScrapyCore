﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;
using log4net.Repository;
using Newtonsoft.Json;
using ScrapyCore.Core.Caches;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.Caches;
using ScrapyCore.Core.Configure.ElasticSearch;
using ScrapyCore.Core.Configure.MessageQueue;
using ScrapyCore.Core.Configure.Storage;
using ScrapyCore.Core.Configure.UserAgents;
using ScrapyCore.Core.Consts;
using ScrapyCore.Core.ElasticSearch;
using ScrapyCore.Core.HostMachine;
using ScrapyCore.Core.Injection;
using ScrapyCore.Core.MessageQueues;
using ScrapyCore.Core.Storages;
using ScrapyCore.Core.UserAgents;

namespace ScrapyCore.Core
{
    public class Bootstrap
    {
        private static Bootstrap defaultInstance;
        public static Bootstrap DefaultInstance
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new Bootstrap();
                }
                return defaultInstance;
            }
        }

        private const int MAX_THREAD = 10;
        private static ILog logger;
        public IHostedMachine HostedMachine;

        private IStorage initialStorage;

        public Bootstrap()
            : this("Bootstrap.json") { }

        public Bootstrap(string boostrapFile)
        {
            ILoggerRepository repository = LogManager.CreateRepository(LogConst.SCRAPY_CORE_REPO);
            ILoggerRepository repositoryFundalmental = LogManager.CreateRepository(LogConst.SCRAPY_FUNDAMENTAL);
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            log4net.Config.XmlConfigurator.Configure(repositoryFundalmental, new FileInfo("log4net-fundamental.config"));

            logger = LogManager.GetLogger(repository.Name, typeof(Bootstrap));
            string location = Assembly.GetCallingAssembly().Location;
            var applicationPath = Path.GetDirectoryName(location);

            logger.Debug("Application Location:" + applicationPath);

            ////TODO :Temp state here, refactor to other way.
            ElasticSearchTypeManager.RegistAssemblyModels(Assembly.LoadFrom(Path.Combine(applicationPath, "ScrapyCore.Fundamental.dll")));

            initialStorage = StorageFactory.Factory.GetLocalStorage(applicationPath);
            var model = JsonConvert.DeserializeObject<Model>(initialStorage.GetString(boostrapFile));
            Provisioning = new ProvisioningModel(model, initialStorage);
            InjectionProvider = new InjectionProvider(this);
            HostedMachine = HotedMachineManager.GetHostedMachine(this.GetVariableSet("environment"));
        }

        public string GetVariableSet(string variableKey)
        {
            if (this.Provisioning.Variables.ContainsKey(variableKey))
            {
                return this.Provisioning.Variables[variableKey];
            }
            return string.Empty;
        }

        public IMessageQueue GetMessageQueueFromVariableSet(string variableKey)
        {
            if (this.Provisioning.Variables.ContainsKey(variableKey))
            {
                string key = this.Provisioning.Variables[variableKey];
                return this.Provisioning.MessageQueues[key];
            }
            return null;
        }

        public ICache GetCachedFromVariableSet(string variableKey)
        {
            if (this.Provisioning.Variables.ContainsKey(variableKey))
            {
                string key = this.Provisioning.Variables[variableKey];
                return this.Provisioning.Caches[key];
            }
            return null;
        }

        public IStorage GetStorageFromVariableSet(string variableKey)
        {
            if (this.Provisioning.Variables.ContainsKey(variableKey))
            {
                string key = this.Provisioning.Variables[variableKey];
                return this.Provisioning.Storages[key];
            }
            return null;
        }

        public InjectionProvider InjectionProvider { get; }

        public ProvisioningModel Provisioning { get; }

        public class ProvisioningModel
        {
            public Dictionary<string, string> Variables { get; private set; }
            public IThreadManager ThreadManager { get; }
            public ProvisioningModel(Model model, IStorage storage)
            {
                Variables = model.Varables.ToDictionary(x => x[0], x => x[1]);
                this.ThreadManager = Threading.ThreadManager.BuildThreadManager(model.Bootstrap.ThreadMode, MAX_THREAD);
                Storage = storage;
                Dictionary<string, IStorage> storages = new Dictionary<string, IStorage>();
                Dictionary<string, ICache> caches = new Dictionary<string, ICache>();
                Dictionary<string, IUserAgentPool> useragentPools = new Dictionary<string, IUserAgentPool>();
                Dictionary<string, IMessageQueue> messageQueues = new Dictionary<string, IMessageQueue>();
                Dictionary<String, IElasticSearch> elasticSearch = new Dictionary<string, IElasticSearch>();

                logger.Info("Provisioning Storage.");
                Provision(model.Provisioning.Storages,
                    storages,
                    StorageConfigureFactory.Factory,
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

                logger.Info("Provision Elasticsearch");
                Provision(model.Provisioning.ElasticSearch,
                    elasticSearch,
                    ElasticSearchConfigureFactory.Factory,
                    ElasticSearchFactory.Factory
                    );


                Storages = storages;
                Caches = caches;
                UseragentPools = useragentPools;
                MessageQueues = messageQueues;
                ElasticSearch = elasticSearch;

                logger.Info($"Provisioned:Storage-{storages.Count},Cache-{caches.Count},UserAgentPools-{useragentPools.Count},MessageQueues-{messageQueues.Count},ElasticSearch-{elasticSearch.Count}");
            }
            public IReadOnlyDictionary<string, IElasticSearch> ElasticSearch { get; }

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

                if (nameConfigures != null)
                {
                    logger.Info("Provisioning...");
                    foreach (var item in nameConfigures)
                    {
                        try
                        {
                            Variables.ToList().ForEach(x => item.ConfigureFile = item.ConfigureFile.Replace("{$" + x.Key + "}", x.Value));
                            logger.Debug($"Provisioning:{item.Name}");
                            logger.Info("Configure File:" + item.ConfigureFile);
                            var configure = configurationFactory.CreateConfigure(this.Storage, item.ConfigureFile);
                            var instance = serviceFactory.GetService(configure);
                            container[item.Name] = instance;
                            logger.Debug($"Provisioned:{item.Name}");
                        }
                        catch (Exception ex)
                        {
                            logger.Error($"{item.Name} provision fail, Caused by{ex.Message}");
                        }
                    }
                    logger.Info("Provisioned");
                }
                else
                {
                    logger.Warn("Nothing Provision");
                }


            }
        }

        public class Model
        {
            public string[][] Varables { get; set; }

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
            public NameConfigure[] ElasticSearch { get; set; }

        }

    }
}
