using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class PriceCardTranslation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Language { get; set; } // "en", "ar", "de" ...

        public int Discount { get; set; }


        [Required, MaxLength(250)]
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal PriceService { get; set; }

        public string IncludeFirst { get; set; }
        public string IncludeSecond { get; set; }

        public string IncludeThird { get; set; }

        public string IncludeForth { get; set; }

        // FK
        public int PriceId { get; set; }

        [ForeignKey(nameof(PriceId))]
        public Price Price { get; set; }
        



    }
}
