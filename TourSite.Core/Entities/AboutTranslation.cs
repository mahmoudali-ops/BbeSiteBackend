using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class AboutTranslation
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; } // "en", "ar", "de" ...
        public string Description { get; set; }

        // FK
        public int AboutId { get; set; }

        [ForeignKey(nameof(AboutId))]  
        public About About { get; set; }
    }
}
