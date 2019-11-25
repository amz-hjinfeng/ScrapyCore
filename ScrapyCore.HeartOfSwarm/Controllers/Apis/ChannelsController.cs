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
        [Route("congestion")]
        public async Task<ActionResult> GetChanelCongestion()
        {
            var listkeys = cache.SearchKeys("channel*").Result;
            List<ChannelModel> chanelCongestions = new List<ChannelModel>();
            foreach (var item in listkeys)
            {
                chanelCongestions.Add(await cache.RestoreAsync<ChannelModel>(item));
            }
            return Json(chanelCongestions);
        }

    }
}
