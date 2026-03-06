using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class ContactTranslation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Language { get; set; } // "en", "ar", etc.

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        // FK
        public int ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        public Contact Contact { get; set; }

    }
}
