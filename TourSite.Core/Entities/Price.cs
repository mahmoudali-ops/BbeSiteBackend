using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class Price
    {
        [Key]
        public int Id { get; set; }

        public string ImageCover { get; set; }

        public string ReferneceName { get; set; }


        public string? MetaDescription { get; set; }
        public string? MetaKeyWords { get; set; }




        // ✅ العلاقه مع جدول الترجمه
        public ICollection<PriceCardTranslation> priceCardTranslations { get; set; } = new List<PriceCardTranslation>();
    }
}
