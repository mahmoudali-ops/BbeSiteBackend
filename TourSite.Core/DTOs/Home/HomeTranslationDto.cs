using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Home
{
    public class HomeTranslationDto
    {
        public int Id { get; set; }
        public string Language { get; set; } // "en", "ar", "de" ...
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
