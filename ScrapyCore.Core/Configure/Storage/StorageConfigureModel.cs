using System;
namespace ScrapyCore.Core.Configure
{
    public class StorageConfigureModel
    {
        public string StorageType { get; set; }
        public string Prefix { get; set; }
        public string[][] Configure { get; set; }
    }
}
