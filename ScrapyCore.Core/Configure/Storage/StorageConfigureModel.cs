using System;
namespace ScrapyCore.Core.Configure
{
    public class StorageConfigureModel
    {
        public string storageConfigureType { get; set; }
        public string Prefix { get; set; }
        public string[][] Configure { get; set; }
    }
}
