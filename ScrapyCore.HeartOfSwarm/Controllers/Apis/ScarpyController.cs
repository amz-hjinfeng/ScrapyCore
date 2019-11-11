using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.HeartOfSwarm.Controllers.Apis
{
    [Route("api/[controller]")]
    public class ScarpyController : Controller
    {
        private readonly ICache cache;
        public ScarpyController(ICache cache)
        {
            this.cache = cache;
        }

        [Route("task-list")]
        public ActionResult ScrapyTaskList()
        {
            return Json(new object[]
            {
                new {
                        Id = Guid.NewGuid(),
                        Name= "SinaScrapyTask",
                        Status = "Processing",
                        StartTime =DateTime.Now.ToString(),
                        SubTask =60,
                        Completed =30
                    },
                new {
                        Id = Guid.NewGuid(),
                        Name= "SinaScrapyTask2",
                        Status = "Completed",
                        StartTime =DateTime.Now.ToString(),
                        SubTask =60,
                        Completed= 60
                    }
            });
        }

    }
}
