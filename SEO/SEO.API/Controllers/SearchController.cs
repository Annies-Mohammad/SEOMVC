using Microsoft.AspNetCore.Mvc;
using SEO.API.Models;
using SEO.BusinessLogicLayer.Common;
using SEO.BusinessLogicLayer.Models.Interfaces;

namespace SEO.API.Controllers
{
    [Route("Search")]
     public class SearchController : Controller
    {
        private readonly ISearchUrl _searchUrl;

        public SearchController(ISearchUrl searchUrl)
        {
            _searchUrl = searchUrl;
        }

        [HttpGet(Name = "GetSearchPositions")]
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