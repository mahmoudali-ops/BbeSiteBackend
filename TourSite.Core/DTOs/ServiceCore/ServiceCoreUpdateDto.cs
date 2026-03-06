using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.ServiceCore
{
    public class ServiceCoreUpdateDto
    {

        public IFormFile? ImageFile { get; set; }
        public string? TranslationsJson { get; set; }

        public List<ServiceCoreTranlationDto> serviceCoreTranlationDtos { get; set; }=new List<ServiceCoreTranlationDto>();
    }
}
