using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.HeartOfSwarm.Controllers.Views
{
    [Route("views/[controller]")]
    public class SearchController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}
