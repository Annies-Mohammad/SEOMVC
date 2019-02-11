using System;
using System.Collections.Generic;
using System.Text;
using SEO.BusinessLogicLayer.Models.Interfaces;

namespace SEO.BusinessLogicLayer.Models.Implementation
{
    public class SearchUrl : ISearchURL
    {
        public IEnumerable<string> GetSearchUrls(string keywords)
        {
            throw new NotImplementedException();
        }
    }
}
