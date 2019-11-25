using ScrapyCore.Core;
using ScrapyCore.Core.Configure.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.ElasticSearch
{
    public class ElasticSearchFactory : IServiceFactory<IElasticSearch, IElasticSearchConfigure>
    {
        private static ElasticSearchFactory _factory;
        public static ElasticSearchFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new ElasticSearchFactory();
                return _factory;
            }
        }

        public IElasticSearch GetService(IElasticSearchConfigure configure)
        {
            return new ElasticSearchService(configure);
        }

        public IList<string> GetServiceKeys()
        {
            return new List<string>() { "ElasticSearchService" };
        }
    }
}
