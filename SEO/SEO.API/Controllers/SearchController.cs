using Microsoft.AspNetCore.Mvc;
using SEO.BusinessLogicLayer.Models.Interfaces;

namespace SEO.API.Controllers
{
    [Route("Search")]
    public class SearchController : Controller
    {
        private readonly ISearchURL _searchUrl;

        public SearchController(ISearchURL searchUrl)
        {
            _searchUrl = searchUrl;
        }

        [HttpGet]
        public IActionResult Get(string keywords)
        {
            var listOfUrlPositions = _searchUrl.GetSearchUrls(keywords);
            return View(listOfUrlPositions);
        }
    }
}