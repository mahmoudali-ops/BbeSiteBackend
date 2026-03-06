using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Price
{
    public class PriceUpdateDto
    {

        public IFormFile? ImageFile { get; set; }


        public string? TranslationsJson { get; set; }

   
        public List<PriceTranlationDto> priceTranlationDtos { get; set; }=new List<PriceTranlationDto>();
    }
}
