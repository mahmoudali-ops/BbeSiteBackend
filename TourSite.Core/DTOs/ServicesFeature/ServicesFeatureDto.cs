using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.ServicesFeature
{
    public class ServicesFeatureDto
    {
        public int Id { get; set; }

        public string ImageCover { get; set; }

        public ICollection<ServicesFeatureTranslationDto> servicesFeatureTranslationDtos { get; set; }
    }
}
