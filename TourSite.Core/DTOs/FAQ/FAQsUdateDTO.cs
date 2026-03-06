using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.FAQ
{
    public class FAQsUdateDTO
    {
   
        public IFormFile? ImageFile { get; set; }



        public string? TranslationsJson { get; set; }

        public List<FAQsTranslationDTo> fAQsTranslationDTos { get; set; } = new List<FAQsTranslationDTo>();
    }
}
