using ScrapyCore.Core.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core
{
    public interface IElasticSearch
    {
        Task<string> InsertDocument<T>(T model) where T : class, IElasticSearchModel;
        Task<IEnumerable<T>> GetDocument<T>(Expression<Func<T, object>> objectPath, string value) where T : class, IElasticSearchModel;
        Task<bool> DocumentExist<T>(string field, string value) where T : class, IElasticSearchModel;
    }
}
