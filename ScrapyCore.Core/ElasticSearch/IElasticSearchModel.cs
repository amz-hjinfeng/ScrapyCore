using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.ElasticSearch
{
    public interface IElasticSearchModel
    {
        void Deserialze(IEnumerable<KeyValuePair<string, List<string>>> fieldWithValue);

        string Index { get; }

        bool IsEmpty { get; }

        string ExistSearchKey { get; }

        string ExistSearchValue { get; }
    }
}
