using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class AboutTeam
    {
        [Key]
        public int Id { get; set; }
        public string ImageCover { get; set; }

        public ICollection<AboutTeamTranslation> AboutTeamTranslations { get; set; } 



    }
}
