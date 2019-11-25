using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using ScrapyCore.Core.ElasticSearch;
using ScrapyCore.Core.External;

namespace ScrapyCore.Fundamental.ElasticSearchModel.Sina
{
    //Need to refactor as common as configure file and do not es framework.
    [ElasticsearchType(RelationName = nameof(SinaArtical), IdProperty = "Id")]
    public class SinaArtical : IElasticSearchModel
    {
        [Text(Name = "Id", Index = true)]
        public string Id { get; set; }

        [Keyword(Name = "Name", Index = true)]
        public string Name { get; set; }

        [Text(Name = "Title", Index = true, Analyzer = "ik_max_word")]
        public string Title { get; set; }

        [Text(Name = "Content", Index = true, Analyzer = "ik_max_word")]
        public string Content { get; set; }

        [Text(Name = "Source", Index = true, Analyzer = "ik_max_word")]
        public string Source { get; set; }

        [Date(Name = "PublishTime")]
        public string PublishTime { get; set; }

        [Text(Name = "Tags", Index = true, Analyzer = "ik_max_word")]
        public string Tags { get; set; }

        public string Index => nameof(SinaArtical).ToLower();

        public bool IsEmpty => string.IsNullOrEmpty(this.Title);

        public string ExistSearchKey => nameof(Name);

        public string ExistSearchValue => this.Name;

        public void Deserialze(IEnumerable<KeyValuePair<string, List<string>>> fieldWithValues)
        {
            var fv = fieldWithValues.ToDictionary(x => x.Key, x => x.Value[0]);
            Name = fv.DefaultValue(nameof(Name));
            Title = fv.DefaultValue(nameof(Title));
            Content = fv.DefaultValue(nameof(Content));
            Source = fv.DefaultValue(nameof(Source));
            PublishTime = fv.DefaultValue(nameof(PublishTime));
            Tags = fv.DefaultValue(nameof(Tags));
            Id = Guid.NewGuid().ToString();
        }
    }
}
