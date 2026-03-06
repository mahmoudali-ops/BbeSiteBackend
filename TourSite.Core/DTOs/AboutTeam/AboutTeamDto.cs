using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.AboutTeam
{
    public class AboutTeamDto
    {
        public int Id { get; set; }
        public string ImageCover { get; set; }

        public ICollection<AboutTeamTranlationDto> aboutTeamTranlationDtos { get; set; }
    }
}
