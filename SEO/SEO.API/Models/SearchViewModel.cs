using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEO.API.Models
{
    public class SearchViewModel
    {
        [Required]
        public string SearchTerm { get; set; }

        [Required]
        public string Lookup { get; set; }
    }
}
