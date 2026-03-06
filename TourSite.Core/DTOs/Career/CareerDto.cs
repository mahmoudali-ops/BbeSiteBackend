using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.Career
{
    public class CareerDto
    {
        public int Id { get; set; }

        public string ImageCover { get; set; }

        public string ReferneceName { get; set; }

        public ICollection<CareerTranslationDto> careerCardTranslationsDto { get; set; }
    }
}
