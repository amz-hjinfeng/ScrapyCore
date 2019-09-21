using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ScrapyCore.Core.Configure.Storage
{
    public class StorageConfigureFactory : IConfigurationFactory<IStorageConfigure>
    {
        private static StorageConfigureFactory _instance;
        public static StorageConfigureFactory Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new StorageConfigureFactory();
                }
                return _instance;
            }
        }

        private Dictionary<string, Type> storageTypes;

        private StorageConfigureFactory()
        {
            storageTypes = typeof(StorageConfigureFactory).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => x.GetInterfaces().Contains(typeof(IStorageConfigure)))
                .ToDictionary(x=>x.Name.Substring(0,x.Name.Length - "Configure".Length),x=>x);

        }

        public IStorageConfigure CreateConfigure(IStorage storage, string path)
        {
            string configureData = storage.GetString(path);
            StorageConfigureModel model = JsonConvert.DeserializeObject<StorageConfigureModel>(configureData);
            if (this.storageTypes.ContainsKey(model.StorageType))
            {
                return Activator.CreateInstance(this.storageTypes[model.StorageType], model) as IStorageConfigure;
            }
            return null;
        }
    }
}
