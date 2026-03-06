using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class FAQsTranslation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Language { get; set; } // "en", "ar"

        [Required, MaxLength(200)]
        public string Question { get; set; }

        public string Answer { get; set; }

        public int FAQsId { get; set; }

        [ForeignKey(nameof(FAQsId))]
        public FAQs FAQs {get; set;}

    }
}
