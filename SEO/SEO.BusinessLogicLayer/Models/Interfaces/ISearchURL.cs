using System;
using System.Collections.Generic;
using System.Text;

namespace SEO.BusinessLogicLayer.Models.Interfaces
{
   public interface ISearchURL
   {
       IEnumerable<string> GetSearchUrls(string keywords);
   }
}
