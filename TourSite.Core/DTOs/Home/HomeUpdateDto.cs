using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Home
{
    public class HomeUpdateDto
    {
        public IFormFile? MainCoverImageFile { get; set; }
        public IFormFile? MultiLangImageImageFile { get; set; }
        public IFormFile? TeamImageImageFile { get; set; }
        public IFormFile? HelpImageImageFile { get; set; }


        public string? TranslationsJson { get; set; }

        public List<HomeTranslationDto> homeTranslationDtos { get; set; } = new();
    }
}
