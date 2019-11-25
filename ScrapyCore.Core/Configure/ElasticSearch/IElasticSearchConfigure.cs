using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Configure.ElasticSearch
{
    public interface IElasticSearchConfigure : IConfigure
    {
        /// <summary>
        /// It should be http://server1:9200;http//server2:9200
        /// </summary>
        string Nodes { get; set; }

        string ElasticSearchEngine { get; set; }
    }
}
