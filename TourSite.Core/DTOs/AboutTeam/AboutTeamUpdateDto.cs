using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.AboutTeam
{
    public class AboutTeamUpdateDto
    {
        public IFormFile? ImageFile { get; set; }

        public string? TranslationsJson { get; set; }
        public List<AboutTeamTranlationDto> aboutTeamTranlationDtos { get; set; }= new List<AboutTeamTranlationDto>();
    }
}
