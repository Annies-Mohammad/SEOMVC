using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SEO.API.Controllers
{
    [Route("Search")]
    public class SearchController : Controller
    {
        [HttpGet]
        public IActionResult Get(string keywords)
        {
             return View("Get");
        }
    }
}