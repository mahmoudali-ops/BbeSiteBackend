using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.ServicesFeature
{
    public class ServicesFeatureUdateDto
    {
        public IFormFile? ImageFile { get; set; }
        public string? TranslationsJson { get; set; }

        public List<ServicesFeatureTranslationDto> servicesFeatureTranslationDtos { get; set; }=new List<ServicesFeatureTranslationDto>();
    }
}
