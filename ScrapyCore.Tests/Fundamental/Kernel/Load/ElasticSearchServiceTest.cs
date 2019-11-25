
using ScrapyCore.Core.Configure.ElasticSearch;
using ScrapyCore.Core.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Tests.Fundamental.Kernel.Load
{
    public class ElasticSearchServiceTest
    {
        ElasticSearchService elasticSearchService;
        public ElasticSearchServiceTest()
        {
            IElasticSearchConfigure elasticSearchConfigure = Moq.Mock.Of<IElasticSearchConfigure>();
            //elasticSearchConfigure.
        }
    }
}
