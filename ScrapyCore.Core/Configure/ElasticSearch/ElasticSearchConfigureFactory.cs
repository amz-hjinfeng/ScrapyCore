using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Configure.ElasticSearch
{
    public class ElasticSearchConfigureFactory : IConfigurationFactory<IElasticSearchConfigure>
    {
        public static ElasticSearchConfigureFactory _factory;

        public static ElasticSearchConfigureFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new ElasticSearchConfigureFactory();
                }
                return _factory;
            }
        }


        public IElasticSearchConfigure CreateConfigure(IStorage storage, string path)
        {
            var configureObject = JsonConvert.DeserializeObject<ElasticSearchConfigureModel>(storage.GetString(path));
            return configureObject;
        }
    }
}
