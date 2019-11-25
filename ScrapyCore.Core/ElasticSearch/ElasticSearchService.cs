using Elasticsearch.Net;
using log4net;
using Nest;
using ScrapyCore.Core.Configure.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.ElasticSearch
{
    public class ElasticSearchService : IElasticSearch
    {
        protected static ILog Logger => LogManager.GetLogger("Scrapy-Repo", typeof(ElasticSearchService));
        private ElasticClient client;

        public ElasticSearchService(IElasticSearchConfigure elasticSearchConfigure)
        {
            var connectionPool =
                new StaticConnectionPool(
                    elasticSearchConfigure.Nodes.Split(';')
                    .Select(x => new Uri(x)).ToArray());
            var settings = new ConnectionSettings(connectionPool);
            ElasticSearchTypeManager.NameTypes.ToList().ForEach(x =>
            {
                settings.DefaultMappingFor(x.Value, y => y.IndexName(x.Key.ToLower()));
            });
            client = new ElasticClient(settings);
        }

        public async Task<string> InsertDocument<T>(T model) where T : class, IElasticSearchModel
        {
            try
            {
                if (!client.Indices.Exists(Indices.Parse(model.Index)).Exists)
                {
                    client.Index(model, x => x.Index(model.Index));
                }
                var response = await client.IndexDocumentAsync(model);
                Logger.Info("ElasticSearch Insert Completed: " + response.IsValid);
                return response.Index;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return string.Empty;
            }
        }

        public async Task<IEnumerable<T>> GetDocument<T>(Expression<Func<T, object>> objectPath, string value) where T : class, IElasticSearchModel
        {
            var response = await client.SearchAsync<T>(selector: x =>
                     x.Query(q =>
                        q.Match(m =>
                            m.Field(objectPath)
                              .Fuzziness(Fuzziness.Auto)
                              .Query(value))));

            return response.Hits.Select(x => x.Source);
        }

        public async Task<bool> DocumentExist<T>(string field, string value) where T : class, IElasticSearchModel
        {
            var response = await client.SearchAsync<T>(selector: x =>
                     x.Query(q =>
                        q.Match(m =>
                            m.Field(field + ".keyword")
                              .Query(value))
                         ));

            return response.Hits.Count > 0;
        }
    }
}
