using Microsoft.AspNetCore.Mvc;
using SEO.API.Models;
using SEO.BusinessLogicLayer.Common;
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
            if (string.IsNullOrWhiteSpace(keywords))
            {
                return BadRequest(Constants.INVALID_KEYWORD);
            }

            var listOfUrlPositions = _searchUrl.GetSearchUrls(keywords);

            return View(new SearchViewModel { ResultList = listOfUrlPositions??"0" });
        }
    }
}