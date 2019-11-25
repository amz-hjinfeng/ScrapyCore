using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.ElasticSearch;
using ScrapyCore.Core.Configure.MessageQueue;
using ScrapyCore.Core.Configure.Storage;
using ScrapyCore.Core.ElasticSearch;
using ScrapyCore.Core.MessageQueues;
using ScrapyCore.Core.Storages;
using ScrapyCore.Fundamental.Scheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class LoadProviderManager
    {
        public static Dictionary<string, LoadSuites> LoadMetas { get; }

        static LoadProviderManager()
        {
            LoadMetas = new Dictionary<string, LoadSuites>()
            {
                {
                    "Storage",
                    new LoadSuites(){
                         ConfigureFactory = StorageConfigureFactory.Factory,
                         ServiceFactory = StorageFactory.Factory,
                         ProviderType = typeof(StorageLoadProvider)
                    }
                },
                {
                    "MessageQueue",
                    new LoadSuites()
                    {
                         ConfigureFactory=MessageQueueConfigureFactory.Factory,
                         ProviderType = typeof(MessageQueueLoadProvider),
                         ServiceFactory = MessageQueueFactory.Factory
                    }
                },
                {
                    "Schedule",
                    new LoadSuites()
                    {
                         ConfigureFactory= ScheduleConfigureFactory.Factory,
                          ProviderType = typeof(ScheduleLoadProvider),
                          ServiceFactory = ScheduleServiceFactory.Factory
                    }
                },
                {
                    "ElasticSearch",
                    new LoadSuites()
                    {
                        ConfigureFactory = ElasticSearchConfigureFactory.Factory,
                        ProviderType = typeof(ElasticSearchLoadProvider),
                        ServiceFactory = ElasticSearchFactory.Factory
                    }
                }
            };
        }

        private Dictionary<string, ILoadProvider> LoadProvider { get; set; }

        public LoadProviderManager()
        {
            LoadProvider = new Dictionary<string, ILoadProvider>();
        }

        public ILoadProvider GetLoadProvider(string type, string name, object context)
        {
            if (LoadMetas.ContainsKey(type))
            {
                lock (LoadMetas)
                {
                    string providerName = type + "-" + name;
                    IStorage memoryStorage = StorageFactory.Factory.GetMemoryAsStorage();
                    memoryStorage.WriteBytes(Encoding.UTF8.GetBytes(context.ToString()), providerName).Wait();
                    if (!LoadProvider.ContainsKey(providerName))
                    {
                        LoadSuites loadSuites = LoadMetas[type];
                        var tConfigureFactory = loadSuites.ConfigureFactory.GetType();
                        var objConfigure = tConfigureFactory.InvokeMember(
                                "CreateConfigure",
                                System.Reflection.BindingFlags.InvokeMethod,
                                null, loadSuites.ConfigureFactory,
                                new object[] { memoryStorage, providerName });

                        var tServiceFactory = loadSuites.ServiceFactory.GetType();
                        var depObject = tServiceFactory.InvokeMember(
                            "GetService",
                             System.Reflection.BindingFlags.InvokeMethod,
                              null, loadSuites.ServiceFactory,
                              new object[] { objConfigure }
                            );
                        LoadProvider[providerName] = Activator.CreateInstance(loadSuites.ProviderType, depObject) as ILoadProvider;
                    }
                    return LoadProvider[providerName];
                }
            }
            return null;
        }


        //https://news.sina.com.cn/gov/xlxw/2019-11-21/doc-iihnzhfz0646656.shtml
    }
}
