using System;
using Microsoft.AspNetCore.Mvc;
using SEO.API.Models;
using SEO.BusinessLogicLayer.Common;
using SEO.BusinessLogicLayer.Models.Interfaces;
using SEO.WorkerService.Exceptions;

namespace SEO.API.Controllers
{
    /// <summary>
    /// Controller to provide end point to get feature
    /// </summary>
    [Route("Search")]
     public class SearchController : Controller
    {
        private readonly ISearchUrl _searchUrl;

        public SearchController(ISearchUrl searchUrl)
        {
            _searchUrl = searchUrl;
        }

        /// <summary>
        /// Get to take search term and lookup url - fetch positions of look up url 
        /// </summary>
        /// <param name="searchViewModel"> holds search term and lookup</param>
        /// <returns></returns>
        [HttpGet(Name = "GetSearchPositions")]
        public IActionResult Get(SearchViewModel searchViewModel)
        {
            try
            {
                if (!(ModelState.IsValid))
                {
                    return BadRequest(Constants.INVALID_KEYWORD);
                }
                if (string.IsNullOrWhiteSpace(searchViewModel.SearchTerm) || string.IsNullOrWhiteSpace(searchViewModel.Lookup))
                {
                    return BadRequest(Constants.INVALID_KEYWORD);
                }

                var listOfUrlPositions = _searchUrl.GetSearchUrls(searchViewModel.SearchTerm, searchViewModel.Lookup);

                ViewBag.ListOfUrlPositions = listOfUrlPositions ?? "0";
            }
            catch (SEOValidationException e)
            {
                ViewBag.Error = e.Message;
                return BadRequest(e.Message);
            }

            return View();
        }
    }
}