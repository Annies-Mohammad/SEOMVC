using System;
using System.Net;
using SEO.BusinessLogicLayer.Models.Interfaces;
using SEO.WorkerService.Interfaces;

namespace SEO.BusinessLogicLayer.Models.Implementation
{
    public class SearchUrl : ISearchUrl
    {
        private readonly ISEORequestService _seoRequestService;
        public SearchUrl(ISEORequestService seoRequestService)
        {
            _seoRequestService = seoRequestService;
        }
        public string GetSearchUrls(string searchTerm,string lookUp)
        {
            try
            {
                var request = _seoRequestService.CreateRequest(searchTerm);

                var matchPositions = _seoRequestService.GetResponse(request, lookUp);

                return matchPositions;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
