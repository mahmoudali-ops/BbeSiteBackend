using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.SocialElements
{
    public class SocialElementsUpdate
    {
        public IFormFile? ImageFile { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string Email { get; set; }
    }
}

