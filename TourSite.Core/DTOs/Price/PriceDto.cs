using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.Price
{
    public class PriceDto
    {
        public int Id { get; set; }

        public string ImageCover { get; set; }


        // ✅ العلاقه مع جدول الترجمه
        public ICollection<PriceTranlationDto> priceTranlationDtos { get; set; }
    }
}
