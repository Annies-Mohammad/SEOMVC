using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEO.API.Models
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public string Lookup { get; set; }
    }
}
