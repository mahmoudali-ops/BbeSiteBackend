using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Career
{
    public class CareerUpdateDto
    {

        public IFormFile? ImageFile { get; set; }

        public string? TranslationsJson { get; set; }

        public List<CareerTranslationDto> careerCardTranslations { get; set; }=new List<CareerTranslationDto>();
    }
}
