using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ScrapyCore.Core.Consts;

namespace ScrapyCore.Core.Configure
{
    public class AmazonS3Configure :IStorageConfigure
    {

        public AmazonS3Configure(StorageConfigureModel model)
        {

            this.Prefix = model.Prefix;
            this.ConfigureDetail = model.Configure.ToDictionary(x => x[0], x => x[1]);
        }

        public string Prefix { get; }
        public string StorageType => "AmazonS3";
        public IDictionary<string, string> ConfigureDetail { get; private set; }

    }
}
