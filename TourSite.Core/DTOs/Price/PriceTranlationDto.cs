using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Price
{
    public class  PriceTranlationDto
    {
        public int Id { get; set; }
        public string Language { get; set; } // "en", "ar", "de" ...

        public int Discount { get; set; }


        [Required, MaxLength(250)]
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal PriceService { get; set; }

        public string IncludeFirst { get; set; }
        public string IncludeSecond { get; set; }

        public string IncludeThird { get; set; }

        public string IncludeForth { get; set; }
    }
}
