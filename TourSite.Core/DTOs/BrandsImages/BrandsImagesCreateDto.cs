using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.BrandsImages
{
    public class BrandsImagesCreateDto
    {
        public IFormFile? ImageFile { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
