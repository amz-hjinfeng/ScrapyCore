using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.HeartOfSwarm.Controllers.Views
{
    [Route("views/[controller]")]
    public class ScrapyTaskController : Controller
    {
        public ScrapyTaskController()
        {

        }

        public ViewResult Index()
        {
            return View();
        }


    }
}
