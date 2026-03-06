using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.FAQ
{
    public class FAQsTranslationDTo
    {
        public int Id { get; set; }
        public string Language { get; set; } // "en", "ar"

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
