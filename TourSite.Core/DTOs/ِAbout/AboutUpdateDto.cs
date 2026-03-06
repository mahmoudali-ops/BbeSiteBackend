using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs._ِAbout
{
    public class AboutUpdateDto
    {
        public IFormFile? ImageFile { get; set; }

        public string ReferneceName { get; set; }

        public string? MetaDescription { get; set; }

        public string? MetaKeyWords { get; set; }

        public string? TranslationsJson { get; set; }

        public List<AboutTranslationDto> aboutTranslationDtos { get; set; } = new();



    }
}
