using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Configure.Storage
{
    public class LocalFileSystemConfigure : IStorageConfigure
    {
        public LocalFileSystemConfigure(StorageConfigureModel storageConfigureModel)
        {
            this.Prefix = storageConfigureModel.Prefix;
        }
        public string Prefix { get; }

        public string StorageType => "LocalFileSystemStorage";

        public IDictionary<string, string> ConfigureDetail => new Dictionary<string, string>();
    }
}
