using ScrapyCore.Core;
using ScrapyCore.Core.Configure.ElasticSearch;
using ScrapyCore.Core.ElasticSearch;
using ScrapyCore.Fundamental.ElasticSearchModel.Sina;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScrapyCore.IntegrationTests.Fundamental.Kernel.Load
{
    public class ElasticSearchServiceTest
    {
        ElasticSearchService elasticSearchService;
        public ElasticSearchServiceTest()
        {
            var bootstrap = new Bootstrap();
            IElasticSearchConfigure elasticSearchConfigure = Moq.Mock.Of<IElasticSearchConfigure>();
            Moq.Mock.Get(elasticSearchConfigure).Setup(x => x.Nodes).Returns("http://localhost:9200");
            elasticSearchService = new ElasticSearchService(elasticSearchConfigure);
        }

        [Fact]
        public async Task InsertArticalTest()
        {
            SinaArtical sinaArtical = new SinaArtical()
            {
                Content = "新京报讯（记者 陈维城）11月21日，交通运输部、国家发改委发布关于深化道路运输价格改革的意见。其中提到，规范网络预约出租汽车（以下简称网约车）价格行为，对网约车实行市场调节价。对此，滴滴出行向新京报记者表示，将严格按照主管部门要求执行价格相关工作，并接受社会监督。在主管部门的指导和帮助下，为乘客提供更好的服务",
                Name = "滴滴回应\"网约车市场调价\":严格执行价格相关工作",
                PublishTime = "2019年11月21日",
                Source = "新京报",
                Tags = "调价",
                Title = "滴滴回应\"网约车市场调价\":严格执行价格相关工作",
                Id = "HelloId"
            };

            string index = await elasticSearchService.InsertDocument(sinaArtical);
            Assert.NotNull(index);
            IEnumerable<SinaArtical> sinaArticals = await elasticSearchService.GetDocument<SinaArtical>(x => x.Content, "滴滴");
            Assert.NotNull(sinaArtical);

        }

        [Fact]
        public async Task ExistArticalTest()
        {
            var exist = await elasticSearchService.DocumentExist<SinaArtical>("Name", "王毅会见加拿大外长：中加关系遇困难 症结在这儿");
            Assert.False(exist);
        }
    }
}
