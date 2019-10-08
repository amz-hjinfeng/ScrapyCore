using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform.Processors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.HeartOfSwarm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : Controller
    {
        private readonly ICache cache;
        public ChannelsController(ICache cache)
        {
            this.cache = cache;
        }

        [HttpGet]
        public IEnumerable<string> GetKeys()
        {
            return cache.SearchKeys("channel*").Result;
        }

        [HttpGet("{id}")]
        public ActionResult<ChannelModel> Get(string id)
        {
            return cache.Restore<ChannelModel>(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return cache.Remove(id);
        }
    }
}
