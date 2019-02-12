using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEO.API.Models
{
    public class SearchViewModel
    {
        [Required]
        [DefaultValue("online title search")]
        public string SearchTerm { get; set; }

        [ReadOnly(true)]
        [DefaultValue("www.infotrack.com.au")]
        public string Lookup { get; set; }
    }
}
