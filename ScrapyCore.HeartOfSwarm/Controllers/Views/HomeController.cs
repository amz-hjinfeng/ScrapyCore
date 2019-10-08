using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.HeartOfSwarm.Controllers.Views
{
    [Route("views/[controller]")]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
