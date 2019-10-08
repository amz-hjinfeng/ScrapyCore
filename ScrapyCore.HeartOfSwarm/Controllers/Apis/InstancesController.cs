using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform.Processors.Model;

namespace ScrapyCore.HeartOfSwarm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstancesController : Controller
    {
        private readonly ICache cache;

        public InstancesController(ICache cache)
        {
            this.cache = cache;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> GetKeys()
        {
            return cache.SearchKeys("instance*").Result;
        }

        [HttpGet("{id}")]
        public ActionResult<HeartBeatModel> Get(string id)
        {
            return cache.Restore<HeartBeatModel>(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return cache.Remove(id);
        }
    }
}
