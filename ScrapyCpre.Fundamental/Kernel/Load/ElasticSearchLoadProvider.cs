using log4net;
using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Consts;
using ScrapyCore.Core.ElasticSearch;
using ScrapyCore.Fundamental.Kernel.Transform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class ElasticSearchLoadProvider : LoadProvider
    {
        private static ILog logger = LogManager.GetLogger(LogConst.SCRAPY_FUNDAMENTAL, nameof(ElasticSearchLoadProvider));

        private readonly IElasticSearch elasticSearch;

        private ElasticSearchTypeManager elasticSearchType = ElasticSearchTypeManager.Manager;

        public ElasticSearchLoadProvider(IElasticSearch elasticSearch)
        {
            this.elasticSearch = elasticSearch;
        }

        public override async Task Load(Stream content, LoadContext ldContext)
        {

            logger.Info("elasticsearch loaded:" + ldContext.LoadEvent.JobId);
            StreamReader reader = new StreamReader(content);
            List<TransformFieldWithValue> values =
                JsonConvert.DeserializeObject<List<TransformFieldWithValue>>(
                    await reader.ReadToEndAsync());

            var instanceWithType = elasticSearchType.GetInstanceWithName(ldContext.Parameter.ToString());
            instanceWithType.Model.Deserialze(values.Select(x => new KeyValuePair<string, List<string>>(x.Name, x.Value)));
            if (!instanceWithType.Model.IsEmpty)
            {
                var documentExist = elasticSearch.GetType().GetMethod(nameof(elasticSearch.DocumentExist))
                   .MakeGenericMethod(instanceWithType.ModelType)
                   .Invoke(elasticSearch, new object[] { instanceWithType.Model.ExistSearchKey, instanceWithType.Model.ExistSearchValue }) as Task<bool>;
                if (!await documentExist)
                {
                    var insertDocumentTask = elasticSearch.GetType().GetMethod(nameof(elasticSearch.InsertDocument))
                        .MakeGenericMethod(instanceWithType.ModelType)
                        .Invoke(elasticSearch, new object[] { instanceWithType.Model }) as Task;
                    await insertDocumentTask;
                }
            }
        }
    }
}
