using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Configure.ElasticSearch
{
    public class ElasticSearchConfigureModel : IElasticSearchConfigure
    {
        public string Nodes { get; set; }

        public string ElasticSearchEngine { get; set; }

        public IDictionary<string, string> ConfigureDetail { get; set; }
    }
}
