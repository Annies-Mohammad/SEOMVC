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
        public string GetSearchUrls(string keywords)
        {
            var request = _seoRequestService.CreateRequest(keywords);

            var matchPositions = _seoRequestService.GetResponse(request);

            return matchPositions;
            //throw new NotImplementedException();
        }
    }
}
