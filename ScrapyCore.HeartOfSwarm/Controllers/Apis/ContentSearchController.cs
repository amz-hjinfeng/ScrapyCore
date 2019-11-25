using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core;
using ScrapyCore.Core.ElasticSearch;
using ScrapyCore.Fundamental.ElasticSearchModel.Sina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.HeartOfSwarm.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentSearchController : Controller
    {
        private readonly IElasticSearch elasticSearchService;

        public ContentSearchController(IElasticSearch elasticSearchService)
        {
            this.elasticSearchService = elasticSearchService;
        }

        [HttpGet("search/{key-word}")]
        public async Task<ActionResult> SearchDocument([FromRoute(Name = "key-word")]string keyWord)
        {
            return Json(await elasticSearchService.GetDocument<SinaArtical>(x => x.Content, keyWord));
        }
    }
}
