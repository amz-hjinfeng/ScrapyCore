using System;
using System.Collections.Generic;
using System.Linq;
using ScrapyCore.Core.External;
using System.Reflection;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.Storages
{
    public class StorageFactory : IServiceFactory<IStorage,IStorageConfigure>
    {
        private static StorageFactory instance;
    
        public static StorageFactory Factory 
        {
            get
            {
                if (instance == null)
                    instance = new StorageFactory();
                return instance;
            }
        }

        private readonly Dictionary<string, Type> storageTypes;
        private StorageFactory()
        {
            storageTypes = typeof(StorageFactory).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => x.GetInterface(nameof(IStorage)) != null)
                .ToDictionary(x => x.Name, x => x);
        }

        public IStorage GetLocalStorage(string prefix)
        {
            return new LocalFileSystemStorage(prefix);
        }

        public IStorage GetService(IStorageConfigure configure)
        {
            var storageType = storageTypes.DefaultValue(configure.StorageType);
            if (storageType != null)
            {
                return Activator.CreateInstance(storageType, configure) as IStorage;
            }
            return null;
        }
    }
}
