using System;
using SEO.BusinessLogicLayer.Models.Interfaces;

namespace SEO.BusinessLogicLayer.Models.Implementation
{
    public class SearchUrl : ISearchUrl
    {
        public string GetSearchUrls(string keywords)
        {
            return "sf";
            //throw new NotImplementedException();
        }
    }
}
