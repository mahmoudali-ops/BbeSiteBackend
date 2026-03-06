using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class AboutTeamTranslation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Language { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Position { get; set; }
        public string Description { get; set; }
        // FK
        public int AboutTeamId { get; set; }
        [ForeignKey(nameof(AboutTeamId))]
        public AboutTeam AboutTeam { get; set; }


    }
}
