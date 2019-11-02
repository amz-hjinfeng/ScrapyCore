using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.MessageQueue;
using ScrapyCore.Core.Configure.Storage;
using ScrapyCore.Core.MessageQueues;
using ScrapyCore.Core.Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class LoadProviderManager
    {
        public Dictionary<string, LoadSuites> LoadMetas { get; }

        private Dictionary<string, ILoadProvider> LoadProvider { get; set; }

        public LoadProviderManager()
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
                 }
            };
        }

        public ILoadProvider GetLoadProvider(string type, string name, object context)
        {
            if (LoadMetas.ContainsKey(type))
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
            return null;
        }



    }
}
