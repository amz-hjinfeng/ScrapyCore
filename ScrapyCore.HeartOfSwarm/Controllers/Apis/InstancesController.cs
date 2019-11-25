using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core;
using ScrapyCore.Core.External.Utils;
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


        [Route("instance-lists/{model}")]
        [HttpGet]
        public async Task<IEnumerable<object>> GetInstanceInformations(
            [FromRoute(Name = "model")]string modelType)
        {
            var keys = this.GetKeys();
            List<object> result = new List<object>();
            foreach (var key in keys)
            {
                var heartBeatModel = await cache.RestoreAsync<HeartBeatModel>(key);
                if (heartBeatModel.Model == modelType)
                {
                    result.Add(heartBeatModel.External);
                }
            }
            return result;
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
