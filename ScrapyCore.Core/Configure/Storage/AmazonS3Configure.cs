using System.Collections.Generic;
using System.Linq;

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
        public string StorageType => "AmazonS3Storage";
        public IDictionary<string, string> ConfigureDetail { get; private set; }

    }
}
