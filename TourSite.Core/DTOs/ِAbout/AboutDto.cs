using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs._ِAbout
{
    public class AboutDto
    {
        public int Id { get; set; }

        public string ImageCover { get; set; }

        public string ReferneceName { get; set; }

        public string? MetaDescription { get; set; }

        public string? MetaKeyWords { get; set; }


        public ICollection<AboutTranslationDto>  aboutTranslationDtos { get; set; }
    }
}
