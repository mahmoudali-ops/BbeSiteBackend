using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs._ِAbout
{
    public class AboutTranslationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; } // "en", "ar", "de" ...
        public string Description { get; set; }
    }
}
