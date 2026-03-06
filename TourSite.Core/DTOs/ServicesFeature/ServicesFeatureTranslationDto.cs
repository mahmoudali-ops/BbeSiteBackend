using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.ServicesFeature
{
    public class ServicesFeatureTranslationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Language { get; set; } // en, ar, de, nl ...

        public string Description { get; set; }

        public string IncludeFirst { get; set; }

        public string IncludeSecond { get; set; }
    }
}
