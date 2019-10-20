using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform.Processors.Model;
using ScrapyCore.Core.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.HeartOfSwarm.Controllers.Apis
{
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        private readonly ICache cache;

        public StatisticsController(ICache cache)
        {
            this.cache = cache;
        }


        [Route("menu-number")]
        public ActionResult MenuNumber()
        {
            var keys = cache.SearchKeys("instance*").Result;
            var groups = keys.Select(x => cache.Restore<HeartBeatModel>(x)).GroupBy(y => y.ChannelId);
            var dic = groups.ToDictionary(x => x.Key, x => x.Count());

            if (dic == null || dic.Count < 1)
            {
                return Json(new
                {
                    kerrrigan = 0,
                    hydralisk = 0,
                    utralisks = 0
                });
            }
            return Json(new
            {
                kerrrigan = dic.DefaultValue("kerrigan-hydralisk"),
                hydralisk = dic.DefaultValue("hydralisk-utralisks"),
                utralisks = dic.DefaultValue("utralisks-kerrigan")
            });

        }
    }
}
