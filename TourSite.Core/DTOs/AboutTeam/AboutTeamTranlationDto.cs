using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.AboutTeam
{
    public class AboutTeamTranlationDto
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
    }
}
