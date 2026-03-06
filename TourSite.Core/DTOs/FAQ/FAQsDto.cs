using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.FAQ;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.CategoryTour
{
    public class FAQsDto
    {
        public int Id { get; set; }

        public string ImageCover { get; set; }

        public string ReferneceName { get; set; }

        public string? MetaDescription { get; set; }
        public string? MetaKeyWords { get; set; }

        public ICollection<FAQsTranslationDTo>  fAQsTranslationDTos { get; set; }
    }
}
